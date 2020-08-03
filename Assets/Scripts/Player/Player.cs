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
    
    public PlayerStatus Status    { get => _status;    }
    public PlayerMovement Movement { get => _movement; }
    public PlayerActions Actions   { get => _actions;  }
    public PlayerCollider Collider { get => _col;      }
    public Animator Animator      { get => _anim;      }
    public AnimationEvents Events { get => _events;    }
    public AudioSource Audio      { get => _src;       }

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
