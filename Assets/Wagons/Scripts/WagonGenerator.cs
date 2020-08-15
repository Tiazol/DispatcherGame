using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public static WagonGenerator Instance { get; private set; }

    public Wagon wagonPrefab;
    [Range(2f, 60f)]
    public float createInterval;
    [Range(2f, 60f)]
    public float launchInterval;
    //public float wagonGeneratorInterval;
    [Range(1f, 10f)]
    public float wagonSpeed;
    [Range(1f, 100f)]
    public int wagonsToLaunch;

    public event System.Action<WagonType> WagonPrepared;
    public event System.Action WagonLaunched;

    private int passedWagonsCount;
    //private float time = 3.0f;
    private WagonType currentType;
    private WagonType prevType;
    private const string wagonSpritesConstant = "Sprites/Wagons";
    private Dictionary<WagonType, List<Sprite>> spriteCollection;

    private void Awake()
    {
        Instance = this;

        spriteCollection = new Dictionary<WagonType, List<Sprite>>();
        foreach (var type in System.Enum.GetValues(typeof(WagonType)))
        {
            spriteCollection[(WagonType)type] = new List<Sprite>();
        }
        LoadSprites();
    }

    private void Start()
    {
        //InvokeRepeating("GenerateWagon", 0f, wagonGeneratorInterval);

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

    private IEnumerator PrepareWagon()
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

        yield return null;
        yield return new WaitForSeconds(launchInterval);
        yield return StartCoroutine(LaunchWagon());
    }

    private IEnumerator LaunchWagon()
    {
        var segment = RailroadManager.Instance.GetFirstRailroadSegment();
        var wagon = Instantiate(wagonPrefab, segment.GetPoint(0), Quaternion.identity, transform);

        var list = spriteCollection[currentType];
        wagon.SetWagonType(currentType, list[Random.Range(0, list.Count)]);
        wagon.Speed = wagonSpeed * Random.Range(0.9f, 1.1f); ;

        passedWagonsCount++;
        WagonLaunched?.Invoke();

        yield return null;
        yield return new WaitForSeconds(createInterval);
        yield return StartCoroutine(PrepareWagon());
    }



    //private void GenerateWagon()
    //{
    //    if (passedWagonsCount == wagonsToLaunch)
    //    {
    //        return;
    //    }

    //    var typesCount = System.Enum.GetNames(typeof(WagonType)).Length;
    //    int typeIndex;
    //    bool ok;
    //    do
    //    {
    //        typeIndex = Random.Range(0, typesCount); // warning! max int is EXCLUSIVE!
    //        ok = CheckpointsManager.Instance.UsedWagonTypes.Contains((WagonType)typeIndex) && ((WagonType)typeIndex != prevType);
    //    } while (!ok);

    //    WagonPrepared?.Invoke((WagonType)typeIndex);
    //    prevType = (WagonType)typeIndex;
    //    StartCoroutine(InstantiateWagon(typeIndex));
    //}

    //private IEnumerator InstantiateWagon(int typeIndex)
    //{
    //    yield return new WaitForSeconds(time);

    //    var segment = RailroadManager.Instance.GetFirstRailroadSegment();
    //    var wagon = Instantiate(wagonPrefab, segment.GetPoint(0), Quaternion.identity, transform);

    //    var list = spriteCollection[(WagonType)typeIndex];
    //    wagon.SetWagonType((WagonType)typeIndex, list[Random.Range(0, list.Count)]);
    //    wagon.Speed = wagonSpeed * Random.Range(0.9f, 1.1f);;

    //    passedWagonsCount++;
    //    WagonLaunched?.Invoke();
    //    yield break;
    //}
}
