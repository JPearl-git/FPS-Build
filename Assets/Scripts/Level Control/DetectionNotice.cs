using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectionNotice : MonoBehaviour
{
    List<Detection> detectors = new List<Detection>();

    public void AddDetector(Detection detector)
    {
        detectors.Add(detector);
    }

    public void RemoveDetector(Detection detector)
    {
        detectors.Remove(detector);
    }

    public void CallDetectors(Vector3 position)
    {
        detectors = detectors.Where(x => x != null).ToList();

        foreach(var detector in detectors)
        {
            float distance = (position - detector.transform.position).magnitude;
            if(distance <= detector.detectionRange)
                detector.SetDetectTarget(position);
        }
    }
}
