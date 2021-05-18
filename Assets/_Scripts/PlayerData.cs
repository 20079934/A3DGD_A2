﻿using System.Collections;
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

}
