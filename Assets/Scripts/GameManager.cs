using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Home[] homes;
    private Frogger frogger;
    public GameObject stopTimer;
    public GameObject gameOverMenu;
    public Text timeText;
    public Text livesText;
    public Text scoreText;
    public int home1, home2, home3, home4, home5;
    public int lives;
    public int score;
    public int time;
    public int Lives => lives;
    public int Score => score;
    public int Time => time;
    private float farthestRow = -6;



    private void Awake()
    {
        frogger = FindObjectOfType<Frogger>();
    }

    private void Start()
    {
        LoadPlayer();
    }

    private void NewGame()
    {
        gameOverMenu.SetActive(false);
        stopTimer.SetActive(false);
        farthestRow = -6;

        ResetGame();
    }

    private void NewLevel()
    {
        for (int i = 0; i < homes.Length; i++) {
            homes[i].enabled = false;
        }
        farthestRow = -6;

        Respawn();
    }


    private void Respawn()
    {
        frogger.Respawn();

        StopAllCoroutines();
        StartCoroutine(Timer(30));
        SavePlayer();
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;
        timeText.text = time.ToString();

        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
            timeText.text = time.ToString();
        }

        frogger.Death();
    }

    public void HomeOccupied(int homeNum)
    {
        switch (homeNum)
        {
            case 1 :
                home1 = 1;
                break;
            case 2 :
                home2 = 1;
                break;
            case 3 :
                home3 = 1;
                break;
            case 4 :
                home4 = 1;
                break;
            case 5 :
                home5 = 1;
                break;
        }
        frogger.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50);
        
        if (Cleared())
        {
            SetLives(lives + 1);
            SetScore(score + 1000);
            home1 = 0;
            home2 = 0;
            home3 = 0;
            home4 = 0;
            home5 = 0;

            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            farthestRow = -6;
            Invoke(nameof(Respawn), 1f);
            SavePlayer();
        }
    }
    
     public void AdvancedRow(float curY)
    {
        if (curY > farthestRow)
        {
            farthestRow = curY;
            SetScore(score + 10);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled) {
                return false;
            }
        }

        return true;
    }

    public void Died()
{
    SetLives(lives - 1);

    if (lives > 0)
    {
        Invoke(nameof(Respawn), 1f);

        if (lives == 1)
        {
            stopTimer.SetActive(true);
            }   
        
    }
     
    
    else
    {
        Invoke(nameof(GameOver), 1f);
    }
}

     private void GameOver()
    {
        frogger.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(CheckForPlayAgain());
    }

    private IEnumerator CheckForPlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return)) {
                playAgain = true;
            }

            yield return null;
        }

        NewGame();
    }

    public void AddLife()
    {
        lives++;
        SetLives(lives);
    }

    

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
        SavePlayer();
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(score, lives, home1, home2, home3, home4, home5);
    }

    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();

        if (data == null)
        {
            Debug.Log("new game");
            NewGame();
            return;
        }

        lives = data.lives;
        score = data.score;
        home1 = data.home1;
        home2 = data.home2;
        home3 = data.home3;
        home4 = data.home4;
        home5 = data.home5;

        if (home1 == 1)
        {
            homes[0].enabled = true;
        }
        if (home2 == 1)
        {
            homes[1].enabled = true;
        }
        if (home3 == 1)
        {
            homes[2].enabled = true;
        }
        if (home4 == 1)
        {
            homes[3].enabled = true;
        }
        if (home5 == 1)
        {
            homes[4].enabled = true;
        }
        gameOverMenu.SetActive(false);
        Respawn();
        SetLives(lives);
        SetScore(score);
    }

    public void ResetGame()
    {
        SetScore(0);
        SetLives(3);
        home1 = 0;
        home2 = 0;
        home3 = 0;
        home4 = 0;
        home5 = 0;
        NewLevel();
        SavePlayer();
        farthestRow = -6;

    }
}


