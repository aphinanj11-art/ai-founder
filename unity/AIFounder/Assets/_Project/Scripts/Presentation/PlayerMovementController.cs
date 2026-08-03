using UnityEngine;
using UnityEngine.InputSystem;

namespace AIFounder.Presentation
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string actionMapName = "Player";
        [SerializeField] private string moveActionName = "Move";
        [SerializeField] private float moveSpeed = 4.5f;
        [SerializeField] private Vector3 isometricForward = new(1f, 0f, 1f);
        [SerializeField] private Vector3 isometricRight = new(1f, 0f, -1f);

        private CharacterController characterController;
        private InputAction moveAction;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = Mathf.Max(0f, value);
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            moveAction = FindAction(moveActionName);
            moveAction?.Enable();
        }

        private void OnDisable()
        {
            moveAction?.Disable();
        }

        private void Update()
        {
            Vector2 input = moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
            Vector3 movement = CalculateWorldMove(input) * moveSpeed;
            movement.y = -2f;
            characterController.Move(movement * Time.deltaTime);
        }

        public Vector3 CalculateWorldMove(Vector2 input)
        {
            Vector2 clampedInput = Vector2.ClampMagnitude(input, 1f);
            Vector3 forward = FlattenAndNormalize(isometricForward, Vector3.forward);
            Vector3 right = FlattenAndNormalize(isometricRight, Vector3.right);
            Vector3 worldMove = (right * clampedInput.x) + (forward * clampedInput.y);
            return Vector3.ClampMagnitude(worldMove, 1f);
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

        private static Vector3 FlattenAndNormalize(Vector3 value, Vector3 fallback)
        {
            value.y = 0f;
            return value.sqrMagnitude > 0.0001f ? value.normalized : fallback;
        }
    }
}