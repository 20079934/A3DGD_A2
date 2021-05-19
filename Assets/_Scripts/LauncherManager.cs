using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LauncherManager : MonoBehaviour
{
    private static bool darkmode = false;

    [Header("Screens")]
    [SerializeField]
    GameObject authScreen;
    [SerializeField]
    GameObject gameScreen;
    [SerializeField]
    GameObject newGame;
    [SerializeField]
    GameObject continueGame;


    [Header("Auth Accessors")]
    /// <summary>
    /// Accessors for authentication
    /// </summary>
    [SerializeField]
    TMP_InputField username;
    [SerializeField]
    TMP_InputField password;
    [SerializeField]
    TextMeshProUGUI incorrect;

    [Header("Game Accessors")]
    [SerializeField]
    TextMeshProUGUI welcome;
    [SerializeField]
    TextMeshProUGUI score;

    [Header("UI")]
    /// <summary>
    /// Dark modeeeeeee
    /// </summary>
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI DMButton;
    [SerializeField]
    Image BG;

    /// private variables
    bool loginSwitch = true;



    private void Start()
    {
        incorrect.gameObject.SetActive(false);
        modifyDarkMode(darkmode);
        authSignedIn(GameManager.player != null);
    
    }

    /// <summary>
    /// If signed in, then disable authentication screen, and enable the game screen
    /// </summary>
    /// <param name="yes"></param>
    public void authSignedIn(bool yes)
    {
        authScreen.SetActive(!yes);
        gameScreen.SetActive(yes);

        if(yes)
        {
            welcome.text = $"Welcome {GameManager.player.getName()}";
            score.text = $"Your score: {GameManager.player.getScore()}";
            GameManager.currentLevel = PlayerPrefs.GetInt("currentLevel");
            if(GameManager.currentLevel == 0)
            {
                newGame.SetActive(true);
                continueGame.SetActive(false);
            }
            else
            {
                newGame.SetActive(false);
                continueGame.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Sign in workflow
    /// </summary>
    public void signIn()
    {
        loginSwitch = true;
        StartCoroutine("dbAuth");
    }

    public void signUp()
    {
        loginSwitch = false;
        StartCoroutine("dbAuth");
    }

    public IEnumerator dbAuth()
    {
        bool login = loginSwitch;
        if (username.text == "")
        {
            incorrect.gameObject.SetActive(true);
            incorrect.text = "Please enter a username";
            StopCoroutine("dbAuth");
        }
        else if (password.text == "")
        {
            incorrect.gameObject.SetActive(true);
            incorrect.text = "Please enter a password";
            StopCoroutine("dbAuth");
        }
        if (login)
        {
            yield return Database.SignIn(username.text, password.text);
            if (GameManager.player == null)
            {
                incorrect.gameObject.SetActive(true);
                incorrect.text = "Username or Password is incorrect";
            }
            else
                authSignedIn(true);
        }
        else
        {
            yield return Database.SignUp(username.text, password.text);
            if (Database.state == "suc")
            {
                loginSwitch = true;
                yield return StartCoroutine("dbAuth");
            }
            else
            {
                incorrect.gameObject.SetActive(true);
                incorrect.text = "Username already exists!";
            }
        }
    }

    public void toggleDarkMode()
    {
        darkmode = !darkmode;
        modifyDarkMode(darkmode);
    }


    private void modifyDarkMode(bool on)
    {
        title.color = on ? Color.white : Color.black;
        BG.color = on ? Color.black : Color.white;
        welcome.color = on ? Color.white : Color.black;
        score.color = on ? Color.white : Color.black;


        DMButton.text = (on ? "not " : "") + "Dark Mode";
    }

    public void loadCurrentLevel()
    {
        SceneManager.LoadScene($"Level{GameManager.currentLevel}");
    }

    public void startNewGame()
    {
        GameManager.currentLevel = 0;
        StartCoroutine("resetPlayerScore");
        PlayerPrefs.SetInt("currentLevel", GameManager.currentLevel);
        SceneManager.LoadScene("Level0");
    }

    IEnumerator resetPlayerScore()
    {
        yield return GameManager.player.resetScore();
    }
}
