using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem usedParticleSystem;

    public void Awake()
    {
        usedParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Update()
    {
        if (usedParticleSystem)
        {
            if (!usedParticleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
