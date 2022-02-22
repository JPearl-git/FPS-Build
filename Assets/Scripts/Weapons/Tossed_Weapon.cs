using UnityEngine;

public class Tossed_Weapon : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] float waitForPickup, heightOffGround = 3f;

    IWeapon weapon;

    void Start()
    {
        if(pickupPrefab == null || waitForPickup < 0f)
            return;

        weapon = GetComponentInChildren<IWeapon>();
        if(weapon != null)
            Invoke("SpawnPickup", waitForPickup);
    }

    void SpawnPickup()
    {
        Vector3 spawnLocation = weapon.transform.position;
        spawnLocation.y += heightOffGround;

        var pickup = Instantiate(pickupPrefab, spawnLocation, Quaternion.identity);
        if(pickup.TryGetComponent<Weapon_Pickup>(out Weapon_Pickup wp))
        {
            RemoveComponents(weapon.gameObject);
            wp.Initialize(weapon.gameObject);
        }

        Destroy(gameObject);
    }

    void RemoveComponents(GameObject obj)
    {
        DestroyImmediate(obj.GetComponent("Rigidbody"));
        DestroyImmediate(obj.GetComponent("MeshCollider"));
    }
}
