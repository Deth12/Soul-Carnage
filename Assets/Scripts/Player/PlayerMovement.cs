using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerStatus status;
    private Animator anim;
    
    private float currentMoveSpeed = 0f;
    
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float rotateSpeed = 0.5f;
    
    [SerializeField] private float maxX = 6.5f;
    [SerializeField] private float minX = -6.5f;
    [SerializeField] private float jumpSpeed = 100f;
    [SerializeField] private float gravity = 9.81f;

    private Vector3 moveDirection;
    private float vSpeed = 0;

    public LayerMask obstacles;
    public bool CanMove = false;

    public void Init(Player p)
    {
        controller = GetComponent<CharacterController>();
        anim = p.Animator;
        status = p.Status;
        currentMoveSpeed = walkSpeed;
    }

    void Update()
    {
        if(InputManager.Instance.StartTrigger && !CanMove)
        {
            InputManager.Instance.StartTrigger = false;
            CanMove = true;
            anim.SetBool("Move", CanMove);
        }
        if(CanMove)
            HandleControl();
    }

    void HandleControl()
    {
        anim.SetBool("Grounded", controller.isGrounded);
        HandleMovement();        
    }
    
    void HandleMovement()
    {
        moveDirection = Vector3.forward * (status.IsAlive ? currentMoveSpeed : 0);
        vSpeed = (controller.isGrounded ? -1 : vSpeed - gravity * Time.deltaTime);
        if(status.IsAlive)
            HandleInput();
        moveDirection.y = vSpeed;
        controller.Move(moveDirection * Time.deltaTime);
//        Debug.Log("Grounded: " + controller.isGrounded + " vSpeed: " + vSpeed);
    }

    void HandleInput()
    {
        // Mobile
        if(Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.JumpTriggered) 
            Jump();
        if(InputManager.Instance.LeftTapHold)
            Move(Vector3.left);
        if(InputManager.Instance.RightTapHold)
            Move(Vector3.right);

        // Editor
        if (Input.GetKey(KeyCode.D))
            Move(Vector3.right);
        if (Input.GetKey(KeyCode.A))
            Move(Vector3.left);
        
        bool isRunning = InputManager.Instance.RunHold;
        currentMoveSpeed = isRunning ? runSpeed : walkSpeed;
        CameraManager.instance.ChangeFOV(isRunning ? FOVMode.fov_sprint : FOVMode.fov_default);
        anim.SetBool("Run", isRunning);
    }

    void Jump()
    {
        if (controller.isGrounded) 
        {
            anim.SetTrigger("Jump");
            vSpeed = jumpSpeed;
        }
    }
    
    void Move(Vector3 direction)
    {
        if (direction == Vector3.left)
        {
            if (transform.position.x > minX)
            {
                controller.Move((direction) * (rotateSpeed * Time.deltaTime));
            }
        }

        if (direction == Vector3.right)
        {
            if (transform.position.x < maxX)
            {
                controller.Move((direction) * (rotateSpeed * Time.deltaTime));
            }
        }
    }

    /*void HandleTailMovement()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            if (i == 0)
            {
                float dis = Vector3.Distance(head.position, tail[i].position);
                Vector3 newPos = new Vector3(head.position.x, head.position.y - 0.25f, head.position.z - 0.5f);
                float t = Time.deltaTime * dis / tailSegmentsDistance * currentMoveSpeed;
                if (t > 0.5)
                    t = 0.5f;
                tail[i].position = Vector3.Slerp(tail[i].position, newPos, t);
                tail[i].rotation = Quaternion.Slerp(tail[i].rotation, head.rotation, t);
                // tail[i].transform.Translate(Vector3.forward * (currentMoveSpeed * Time.deltaTime));
                // tail[i].transform.LookAt(head);
            }
            else
            {
                float dis = Vector3.Distance(tail[i - 1].position, tail[i].position);
                Vector3 newPos = tail[i - 1].position;
                float t = Time.deltaTime * dis / tailSegmentsDistance * currentMoveSpeed;
                if (t > 0.5)
                    t = 0.5f;
                tail[i].position = Vector3.Slerp(tail[i].position, newPos, t);
                tail[i].rotation = Quaternion.Slerp(tail[i].rotation, tail[i - 1].rotation, t);
            }
        }
    }
    */
    void Spawn()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     var spawnPos = tail.Count == 0
        //         ? new Vector3(head.position.x, head.position.y, head.position.z + 0f)
        //         : new Vector3(tail[tail.Count - 1].position.x, tail[tail.Count - 1].position.x,
        //             tail[tail.Count - 1].position.z + 0f);
        //     var g = Instantiate(tailPrefab, spawnPos, Quaternion.identity, transform);
        //     tail.Add(g.transform);
        // }
    }
}
