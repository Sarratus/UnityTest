using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FighterController))]
public class PlayerController : MonoBehaviour
{
    private FighterController targetFighter;

    private void Start() {
        targetFighter = GetComponent<FighterController>();
    }

    private void Update() {
        if(Keyboard.current.eKey.wasPressedThisFrame) {
            targetFighter.RightPunch();
        }

        if(Keyboard.current.qKey.wasPressedThisFrame) {
            targetFighter.LeftPunch();
        }

        if(Keyboard.current.wKey.isPressed) {
            targetFighter.EnterBlock();
        } else if(Keyboard.current.wKey.wasReleasedThisFrame) {
            targetFighter.ExitBlock();
        }
    }
}
