using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

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

        RippleParticle particle = PoolManager.Instance.Pop(_rippleParticle.name) as RippleParticle;
        particle.transform.position = mousePos;
		particle.SetValue(_forceRadius * 2.0f);
        //_particlePlayer.Play(mousePos);
    }
}
