using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour, IInteractionPrompt
{
    [SerializeField] private string sceneName;

    public void Enter()
    {
        Debug.Log("Loading: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public string GetPrompt()
    {
        return  Prompt.Press("enter");
    }
}