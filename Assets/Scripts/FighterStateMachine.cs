using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class FighterStateMachine : MonoBehaviour {

    public FighterState CurrentState { get; private set; }
    private Animator animator;

    public FighterStateMachine() {

    }

    void Start() {
        animator = GetComponent<Animator>();        
    }

    public void SwitchState(FighterState newState) {
        CurrentState = newState;

        switch(CurrentState) {
            default:
            case FighterState.idle:
                break;

            case FighterState.leftPunch:
                break;

            case FighterState.rightPunch:
                break;

            case FighterState.block:
                break;

            case FighterState.takeDmg:
                break;
        }
    }

    void Update() {
        
        //if(Keyboard.current.eKey.isPressed) {
        //    rightPunch.Enter();
        //} 
        //else {
        //    animator.SetBool(rightPunch.animBoolString, false);
        //}

        //if(Keyboard.current.qKey.isPressed) {
        //    animator.SetBool("leftPunch", true);
        //} 
        //else {
        //    animator.SetBool("leftPunch", false);
        //}

        //if(Keyboard.current.wKey.isPressed) {
        //    animator.SetBool("block", true);
        //} 
        //else {
        //    animator.SetBool("block", false);
        //}
    }
}

public enum FighterState {
    idle,
    rightPunch,
    leftPunch,
    block,
    takeDmg
}
