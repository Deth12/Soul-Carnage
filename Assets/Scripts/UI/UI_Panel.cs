using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel : MonoBehaviour
{
    private Animator anim;

    [SerializeField] bool isHidden;
    public bool IsHidden
    {
        get
        {
            return isHidden;
        }
        set
        {
            isHidden = value;
            anim.SetBool("isHidden", IsHidden);
        }
    }
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isHidden", IsHidden);
    }

    public void Hide()
    {
        IsHidden = true;
        anim.SetBool("isHidden", IsHidden);
    }

    public void Show()
    {
        IsHidden = false;
        anim.SetBool("isHidden", IsHidden);
    }
}
