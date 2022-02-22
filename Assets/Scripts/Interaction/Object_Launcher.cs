using UnityEngine;

public class Object_Launcher : MonoBehaviour
{
    [SerializeField] GameObject launchPrefab;
    public float forceMultiplier = 3f;
    
    ///<summary>Launch an object</summary>
    ///<param name="scale">Scale of the projectile object</param>
    ///<param name="obj">Optional child object</param>
    ///<param name="needMeshCollider">Optional add meshCollider to object</param>
    public void LaunchObject(Vector3 scale, GameObject obj = null, bool needMeshCollider = false)
    {
        var newObj = Instantiate(launchPrefab, transform.position, Quaternion.identity);
        newObj.transform.localScale = scale;

        if(obj != null)
        {
            var child = Instantiate(obj, newObj.transform);
            child.SetActive(true);
            child.transform.localPosition = Vector3.zero;

            AttachRigidBody(child, needMeshCollider);
        }
        else
            AttachRigidBody(newObj, needMeshCollider);
    }

    void AttachRigidBody(GameObject obj, bool needMeshCollider)
    {
        var rb = obj.AddComponent<Rigidbody>();

        if(needMeshCollider)
        {
            var skin = obj.GetComponentInChildren<SkinnedMeshRenderer>();
            if(skin != null)
            {
                obj = skin.gameObject;

                var mc = AddMeshCollider(obj);
                mc.sharedMesh = skin.sharedMesh;
            }
            else
                AddMeshCollider(obj);
        }

        rb.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
    }

    MeshCollider AddMeshCollider(GameObject obj)
    {
        var mc = obj.AddComponent<MeshCollider>();
        mc.convex = true;

        return mc;
    }
}
