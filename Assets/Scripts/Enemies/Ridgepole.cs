using UnityEngine;

public class Ridgepole : MonoBehaviour
{
    private Vector3 defaultScale;
    private Vector3 centerPos;

    public LayerMask layerMask;
    [SerializeField] private ParticleSystem breakEffect;

    private void OnEnable()
    {
        Positioning();
    }

    private void Positioning()
    {
        RaycastHit l;
        RaycastHit r;
        if  (
            Physics.Raycast(transform.position, Vector3.left, out l, 30f, layerMask) &&
            Physics.Raycast(transform.position, Vector3.right, out r, 30f, layerMask)
        )
        {
            centerPos = new Vector3((l.point.x + r.point.x), transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = centerPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerStatus s = other.GetComponent<PlayerStatus>();
            if (!s.IsInvincible)
                other.GetComponent<PlayerStatus>().ChangeHealth(-100);
            else
                Break();
        }
    }

    private void Break()
    {
        breakEffect.Play();
    }
}
