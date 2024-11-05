using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Pool<PoolableMono>> pools;

	[SerializeField] private Transform _poolParent;
	[SerializeField] private PoolingListSO[] _poolingLists;

	private void Awake()
	{
		pools = new Dictionary<string, Pool<PoolableMono>>();

		foreach (var list in _poolingLists)
		{
			foreach (PoolingPair pair in list.PoolingList)
			{
				CreatePool(pair.Prefab, pair.Count);
			}
		}
	}

	public void CreatePool(PoolableMono prefab, int count)
	{
		if (pools.ContainsKey(prefab.name))
		{
			Debug.Log($"{prefab.name} pool already exists.");
			return;
		}

		Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _poolParent);
		pools.Add(prefab.name, pool);

		for (int i = 0; i < count; ++i)
		{
			PoolableMono inst = Instantiate(prefab, _poolParent);
			inst.gameObject.name = inst.gameObject.name.Replace("(Clone)", string.Empty);
			inst.gameObject.SetActive(false);
			pools[prefab.name].Push(inst);
		}
	}

	public void Push(PoolableMono mono)
	{
		if (!pools.ContainsKey(mono.name))
		{
			Debug.Log($"{mono.name} pool doesn't exists.");
			return;
		}

		mono.gameObject.SetActive(false);
		pools[mono.name].Push(mono);
	}

	public PoolableMono Pop(string name)
	{
		if (!pools.ContainsKey(name))
		{
			Debug.Log($"{name} pool doesn't exists.");
			return null;
		}

		return pools[name].Pop();
	}
}
