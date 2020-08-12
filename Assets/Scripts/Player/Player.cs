using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStatus _status;
    private PlayerMovement _movement;
    private PlayerActions _actions;
    private PlayerCollider _col;
    private Animator _anim;
    private AnimationEvents _events;
    private AudioSource _src;

    public PlayerStatus Status => _status;

    public PlayerMovement Movement => _movement;

    public PlayerActions Actions => _actions;

    public PlayerCollider Collider => _col;
    public Animator Animator => _anim;
    public AnimationEvents Events => _events;
    public AudioSource Audio => _src;

    private void Start()
    {
        _anim = GetComponentsInChildren<Animator>()[1];
        _status = GetComponent<PlayerStatus>();
        _movement = GetComponent<PlayerMovement>();
        _actions = GetComponent<PlayerActions>();
        _col = GetComponentInChildren<PlayerCollider>();
        _src = GetComponentInChildren<AudioSource>();
        _events = GetComponentInChildren<AnimationEvents>();
        
        Status.Init(this);
        Movement.Init(this);
        Actions.Init(this);
        Collider.Init(this);
        Events.Init(this);
    }
}
