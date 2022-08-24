using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FighterStateMachine))]
public class FighterController : MonoBehaviour
{
    [SerializeField] 
    private FighterStats fighterStats;
    [SerializeField]
    private FighterController opponent;

    private FighterStateMachine stateMachine;

    public delegate void TakeDmgAction(int damage, bool isLeftPunch);
    public delegate void HealthChangeAction(int health, int maxHealth);
    
    public event TakeDmgAction Punch;
    public event HealthChangeAction HealthChanged;

    private int health;
    private int damage;

    private void Start() {
        stateMachine = GetComponent<FighterStateMachine>();

        health = fighterStats.maxHealth;
        damage = fighterStats.baseDamage;
    }

    private void Update() {
        
    } 

    public void GetHit(int damage, bool isLeftPunch) {
        if(!CheckHit()) return;

        stateMachine.SwitchState(FighterState.takeDmg);
        
        if(isLeftPunch && Punch != null)
            Punch(damage, isLeftPunch: true);
        else
            Punch(damage, isLeftPunch: false);

        SetHealth(health - damage);
    }

    private bool CheckHit() {
        if(stateMachine.CurrentState == FighterState.block) return false;

        if(stateMachine.CurrentState == FighterState.leftPunch || stateMachine.CurrentState == FighterState.rightPunch) {
            
        }

        return true;
    }

    private void SetHealth(int newHealth) {
        health = Mathf.Max(newHealth, 0);
        HealthChanged(health, fighterStats.maxHealth);

        Debug.Log(health);

        if (health == 0) {
            // TODO: DIE
            Debug.Log("Dead");
        }
    }
    
    // State Control ////////////
    // Block ///////
    public void EnterBlock() {
        stateMachine.SwitchState(FighterState.block);
    }
    public void ExitBlock() {
        stateMachine.SwitchState(FighterState.idle);
    }
    /////////////////
    // Punches //////
    public void RightPunch() {
        stateMachine.SwitchState(FighterState.rightPunch);
    }
    public void LeftPunch() {
        stateMachine.SwitchState(FighterState.leftPunch);
    }
    //////////////////////////////
    void OnPunchAnimationEnd() {
        bool isLeftPunch = stateMachine.CurrentState == FighterState.leftPunch;
        opponent.GetHit(damage, isLeftPunch);
    }
}
