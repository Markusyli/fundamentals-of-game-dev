using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartButton()
    {
        Destroy(PlayerManager.instance.player);
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ShowUp()
    {
        CursorControl.EnableCursor();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        CursorControl.DisableCursor();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        PlayerStats.OnPlayerKilled += ShowUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf)
                Hide();
            else
                ShowUp();
        }
    }

    private void OnDestroy()
    {
        PlayerStats.OnPlayerKilled -= ShowUp;
    }
}
