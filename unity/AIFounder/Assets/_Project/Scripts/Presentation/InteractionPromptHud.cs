using UnityEngine;
using UnityEngine.UI;

namespace AIFounder.Presentation
{
    public sealed class InteractionPromptHud : MonoBehaviour
    {
        [SerializeField] private Text promptText;
        [SerializeField] private string inputHint = "E";

        public string VisibleText => promptText != null ? promptText.text : string.Empty;
        public bool IsVisible => promptText != null && promptText.enabled;

        private void Awake()
        {
            Hide();
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
    }
}