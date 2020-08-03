using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UI_Button : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SlideIn(bool left)
    {
        anim.SetTrigger(left ? "SlideInLeft" : "SlideInRight");
    }

    public void SlideOut(bool left)
    {
        anim.SetTrigger(left ? "SlideOutLeft" : "SlideOutRight");        
    }

}
