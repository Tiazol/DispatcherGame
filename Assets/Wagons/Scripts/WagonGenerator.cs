using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public static WagonGenerator Instance { get; private set; }

    public Wagon wagonPrefab;
    [Range(1f, 10f)]
    public float createInterval;
    [Range(1f, 10f)]
    public float launchInterval;
    [Range(1f, 10f)]
    public float wagonSpeed;
    [Range(0f, 100f)] // 0 означает бесконечность
    public int wagonsToLaunch;

    public event System.Action<WagonType> WagonPrepared;
    public event System.Action WagonLaunched;

    private int passedWagonsCount;
    protected WagonType currentType;
    private WagonType prevType;
    private const string wagonSpritesConstant = "Sprites/Wagons";
    protected Dictionary<WagonType, List<Sprite>> spriteCollection;

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
    }

    private void Start()
    {
        StartCoroutine(PrepareWagon());
    }

    private void LoadSprites()
    {
        var sprites = Resources.LoadAll<Sprite>(wagonSpritesConstant);
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

    public virtual void SetRandomIntervals()
    {

    }

    protected virtual IEnumerator PrepareWagon()
    {
        if (passedWagonsCount == wagonsToLaunch)
        {
            yield break;
        }

        bool ok;
        do
        {
            currentType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);
            ok = CheckpointsManager.Instance.UsedWagonTypes.Contains(currentType) && (currentType != prevType);
        } while (!ok);

        WagonPrepared?.Invoke(currentType);
        prevType = currentType;

        //yield return null; // а нужно ли?
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

        passedWagonsCount++;
        WagonLaunched?.Invoke();

        //yield return null; // а нужно ли?
        yield return new WaitForSeconds(createInterval);
        yield return StartCoroutine(PrepareWagon());
    }
}
