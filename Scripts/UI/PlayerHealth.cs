using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int maxTemp;
    public int maxHunger;
    public float currentHealth;
    public float currentTemp;
    public float currentHunger;

    public HealthBar healthBar;

    public UnityEvent OnDeath;

    public float safeTime = 1f;
    float _safeTimeCooldown;

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }

    public void Start()
    {
        currentHealth = maxHealth;
        currentTemp = maxTemp;
        currentHunger = maxHunger;

        healthBar.UpdateBars(currentHealth, maxHealth, currentTemp, maxTemp, currentHunger, maxHunger);
    }

    public void TakeDamage(int damage)
    {
        if (_safeTimeCooldown <= 0)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                OnDeath.Invoke();
            }

            _safeTimeCooldown = safeTime;
            healthBar.UpdateBars(currentHealth, maxHealth, currentTemp, maxTemp, currentHunger, maxHunger);
        }
    }

    public void Death() 
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        _safeTimeCooldown -= Time.deltaTime;

        if (currentHunger <= 0)
        {
            // Giảm dần health theo thời gian nếu hunger <= 0
            currentHealth -= Time.deltaTime * 1f; // Tốc độ giảm health
            if (currentHealth < 0)
            {
                currentHealth = 0;
                OnDeath.Invoke();
            }
        }
        else
        {
            // Kiểm tra điều kiện để tăng tốc độ giảm hunger
            if (currentHunger > 0.6f * maxHunger && currentHealth < maxHealth)
            {
                // Tăng gấp đôi độ đói khi thanh máu hồi lại
                currentHunger -= Time.deltaTime * 2f; // Giảm tốc độ đói gấp đôi
            }
            else
            {
                // Giảm độ đói bình thường
                currentHunger -= Time.deltaTime * 1.5f; // Tốc độ giảm hunger bình thường
            }

            // Đảm bảo hunger không nhỏ hơn 0
            if (currentHunger < 0)
                currentHunger = 0;

            // Kiểm tra nếu hunger > 60% thì health sẽ tăng dần
            if (currentHunger > 0.6f * maxHunger)
            {
                currentHealth += Time.deltaTime * 2f; // Tốc độ tăng health
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth; // Giới hạn giá trị health
                }
            }
        }

        // Cập nhật lại các thanh trạng thái
        healthBar.UpdateBars(currentHealth, maxHealth, currentTemp, maxTemp, currentHunger, maxHunger);

    }
}
