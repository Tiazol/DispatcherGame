using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGeneratorPositioned : WagonGenerator
{
    public override void SetRandomIntervals()
    {
        createInterval = Random.Range(2f, 4f);
        launchInterval = Random.Range(2f, 4f);
    }

    protected override IEnumerator PrepareWagon()
    {
        currentType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);

        yield return new WaitForSeconds(launchInterval);
        yield return StartCoroutine(LaunchWagon());
    }

    protected override IEnumerator LaunchWagon()
    {
        var segment = RailroadManager.Instance.GetRailroadSegmentForPosition(transform.position);
        var wagon = Instantiate(wagonPrefab, segment.FirstPoint, Quaternion.identity, transform);
        var list = spriteCollection[currentType];

        wagon.SetType(currentType, list[Random.Range(0, list.Count)]);
        wagon.SetStartingSegment(segment);
        wagon.Speed = wagonSpeed * Random.Range(0.9f, 1.1f);

        yield return new WaitForSeconds(createInterval);
        yield return StartCoroutine(PrepareWagon());
    }
}
