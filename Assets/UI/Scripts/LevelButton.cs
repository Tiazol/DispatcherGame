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
        //switch (score)
        //{
        //    case 3:
        //        if (!ProgressManager.Instance.Progress[levelNumber].Item1)
        //        {
        //            starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = null;
        //        }
        //        else
        //        {
        //            starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = starSprites[1];
        //        }
        //        break;
        //    case 2:
        //        starBoxes[0].sprite = starBoxes[1].sprite = starSprites[1];
        //        starBoxes[2].sprite = starSprites[0];
        //        break;
        //    case 1:
        //        starBoxes[0].sprite = starSprites[1];
        //        starBoxes[1].sprite = starBoxes[2].sprite = starSprites[0];
        //        break;
        //    case 0:
        //        starBoxes[0].sprite = starBoxes[1].sprite = starBoxes[2].sprite = starSprites[0];
        //        break;
        //}


        if (ProgressManager.Instance.Progress.ContainsKey(levelNumber))
        {
            starBoxes[0].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 1 ? starSprites[1] : starSprites[0];
            starBoxes[1].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 2 ? starSprites[1] : starSprites[0];
            starBoxes[2].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 3 ? starSprites[1] : starSprites[0];
        }
        else
        {
            starBoxes[0].color = new Color(starBoxes[0].color.r, starBoxes[0].color.b, starBoxes[0].color.a, 0f);
            starBoxes[1].color = new Color(starBoxes[1].color.r, starBoxes[1].color.b, starBoxes[1].color.a, 0f);
            starBoxes[2].color = new Color(starBoxes[2].color.r, starBoxes[2].color.b, starBoxes[2].color.a, 0f);
        }
    }
}
