using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void ShowUp(int experiencePoints)
    {
        CursorControl.EnableCursor();
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        PlayerStats.OnPlayerKilled += ShowUp;
    }

    private void OnDestroy()
    {
        PlayerStats.OnPlayerKilled -= ShowUp;
    }
}
