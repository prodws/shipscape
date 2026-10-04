using UnityEngine;
using UnityEngine.UI;

public class UnderwaterEffect : MonoBehaviour
{
    [SerializeField] private Image overlay;

    private Camera playerCamera;
    private WaterLevel waterLevel;

    [SerializeField] private float waterOffset = 0.1f;

    private void Awake()
    {
        playerCamera = GetComponentInParent<Camera>();
        waterLevel = FindAnyObjectByType<WaterLevel>();

        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }

    private bool ValidateDependencies()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Camera not found in parent.", this);
            return false;
        }

        if (waterLevel == null)
        {
            Debug.LogError("WaterLevel not found.", this);
            return false;
        }

        if (overlay == null)
        {
            Debug.LogError("Overlay not assigned.", this);
            return false;
        }

        return true;
    }

    private void Update()
    {
        //overlay.enabled = playerCamera.transform.position.y < waterLevel.SurfaceY + waterOffset;
    }
}