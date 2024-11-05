using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	[SerializeField] private ParticleMono _particle;

	public void Play(Vector2 position)
	{
		ParticleMono particle = PoolManager.Instance.Pop(_particle.name) as ParticleMono;
		particle.transform.position = position;
	}
}
