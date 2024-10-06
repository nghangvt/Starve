using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    float currentHealth;

    public GameObject meatItemPrefab;

    //Tạo hiệu ứng khi enemy chết
    //public GameObject enemyHealthEF;

    public Slider enemyHealthSlider;

    void Start()
    {
        currentHealth = MaxHealth;
        enemyHealthSlider.maxValue = MaxHealth;
        enemyHealthSlider.value = MaxHealth;
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        enemyHealthSlider.value = currentHealth;
        if (currentHealth <= 0 )
        {
            makeDead();
        }
    }

    void makeDead()
    {
        Instantiate(meatItemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void Update()
    {
        
    }
}
