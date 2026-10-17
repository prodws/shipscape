using UnityEngine;

public class Screwdriver : MonoBehaviour, ITool, IPickup
{
    [SerializeField] private GameObject heldPrefab;
    [SerializeField] private GameObject pickupPrefab;

    public GameObject HeldPrefab => heldPrefab;
    public GameObject WorldObject => gameObject;
    public GameObject PickupPrefab => pickupPrefab;
    public ToolInput Input => ToolInput.Interact;



    public bool Use(RaycastHit hit)
    {
        Screw screw = hit.collider.GetComponentInParent<Screw>();

        if (screw != null){
            screw.Unscrew();
            return true;
        }
        return false;
    }
}