using UnityEngine;

public class MainLightController : MonoBehaviour
{
    [SerializeField] private Light mainLight = null;
    [SerializeField] private LayerMask targetLayerMask;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Switching");
        mainLight.cullingMask = targetLayerMask;
        this.gameObject.SetActive(false);
    }
}
