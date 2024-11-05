using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingPair
{
    public PoolableMono Prefab;
    public int Count;
}

[CreateAssetMenu(menuName = "SO/PoolingList", fileName = "PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> PoolingList;
}
