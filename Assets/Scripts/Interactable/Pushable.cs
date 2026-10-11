using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pushSpeed = 1.5f;

    private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

    [SerializeField] private float pushSoundCooldown = 0.4f;
    private float nextPushSoundTime;

    public void Push(Vector3 direction)
    {
        rb.linearVelocity = direction * pushSpeed;

        if (Time.time >= nextPushSoundTime)
        {
            AudioManager.Instance.PlayBoxPush();
            nextPushSoundTime = Time.time + pushSoundCooldown;
        }
    }
}
