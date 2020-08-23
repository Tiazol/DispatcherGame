using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    public Dictionary<SystemLanguage, Dictionary<string, string>> LocalizationDictionary { get; private set; }
    public SystemLanguage CurrentLanguage
    {
        get => currentLanguage;
        set
        {
            currentLanguage = value;
            PlayerPrefs.SetString(pp_language, currentLanguage.ToString());
            LanguageChanged?.Invoke();
        }
    }

    public event Action LanguageChanged;

    private const string pp_language = "Language";
    private const string localizationPath = "Localization";
    private SystemLanguage currentLanguage;

    private void Awake()
    {
        Instance = this;

        LocalizationDictionary = new Dictionary<SystemLanguage, Dictionary<string, string>>();
        LoadLocalizationResources();
        InitializeLanguage();
    }

    private void InitializeLanguage()
    {
        if (PlayerPrefs.HasKey(pp_language))
        {
            CurrentLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), PlayerPrefs.GetString(pp_language));
        }
        else
        {
            CurrentLanguage = Application.systemLanguage;
        }
    }

    private void LoadLocalizationResources()
    {
        var texts = Resources.LoadAll<TextAsset>(localizationPath);

        /*
         * текущий формат:
         * 
         * language=English
         * play=PLAY
         * quit=QUIT
         * 
         * language=Russian
         * play=ИГРАТЬ
         * quit=ВЫЙТИ
         */

        foreach (var text in texts)
        {
            var separators = new char[] { '\r', '\n' };
            var lines = text.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var dictionary = new Dictionary<string, string>();

            for (int i = 1; i < lines.Length; i++)
            {
                dictionary.Add(lines[i].Split('=')[0], lines[i].Split('=')[1]);
            }

            var language = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), lines[0].Split('=')[1]);
            LocalizationDictionary.Add(language, dictionary);
        }
    }

    public void ChangeLanguageTo(string language)
    {
        CurrentLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), language);
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
