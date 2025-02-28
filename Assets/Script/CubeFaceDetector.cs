using System;
using UnityEngine;

public class CubeFaceDetector : MonoBehaviour
{
    // Singleton instance for global access
    public static CubeFaceDetector instance = null;

    // Inspector-assigned reference to the player object
    public Transform player;

    // Normal vectors representing the six faces of the cube
    private Vector3[] faceNormals = new Vector3[]
    {
        Vector3.up,     // Top (Y+)
        Vector3.down,   // Bottom (Y-)
        Vector3.left,   // Left (X-)
        Vector3.right,  // Right (X+)
        Vector3.forward, // Front (Z+)
        Vector3.back    // Back (Z-)
    };

    // Previous detected face index
    private int prevIdx = 0;

    // Callback action to handle face changes
    private Action<int, int> callback = null;

    #region Unity Lifecycle

    private void Awake()
    {
        // Set the singleton instance
        instance = this;
    }

    private void Start()
    {
        // Log initial face information
        Debug.Log("Player is on: " + prevIdx + " START!");
    }

    private void Update()
    {
        // Update the detected face if the player reference is set
        if (player != null)
        {
            int detectedFace = DetectPlayerSide();

            // Invoke callback if the face changes
            if (detectedFace != prevIdx)
            {
                callback?.Invoke(prevIdx, detectedFace);
                prevIdx = detectedFace;

                // Log the updated face information
                Debug.Log("Player is on: " + detectedFace);
            }
        }
    }

    #endregion

    #region Core Logic

    private int DetectPlayerSide()
    {
        // Calculate the direction from the cube's center to the player
        Vector3 cubeCenter = transform.position;
        Vector3 playerDirection = (player.position - cubeCenter).normalized;

        float maxDot = -Mathf.Infinity;
        int bestMatchIndex = -1;

        // Find the face normal that best matches the player's direction
        for (int i = 0; i < faceNormals.Length; i++)
        {
            float dot = Vector3.Dot(playerDirection, faceNormals[i]);

            if (dot > maxDot)
            {
                maxDot = dot;
                bestMatchIndex = i;
            }
        }

        return bestMatchIndex;
    }

    #endregion

    #region Public Methods

    public void SetCallBack(Action<int, int> c)
    {
        // Set the callback to handle face changes
        callback = c;
    }

    #endregion
}
