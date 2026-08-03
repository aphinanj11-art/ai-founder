using UnityEngine;

namespace AIFounder.Presentation
{
    public sealed class IsometricFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 8f, -8f);
        [SerializeField] private float followResponsiveness = 10f;
        [SerializeField] private bool lookAtTarget = true;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public Vector3 Offset
        {
            get => offset;
            set => offset = value;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desiredPosition = target.position + offset;
            float t = 1f - Mathf.Exp(-Mathf.Max(0f, followResponsiveness) * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, t);

            if (lookAtTarget)
            {
                transform.LookAt(target.position);
            }
        }
    }
}