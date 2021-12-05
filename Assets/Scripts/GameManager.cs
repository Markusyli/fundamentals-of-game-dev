using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverMenu gameOverMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverMenu?.gameObject.activeInHierarchy == true && !PlayerManager.instance.player.GetComponent<CharacterStats>().isDead)
                gameOverMenu.Hide();
            else
                gameOverMenu.ShowUp();
        }
    }
}
