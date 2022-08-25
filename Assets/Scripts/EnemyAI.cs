using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FighterController))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIStats stats;
    
    private FighterController targetFighter;

    private float elapsedTime = 0f;
    private float aiUpdatePeriod;
    private float punchChance;
    private float blockChance;

    private void Start() {
        targetFighter = GetComponent<FighterController>();

        aiUpdatePeriod = stats.aiUpdatePeriod;
        punchChance = stats.punchChance;
        blockChance = stats.blockChance;
    }

    bool isPunchChecked = false;
    FighterState lastOpponentState = FighterState.idle;
    private void Update() {
        elapsedTime += Time.deltaTime;

        // block behaviour
        var opponentState = targetFighter.GetOpponentState();
        var isOpponentPunch = opponentState == FighterState.leftPunch || opponentState == FighterState.rightPunch;

        if(isOpponentPunch && !isPunchChecked)
            if(targetFighter.GetCurrentState() != FighterState.block)
                if(Random.value <= blockChance) {
                    Debug.Log("Block");
                    targetFighter.EnterBlock();
                    StartCoroutine(ExitBlockAfterSeconds(0.4f * Random.Range(0.8f, 1.1f)));
                } else {
                    Debug.Log("Don't Blocked");
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

    IEnumerator ExitBlockAfterSeconds(float blockDuration) {
        yield return new WaitForSeconds(blockDuration);
        targetFighter.ExitBlock();
    }
}
