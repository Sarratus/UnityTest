using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathController : MonoBehaviour
{
    [Header("Timer properties")]
    [SerializeField] 
    private DeathTimer deathTimer;
    [SerializeField]
    private float timerDuration;

    [Header("UI")]
    [SerializeField] 
    private Canvas gameOverCanvas;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private Button AfterdeathActionButton;

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
        deathTimer.Setup(timerDuration);
        
        deathTarget = target;
        this.resurrectionChance = resurrectionChance;
    }

    private void ResurrectionTimerEnd() {
        if(Random.value <= resurrectionChance) {
            // Resurrection
            deathTarget.Ressurect();

        } else {
            // Permanent Death
            gameOverCanvas.enabled = true;

            if(deathTarget == player) {
                gameOverText.text =  "Game Over";
                AudioManager.instance.Play("Lose");

                // Reload scene on button click (lose)
                AfterdeathActionButton.onClick.RemoveAllListeners();
                AfterdeathActionButton.onClick.AddListener(LevelController.Instance.ReloadCurrentLevel);

                AfterdeathActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Restart";

            } else {
                gameOverText.text = "Congratulations";
                AudioManager.instance.Play("Win");

                AfterdeathActionButton.onClick.RemoveAllListeners();

                AfterdeathActionButton.onClick.AddListener(LevelController.Instance.NextLevel);
                AfterdeathActionButton.onClick.AddListener(() => gameOverCanvas.enabled = false);

                AfterdeathActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next level";
            }
        }
    }
}
