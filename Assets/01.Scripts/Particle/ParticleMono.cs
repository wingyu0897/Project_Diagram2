using UnityEngine;

public class ParticleMono : PoolableMono
{
    private ParticleSystem _particle;

	protected void Awake()
	{
		_particle = GetComponent<ParticleSystem>();
	}

	public override void PoolInitialize()
	{
		_particle.Play();
	}

	private void OnParticleSystemStopped()
	{
		PoolManager.Instance.Push(this);
	}
}
