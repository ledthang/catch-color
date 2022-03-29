using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] Button playpauseButton;
    [SerializeField] Image playImg;
    [SerializeField] Image pauseImg;
    [SerializeField] Image replayImg;

    bool isPause;
    public Text bestScoreText;
    protected override void Initialize()
    {
    }

    private void Awake()
    {
        SoundManager.GetInstance().LoadSoundButtonImg();
        isPause = false;
        UpdatePlayPauseButton();
        playpauseButton.onClick.AddListener(PlayPauseOnClick);
    }

    void Update()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        bestScoreText.text = "Best: " + bestScore;
    }

    public void UpdatePlayPauseButton()
    {
        if (GameController.GetInstance().gameOver)
        {
            playImg.gameObject.SetActive(false);
            pauseImg.gameObject.SetActive(false);
            replayImg.gameObject.SetActive(true);
        }
        else
        {
            playImg.gameObject.SetActive(isPause);
            pauseImg.gameObject.SetActive(!isPause);
            replayImg.gameObject.SetActive(false);
            if (isPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    void PlayPauseOnClick()
    {
        SoundManager.GetInstance().PlayClickSound();
        if (GameController.GetInstance().gameOver)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            isPause = !isPause;
            UpdatePlayPauseButton();
        }
    }

    private void OnDestroy()
    {
        playpauseButton.onClick.RemoveListener(PlayPauseOnClick);
    }

}
