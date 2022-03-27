using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    LineRenderer lineRenderer;
    ParticleSystem emitter;

    ParticleSystem.MainModule mod;
    ParticleSystem.TrailModule trail;

    public float length = 3f, trailLength = 3f;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        emitter = GetComponent<ParticleSystem>();

        mod = emitter.main;
        trail = emitter.trails;
        
        DrawLine(transform.position, transform.position + transform.forward * length);
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        emitter.Stop();

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        //mod.startLifetime = Vector3.Distance(start,end)/emitter.main.startSpeed.constant;
        mod.startLifetime = length/emitter.main.startSpeed.constant;
        trail.lifetime = trailLength / length;

        emitter.Play();
    }

    void OnValidate()
    {
        if(emitter != null)
            DrawLine(transform.position, transform.position + transform.forward * length);
    }
}
