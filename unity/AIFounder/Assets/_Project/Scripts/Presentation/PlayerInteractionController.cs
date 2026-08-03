using UnityEngine;
using UnityEngine.InputSystem;

namespace AIFounder.Presentation
{
    public sealed class PlayerInteractionController : MonoBehaviour
    {
        private const float EqualDistanceTolerance = 0.0001f;

        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string actionMapName = "Player";
        [SerializeField] private string interactActionName = "Interact";
        [SerializeField] private float interactionRadius = 2f;
        [SerializeField] private LayerMask interactionLayers = ~0;
        [SerializeField] private InteractionPromptHud promptHud;

        private readonly Collider[] overlapResults = new Collider[16];
        private InputAction interactAction;
        private IPrototypeInteractable activeInteractable;
        private string activeCandidateKey = string.Empty;

        public IPrototypeInteractable ActiveInteractable => activeInteractable;

        private void OnEnable()
        {
            interactAction = FindAction(interactActionName);
            if (interactAction != null)
            {
                interactAction.performed += OnInteractPerformed;
                interactAction.Enable();
            }
        }

        private void OnDisable()
        {
            if (interactAction != null)
            {
                interactAction.performed -= OnInteractPerformed;
                interactAction.Disable();
            }
        }

        private void Update()
        {
            RefreshCandidate();
        }

        public void RefreshCandidate()
        {
            activeInteractable = FindNearestInteractable();

            if (activeInteractable != null)
            {
                promptHud?.Show(activeInteractable);
            }
            else
            {
                promptHud?.Hide();
            }
        }

        public bool TryInteract()
        {
            RefreshCandidate();
            if (activeInteractable == null || !activeInteractable.IsAvailable)
            {
                return false;
            }

            activeInteractable.Interact();
            promptHud?.Show(activeInteractable);
            promptHud?.ShowStatus(activeInteractable.InteractionStatusMessage);
            return true;
        }

        private IPrototypeInteractable FindNearestInteractable()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, overlapResults, interactionLayers, QueryTriggerInteraction.Collide);
            IPrototypeInteractable nearest = null;
            float nearestDistance = float.PositiveInfinity;
            activeCandidateKey = string.Empty;

            for (int i = 0; i < count; i++)
            {
                Collider candidate = overlapResults[i];
                if (candidate == null)
                {
                    continue;
                }

                IPrototypeInteractable interactable = candidate.GetComponentInParent<IPrototypeInteractable>();
                if (interactable == null || !interactable.IsAvailable)
                {
                    continue;
                }

                float distance = Vector3.SqrMagnitude(candidate.transform.position - transform.position);
                string candidateKey = BuildCandidateKey(interactable, candidate.transform);
                if (IsBetterCandidate(distance, candidateKey, nearestDistance, activeCandidateKey))
                {
                    nearestDistance = distance;
                    activeCandidateKey = candidateKey;
                    nearest = interactable;
                }
            }

            return nearest;
        }

        private static bool IsBetterCandidate(float distance, string candidateKey, float nearestDistance, string nearestKey)
        {
            if (distance < nearestDistance - EqualDistanceTolerance)
            {
                return true;
            }

            if (Mathf.Abs(distance - nearestDistance) <= EqualDistanceTolerance)
            {
                return string.CompareOrdinal(candidateKey, nearestKey) < 0;
            }

            return false;
        }

        private static string BuildCandidateKey(IPrototypeInteractable interactable, Transform candidateTransform)
        {
            return $"{interactable.PromptLabel}|{BuildHierarchyPath(candidateTransform)}";
        }

        private static string BuildHierarchyPath(Transform current)
        {
            string path = current.name;
            while (current.parent != null)
            {
                current = current.parent;
                path = $"{current.name}/{path}";
            }

            return path;
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            TryInteract();
        }

        private InputAction FindAction(string actionName)
        {
            if (inputActions == null)
            {
                return null;
            }

            InputActionMap map = inputActions.FindActionMap(actionMapName, false);
            return map?.FindAction(actionName, false);
        }
    }
}
