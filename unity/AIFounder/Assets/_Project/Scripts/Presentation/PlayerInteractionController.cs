using UnityEngine;
using UnityEngine.InputSystem;

namespace AIFounder.Presentation
{
    public sealed class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string actionMapName = "Player";
        [SerializeField] private string interactActionName = "Interact";
        [SerializeField] private float interactionRadius = 2f;
        [SerializeField] private LayerMask interactionLayers = ~0;
        [SerializeField] private InteractionPromptHud promptHud;

        private readonly Collider[] overlapResults = new Collider[16];
        private InputAction interactAction;
        private IPrototypeInteractable activeInteractable;

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
            return true;
        }

        private IPrototypeInteractable FindNearestInteractable()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, overlapResults, interactionLayers, QueryTriggerInteraction.Collide);
            IPrototypeInteractable nearest = null;
            float nearestDistance = float.PositiveInfinity;

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
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = interactable;
                }
            }

            return nearest;
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