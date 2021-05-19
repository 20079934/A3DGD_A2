using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    string name;
    int score;

    public PlayerData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string getName()
    {
        return name;
    }

    public int getScore()
    {
        return score;
    }

    public IEnumerator resetScore()
    {
        score = 0;
        yield return Database.updatePlayer(name, score);
    }

    public void setScore(int scr)
    {
        score = scr;
    }

}
