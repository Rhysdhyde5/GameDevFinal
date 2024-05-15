using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int score;
    public int lives;
    public int home1, home2, home3, home4, home5;

    public PlayerData (int score, int lives, int home1, int home2, int home3, int home4, int home5)
    {
        this.lives = lives;
        this.score = score;
        this.home1 = home1;
        this.home2 = home2;
        this.home3 = home3;
        this.home4 = home4;
        this.home5 = home5;
    }
}
