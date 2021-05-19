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
        if(currentLevel<4)
        {
            currentLevel++;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            StartCoroutine("updatePlayer");
            SceneManager.LoadScene($"Level{currentLevel}");
        }
    }


    IEnumerator updatePlayer()
    {
        yield return Database.updatePlayer(player.getName(), 4 * (currentLevel));
    }
}
