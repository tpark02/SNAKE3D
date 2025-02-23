using UnityEngine;

public class RayCastBox : MonoBehaviour
{
    public bool isHit = false;
    public int idx = 0;
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, 3f)
            && hit.collider.gameObject.tag == "Stage")
        {
            Snake.rayHit[idx] = true;
        }
        else
        {
            Snake.rayHit[idx] = false;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);

        float maxDistance = 3f;
        RaycastHit hit;
        isHit = Physics.Raycast(transform.position, fwd, out hit, maxDistance);


        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, fwd * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, fwd * maxDistance);
        }
    }
}
