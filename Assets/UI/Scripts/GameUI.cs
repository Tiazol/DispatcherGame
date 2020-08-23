using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [Header("Stars")]
    [SerializeField] private Image[] slots;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite pickedUpStar;
    [Header("Texts")]
    [SerializeField] private Text wagonsCount;
    [SerializeField] private Text levelCompletedText;
    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [Header("Panels")]
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject levelCompletedDialog;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ProgressManager.Instance.LevelCompleted += ShowLevelCompletedDialog;
    }

    public void OnNextOrRetryButtonPressed()
    {
        if (ProgressManager.Instance.CurrentStarsCount == 0)
        {
            GameManager.Instance.LoadThisLevel();
        }
        else
        {
            GameManager.Instance.LoadNextLevel();
        }
    }

    public void ShowQuitConfirmation()
    {
        GameManager.Instance.PauseGame();
        quitConfirmation.SetActive(true);
    }

    public void CloseQuitConfirmation()
    {
        GameManager.Instance.ResumeGame();
        quitConfirmation.SetActive(false);
    }

    public void ShowLevelCompletedDialog()
    {
        var currentStars = ProgressManager.Instance.CurrentStarsCount;
        var savedStars = ProgressManager.Instance.SavedStarsCount;
        
        if (currentStars == 0)
        {
            levelCompletedText.text = LocalizationManager.Instance.GetLocalizedString("levelNotCompleted");
            nextButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedString("retry");

            slots[0].sprite = slots[1].sprite = slots[2].sprite = emptyStar;
        }
        else
        {
            levelCompletedText.text = LocalizationManager.Instance.GetLocalizedString("levelCompleted");
            nextButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedString("next");

            slots[0].sprite = currentStars >= 1 ? fullStar : savedStars >= 1 ? pickedUpStar : emptyStar;
            slots[1].sprite = currentStars >= 2 ? fullStar : savedStars >= 2 ? pickedUpStar : emptyStar;
            slots[2].sprite = currentStars >= 3 ? fullStar : savedStars >= 3 ? pickedUpStar : emptyStar;
        }

        wagonsCount.text = $"{ProgressManager.Instance.SuccessfulWagons} / {ProgressManager.Instance.TotalWagons}";

        levelCompletedDialog.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.ResumeGame(); // а точно нужно?
        GameManager.Instance.LoadLevel(0);
    }
}
