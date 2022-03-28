using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : ISwitchable
{
    public Material material;
    LineRenderer lineRenderer;
    ParticleSystem emitter;

    ParticleSystem.MainModule mod;
    ParticleSystem.TrailModule trail;

    public float length = 3f, trailLength = 3f, maxBeamDistance = 50f;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        emitter = GetComponent<ParticleSystem>();
        SetMaterial();

        mod = emitter.main;
        trail = emitter.trails;
        
        lineRenderer.enabled = false;
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        lineRenderer.enabled = true;
        emitter.Stop();

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        //mod.startLifetime = Vector3.Distance(start,end)/emitter.main.startSpeed.constant;
        mod.startLifetime = length/emitter.main.startSpeed.constant;
        trail.lifetime = trailLength / length;

        emitter.Play();
    }

    void SetMaterial()
    {
        lineRenderer.material = material;
        GetComponent<ParticleSystemRenderer>().trailMaterial = material;
    }

    void CheckBeamDistance()
    {
        if(Physics.Raycast(transform.position + transform.forward, transform.forward, out RaycastHit hit, maxBeamDistance))
        {
            float temp = Vector3.Distance(transform.position, hit.point);
            if(temp != length)
            {
                length = temp;
                DrawLine(transform.position, hit.point);
            }
        }
        else
        {
            length = maxBeamDistance;
            DrawLine(transform.position, transform.position + transform.forward * length);
        }
    }

    void OnValidate()
    {
        if(emitter != null && bActive)
            DrawLine(transform.position, transform.position + transform.forward * length);
    }

    public override void Activate()
    {
        //DrawLine(transform.position, transform.position + transform.forward * length);
        InvokeRepeating("CheckBeamDistance", 0f, 0.05f);
    }

    public override void Deactivate()
    {
        CancelInvoke("CheckBeamDistance");
        lineRenderer.enabled = false;
        emitter.Stop();
    }
}
