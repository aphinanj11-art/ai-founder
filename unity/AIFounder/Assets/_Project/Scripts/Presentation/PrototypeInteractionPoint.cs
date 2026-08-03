using UnityEngine;

namespace AIFounder.Presentation
{
    [RequireComponent(typeof(Collider))]
    public sealed class PrototypeInteractionPoint : MonoBehaviour, IPrototypeInteractable
    {
        [SerializeField] private string promptLabel = "Interaction Point";
        [SerializeField] private string interactionVerb = "Use";
        [SerializeField] private bool isAvailable = true;
        [SerializeField] private Renderer feedbackRenderer;
        [SerializeField] private Color idleColor = Color.gray;
        [SerializeField] private Color interactedColor = Color.green;

        public string PromptLabel => promptLabel;
        public string InteractionVerb => interactionVerb;
        public bool IsAvailable => isAvailable;
        public int InteractionCount { get; private set; }
        public string LastInteractionMessage { get; private set; } = string.Empty;

        private void Awake()
        {
            ApplyColor(idleColor);
        }

        public void Configure(string label, string verb)
        {
            promptLabel = label;
            interactionVerb = verb;
        }

        public void Interact()
        {
            if (!isAvailable)
            {
                return;
            }

            InteractionCount++;
            LastInteractionMessage = $"Interacted with {promptLabel}";
            ApplyColor(interactedColor);
            Debug.Log(LastInteractionMessage, this);
        }

        private void ApplyColor(Color color)
        {
            if (feedbackRenderer != null)
            {
                feedbackRenderer.material.color = color;
            }
        }
    }
}