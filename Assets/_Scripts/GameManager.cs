using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static PlayerData player;
    public static int currentLevel;

    public void nextLevel()
    {
        if(currentLevel<3)
        {

            if(currentLevel==2)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            currentLevel++;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            StartCoroutine("updatePlayer");
            SceneManager.LoadScene($"Level{currentLevel}");
        }
        else
        {
            //highscore
            currentLevel++;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            StartCoroutine("updatePlayer");
            SceneManager.LoadScene($"Launcher");
        }
    }


    IEnumerator updatePlayer()
    {
        GameManager.player.setScore(4 * (currentLevel));
        yield return Database.updatePlayer(player.getName(), 4 * (currentLevel));
    }
}
