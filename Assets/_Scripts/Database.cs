using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static PlayerData SignIn(string user, string pass)
    {
        string signin = $"https://pulseless-electrici.000webhostapp.com/get.php?user={user}&pass={pass}";

        WWW www = new WWW(signin);

        do { } while (www.isDone == false);

        //yield return www;
        if(www.text != "")
        {
            string[] text = www.text.Split('\t');
            return new PlayerData(text[0], int.Parse(text[1]));
        }
        return null;
    }

    public static bool SignUp(string user, string pass)
    {
        string signup = $"https://pulseless-electrici.000webhostapp.com/signup.php?user={user}&pass={pass}";
        WWW www = new WWW(signup);
        //yield return www;
        do { } while (www.isDone == false);
        string result = www.text;

        return result == "suc";
    }
}
