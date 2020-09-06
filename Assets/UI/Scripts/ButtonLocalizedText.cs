using UnityEngine;
using UnityEngine.UI;

public class ButtonLocalizedText : MonoBehaviour
{
    [SerializeField] private string stringID;

    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        LoadLocalization();
        LocalizationManager.Instance.LanguageChanged += LoadLocalization;
    }

    private void LoadLocalization()
    {
        text.text = LocalizationManager.Instance.GetLocalizedString(stringID);
    }
}
