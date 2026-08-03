using System.Collections;
using AIFounder.Presentation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace AIFounder.Tests.PlayMode
{
    public sealed class InteractionFoundationTests
    {
        [UnityTest]
        public IEnumerator PlayerInteraction_ShowsPromptAndExecutesNearestInteractable()
        {
            var player = new GameObject("player");
            player.transform.position = Vector3.zero;
            var interaction = player.AddComponent<PlayerInteractionController>();

            var canvas = new GameObject("canvas");
            var textObject = new GameObject("prompt");
            textObject.transform.SetParent(canvas.transform);
            var promptText = textObject.AddComponent<Text>();
            var hud = canvas.AddComponent<InteractionPromptHud>();
            SetPrivateField(hud, "promptText", promptText);
            SetPrivateField(interaction, "promptHud", hud);
            SetPrivateField(interaction, "interactionRadius", 2f);

            var point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            point.name = "Workshop Interaction";
            point.transform.position = Vector3.right;
            var interactable = point.AddComponent<PrototypeInteractionPoint>();
            interactable.Configure("Workshop", "Use");

            yield return null;
            interaction.RefreshCandidate();

            Assert.IsTrue(hud.IsVisible);
            Assert.That(hud.VisibleText, Does.Contain("Workshop"));
            Assert.IsTrue(interaction.TryInteract());
            Assert.AreEqual(1, interactable.InteractionCount);

            Object.Destroy(player);
            Object.Destroy(canvas);
            Object.Destroy(point);
        }

        private static void SetPrivateField<T>(object target, string fieldName, T value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field.SetValue(target, value);
        }
    }
}