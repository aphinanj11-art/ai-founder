using System.Collections;
using AIFounder.Presentation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AIFounder.Tests.PlayMode
{
    public sealed class IsometricFollowCameraTests
    {
        [UnityTest]
        public IEnumerator FollowCamera_MovesTowardTargetOffset()
        {
            var target = new GameObject("target");
            target.transform.position = Vector3.zero;
            var cameraObject = new GameObject("camera");
            cameraObject.transform.position = new Vector3(0f, 20f, -20f);
            var followCamera = cameraObject.AddComponent<IsometricFollowCamera>();
            followCamera.Target = target.transform;
            followCamera.Offset = new Vector3(0f, 8f, -8f);

            float initialDistance = Vector3.Distance(cameraObject.transform.position, target.transform.position + followCamera.Offset);
            yield return null;
            float nextDistance = Vector3.Distance(cameraObject.transform.position, target.transform.position + followCamera.Offset);

            Assert.Less(nextDistance, initialDistance);
            Object.Destroy(target);
            Object.Destroy(cameraObject);
        }
    }
}