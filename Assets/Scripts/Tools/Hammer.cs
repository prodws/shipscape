using UnityEngine;

public class Hammer : MonoBehaviour, ITool, IPickup
{
    [SerializeField] private GameObject heldPrefab;
    [SerializeField] private GameObject pickupPrefab;

    public GameObject HeldPrefab => heldPrefab;
    public GameObject WorldObject => gameObject;
    public GameObject PickupPrefab => pickupPrefab;
    public ToolInput Input => ToolInput.Attack;

    public bool Use(RaycastHit hit)
    {
        Debris debris = hit.collider.GetComponentInParent<Debris>();

        if (debris != null)
        {
            debris.Hit(transform.forward);
            return true;
        }
        return false;
    }
}