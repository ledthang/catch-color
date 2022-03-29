using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Random = UnityEngine.Random;


public class GameController : Singleton<GameController>
{
    public GameObject[] Balls;
    public GameObject[] NextBalls;
    public Vector3 SpawnPoint;
    public GameObject gameOverText;
    public bool gameOver = false;

    private Text countdownText;
    private Text scoreText;
    public int score { get; private set; }
    private List<GameObject> ballsList;

    protected override void Initialize()
    {
    }

    void Awake()
    {
        ballsList = new List<GameObject>();
        InitGame();
    }

    void InitGame()
    {
        score = 0;
        ballsList.Clear();
        countdownText = GameObject.Find("Countdown").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "0";
        scoreText.gameObject.SetActive(false);
        gameOverText.SetActive(false);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        float timeRemaining = 4;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            countdownText.text = (int)timeRemaining + "";
            yield return null;
        }
        countdownText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        StartCoroutine(SpawnBall());
    }


    private IEnumerator SpawnBall()
    {
        while (!gameOver)
        {
            int randomIndex = Random.Range(0, Balls.Length);
            UpdateNextBall(randomIndex);
            GameObject ball = Instantiate(Balls[randomIndex], SpawnPoint, Quaternion.identity) as GameObject;
            ballsList.Add(ball);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void UpdateNextBall(int index)
    {
        for (int i = 0; i < NextBalls.Length; i++)
        {
            if (i != index)
            {
                NextBalls[i].SetActive(false);
            }
            else
            {
                NextBalls[i].SetActive(true);
            }
        }
    }

    public void Score()
    {
        SoundManager.GetInstance().PlayScoreSound();
        score += 1;
        scoreText.text = "" + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        SoundManager.GetInstance().PlayGameOverSound();
        foreach (GameObject ball in ballsList)
        {
            Destroy(ball);
        }
        ballsList.Clear();
        gameOver = true;
        GameUI.GetInstance().UpdatePlayPauseButton();
        gameOverText.SetActive(true);
        scoreText.gameObject.transform.DOScale(0.25f, 1);
        StopCoroutine(SpawnBall());
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
