using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
    [Space]
    [Header("sound button img")]
    Image soundOnImg;
    Image soundOffImg;

    public AudioClip clickSound;
    public AudioClip scoreSound;
    public AudioClip gameOverSound;

    public AudioSource mySource;
    public bool soundOn
    {
        get
        {
            return PlayerPrefs.GetInt("SoundOn") == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt("SoundOn", value ? 1 : 0);
        }
    }
    protected override void Initialize()
    {
        GameObject.DontDestroyOnLoad(this);
    }
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SoundOn"))
        {
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        LoadSoundButtonImg();
        UpdateSoundButton();
        Initialize();
    }

    public void LoadSoundButtonImg()
    {
        if (soundOnImg == null)
        {
            soundOnImg = GameObject.Find("Canvas/SettingsMenu/SoundButton/SoundOn").GetComponent<Image>();
            soundOffImg = GameObject.Find("Canvas/SettingsMenu/SoundButton/SoundOff").GetComponent<Image>();
        }
    }

    public void EnableSound()
    {
        mySource.volume = 1f;
    }

    public void DisableSound()
    {
        mySource.volume = 0f;
    }

    public void PlayClickSound()
    {
        mySource.clip = clickSound;
        MakeSound();
    }

    public void PlayScoreSound()
    {
        mySource.clip = scoreSound;
        MakeSound();
    }

    public void PlayGameOverSound()
    {
        mySource.clip = gameOverSound;
        MakeSound();
    }

    void MakeSound()
    {
        mySource.Play();
    }

    public void UpdateSoundButton()
    {
        if (soundOn)
        {
            soundOnImg.gameObject.SetActive(true);
            soundOffImg.gameObject.SetActive(false);
            EnableSound();
        }
        else
        {
            soundOnImg.gameObject.SetActive(false);
            soundOffImg.gameObject.SetActive(true);
            DisableSound();
        }
    }
}
