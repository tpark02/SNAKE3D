using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public float raycastDistance = 1f;
    private float distance = 1f;
    private Vector3 moveDirection = new Vector3(1, 0, 0); // Initial move direction    

    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void MoveBySteps()
    {
        Vector3 newPosition = this.transform.position + moveDirection * distance;
        this.transform.position = newPosition;
    }

    void Update()
    {
        HandleInput();
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //moveDirection = new Vector3(0, 0, -1);
            moveDirection = Vector3.forward;
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //moveDirection = new Vector3(0, 0, 1);
            moveDirection = Vector3.back;
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //moveDirection = new Vector3(-1, 0, 0);
            moveDirection = Vector3.left;
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //moveDirection = new Vector3(1, 0, 0);
            moveDirection = Vector3.right;
            MoveBySteps();
        }

    }
}
