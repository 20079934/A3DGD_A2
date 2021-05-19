using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static string state = "fail";

    /// <summary>
    /// for use before using db commands
    /// </summary>

    public static IEnumerator SignIn(string user, string pass)
    {
        string signin = $"https://pulseless-electrici.000webhostapp.com/get.php?user={user}&pass={pass}";

        WWW www = new WWW(signin);

        yield return www;

        if(www.text != "")
        {
            string[] text = www.text.Split('\t');
            GameManager.player = new PlayerData(text[0], int.Parse(text[1]));
        }
    }

    public static IEnumerator SignUp(string user, string pass)
    {
        string signup = $"https://pulseless-electrici.000webhostapp.com/signup.php?user={user}&pass={pass}";
        WWW www = new WWW(signup);
        yield return www;

        state = www.text;
    }

    public static IEnumerator updatePlayer(string user, int score)
    {
        string update = $"https://pulseless-electrici.000webhostapp.com/update.php?user={user}&score={score}";
        WWW www = new WWW(update);
        yield return www;

        state = www.text;
    }
}
