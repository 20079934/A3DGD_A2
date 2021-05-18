using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviour
{
    private static bool darkmode = false;

    [Header("Screens")]
    [SerializeField]
    GameObject authScreen;
    [SerializeField]
    GameObject gameScreen;

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
        }
    }

    /// <summary>
    /// Sign in workflow
    /// </summary>
    public void signIn()
    {
        dbAuth(true);
    }

    public void signUp()
    {
        dbAuth(false);
    }

    public void dbAuth(bool login)
    {
        if (username.text == "")
        {
            incorrect.gameObject.SetActive(true);
            incorrect.text = "Please enter a username";
            return;
        }
        else if (password.text == "")
        {
            incorrect.gameObject.SetActive(true);
            incorrect.text = "Please enter a password";
            return;
        }
        if (login)
        {
            GameManager.player = Database.SignIn(username.text, password.text);
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
            if(Database.SignUp(username.text, password.text))
            {
                dbAuth(true);
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
}
