using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Level[] levels;

    private FighterController player;
    private FighterController opponent;
    private OpponentAI opponentAI;

    private int currentLevel = 0;

    [HideInInspector]
    public static LevelController Instance;

    private void Awake() {
        // singletone pattern
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // find and differentiate all fighters
        FighterController[] fighters = GameObject.FindObjectsOfType<FighterController>();
        foreach(var fighter in fighters) {
            if(fighter.gameObject.TryGetComponent<PlayerKeyboardControl>(out PlayerKeyboardControl playerController)) {
                if(!player) player = fighter;
                continue;
            }
            if(fighter.gameObject.TryGetComponent<OpponentAI>(out OpponentAI enemyAI)) {
                if(!opponent) opponent = fighter;
                opponentAI = opponent.GetComponent<OpponentAI>();
                continue;
            }
        }
    }

    private void Start() {
        SetLevel(currentLevel);        
    }

    public void SetLevel(int level) {
        currentLevel = level;
        var levelStats = levels[currentLevel];

        print("Loading " + level.ToString() + " level");

        player.SetStatsAndReset(levelStats.PlayerStats);
        opponent.SetStatsAndReset(levelStats.OpponentStats);
        opponentAI.SetStatsAndReset(levelStats.AIDifficulty);
    }

    public void ReloadCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetLevel(currentLevel);
    }

    public void NextLevel() {
        SetLevel(currentLevel + 1);
    }
}
