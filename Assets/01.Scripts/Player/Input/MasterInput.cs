using System;
using UnityEngine;

public class MasterInput : MonoBehaviour
{
	public event Action OnPrimaryTouch;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnPrimaryTouch?.Invoke();
		}
	}

	public static Vector2 GetMouseWorldPosition()
	{
		Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return mouseWorldPos;
	}
}
