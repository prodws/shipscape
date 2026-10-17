using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Volume blurVolume;
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private float fadeSpeed = 8f;
    [SerializeField] private GameObject hudRoot;

    [SerializeField] private PlayerInput playerInput;
    private InputAction pauseAction;

    private bool paused;
    public static bool IsPaused { get; private set; }

    private void Awake()
    {
        pauseAction = playerInput.actions["Pause"];
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame())
            SetPaused(!paused);

        float target = paused ? 1f : 0f;
        blurVolume.weight = Mathf.MoveTowards(
            blurVolume.weight, target, fadeSpeed * Time.unscaledDeltaTime);
    }

    public void SetPaused(bool value)
    {
        IsPaused = value;
        hudRoot.SetActive(!value);
        AudioListener.pause = value;
        paused = value;
        menuRoot.SetActive(value);
        Time.timeScale = value ? 0f : 1f;

        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }
}