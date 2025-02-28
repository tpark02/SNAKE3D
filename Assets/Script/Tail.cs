using System.Collections.Generic;
using UnityEngine;

// The Tail class manages the snake's tail segments, allowing them to grow and reset.
public class Tail : MonoBehaviour
{
    // #region Variables
    #region Variables
    public static Tail instance = null;                 // Singleton instance of the Tail class.
    public int initialSize = 3;                         // Initial size of the snake's tail.
    public List<Transform> tailSegment = new List<Transform>(); // List to store all tail segments.
    #endregion

    // #region Unity Lifecycle Methods
    #region Unity Lifecycle Methods
    // Called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        // Set the singleton instance of this class.
        instance = this;
    }
    #endregion

    // #region Tail Management Methods
    #region Tail Management Methods
    // Resets the tail by destroying all existing segments and clearing the list.
    public void ResetTail()
    {
        // Iterate through each tail segment and destroy its GameObject.
        foreach (Transform segment in tailSegment)
        {
            Destroy(segment.gameObject);
        }

        // Clear the list of tail segments.
        tailSegment.Clear();
    }
    #endregion
}
