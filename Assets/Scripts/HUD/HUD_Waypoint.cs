using UnityEngine;
using UnityEngine.UI;

public class HUD_Waypoint : MonoBehaviour
{
    [SerializeField] Transform player;

    [Header("Objectives")]
    public GameObject ObjectivePanel;
    public Text ObjectiveText;
    [HideInInspector] public Vector3 target;

    [Header("Waypoint Variables")]
    public Image waypoint;
    public Text meterDistance;
    public Vector3 waypointOffset;
    [HideInInspector] public bool bLocation;
    
    void Update()
    {
        if(target == null || !bLocation)
            return;

        PositionWaypoint();
    }

    // Credit to Omar Balfaqih yt, "Waypoint Marker | Unity"
    void PositionWaypoint()
    {
        float minX = waypoint.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = waypoint.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target + waypointOffset);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        if(Vector3.Dot((target - player.position), player.forward) < 0)
        {
            //Target is behind player
            if(pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }

        waypoint.transform.position = pos;
        meterDistance.text = Vector3.Distance(target, player.position).ToString("0") + "m";
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(player.position, player.position + player.forward);
    }
}
