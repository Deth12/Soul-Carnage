using UnityEngine;

public enum FOVMode
{
    fov_default,
    fov_sprint
}

public enum CameraMode
{
    mode_default,
    mode_inShop
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);	
        instance = this;
    }

    private Camera cam;
    private CameraMode cameraMode = CameraMode.mode_default;
    private Transform target;
    
    [SerializeField] float followSpeed = 10f;
    [SerializeField] float fovSpeed = 10f;
    [SerializeField] float targetFOV;

    [Header("FOV Settings")] 
    [SerializeField] float defaultFOV = 90f;
    [SerializeField] float sprintFOV = 100f;
    
    [Header("Camera Tweening")]
    public CameraPositionTemplate defaultCamera;
    public CameraPositionTemplate shopCamera;
    public float changePosTemplateTime;
    
    private bool IsFollowingTarget = false;

    void Start()
    {
        target = GameManager.Instance.Player;
        cam = GetComponentInChildren<Camera>();
        targetFOV = defaultFOV;
        GameManager.Instance.OnCutsceneStart += delegate { IsFollowingTarget = true; };
    }

    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSpeed * Time.deltaTime);
        if(IsFollowingTarget)
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
    }

    public void ChangeFOV(FOVMode f)
    {
        switch (f)
        {
            case FOVMode.fov_default:
                targetFOV = defaultFOV;
                break;
            case FOVMode.fov_sprint:
                targetFOV = sprintFOV;
                break;
        }
    }

    public void ChangeCameraMode(CameraMode m)
    {
        switch (m)
        {
            case CameraMode.mode_default:
                switch (cameraMode)
                {
                    case CameraMode.mode_inShop: 
                        TweenCamera(defaultCamera);
                        break;
                }
                cameraMode = m;
                break;
            case CameraMode.mode_inShop:
                cameraMode = CameraMode.mode_inShop;
                TweenCamera(shopCamera);
                break;
        }
    }
    
    public void TweenCamera(CameraPositionTemplate template)
    {
        LeanTween.moveLocal(gameObject, template.targetPosition, changePosTemplateTime);
        LeanTween.rotateLocal(cam.gameObject, template.targetRotation, changePosTemplateTime);
    }
}

[System.Serializable]
public class CameraPositionTemplate
{
    public Vector3 targetPosition;
    public Vector3 targetRotation;
}
