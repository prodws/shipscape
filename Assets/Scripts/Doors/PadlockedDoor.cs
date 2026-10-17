using UnityEngine;

public class PadlockedDoor : MonoBehaviour, IInteractionPrompt
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private float doorLength = 1.5f;
    [SerializeField] private float step = 0.5f; // increased speed (units per second)

    private Vector3 startLocalPosition;

    void Awake()
    {
        padlock = GetComponentInChildren<Padlock>();   
        startLocalPosition = transform.localPosition;
    }

    public void Open()
    {
        if (padlock.IsLocked || IsFullyOpen)
        return;

        float distance = Vector3.Distance(startLocalPosition, transform.localPosition);

        if (distance < doorLength)
        {
            transform.localPosition -= Vector3.forward * step * Time.deltaTime;
        }
    }
    
    private bool IsFullyOpen =>
    Vector3.Distance(startLocalPosition, transform.localPosition) >= doorLength;


    public string GetPrompt()
    {
        return padlock.IsLocked || IsFullyOpen ? null : Prompt.Hold("open");
    }


}
