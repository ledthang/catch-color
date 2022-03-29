using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button playButton;
    GameObject hexagon;

    private void Awake()
    {
        playButton = GameObject.Find("Play").GetComponent<Button>();
        hexagon = GameObject.Find("Hexagon");

        playButton.transform.position = hexagon.transform.position;
        playButton.onClick.AddListener(PlayOnClick);
    }

    void PlayOnClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayOnClick);
    }
}
