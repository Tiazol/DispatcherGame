using System;
using System.Collections;

using UnityEngine;

public class SwipeAnimationTutorial : MonoBehaviour
{
    public event Action SwipeAnimationCompleted;

    [SerializeField] private AnimationCurve xCoordinate;
    [SerializeField] private AnimationCurve alphaChannel;

    [SerializeField] private RailroadSwitch rSwitch;
    [SerializeField] private float animationSpeed;

    private SpriteRenderer sr;
    private float currentTime;
    private bool switched;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rSwitch.StateChanged += state => switched = true;
    }

    public void StartWork()
    {
        if (ProgressManager.Instance.SavedStarsCount > 0)
        {
            SwipeAnimationCompleted?.Invoke();
            Destroy(gameObject);
        }

        StartCoroutine(ShowFirstTutorial());
    }

    private IEnumerator ShowFirstTutorial()
    {
        transform.localPosition = new Vector3(xCoordinate.Evaluate(currentTime), transform.localPosition.y, transform.localPosition.z);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alphaChannel.Evaluate(currentTime));

        currentTime += Time.deltaTime * animationSpeed;

        if (switched)
        {
            SwipeAnimationCompleted?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(ShowFirstTutorial());
        }
    }
}
