using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score;
        GameManager.Instance.ScoreChanged += () => scoreText.text = "Score: " + GameManager.Instance.Score;
    }
}
