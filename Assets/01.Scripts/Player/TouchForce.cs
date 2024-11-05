using UnityEngine;

public class TouchForce : MonoBehaviour
{
	private ParticlePlayer _particlePlayer;
	
	[SerializeField] private float _forceRadius = 1f;

	private void Awake()
	{
		_particlePlayer = GetComponent<ParticlePlayer>();

		GetComponent<MasterInput>().OnPrimaryTouch += HandleTouch;
	}

	private void Update()
	{
		// 포스가 퍼지는 속도에 따라 셀이 사라지도록 해야함
	}

	private void HandleTouch()
	{
		Vector2 mousePos = MasterInput.GetMouseWorldPosition();

		Collider2D[] cols = Physics2D.OverlapCircleAll(mousePos, _forceRadius, 1 << LayerMask.NameToLayer("Cell"));
		foreach (Collider2D col in cols)
		{
			Destroy(col.gameObject);
			CellSpawner.Instance.ModifyCellCount(-1);
		}

		_particlePlayer.Play(mousePos);
	}
}
