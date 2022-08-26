using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FighterController))]
public class OpponentAI : MonoBehaviour
{
    private AIStats stats;
    
    private FighterController targetFighter;

    private float elapsedTime = 0f;
    private float aiUpdatePeriod;
    private float punchChance;
    private float blockChance;

    private void Awake() {
        targetFighter = GetComponent<FighterController>();        
    }

    private bool isPunchChecked = false;
    private FighterState lastOpponentState = FighterState.idle;
    private void Update() {
        elapsedTime += Time.deltaTime;

        // block behaviour
        var opponentState = targetFighter.GetOpponentState();
        var isOpponentPunch = opponentState == FighterState.leftPunch || opponentState == FighterState.rightPunch;

        if(isOpponentPunch && !isPunchChecked)
            if(targetFighter.GetCurrentState() != FighterState.block)
                if(Random.value <= blockChance) {
                    targetFighter.EnterBlock();
                    StartCoroutine(ExitBlockAfterSeconds(0.5f * Random.Range(0.8f, 1.1f)));
                    return;

                } else {
                    isPunchChecked = true;
                }

        // punch behaviour
        if(elapsedTime >= aiUpdatePeriod) {
            elapsedTime %= aiUpdatePeriod;
            if(Random.value >= punchChance) return;

            if(Random.value <= 0.5f)
                targetFighter.LeftPunch();
            else
                targetFighter.RightPunch();
        }

        lastOpponentState = opponentState;

        if(targetFighter.GetCurrentState() == FighterState.takeDmg) isPunchChecked = false;
        if(opponentState != lastOpponentState) isPunchChecked = false;
    }

    private IEnumerator ExitBlockAfterSeconds(float blockDuration) {
        yield return new WaitForSeconds(blockDuration);
        targetFighter.ExitBlock();
    }

    public void SetStatsAndReset(AIStats stats) {
        this.stats = stats;
        ResetStats();
    }

    private void ResetStats() {
        aiUpdatePeriod = stats.aiUpdatePeriod;
        punchChance = stats.punchChance;
        blockChance = stats.blockChance;
    }
}
