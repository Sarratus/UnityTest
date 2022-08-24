using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [SerializeField] 
    private FighterStats fighterStats;

    [SerializeField]
    private FighterController opponent;

    private FighterStateMachine stateMachine;

    public int Health { get; private set; }
    public int Damage { get; private set; }

    private void Start() {
        stateMachine = new FighterStateMachine();

        Health = fighterStats.maxHealth;
        Damage = fighterStats.baseDamage;
    }

    private void Update() {
        
    }

    public void GetHit(int damage) {
        if(!CheckHit()) return;

        SetHealth(Health - damage);
    }

    private bool CheckHit() {
        if(stateMachine.CurrentState == FighterState.block) return false;

        if(stateMachine.CurrentState == FighterState.leftPunch || stateMachine.CurrentState == FighterState.rightPunch) {
            
        }

        return true;
    }

    private void SetHealth(int newHealth) {
        Health = Mathf.Max(newHealth, 0);

        if (Health == 0) {
            // TODO: DIE
            Debug.Log("Dead");
        }
    }

    // Animation event
    void OnPunchAnimationEnd() {
        opponent.GetHit(Damage);
    }
}
