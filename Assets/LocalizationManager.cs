using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    public Dictionary<string, Dictionary<string, string>> LocalizationDictionary { get; private set; }
    public string CurrentLanguage { get; private set; }

    public event Action LanguageChanged;

    private const string localizationPath = "Localization";

    private void Awake()
    {
        Instance = this;

        LocalizationDictionary = new Dictionary<string, Dictionary<string, string>>();
        LoadLocalizations();
    }

    private void LoadLocalizations()
    {
        var texts = Resources.LoadAll<TextAsset>(localizationPath);

        /*
         * текущий формат:
         * 
         * language=en
         * play=PLAY
         * quit=QUIT
         * 
         * language=ru
         * play=ИГРАТЬ
         * quit=ВЫЙТИ
         */

        foreach (var text in texts)
        {
            var separators = new char[] { '\r', '\n' };
            var lines = text.text.Split(separators);
            var dictionary = new Dictionary<string, string>();

            for (int i = 1; i < lines.Length; i++)
            {
                dictionary.Add(lines[i].Split('=')[0], lines[i].Split('=')[1]);
            }

            LocalizationDictionary.Add(lines[0].Split('=')[1], dictionary);
        }
    }

    public void ChangeLanguageTo(string language)
    {
        CurrentLanguage = language;
        LanguageChanged?.Invoke();
    }

    public string GetLocalizedString(string id)
    {
        var str = id;

        if (LocalizationDictionary.ContainsKey(CurrentLanguage))
        {
            if (LocalizationDictionary[CurrentLanguage].ContainsKey(id))
            {
                str = LocalizationDictionary[CurrentLanguage][id];
            }
        }

        return str;
    }
}
