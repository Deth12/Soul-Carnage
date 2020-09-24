using System.Collections;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerStatus status;
    
    [SerializeField] private BoxCollider rageCollider;

    public void Init(Player p)
    {
        status = p.Status;
        rageCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IHittable ih = (IHittable) other.GetComponent( typeof(IHittable) );
        if(ih != null)
            ih.TakeHit(status);
    }

    public void EnableRageCollider()
    {
        rageCollider.enabled = true;
    }

    public void DisableRageCollider()
    {
        rageCollider.enabled = false;
    }
}
