using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLocalizedText : MonoBehaviour
{
    public string stringID;

    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        LoadString();
        LocalizationManager.Instance.LanguageChanged += LoadString;
    }

    private void LoadString()
    {
        text.text = LocalizationManager.Instance.GetLocalizedString(stringID);
    }
}
