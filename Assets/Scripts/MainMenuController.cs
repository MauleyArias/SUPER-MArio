using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance?.PlayMenuMusic();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1-1");
    }
}
