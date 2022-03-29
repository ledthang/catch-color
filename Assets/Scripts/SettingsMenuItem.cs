using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;

    SettingsMenu settingsMenu;
    Button button;
    int index;

    private void Awake()
    {
        img = GetComponent<Image>();
        if (img == null)
            img = GetComponentInChildren<Image>();

        settingsMenu = transform.parent.GetComponent<SettingsMenu>();
        index = transform.GetSiblingIndex() - 1;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClick);
    }
    private void Update()
    {
        if (img == null)
            img = GetComponentInChildren<Image>();
    }
    void OnItemClick()
    {
        settingsMenu.OnItemClick(index);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnItemClick);
    }
}
