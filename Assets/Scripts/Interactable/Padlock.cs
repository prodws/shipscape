using UnityEngine;

public class Padlock : MonoBehaviour
{
    [SerializeField] private bool isLocked = true;

    public bool IsLocked => isLocked;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider collider;
    [SerializeField] private EquipmentManager equipmentManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
    }
    public void Unlock()
    {
        isLocked = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        AudioManager.Instance.PlayItemDrop();
        equipmentManager.Unequip();
    }
}
