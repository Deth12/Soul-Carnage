using UnityEngine;

public class MainLightController : MonoBehaviour
{
    public Light mainLight;
    public LayerMask TargetLayerMask;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Switching");
        mainLight.cullingMask = TargetLayerMask;
        this.gameObject.SetActive(false);
    }
}
