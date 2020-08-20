using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationData
{
    private string language;
    private Dictionary<string, string> dictionary;

    public LocalizationData(string language)
    {
        this.language = language;
        dictionary = new Dictionary<string, string>();
    }
}
