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

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }


    private void Awake()
    {
        pauseAction = playerInput.actions["Pause"];
        SetPaused(false);
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (paused && settingsPanel.activeSelf)
                CloseSettings();
            else
                SetPaused(!paused);
        }

        float target = paused ? 1f : 0f;
        blurVolume.weight = Mathf.MoveTowards(
            blurVolume.weight, target, fadeSpeed * Time.unscaledDeltaTime);
    }

    public void SetPaused(bool value)
    {
        IsPaused = value;
        paused = value;

        hudRoot.SetActive(!value);
        AudioListener.pause = value;
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetHeartbeatAudible(value);
        menuRoot.SetActive(value);
        blurVolume.weight = value ? 1f : 0f;

        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);

        Time.timeScale = value ? 0f : 1f;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }
}