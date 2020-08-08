using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public Image[] starBoxes;
    public Sprite[] starSprites;

    private void Start()
    {
        ShowStars(ProgressManager.Instance.GetScoreOfLevel(levelNumber));
    }

    private void ShowStars(int score)
    {
        switch (score)
        {
            case 3:
                starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = starSprites[1];
                break;
            case 2:
                starBoxes[0].sprite = starBoxes[1].sprite = starSprites[1];
                starBoxes[2].sprite = starSprites[0];
                break;
            case 1:
                starBoxes[0].sprite = starSprites[1];
                starBoxes[1].sprite = starBoxes[2].sprite = starSprites[0];
                break;
            case 0:
                starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = starSprites[0];
                break;
        }
    }
}
