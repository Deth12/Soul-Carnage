using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerStatus status;

    public void Init(Player p)
    {
        status = p.Status;
    }

    private void OnTriggerEnter(Collider other)
    {
        IHittable ih = (IHittable) other.GetComponent( typeof(IHittable) );
        if(ih != null)
            ih.TakeHit(status);
    }
}
