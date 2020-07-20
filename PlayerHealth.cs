using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float arthurHealth = 3500f;
    public float currentHealth;
    public Slider slider;

    public bool shieldActivated;

    void Start()
    {
        currentHealth = arthurHealth;
        slider.value = UpdateHealth();
    }
        
    public void TakenDamage(float kumaDamage) // Weak Enemy Attack
    {
        if (shieldActivated) // where you can block the attack
        {
            return;
        }

        if (currentHealth > arthurHealth)
        {
            currentHealth = arthurHealth;
        }
        currentHealth -= kumaDamage;
        slider.value = UpdateHealth();
       
        if (currentHealth <= 0)
        {
            Die();
        } // end of Weak Enemy Attack
        

    }

    public void TakenDamage1(float spiDamage) // Range Enemy attack
    {
        if (shieldActivated) // where you can block the attack
        {
            return;
        }
        if (currentHealth > arthurHealth)
        {
            currentHealth = arthurHealth;
        }
        currentHealth -= spiDamage;
        slider.value = UpdateHealth();
        
        if (currentHealth <= 0)
        {
            Die();
        }// end of Ranage Enemy Attack
       
    }


    public void TakenDamage2(float trollDamage) // Strong Enemy Attack, player can not block its attack
    {
        if (currentHealth > arthurHealth)
        {
            currentHealth = arthurHealth;
        }
        currentHealth -= trollDamage;
        slider.value = UpdateHealth();

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

     
    private void Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        Destroy(gameObject);
    }

    float UpdateHealth()
    {
        return currentHealth / arthurHealth;
    }
}
