using System.Collections;
using UnityEngine;

public class Knob : MonoBehaviour, IInteractionPrompt
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

    public string GetPrompt()
    {
        return "[E] turn knob";
    }
}
