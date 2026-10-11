using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Floor 1")
        {
            AudioManager.Instance.PlayHeartBeat();
            AudioManager.Instance.PlayRattle();
            AudioManager.Instance.PlayWaterGushing();
            AudioManager.Instance.PlayEvacuation();
        }
    }   

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
