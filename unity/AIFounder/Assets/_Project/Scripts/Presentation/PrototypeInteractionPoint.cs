using UnityEngine;

namespace AIFounder.Presentation
{
    [RequireComponent(typeof(Collider))]
    public sealed class PrototypeInteractionPoint : MonoBehaviour, IPrototypeInteractable
    {
        [SerializeField] private string promptLabel = "Interaction Point";
        [SerializeField] private string interactionVerb = "Use";
        [SerializeField] private string statusLabel = string.Empty;
        [SerializeField] private bool isAvailable = true;
        [SerializeField] private Renderer feedbackRenderer;
        [SerializeField] private Color idleColor = Color.gray;
        [SerializeField] private Color interactedColor = Color.green;

        public string PromptLabel => promptLabel;
        public string InteractionVerb => interactionVerb;
        public string InteractionStatusMessage => $"{StatusSubject} interaction detected";
        public bool IsAvailable => isAvailable;
        public int InteractionCount { get; private set; }
        public string LastInteractionMessage { get; private set; } = string.Empty;

        private string StatusSubject => string.IsNullOrWhiteSpace(statusLabel) ? promptLabel : statusLabel;

        private void Awake()
        {
            ApplyColor(idleColor);
        }

        public void Configure(string label, string verb)
        {
            promptLabel = label;
            interactionVerb = verb;
        }

        public void ConfigureStatusLabel(string label)
        {
            statusLabel = label;
        }

        public void Interact()
        {
            if (!isAvailable)
            {
                return;
            }

            InteractionCount++;
            LastInteractionMessage = InteractionStatusMessage;
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
