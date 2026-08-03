namespace AIFounder.Presentation
{
    public interface IPrototypeInteractable
    {
        string PromptLabel { get; }
        string InteractionVerb { get; }
        string InteractionStatusMessage { get; }
        bool IsAvailable { get; }
        void Interact();
    }
}
