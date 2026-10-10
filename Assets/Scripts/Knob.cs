using System.Collections;
using UnityEngine;

public class Knob : MonoBehaviour
{
    [SerializeField] Panel panel;
    [SerializeField] TriggerDoor panelDoor;

    public void Interact()
    {
        StartCoroutine(InteractSequence());
    }

    private IEnumerator InteractSequence()
    {
        yield return StartCoroutine(panel.RotateKnob());

        panel.disableKnobCollider();
        panelDoor.Open();
    }
}
