using UnityEngine;
using UnityEngine.UI;

namespace AIFounder.Presentation
{
    public sealed class InteractionPromptHud : MonoBehaviour
    {
        [SerializeField] private Text promptText;
        [SerializeField] private Text statusText;
        [SerializeField] private string inputHint = "E";

        public string VisibleText => promptText != null ? promptText.text : string.Empty;
        public string StatusText => statusText != null ? statusText.text : string.Empty;
        public bool IsVisible => promptText != null && promptText.enabled;
        public bool IsStatusVisible => statusText != null && statusText.enabled;

        private void Awake()
        {
            Hide();
            HideStatus();
        }

        public void Show(IPrototypeInteractable interactable)
        {
            if (promptText == null || interactable == null || !interactable.IsAvailable)
            {
                Hide();
                return;
            }

            promptText.text = $"[{inputHint}] {interactable.InteractionVerb}: {interactable.PromptLabel}";
            promptText.enabled = true;
        }

        public void Hide()
        {
            if (promptText == null)
            {
                return;
            }

            promptText.text = string.Empty;
            promptText.enabled = false;
        }

        public void ShowStatus(string message)
        {
            if (statusText == null || string.IsNullOrWhiteSpace(message))
            {
                HideStatus();
                return;
            }

            statusText.text = message;
            statusText.enabled = true;
        }

        public void HideStatus()
        {
            if (statusText == null)
            {
                return;
            }

            statusText.text = string.Empty;
            statusText.enabled = false;
        }
    }
}
