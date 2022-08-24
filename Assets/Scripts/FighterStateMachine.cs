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
        animator.SetBool(CurrentState.ToString(), false);
        Debug.Log(gameObject.name + ": " + CurrentState.ToString() + " ---> " + newState.ToString());

        switch(newState) {        
            case FighterState.leftPunch:
                animator.SetBool("leftPunch", true);
                break;

            case FighterState.rightPunch:
                animator.SetBool("rightPunch", true);
                break;

            case FighterState.takeDmg:
                animator.CrossFade("TakeDmg", 0.05f);
                animator.SetBool("takeDmg", true);
                break;

            default:
                animator.SetBool(newState.ToString(), true);
                break;
        }
        
        CurrentState = newState;
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
    takeDmg
}
