using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing = new Vector2(35, 0);

    [Space]
    [Header("main button rotation")]
    [SerializeField] float rotationDuration = 0.3f;
    [SerializeField] Ease rotationEase = Ease.Linear;

    [Space]
    [Header("animation")]
    [SerializeField] float expandDuration = 0.3f;
    [SerializeField] float collapseDuration = 0.4f;
    [SerializeField] Ease expandEase = Ease.OutBack;
    [SerializeField] Ease collapseEase = Ease.InBack;

    [Space]
    [Header("fading")]
    [SerializeField] float expandFadeDuration = 0.3f;
    [SerializeField] float collapseFadeDuration = 0.6f;

    Button mainButton;
    SettingsMenuItem[] menuItems;
    bool isExpanded = false;


    Vector2 mainButtonPos;
    int itemsCount;

    void Start()
    {
        itemsCount = transform.childCount - 1; //-1 = main button
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPos = mainButton.transform.position;
        ResetPos();
        SoundManager.GetInstance().UpdateSoundButton();
        //spacing *= ((float)Screen.height / (float)Screen.width) / (16f / 9f);
        spacing.x = Screen.width / 7.5f;
    }

    void ResetPos()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].transform.position = mainButtonPos;
        }
    }

    void ToggleMenu()
    {
        Debug.Log("Toggle");
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].transform
                    .DOMove(mainButtonPos + spacing * (i + 1), expandDuration)
                    .SetEase(expandEase);

                menuItems[i].img
                    .DOFade(1f, expandFadeDuration)
                    .From(0f);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].transform
                    .DOMove(mainButtonPos, collapseDuration)
                    .SetEase(collapseEase);

                menuItems[i].img
                    .DOFade(0f, collapseFadeDuration);
            }
        }

        mainButton.transform
            .DORotate(Vector3.forward * 180, rotationDuration)
            .From(Vector3.zero)
            .SetEase(rotationEase);
    }

    public void OnItemClick(int index)
    {
        SoundManager.GetInstance().PlayClickSound();
        switch (index)
        {
            case 0:
                Debug.Log("Rate");
                Application.OpenURL(""); //app store link
                break;
            case 1:
                Debug.Log("Sound");
                SoundManager.GetInstance().soundOn = !SoundManager.GetInstance().soundOn;
                Debug.Log(SoundManager.GetInstance().soundOn);
                SoundManager.GetInstance().UpdateSoundButton();
                break;
            case 2:
                Debug.Log("Exit");
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
                break;
        }
    }


    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
