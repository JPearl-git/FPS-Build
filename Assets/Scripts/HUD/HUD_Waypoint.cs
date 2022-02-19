using UnityEngine;
using UnityEngine.UI;

public class HUD_Waypoint : MonoBehaviour
{
    [Header("Objectives")]
    public GameObject ObjectivePanel;
    public Text ObjectiveText;
    [HideInInspector] public Vector3 target;

    [Header("Waypoint Variables")]
    public Image waypoint;
    public bool bLocation;
    
    void Update()
    {
        if(target == null || !bLocation)
            return;

        PositionWaypoint();
    }

    void PositionWaypoint()
    {
        float minX = waypoint.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = waypoint.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        waypoint.transform.position = pos;
    }

    public void SetTarget(Vector3 location)
    {
        target = location;
        bLocation = true;
        waypoint.gameObject.SetActive(true);
    }

    public void RemoveTarget()
    {
        bLocation = false;
        waypoint.gameObject.SetActive(false);
    }
}
