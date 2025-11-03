using UnityEngine;

public class ParticleMono : PoolableMono
{
    protected ParticleSystem _particle;

	protected virtual void Awake()
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
