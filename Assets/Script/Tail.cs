using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public static Tail instance = null;
    public int initialSize = 3;
    public List<Transform> tailSegment = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }    
}
