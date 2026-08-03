namespace AIFounder.Presentation
{
    public interface IPrototypeInteractable
    {
        string PromptLabel { get; }
        string InteractionVerb { get; }
        bool IsAvailable { get; }
        void Interact();
    }
}