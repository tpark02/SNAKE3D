using UnityEngine;

public class RayCastBox : MonoBehaviour
{
    #region Variables

    // Flag to indicate if the raycast hit a valid target
    public bool isHit = false;

    // Index used to reference the Snake ray hit array
    public int idx = 0;

    #endregion

    #region Unity Lifecycle Methods

    private void FixedUpdate()
    {
        // Get the direction facing downward relative to the object
        Vector3 fwd = transform.TransformDirection(Vector3.down);

        // Perform a raycast and check for collisions with objects tagged as "Stage"
        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, 3f)
            && hit.collider.gameObject.tag == "Stage")
        {
            // Update Snake's ray hit status based on index
            Snake.rayHit[idx] = true;
        }
        else
        {
            Snake.rayHit[idx] = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the editor
        Vector3 fwd = transform.TransformDirection(Vector3.down);
        float maxDistance = 3f;
        RaycastHit hit;

        // Perform the raycast and check if it hits a valid target
        isHit = Physics.Raycast(transform.position, fwd, out hit, maxDistance);

        // Draw the ray with different colors based on the hit status
        if (isHit)
        {
            Gizmos.color = Color.red; // Red if the ray hits an object
            Gizmos.DrawRay(transform.position, fwd * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green; // Green if no object is hit
            Gizmos.DrawRay(transform.position, fwd * maxDistance);
        }
    }

    #endregion
}
