using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroy : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var module = particle.main;
        module.loop = false;

        particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isPlaying == false)
            Destroy(gameObject);
    }
}
