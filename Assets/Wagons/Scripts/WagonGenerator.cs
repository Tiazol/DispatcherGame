using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public static WagonGenerator Instance { get; private set; }

    public int PassedWagonsCount { get; private set; }
    public int WagonsToLaunch
    {
        get => wagonsToLaunch;
        set => wagonsToLaunch = value;
    }

    public event System.Action<WagonType> WagonPrepared;
    public event System.Action WagonLaunched;

    [SerializeField] protected Wagon wagonPrefab;
    [Range(0.1f, 10f)]
    [SerializeField] protected float createInterval;
    [Range(0.1f, 10f)]
    [SerializeField] protected float launchInterval;
    [Range(1f, 10f)]
    [SerializeField] protected float wagonSpeed;
    [Range(0f, 100f)]
    [SerializeField] private int wagonsToLaunch;

    protected WagonType currentType;
    private List<WagonType> prevTypes;
    private const int prevTypesLimit = 2;
    private const string spritesPath = "Sprites/Wagons";
    protected Dictionary<WagonType, List<Sprite>> spriteCollection;

    protected virtual void SetRandomIntervals()
    {

    }

    public void StartWorking()
    {
        StartCoroutine(PrepareWagon());
    }

    private void Awake()
    {
        Instance = this;

        spriteCollection = new Dictionary<WagonType, List<Sprite>>();
        foreach (var type in System.Enum.GetValues(typeof(WagonType)))
        {
            spriteCollection[(WagonType)type] = new List<Sprite>();
        }
        LoadSprites();

        SetRandomIntervals();

        prevTypes = new List<WagonType>();
    }

    protected virtual void Start()
    {

    }

    private void LoadSprites()
    {
        var sprites = Resources.LoadAll<Sprite>(spritesPath);
        foreach (var sprite in sprites)
        {
            foreach (var type in spriteCollection.Keys)
            {
                if (sprite.name.Contains(type.ToString()))
                {
                    spriteCollection[type].Add(sprite);
                }
            }
        }
    }

    protected virtual IEnumerator PrepareWagon()
    {
        if (PassedWagonsCount == wagonsToLaunch)
        {
            yield break;
        }

        bool tooManySuchTypes = true;
        do
        {
            currentType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);
            if (!CheckpointsManager.Instance.UsedWagonTypes.Contains(currentType))
            {
                continue;
            }

            if (prevTypes.Count == 0)
            {
                prevTypes.Add(currentType);
                tooManySuchTypes = false;
            }
            else if (prevTypes[0] != currentType)
            {
                prevTypes.Clear();
                prevTypes.Add(currentType);
                tooManySuchTypes = false;
            }
            else if (prevTypes.Count < prevTypesLimit)
            {
                prevTypes.Add(currentType);
                tooManySuchTypes = false;
            }

        } while (tooManySuchTypes);

        WagonPrepared?.Invoke(currentType);

        yield return new WaitForSeconds(launchInterval);
        yield return StartCoroutine(LaunchWagon());
    }

    protected virtual IEnumerator LaunchWagon()
    {
        var segment = RailroadManager.Instance.GetFirstRailroadSegment();
        var wagon = Instantiate(wagonPrefab, segment.FirstPoint, Quaternion.identity, transform);
        var list = spriteCollection[currentType];

        wagon.SetType(currentType, list[Random.Range(0, list.Count)]);
        wagon.SetStartingSegment(segment);
        wagon.Speed = wagonSpeed * Random.Range(0.9f, 1.1f);

        PassedWagonsCount++;
        WagonLaunched?.Invoke();

        yield return new WaitForSeconds(createInterval);
        yield return StartCoroutine(PrepareWagon());
    }
}
