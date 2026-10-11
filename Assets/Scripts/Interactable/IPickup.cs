using UnityEngine;

public interface IPickup
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


}