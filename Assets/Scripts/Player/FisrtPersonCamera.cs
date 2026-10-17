using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float sensitivity = 0.15f;
    [SerializeField] float minPitch = -80f;
    [SerializeField] float maxPitch = 80f;

    [SerializeField] CharacterController controller;
    [SerializeField] float eyeOffset = 0.2f;
    public static float SensitivityMultiplier = 1f;

    float pitch;
    

    private void Awake()
    {
        SensitivityMultiplier = PlayerPrefs.GetFloat("sensitivity", 1f);

        controller = GetComponentInParent<CharacterController>();

        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool ValidateDependencies()
    {
        if (controller == null)
        {
            Debug.LogError("CharacterController is missing.", this);
            enabled = false;
            return false;
        }
        return true;
    }

    void Update()
    {
        if (PauseMenu.IsPaused) return;

        if (Mouse.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 mouse = Mouse.current.delta.ReadValue();

        if (float.IsNaN(mouse.x) || float.IsNaN(mouse.y))
        {
            Debug.LogWarning("Invalid mouse delta detected, ignoring frame.");
            return;
        }

        float s = sensitivity * SensitivityMultiplier;

        // Turn the entire player left/right
        transform.parent.Rotate(Vector3.up * mouse.x * s);

        // Turn only the camera up/down
        pitch -= mouse.y * s;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void LateUpdate()
    {
        Vector3 p = transform.localPosition;
        p.y = controller.center.y + controller.height * 0.5f - eyeOffset;
        transform.localPosition = p;
    }
}