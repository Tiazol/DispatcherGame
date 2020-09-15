using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text levelNumber;
    [SerializeField] private Text wagonsCount;
    [SerializeField] private Sprite[] sprites;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        var tutorial = FindObjectOfType<SwipeAnimationTutorial>();
        var wagonGenerator = FindObjectOfType<WagonGenerator>();

        if (wagonGenerator != null)
        {
            if (tutorial == null)
            {
                LaunchWagonGenerator(wagonGenerator);
            }
            else
            {
                tutorial.SwipeAnimationCompleted += () => LaunchWagonGenerator(wagonGenerator);
                tutorial.StartWork();
            }
        }

        levelNumber.text = $"{LocalizationManager.Instance.GetLocalizedString("level")} {GameManager.Instance.CurrentLevelNumber}";
        wagonsCount.text = $"0 / {WagonGenerator.Instance.WagonsToLaunch}";
    }

    private void LaunchWagonGenerator(WagonGenerator wagonGenerator)
    {
        wagonGenerator.WagonPrepared += StartBlinking;
        wagonGenerator.WagonLaunched += StopBlinking;
        wagonGenerator.StartWorking();
    }

    private void StartBlinking(WagonType type)
    {
        image.sprite = sprites[(int)type];
        animator.SetBool("IsBlinking", true);
    }

    private void StopBlinking()
    {
        animator.SetBool("IsBlinking", false);
        wagonsCount.text = $"{WagonGenerator.Instance.PassedWagonsCount} / {WagonGenerator.Instance.WagonsToLaunch}";
    }
}
