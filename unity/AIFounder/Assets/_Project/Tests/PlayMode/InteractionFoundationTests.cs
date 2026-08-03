using System.Collections;
using System.Linq;
using AIFounder.Presentation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace AIFounder.Tests.PlayMode
{
    public sealed class InteractionFoundationTests
    {
        private const string FirstPlayableSceneName = "MVP_A_FirstPlayable";
        private static int fixtureIndex;

        [UnityTest]
        public IEnumerator PromptVisibility_FollowsCandidateRange()
        {
            var fixture = CreateFixture();
            var point = CreatePoint("Workshop Interaction", "Workshop", fixture.Origin + Vector3.right);

            yield return null;
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.IsTrue(fixture.Hud.IsVisible);
            Assert.That(fixture.Hud.VisibleText, Does.Contain("Workshop"));

            fixture.Player.transform.position = fixture.Origin + new Vector3(5f, 0f, 0f);
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.IsFalse(fixture.Hud.IsVisible);
            Assert.AreEqual(string.Empty, fixture.Hud.VisibleText);

            DestroyFixture(fixture, point.gameObject);
        }

        [UnityTest]
        public IEnumerator PlayerInteraction_InvokesCorrectActiveCandidateAndShowsStatus()
        {
            var fixture = CreateFixture();
            var workshop = CreatePoint("Workshop Interaction", "Workshop", fixture.Origin + new Vector3(1.5f, 0f, 0f));
            var shop = CreatePoint("Shop Interaction", "Shop", fixture.Origin + new Vector3(0.5f, 0f, 0f));

            yield return null;
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.AreSame(shop, fixture.Interaction.ActiveInteractable);
            Assert.IsTrue(fixture.Interaction.TryInteract());
            Assert.AreEqual(0, workshop.InteractionCount);
            Assert.AreEqual(1, shop.InteractionCount);
            Assert.IsTrue(fixture.Hud.IsStatusVisible);
            Assert.AreEqual("Shop interaction detected", fixture.Hud.StatusText);

            DestroyFixture(fixture, workshop.gameObject, shop.gameObject);
        }

        [UnityTest]
        public IEnumerator EqualDistanceCandidateSelection_UsesStableLabelTieBreaker()
        {
            var fixture = CreateFixture();
            var workshop = CreatePoint("Workshop Interaction", "Workshop", fixture.Origin + Vector3.left);
            var delivery = CreatePoint("Delivery Interaction", "Delivery", fixture.Origin + Vector3.right);

            yield return null;
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.AreSame(delivery, fixture.Interaction.ActiveInteractable);

            DestroyFixture(fixture, workshop.gameObject, delivery.gameObject);
        }

        [UnityTest]
        public IEnumerator UnavailableOrDisabledInteractable_CannotBeInvoked()
        {
            var fixture = CreateFixture();
            var unavailable = CreatePoint("Unavailable Workshop", "Workshop", fixture.Origin + Vector3.right);
            SetPrivateField(unavailable, "isAvailable", false);

            yield return null;
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.IsNull(fixture.Interaction.ActiveInteractable);
            Assert.IsFalse(fixture.Hud.IsVisible);
            Assert.IsFalse(fixture.Interaction.TryInteract());
            Assert.AreEqual(0, unavailable.InteractionCount);

            unavailable.gameObject.SetActive(false);
            Physics.SyncTransforms();
            fixture.Interaction.RefreshCandidate();

            Assert.IsNull(fixture.Interaction.ActiveInteractable);
            Assert.IsFalse(fixture.Interaction.TryInteract());
            Assert.AreEqual(0, unavailable.InteractionCount);

            DestroyFixture(fixture, unavailable.gameObject);
        }

        [UnityTest]
        public IEnumerator PlayerMovement_SimulatedMoveChangesPosition()
        {
            var player = new GameObject("movement-player");
            var characterController = player.AddComponent<CharacterController>();
            var movement = player.AddComponent<PlayerMovementController>();
            Vector3 start = player.transform.position;

            yield return null;
            characterController.Move(movement.CalculateWorldMove(Vector2.up) * 0.25f);

            Assert.AreNotEqual(start, player.transform.position);
            Object.DestroyImmediate(player);
        }

        [UnityTest]
        public IEnumerator FirstPlayableScene_ContainsRequiredInteractionPoints()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);

            var points = Object.FindObjectsByType<PrototypeInteractionPoint>(FindObjectsSortMode.None);
            string[] labels = points.Select(point => point.PromptLabel).ToArray();

            Assert.That(labels, Does.Contain("Workshop"));
            Assert.That(labels, Does.Contain("Nearby Shop"));
            Assert.That(labels, Does.Contain("Delivery Point"));
        }

        private static InteractionFixture CreateFixture()
        {
            Vector3 origin = new Vector3(1000f + fixtureIndex++ * 100f, 0f, 1000f);
            var player = new GameObject("player");
            player.transform.position = origin;
            var interaction = player.AddComponent<PlayerInteractionController>();

            var canvas = new GameObject("canvas");
            var promptObject = new GameObject("prompt");
            promptObject.transform.SetParent(canvas.transform);
            var promptText = promptObject.AddComponent<Text>();
            var statusObject = new GameObject("status");
            statusObject.transform.SetParent(canvas.transform);
            var statusText = statusObject.AddComponent<Text>();
            var hud = canvas.AddComponent<InteractionPromptHud>();
            SetPrivateField(hud, "promptText", promptText);
            SetPrivateField(hud, "statusText", statusText);
            SetPrivateField(interaction, "promptHud", hud);
            SetPrivateField(interaction, "interactionRadius", 2f);

            return new InteractionFixture(origin, player, interaction, canvas, hud);
        }

        private static PrototypeInteractionPoint CreatePoint(string name, string label, Vector3 position)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            point.name = name;
            point.transform.position = position;
            var interactable = point.AddComponent<PrototypeInteractionPoint>();
            interactable.Configure(label, "Use");
            return interactable;
        }

        private static void DestroyFixture(InteractionFixture fixture, params GameObject[] extraObjects)
        {
            Object.DestroyImmediate(fixture.Player);
            Object.DestroyImmediate(fixture.Canvas);
            foreach (GameObject extraObject in extraObjects)
            {
                Object.DestroyImmediate(extraObject);
            }
        }

        private static void SetPrivateField<T>(object target, string fieldName, T value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field.SetValue(target, value);
        }

        private readonly struct InteractionFixture
        {
            public InteractionFixture(Vector3 origin, GameObject player, PlayerInteractionController interaction, GameObject canvas, InteractionPromptHud hud)
            {
                Origin = origin;
                Player = player;
                Interaction = interaction;
                Canvas = canvas;
                Hud = hud;
            }

            public Vector3 Origin { get; }
            public GameObject Player { get; }
            public PlayerInteractionController Interaction { get; }
            public GameObject Canvas { get; }
            public InteractionPromptHud Hud { get; }
        }
    }
}
