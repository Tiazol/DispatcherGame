using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int LevelNumber { get; set; }

    private Button button;
    [SerializeField] private Image[] starBoxes;
    [Header("StarSprites")  ]
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite pickedUpStar;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        UpdateState(ProgressManager.Instance.IsLevelAvailable(LevelNumber), ProgressManager.Instance.GetSavedStarsCountForLevel(LevelNumber));
        ProgressManager.Instance.ProgressChanged += () => UpdateState(ProgressManager.Instance.IsLevelAvailable(LevelNumber), ProgressManager.Instance.GetSavedStarsCountForLevel(LevelNumber));
    }

    private void UpdateState(bool interactable, int starsCount)
    {
        button.interactable = interactable;

        var emptyColor = new Color(1f, 1f, 1f, 0f);
        var fullColor = new Color(1f, 1f, 1f, 1f);

        starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = fullStar;
        starBoxes[0].color = starBoxes[1].color = starBoxes[2].color = fullColor;

        if (starsCount == 0)
        {
            starBoxes[0].color = starBoxes[1].color = starBoxes[2].color = emptyColor;
        }

        if (starsCount == 1)
        {
            starBoxes[0].sprite = fullStar;
            starBoxes[1].color = starBoxes[2].color = emptyColor;
        }

        if (starsCount == 2)
        {
            starBoxes[0].sprite = starBoxes[1].sprite = fullStar;
            starBoxes[2].color = emptyColor;
        }

        if (starsCount == 3)
        {
            starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = fullStar;
        }
    }
}
