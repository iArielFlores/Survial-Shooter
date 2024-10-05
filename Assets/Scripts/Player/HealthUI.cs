using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(playerHealth != null)
        {
            playerHealth.onHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(playerHealth.currentHealth);
        }
        else
        {
            Debug.LogError("PlayerHealth reference is not assigned in HealthUI");
        }
    }

    // Update is called once per frame
    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
