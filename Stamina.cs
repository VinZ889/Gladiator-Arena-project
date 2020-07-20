using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    private float MaxStamina = 1000f;
    private float currentStamina;
    public Slider staminaBar;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    public static Stamina instance;

    void Awake()
    {
        instance = this;
    
    }

    void Start()
    {
        currentStamina = MaxStamina;
        staminaBar.maxValue = MaxStamina;
        staminaBar.value = MaxStamina;
    }

    public void ReduceStamina(float Amount)
    {
        if (currentStamina - Amount >= 0)
        {
            currentStamina -= Amount;
            staminaBar.value = currentStamina;
            if (regen != null)
                StopCoroutine(regen);

                regen = StartCoroutine(RegainStamina());
            
        }
        else
            Debug.Log("Not enough stamina");
    }

    private IEnumerator RegainStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < MaxStamina)
        {
            currentStamina += MaxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
