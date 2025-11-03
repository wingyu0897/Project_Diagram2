using UnityEngine;

public class RippleParticle : ParticleMono
{
    public void SetValue(float size)
    {
        ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = _particle.sizeOverLifetime;
        AnimationCurve curve = sizeOverLifetime.size.curve;
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(size, curve);
    }
}
