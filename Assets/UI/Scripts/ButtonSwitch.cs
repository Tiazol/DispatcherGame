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

        var segment = (RailroadSegment)button.onClick.GetPersistentTarget(0);
        segment.SelectedRailroadSegmentChanged += isNext1 => text.text = isNext1 ? firstState : secondState;
    }
}
