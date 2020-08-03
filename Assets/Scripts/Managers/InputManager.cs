using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
    }

    public bool StartTrigger = false;
    public bool AttackTriggered = false;
    public bool JumpTriggered = false;
    public bool RunHold = false;
    public bool LeftTapHold = false;
    public bool RightTapHold = false;

    private void Update()
    {
        /*if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            int desiredWidth = Screen.width / 2;
            int desiredHeight = Screen.height / 6;
            if (touch.position.x < desiredWidth && touch.position.y > desiredHeight)
            {
                rightTapHold = false;
                leftTapHold = true;
            }
            if (touch.position.x > desiredWidth && touch.position.y > desiredHeight)
            {
                leftTapHold = false;
                rightTapHold = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                leftTapHold = false;
                rightTapHold = false;
            }
        }*/
        // PC control
        if (Input.GetKeyDown(KeyCode.W))
            Run(true);
        if (Input.GetKeyUp(KeyCode.W))
            Run(false);   
    }
    
    private void LateUpdate()
    {
        JumpTriggered = false;
        AttackTriggered = false;
    }

    public void StartGame()
    {
        StartTrigger = true;
    }

    public void Left(bool status)
    {
        LeftTapHold = status;
        RightTapHold = status ? false : RightTapHold;
    }

    public void Right(bool status)
    {
        RightTapHold = status;
        LeftTapHold = status ? false : LeftTapHold;
    }
    
    public void Run(bool status)
    {
        RunHold = status;
    }

    public void Attack()
    {
        AttackTriggered = true;
    }
    
    public void Jump()
    {
        JumpTriggered = true;
    }
}
