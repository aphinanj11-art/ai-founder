using AIFounder.Presentation;
using NUnit.Framework;
using UnityEngine;

namespace AIFounder.Tests.EditMode
{
    public sealed class PlayerMovementControllerTests
    {
        [Test]
        public void CalculateWorldMove_ClampsDiagonalMagnitude()
        {
            var gameObject = new GameObject("movement-test");
            var controller = gameObject.AddComponent<CharacterController>();
            var movement = gameObject.AddComponent<PlayerMovementController>();

            Vector3 result = movement.CalculateWorldMove(new Vector2(1f, 1f));

            Assert.LessOrEqual(result.magnitude, 1.0001f);
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void CalculateWorldMove_ReturnsZeroForReleasedInput()
        {
            var gameObject = new GameObject("movement-test");
            var controller = gameObject.AddComponent<CharacterController>();
            var movement = gameObject.AddComponent<PlayerMovementController>();

            Vector3 result = movement.CalculateWorldMove(Vector2.zero);

            Assert.AreEqual(Vector3.zero, result);
            Object.DestroyImmediate(gameObject);
        }
    }
}