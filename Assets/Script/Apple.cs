using UnityEngine;

public class Apple : MonoBehaviour
{
    public Transform cubeTransform; // Assign your cube in the Inspector
    public float appleRadius = 0.5f; // Adjust this to match your apple's radius

    private Vector3 cubeSize;

    private void Start()
    {
        if (cubeTransform == null)
        {
            Debug.LogError("Cube Transform is not assigned!");
            return;
        }

        // Get the cube's actual world size
        BoxCollider cubeCollider = cubeTransform.GetComponent<BoxCollider>();
        if (cubeCollider != null)
        {
            cubeSize = Vector3.Scale(cubeCollider.size, cubeTransform.lossyScale); // Correct scaling
        }
        else
        {
            Debug.LogError("Cube does not have a BoxCollider!");
            return;
        }

        RandomizePosition();
    }

    void RandomizePosition()
    {
        int faceIndex = Random.Range(0, 6);
        Vector3 newPosition = GetRandomPointOnCubeFace(faceIndex);
        transform.position = newPosition;
    }

    Vector3 GetRandomPointOnCubeFace(int faceIndex)
    {
        Vector3 cubeCenter = cubeTransform.position;
        Vector3 halfSize = cubeSize / 2;

        float x = Random.Range(-halfSize.x + appleRadius, halfSize.x - appleRadius);
        float y = Random.Range(-halfSize.y + appleRadius, halfSize.y - appleRadius);
        float z = Random.Range(-halfSize.z + appleRadius, halfSize.z - appleRadius);

        // Offset the apple so it's fully outside the cube
        Vector3 offset = Vector3.zero;
        switch (faceIndex)
        {
            case 0: offset = new Vector3(0, 0, appleRadius); return cubeCenter + new Vector3(x, y, halfSize.z) + offset; // Front face
            case 1: offset = new Vector3(0, 0, -appleRadius); return cubeCenter + new Vector3(x, y, -halfSize.z) + offset; // Back face
            case 2: offset = new Vector3(0, appleRadius, 0); return cubeCenter + new Vector3(x, halfSize.y, z) + offset; // Top face
            case 3: offset = new Vector3(0, -appleRadius, 0); return cubeCenter + new Vector3(x, -halfSize.y, z) + offset; // Bottom face
            case 4: offset = new Vector3(appleRadius, 0, 0); return cubeCenter + new Vector3(halfSize.x, y, z) + offset; // Right face
            case 5: offset = new Vector3(-appleRadius, 0, 0); return cubeCenter + new Vector3(-halfSize.x, y, z) + offset; // Left face
            default: return cubeCenter; // Fallback (should not happen)
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Snake"))
        {
            collision.GetComponent<Snake>().Grow();
            RandomizePosition();
        }
    }
}
