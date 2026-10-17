using System.Collections;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject knob;
    [SerializeField] private int knobRotationAngle = 120;
    private Rigidbody cupRb;
    private BoxCollider knobCollider;

    private Screw[] screws;

    private void Awake()
    {
        screws = GetComponentsInChildren<Screw>();

        cupRb = cup.GetComponent<Rigidbody>();
        cupRb.isKinematic = true;
        cupRb.useGravity = false;

        knobCollider = knob.GetComponent<BoxCollider>();
        knobCollider.enabled = false;
    }

    private void Update()
    {
        CheckScrews();
    }

    private void CheckScrews()
    {
        foreach (Screw screw in screws)
        {
            if (screw.IsScrewedIn)
                return;
        }

        DropCup();
        knobCollider.enabled = true;
        enabled = false;
    }

    private void DropCup()
    {
        cup.transform.SetParent(null);

        Rigidbody rb = cup.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        cup.layer = LayerMask.NameToLayer("IgnorePlayer");

    }

    public IEnumerator RotateKnob()
    {
        Quaternion startRotation = knob.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(-knobRotationAngle, 0, 0);

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            knob.transform.rotation = Quaternion.Slerp(
                startRotation, targetRotation, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        knob.transform.rotation = targetRotation;
    }

    public void disableKnobCollider()
    {
        knobCollider.enabled = false;
    }
    
}