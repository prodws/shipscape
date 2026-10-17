using UnityEngine;

public interface IPickup : IInteractionPrompt
{
    GameObject WorldObject { get; }
    GameObject HeldPrefab { get; }

    public virtual void OnPickedUp()
    {
        AudioManager.Instance.PlayItemPickup();
    }

    public virtual void OnDropped()
    {
        AudioManager.Instance.PlayItemDrop();
    }

    string IInteractionPrompt.GetPrompt() =>  Prompt.Press("pick up");
}