using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerStatus status;
    private Transform head;
    private Animator anim;
    //private AudioSource src;
    
    public SphereCollider col { get; private set; }
    
    [SerializeField] AudioClip atk;
    
    public bool InAction = false;
    public bool CanAttack = false;
    
    public void Init(Player p)
    {
        status = p.Status;
        col = GetComponentInChildren<SphereCollider>();
        anim = p.Animator;
       // src = p.Audio;
        CanAttack = true;
    }

    private void Update()
    {
        if (!status.IsAlive)
            return;
        
        UpdateState();
        if (InputManager.Instance.AttackTriggered && !InAction)
            anim.Play("attack_" + Random.Range(1, 3));

        if (Input.GetKeyDown(KeyCode.Q) && !InAction)
            anim.Play("attack_" + Random.Range(1, 3));
    }

    private void UpdateState()
    {
        InAction = anim.GetBool("inAction");
    }
    
}
