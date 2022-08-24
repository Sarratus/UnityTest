using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FighterController))]
public class PlayerController : MonoBehaviour
{
    private FighterController fighter;

    private void Start() {
        fighter = GetComponent<FighterController>();
    }

    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame) {
            fighter.RightPunch();
        }

        if(Keyboard.current.qKey.wasPressedThisFrame) {
            fighter.LeftPunch();
        }

        if(Keyboard.current.wKey.isPressed) {
            fighter.EnterBlock();
        } else if(Keyboard.current.wKey.wasReleasedThisFrame) {
            fighter.ExitBlock();
        }
    }
}
