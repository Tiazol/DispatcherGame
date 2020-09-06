using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class WagonScoreSpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite[] defaultCarSprites;

    private Image image;
    private int index;
    private const float switchTime = 2f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(SwitchSprites());
    }

    private IEnumerator SwitchSprites()
    {
        image.sprite = defaultCarSprites[(int)CheckpointsManager.Instance.UsedWagonTypes[index]];
        index++;
        if (index >= CheckpointsManager.Instance.UsedWagonTypes.Count)
        {
            index = 0;
        }
        yield return new WaitForSeconds(switchTime);
        yield return StartCoroutine(SwitchSprites());
    }
}
