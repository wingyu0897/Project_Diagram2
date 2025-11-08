using UnityEngine;

[RequireComponent(typeof(MasterInput))]
public class TouchForce : MonoBehaviour
{
	[Range(0.1f, 100f)]
	[SerializeField] private float _forceRadius = 1f;
	[Range(0f, 5f)]
	[SerializeField] private float _forceDuration = 1f;
	[SerializeField] private Ripple _ripple;

	private void Awake()
	{
		GetComponent<MasterInput>().OnPrimaryTouch += HandleTouch;
	}

	private void HandleTouch(Vector2 mousePos)
	{
		Ripple ripple = PoolManager.Instance.Pop(_ripple.name) as Ripple;
		if (ripple == null) return;

		ripple.transform.position = mousePos;
		ripple.Initalize(_forceRadius, _forceDuration);
    }
}
