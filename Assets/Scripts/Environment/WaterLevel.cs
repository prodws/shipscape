using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] private float startScaleY = 1f;
    [SerializeField] private float endScaleY = 10f;
    [SerializeField] private GameTimer gameTimer;

    private void Awake()
    {
        gameTimer = GetComponentInChildren<GameTimer>();

        if (cube == null || gameTimer == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        Vector3 scale = cube.localScale;
        scale.y = Mathf.Lerp(startScaleY, endScaleY, gameTimer.NormalizedTime);
        cube.localScale = scale;
    }
}