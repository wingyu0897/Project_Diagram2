using System;
using UnityEngine;

public class MasterInput : MonoBehaviour
{
	public event Action<Vector2> OnPrimaryTouch;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = GetMouseWorldPosition();
            OnPrimaryTouch?.Invoke(mousePos);
		}
	}

	public static Vector2 GetMouseWorldPosition()
	{
		Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return mouseWorldPos;
	}
}
