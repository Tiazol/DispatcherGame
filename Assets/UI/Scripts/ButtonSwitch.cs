using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    private Button button;
    private Text text;
    private const string firstState = "←";
    private const string secondState = "→";

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        var segment = (RailroadSegment)button.onClick.GetPersistentTarget(0);
        segment.SelectedRailroadSegmentChanged += isTrue => text.text = isTrue ? secondState : firstState;
    }
}
