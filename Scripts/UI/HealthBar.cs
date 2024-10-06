using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Image tempBar;
    public Image hungerBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI tempText;
    public TextMeshProUGUI hungerText;

    public void UpdateBars(float currentHealth, int maxHealth, float currentTemp, int maxTemp, float currentHunger, int maxHunger)
    {
        // Update health bar
        healthBar.fillAmount = currentHealth / (float)maxHealth;
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        // Update temperature bar
        tempBar.fillAmount = currentTemp / (float)maxTemp;
        tempText.text = currentTemp.ToString() + " / " + maxTemp.ToString();

        // Update hunger bar
        hungerBar.fillAmount = currentHunger / (float)maxHunger;
        hungerText.text = currentHunger.ToString() + " / " + maxHunger.ToString();
    }
}
