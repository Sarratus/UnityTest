using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] 
    private DeathTimer deathTimer;
    [SerializeField] 
    private Canvas gameOverCanvas;
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    private FighterController player = null;
    private FighterController opponent = null;

    private FighterController deathTarget;

    private void Start() {        
        deathTimer.enabled = false;
        gameOverCanvas.enabled = false;

        // find and differentiate all fighters
        FighterController[] fighters = GameObject.FindObjectsOfType<FighterController>();
        foreach(var fighter in fighters) {
            if(fighter.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController)) {
                if(!player) player = fighter;
                continue;
            }
            if (fighter.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyAI)) {
                if(!opponent) opponent = fighter;
                continue;
            }
        }

        if(player) player.Death += (float resurrectionChance) => DeathSequence(player, resurrectionChance);
        if(opponent) opponent.Death += (float resurrectionChance) => DeathSequence(opponent, resurrectionChance);

        deathTimer.TimerEnd += ResurrectionTimerEnd;
    }

    private float resurrectionChance = 0f;
    private void DeathSequence(FighterController target, float resurrectionChance) {
        deathTimer.enabled = true;
        deathTimer.Setup(player.fighterStats.resurrectionTimerDuration);
        
        deathTarget = target;
        this.resurrectionChance = resurrectionChance;
    }

    private void ResurrectionTimerEnd() {
        if(Random.value <= resurrectionChance) {
            Debug.Log(deathTarget.name + " Resurrected!");
            deathTarget.Ressurect();

        } else {
            gameOverCanvas.enabled = true;
            gameOverText.text = deathTarget == player ? "Game Over" : "Congratulations";
        }
    }
}
