using System;
using System.Linq;
using UnityEngine;

// Main Snake class that handles snake movement, input, and interactions.
public class Snake : MonoBehaviour
{
    // #region Variables
    #region Variables
    public static Snake instance = null;              // Singleton instance of the Snake class.
    public static bool[] rayHit = new bool[4];        // Array to track raycast hits in four directions.
    public GameObject[] rayCastBox = new GameObject[4]; // Array for raycast direction indicator boxes.
    public GameObject tailPrefab;                     // Prefab for snake tail segments.

    private Action callbck = null;                    // Callback for rotation events.
    private Vector3 moveDirection;                    // Direction the snake is moving.
    private float distance;                           // Distance the snake moves each step.
    private float delay = 0.0f;                       // Initial delay before snake starts moving.
    private float stepTime = 0.5f;                    // Time interval between each movement step.
    private int rayCastCnt = 4;                       // Number of active raycasts.
    #endregion

    // #region Unity Lifecycle Methods
    #region Unity Lifecycle Methods
    // Called when the script instance is being loaded.
    void Awake()
    {
        instance = this; // Set the singleton instance.
    }

    // Called just before any Update method is called for the first time.
    void Start()
    {
        // Calculate the movement distance based on the snake's scale.
        distance = this.transform.localScale.x * 0.8f;

        // Set the initial movement direction.
        moveDirection = Vector3.forward;
    }
    #endregion

    // #region Game Management Methods
    #region Game Management Methods
    // Start the snake's movement by invoking the MoveBySteps method repeatedly.
    public void StartGame()
    {
        InvokeRepeating("MoveBySteps", delay, stepTime);
    }

    // Moves the snake in steps and handles tail movement.
    void MoveBySteps()
    {
        // Check if only one raycast hit is detected and adjust direction.
        if (rayCastCnt == 1)
        {
            int idx = rayHit.ToList().IndexOf(true);
            RotateTowardsDirection(idx);
        }

        // Move the snake's tail segments.
        var t = Tail.instance;
        if (t.tailSegment.Count > 0)
        {
            for (int i = t.tailSegment.Count - 1; i > 0; i--)
            {
                t.tailSegment[i].position = t.tailSegment[i - 1].position;
                t.tailSegment[i].tag = "Tail";
            }
            t.tailSegment[0].position = transform.position;
        }

        // Calculate and set the new position for the snake's head.
        Vector3 newPosition = this.transform.localPosition + this.transform.TransformDirection(moveDirection * distance);
        this.transform.localPosition = newPosition;
    }

    // Handles rotation input and movement updates.
    void Update()
    {
        // Count the number of active raycasts.
        rayCastCnt = rayHit.Select(x => x).Where(x => x == true).Count();

        // If all raycasts are active, handle user input for movement.
        if (rayCastCnt >= 4)
            HandleInput();
    }
    #endregion

    // #region Rotation Methods
    #region Rotation Methods
    // Rotates the snake towards the specified direction.
    void RotateTowardsDirection(int dir)
    {
        Debug.Log("Direction Idx : " + dir);

        float angle = 90f; // Default rotation angle.
        Vector3 vec = Vector3.one; // Rotation axis.
        Vector3 cameraVec = Vector3.one; // Camera adjustment vector.

        // Determine the rotation axis and angle based on the direction index.
        if (dir == 0)
        {
            vec = Vector3.right; angle = -90f;
        }
        else if (dir == 1)
        {
            vec = Vector3.forward;
        }
        else if (dir == 2)
        {
            vec = Vector3.forward; angle = -90f;
            cameraVec = Vector3.right;
        }
        else if (dir == 3)
        {
            vec = Vector3.right;
        }
        transform.Rotate(vec, angle, Space.Self);
        callbck?.Invoke(); // Invoke the rotation callback if set.
    }
    #endregion

    // #region Input Handling
    #region Input Handling
    // Handles player input for changing the snake's movement direction.
    void HandleInput()
    {
        Vector3 newDirection = moveDirection;
        var cnt = Tail.instance.tailSegment.Count;

        // Process input only if movement direction is valid and tail length allows it.
        if (Input.GetKeyDown(KeyCode.UpArrow) && (moveDirection != Vector3.back || cnt <= 0))
        {
            newDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (moveDirection != Vector3.forward || cnt <= 0))
        {
            newDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (moveDirection != Vector3.right || cnt <= 0))
        {
            newDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (moveDirection != Vector3.left || cnt <= 0))
        {
            newDirection = Vector3.right;
        }

        moveDirection = newDirection; // Update the movement direction.
    }
    #endregion

    // #region Utility Methods
    #region Utility Methods
    // Sets a callback function to execute during rotation.
    public void SetRotationCallback(Action callback)
    {
        callbck = callback;
    }

    // Grows the snake by adding tail segments and increasing the score.
    public void Grow()
    {
        for (int i = 0; i < 3; i++)
        {
            var t = Instantiate(tailPrefab);
            t.transform.position = transform.position;
            Tail.instance.tailSegment.Add(t.transform);
        }
        ScoreManager.instance.AddScore(10); // Add 10 points to the score for growth.
    }
    #endregion

    // #region Collision Handling
    #region Collision Handling
    // Handles collision events with other objects.
    private void OnTriggerEnter(Collider collision)
    {
        // If the snake collides with its tail, trigger game over.
        if (collision.CompareTag("Tail") == true)
        {
            Debug.Log("Game Over"); // Log the game over event.
            CancelInvoke("MoveBySteps"); // Stop the snake's movement.
            GameManager.instance.globalVolume.SetActive(true); // Activate global volume.
            GameManager.instance.gameOverPanel.SetActive(true); // Show game over panel.
        }
    }
    #endregion
}
