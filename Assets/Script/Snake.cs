using System;
using System.Drawing;
using System.Linq;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class Snake : MonoBehaviour
{
    public static Snake instance = null;
    public static bool[] rayHit = new bool[4];
    public GameObject[] rayCastBox = new GameObject[4];
    public GameObject tailPrefab;

    private Action callbck = null;
    private Vector3 moveDirection; // Initial move direction    
    private float distance;
    private float delay = 0.0f;
    private float stepTime = 0.5f;
    private int rayCastCnt = 4;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        distance = this.transform.localScale.x * 0.8f;
        moveDirection = Vector3.forward;
        
    }
    public void StartGame()
    {
        InvokeRepeating("MoveBySteps", delay, stepTime);
    }

    void MoveBySteps()
    {
        if (rayCastCnt == 1)
        {
            int idx = rayHit.ToList().IndexOf(true);
            RotateTowardsDirection(idx);
        }
        
        // Move snake body
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

        Vector3 newPosition = this.transform.localPosition + this.transform.TransformDirection(moveDirection * distance);
        this.transform.localPosition = newPosition;     
    }

    void Update()
    {
        rayCastCnt = rayHit.Select(x => x).Where(x => x == true).Count();

        if (rayCastCnt >= 4) // NEW: Block input if rotating
            HandleInput();
    }

    void RotateTowardsDirection(int dir)
    {
        Debug.Log("Direction Idx : " + dir);

        float angle = 90f; // 90 degrees in radians
        Vector3 vec = Vector3.one;
        Vector3 cameraVec = Vector3.one;

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
        callbck?.Invoke();
    }

    void HandleInput()
    {
        Vector3 newDirection = moveDirection;
        var cnt = Tail.instance.tailSegment.Count;

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

        moveDirection = newDirection;
    }

    public void SetRotationCallback(Action callback)
    {
        callbck = callback;
    }
    public void Grow()
    {
        for (int i = 0; i < 3; i++)
        {
            var t = Instantiate(tailPrefab);
            t.transform.position = transform.position;
            Tail.instance.tailSegment.Add(t.transform);
        }
        ScoreManager.instance.AddScore(10); // +10 points per food

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Tail") == true)
        {
            // Game Over
            Debug.Log("Game Over");
            CancelInvoke("MoveBySteps");
            GameManager.instance.globalVolume.SetActive(true);
            GameManager.instance.gameOverPanel.SetActive(true);
        }
    }
}

