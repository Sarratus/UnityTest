using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DeathTimer : MonoBehaviour
{
    private Image circlularIndicator;
    
    private float duration = 5f;
    private float timeLeft;

    public delegate void TimeEndAction();
    public event TimeEndAction TimerEnd;

    private void Awake() {
        circlularIndicator = GetComponent<Image>();
    }

    private void OnEnable() {
        circlularIndicator.enabled = true;
        circlularIndicator.fillAmount = 1f;
        timeLeft = duration;
    }

    private void OnDisable() {
        circlularIndicator.enabled = false;
    }

    private void Update() {               
        timeLeft = Mathf.Max(timeLeft-Time.deltaTime, 0f);

        float fillAmount = timeLeft / duration;
        circlularIndicator.fillAmount = fillAmount;

        if(timeLeft == 0f) {
            if(TimerEnd != null) TimerEnd();
            enabled = false;
        }
    }

    public void Setup(float duration) {
        this.duration = duration;
        AudioManager.instance.Play("NearDeath");
        OnEnable();
    }
}
