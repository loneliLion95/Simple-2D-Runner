using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance = null;
    public Text highScoreText;
    public Text yourScoreText;
    public Text youAreDead;
    public Text newHighScore;
    public GameObject[] obstacles;
    public Transform spawnPoint;
    float nextSpawn;
    public float timeToBoost;
    float nextBoost;
    private int yourScore = 0, highScore = 0;
    public static bool gameStopped;
    float nextScoreIncrease = 0f;
    public static bool deadIsDone;

    void Start()
    {
        if (instance == null)
            instance = this;

        highScore = PlayerPrefs.GetInt("highScore");
        yourScore = 0;
        gameStopped = false;
        Time.timeScale = 1f;
        highScoreText.text = "High Score  " + highScore;
        nextBoost = Time.unscaledTime + timeToBoost;
    }

    void Update()
    {
        if (!gameStopped)
            IncreaseYourScore();

        highScoreText.text = "High Score  " + highScore;
        yourScoreText.text = "Your Score  " + yourScore;

        if (Time.time > nextSpawn)
            SpawnObstacle();

        if (Time.unscaledTime > nextBoost && !gameStopped)
            BoostTime();

        if (deadIsDone)
            RestartGame();
    }

    void SpawnObstacle()
    {
        if (!gameStopped)
        {
            nextSpawn = Time.time + Random.Range(2f, 8f);
            int randomObstacle = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
        }
    }

    void BoostTime()
    {
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.15f;
    }

    void IncreaseYourScore()
    {
        if (Time.unscaledTime > nextScoreIncrease)
        {
            yourScore += 1;
            nextScoreIncrease = Time.unscaledTime + 1;
        }
    }

    public void PlayerHit()
    {
        Time.timeScale = 1;
        gameStopped = true;
    }

    public void RestartGame()
    {
        youAreDead.gameObject.SetActive(true);

        if (yourScore > highScore)
        {   
            SoundManagerScript.PlaySound("win");

            newHighScore.gameObject.SetActive(true);
            newHighScore.text = "NEW HIGH SCORE - " + yourScore + "!!!";

            PlayerPrefs.SetInt("highScore", yourScore);
            PlayerPrefs.Save();
        }
            
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                deadIsDone = false;
                SceneManager.LoadScene(1);
            }
        }
    }
}
