using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivator : MonoBehaviour
{
    private Camera cam;
    
    [SerializeField] [Range(-50f, 0f)] private float disableMinTreshold = -7f;
    [SerializeField] [Range(0f, 500f)] private float disableMaxTreshold = 300f;
    
    void Awake()
    {    
        cam = Camera.main;
    }

    void Update()
    {
        float z = (transform.position - cam.transform.position).z;
        if (z < disableMinTreshold || z > disableMaxTreshold)
            gameObject.SetActive(false);
    }
}
