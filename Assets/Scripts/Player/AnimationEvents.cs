using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerActions actions;
    private Animator anim;
    
    public void Init(Player p)
    {
        actions = p.Actions;
        anim = p.Animator;
    }

    public void OpenDamageCollider()
    {
        actions.col.enabled = true;
    }

    public void CloseDamageCollider()
    {
        actions.col.enabled = false;
    }
}
