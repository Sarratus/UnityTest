using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private FighterController target;
    [SerializeField]
    private RectTransform healthbar;

    private Vector2 initialSize;

    private void Start() {
        initialSize = healthbar.sizeDelta;
        target.HealthChanged += UpdateHealthbar;
    }

    private void OnEnable() {
        Start();
    }

    private void OnDisable() {
        target.HealthChanged -= UpdateHealthbar;
    }

    private void UpdateHealthbar(int currentHealth, int maxHealth) {
        var newWidth = (float) currentHealth / maxHealth * initialSize.x;
        healthbar.sizeDelta = new Vector2(newWidth, initialSize.y);
    }
}
