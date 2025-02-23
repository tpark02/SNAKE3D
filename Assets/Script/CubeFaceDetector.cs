using System;
using UnityEngine;

public class CubeFaceDetector : MonoBehaviour
{
    public static CubeFaceDetector instance = null;
    public Transform player; // Assign player in Inspector
    private Vector3[] faceNormals = new Vector3[]
    {
        Vector3.up,     // Top (Y+)
        Vector3.down,   // Bottom (Y-)
        Vector3.left,   // Left (X-)
        Vector3.right,  // Right (X+)
        Vector3.forward, // Front (Z+)
        Vector3.back    // Back (Z-)
    };
    private int prevIdx = 0;
    private Action<int, int> callback = null;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Debug.Log("Player is on: " + prevIdx + " START!");
    }
    void Update()
    {
        if (player != null)
        {
            int detectedFace = DetectPlayerSide();
            if (detectedFace != prevIdx)
            {                
                callback?.Invoke(prevIdx, detectedFace);
                prevIdx = detectedFace;
                Debug.Log("Player is on: " + detectedFace);
            }
        }
    }

    int DetectPlayerSide()
    {
        Vector3 cubeCenter = transform.position;
        Vector3 playerDirection = (player.position - cubeCenter).normalized;

        float maxDot = -Mathf.Infinity;
        int bestMatchIndex = -1;

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

    public void SetCallBack(Action<int, int> c)
    {
        callback = c;
    }
}
