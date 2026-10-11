using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField, Min(0f)] private float duration = 10f * 60f;

    private float elapsedTime;

    public float ElapsedTime => elapsedTime;
    public float NormalizedTime => Mathf.Clamp01(elapsedTime / duration);
    public bool IsFinished => elapsedTime >= duration;

    private void Update()
    {
        if (IsFinished)
            return;

        elapsedTime += Time.deltaTime;
    }
}