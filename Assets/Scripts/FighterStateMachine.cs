using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FighterStateMachine : MonoBehaviour {
    public FighterState CurrentState { get; private set; }
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        CurrentState = FighterState.idle;
    }

    public void SwitchState(FighterState newState) {
        if(CurrentState == FighterState.death || CurrentState == FighterState.pause)
            if(newState != FighterState.resurrection) return; 
        
        animator.SetBool(CurrentState.ToString(), false);
        CurrentState = newState;
        //Debug.Log(gameObject.name + ": " + CurrentState.ToString() + " ---> " + newState.ToString());

        switch(newState) {
            case FighterState.takeDmg:
                animator.CrossFade("TakeDmg", 0.05f);
                animator.SetBool("takeDmg", true);
                break;

            case FighterState.resurrection:
                CurrentState = FighterState.idle;
                break;

            default:
                animator.SetBool(newState.ToString(), true);
                break;
        }
        
    }

    // Animation events //////
    void OnPunchAnimationEnd() {
        SwitchState(FighterState.idle);
    }

    void OnTakeDmgAnimationEnd() {
        SwitchState(FighterState.idle);
    }
    //////////////////////////
}

public enum FighterState {
    idle,
    rightPunch,
    leftPunch,
    block,
    takeDmg,
    death,
    resurrection,
    pause
}
