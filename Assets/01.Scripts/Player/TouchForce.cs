using UnityEngine;

public class TouchForce : MonoBehaviour
{
	//private ParticlePlayer _particlePlayer;
	
	[SerializeField] private float _forceRadius = 1f;
	[SerializeField] private ParticleMono _rippleParticle;

	private void Awake()
	{
		//_particlePlayer = GetComponent<ParticlePlayer>();

		GetComponent<MasterInput>().OnPrimaryTouch += HandleTouch;
	}

	private void Update()
	{
		// ������ ������ �ӵ��� ���� ���� ��������� �ؾ���
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

		//_particlePlayer.Play(mousePos);
	}
}
