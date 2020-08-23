using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public Image[] starBoxes;
    public Sprite[] starSprites;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        UpdateState();
        ProgressManager.Instance.ProgressChanged += UpdateState;
    }

    private void UpdateState()
    {
        button.interactable = ProgressManager.Instance.Progress[levelNumber].Item1;

        if (ProgressManager.Instance.Progress[levelNumber].Item2 > 0)
        {
            starBoxes[0].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 1 ? starSprites[1] : starSprites[0];
            starBoxes[1].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 2 ? starSprites[1] : starSprites[0];
            starBoxes[2].sprite = ProgressManager.Instance.Progress[levelNumber].Item2 >= 3 ? starSprites[1] : starSprites[0];
        }
        else
        {
            starBoxes[0].color = starBoxes[1].color = starBoxes[2].color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
