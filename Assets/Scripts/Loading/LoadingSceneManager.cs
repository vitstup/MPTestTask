using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Lobby");
    }
}