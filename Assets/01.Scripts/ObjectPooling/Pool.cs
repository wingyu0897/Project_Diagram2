using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
	private T _prefab;
	private Transform _parent;

    private Stack<T> stack;

    public Pool(T prefab, Transform parent)
	{
		_prefab = prefab;
		_parent = parent;

		stack = new Stack<T>();
	}

	public void Push(T mono)
	{
		stack.Push(mono);
	}

	public T Pop()
	{
		T pop;

		if (stack.Count == 0)
		{
			pop = GameObject.Instantiate(_prefab, _parent);
			pop.gameObject.name = pop.gameObject.name.Replace("(Clone)", string.Empty);
		}
		else
		{
			pop = stack.Pop();
		}

		pop.gameObject.SetActive(true);
		pop.PoolInitialize();

		return pop;
	}
}
