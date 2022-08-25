using UnityEngine;

[RequireComponent(typeof(FighterStateMachine))]
public class FighterController : MonoBehaviour
{
    [SerializeField] 
    public FighterStats fighterStats;
    [SerializeField]
    private FighterController opponent;

    private FighterStateMachine stateMachine;

    // Events //
    public delegate void TakeDmgAction(int damage, bool isLeftPunch);
    public delegate void HealthChangeAction(int health, int maxHealth);
    public delegate void DeathAction(float resurrectionChance);
    
    public event TakeDmgAction GetPunch;
    public event HealthChangeAction HealthChanged;
    public event DeathAction Death;
    ////////////

    private int health;
    private int damage;

    private float resurrectionChance;

    private void Start() {
        stateMachine = GetComponent<FighterStateMachine>();

        health = fighterStats.maxHealth;
        damage = fighterStats.baseDamage;

        resurrectionChance = fighterStats.resurrectionChance;
    }

    public void GetHit(int damage, bool isLeftPunch) {
        if(!CheckHit()) return;

        stateMachine.SwitchState(FighterState.takeDmg);

        if(GetPunch != null)
            if(isLeftPunch)
                GetPunch(damage, isLeftPunch: true);
            else
                GetPunch(damage, isLeftPunch: false);

        SetHealth(health - damage);
    }

    private bool CheckHit() {
        if(stateMachine.CurrentState == FighterState.block) return false;

        if(stateMachine.CurrentState == FighterState.leftPunch || stateMachine.CurrentState == FighterState.rightPunch) {
            
        }

        return true;
    }

    public FighterState GetOpponentState() { return opponent.stateMachine.CurrentState; }
    public FighterState GetCurrentState() { return stateMachine.CurrentState; }

    private void SetHealth(int newHealth) {
        health = Mathf.Max(newHealth, 0);
        
        if(HealthChanged != null)
            HealthChanged(health, fighterStats.maxHealth);

        if (health == 0) {
            stateMachine.SwitchState(FighterState.death);
            opponent.OpponentDeath();

            if(Death != null) Death(resurrectionChance);
        }
    }

    public void OpponentDeath() { stateMachine.SwitchState(FighterState.pause); }
    public void OpponentResurrection() { stateMachine.SwitchState(FighterState.resurrection); }

    float numResurrected = 0f;
    public void Ressurect() {
        opponent.OpponentResurrection();
        stateMachine.SwitchState(FighterState.resurrection);

        numResurrected += 1;

        resurrectionChance /= 1.5f;
        var newHealth =  fighterStats.maxHealth - (int)(fighterStats.maxHealth / 4f * numResurrected);
        SetHealth(newHealth);
    }
    
    // State Control ////////////
    // Block ///////
    public void EnterBlock() { stateMachine.SwitchState(FighterState.block); }
    public void ExitBlock() { stateMachine.SwitchState(FighterState.idle); }
    /////////////////
    // Punches //////
    public void RightPunch() { stateMachine.SwitchState(FighterState.rightPunch); }
    public void LeftPunch() { stateMachine.SwitchState(FighterState.leftPunch); }
    /////////////////////////////
    
    // Animation Events //
    void OnPunchAnimationEnd() {
        bool isLeftPunch = stateMachine.CurrentState == FighterState.leftPunch;
        opponent.GetHit(damage, isLeftPunch);
    }
    //////////////////////
}
