using UnityEngine;
using UnityEngine.EventSystems;

public enum State
{
    Left,
    Right
}

public class RailroadSwitch : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RailroadSegment segment;

    private const string animator_SwitchToLeft = "SwitchToLeft";
    private const string animator_SwitchToRight = "SwitchToRight";

    private State state;
    private Animator animator;
    private bool isAnimating;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        state = segment.SelectedRailroadSegment == segment.NextSegment1 ? State.Left : State.Right;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.delta.x > 0)
        {
            SwitchToRight();
        }
        else
        {
            SwitchToLeft();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void SwitchToLeft()
    {
        if (state != State.Left && !isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToLeft);
            state = State.Left;
            segment.SwitchToLeft();
            audioSource.Play();
        }
    }

    public void SwitchToRight()
    {
        if (state != State.Right && !isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToRight);
            state = State.Right;
            segment.SwitchToRight();
            audioSource.Play();
        }
    }

    public void Animator_EndAnimation()
    {
        isAnimating = false;
    }
}
