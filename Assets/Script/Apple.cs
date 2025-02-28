using UnityEngine;

// This class controls the behavior of the apple, including its positioning and interactions with other objects.
public class Apple : MonoBehaviour
{
    // #region Variables
    #region Variables
    public Transform cubeTransform; // Reference to the cube transform; assign in the Inspector.
    public float appleRadius = 0.5f; // Radius of the apple, used to ensure proper positioning on the cube.

    private Vector3 cubeSize; // Stores the actual world size of the cube.
    #endregion

    // #region Unity Lifecycle Methods
    #region Unity Lifecycle Methods
    // Called when the script is initialized.
    private void Start()
    {
        // Ensure that the cube transform has been assigned.
        if (cubeTransform == null)
        {
            Debug.LogError("Cube Transform is not assigned!");
            return;
        }

        // Get the cube's actual world size, accounting for scaling.
        BoxCollider cubeCollider = cubeTransform.GetComponent<BoxCollider>();
        if (cubeCollider != null)
        {
            cubeSize = Vector3.Scale(cubeCollider.size, cubeTransform.lossyScale); // Correct for scaling.
        }
        else
        {
            Debug.LogError("Cube does not have a BoxCollider!");
            return;
        }

        // Randomize the apple's initial position on the cube.
        RandomizePosition();
    }
    #endregion

    // #region Apple Positioning
    #region Apple Positioning
    // Randomly places the apple on one of the cube's six faces.
    void RandomizePosition()
    {
        int faceIndex = Random.Range(0, 6); // Select a random face (0 to 5).
        Vector3 newPosition = GetRandomPointOnCubeFace(faceIndex); // Get a random point on the selected face.
        transform.position = newPosition; // Update the apple's position.
    }

    // Calculates a random point on the specified face of the cube.
    Vector3 GetRandomPointOnCubeFace(int faceIndex)
    {
        Vector3 cubeCenter = cubeTransform.position; // Get the cube's center position.
        Vector3 halfSize = cubeSize / 2; // Calculate half of the cube's size.

        // Generate random positions within the boundaries of the face, respecting the apple radius.
        float x = Random.Range(-halfSize.x + appleRadius, halfSize.x - appleRadius);
        float y = Random.Range(-halfSize.y + appleRadius, halfSize.y - appleRadius);
        float z = Random.Range(-halfSize.z + appleRadius, halfSize.z - appleRadius);

        // Offset the apple to position it on the surface of the selected face.
        Vector3 offset = Vector3.zero;
        switch (faceIndex)
        {
            case 0: offset = new Vector3(0, 0, appleRadius); return cubeCenter + new Vector3(x, y, halfSize.z) + offset; // Front face.
            case 1: offset = new Vector3(0, 0, -appleRadius); return cubeCenter + new Vector3(x, y, -halfSize.z) + offset; // Back face.
            case 2: offset = new Vector3(0, appleRadius, 0); return cubeCenter + new Vector3(x, halfSize.y, z) + offset; // Top face.
            case 3: offset = new Vector3(0, -appleRadius, 0); return cubeCenter + new Vector3(x, -halfSize.y, z) + offset; // Bottom face.
            case 4: offset = new Vector3(appleRadius, 0, 0); return cubeCenter + new Vector3(halfSize.x, y, z) + offset; // Right face.
            case 5: offset = new Vector3(-appleRadius, 0, 0); return cubeCenter + new Vector3(-halfSize.x, y, z) + offset; // Left face.
            default: return cubeCenter; // Fallback in case of invalid face index.
        }
    }
    #endregion

    // #region Collision Handling
    #region Collision Handling
    // Triggered when another collider interacts with the apple's collider.
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the collision is with the snake.
        if (collision.CompareTag("Snake"))
        {
            // Grow the snake and reposition the apple.
            collision.GetComponent<Snake>().Grow();
            RandomizePosition();
        }
    }
    #endregion
}
