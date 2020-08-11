using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth;
    [SerializeField] float gameHealth;
    //[SerializeField] float spiHealth = 1000f;

    //public float IntialHealth;
    //public float IntialHealth1;
    public Slider slider;

    void Start()
    {
        enemyHealth = gameHealth;
        slider.value = UpdateHealth();

    }

    public void TakenDamage(float aurtherDamage)
    {
        enemyHealth -= aurtherDamage;
        slider.value = UpdateHealth();
        if (enemyHealth <= 0)
        {
            Die();
        }
        
    }


    private void Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        Destroy(gameObject, 6f);
    }

    float UpdateHealth()
    {
        return enemyHealth / gameHealth;
    }
}