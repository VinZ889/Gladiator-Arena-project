using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private PlayerHealth health;
    void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    
    public void ActivateShield(bool shieldActive)
    {
        health.shieldActivated = shieldActive;
    }
}
