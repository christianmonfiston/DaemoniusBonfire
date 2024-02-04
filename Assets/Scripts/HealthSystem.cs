using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    // This variable represents the current health amount of the player
    [SerializeField] public float currentHealth;

    // This variable represents the maximum health amount of the player
    [SerializeField] public float maxHealth;

    // This variable represents the maximum health amount of the player
    [SerializeField] public Animator anim;

    //Accessing health bar 
    public Image healthBar;




    void Start()
    {

        // Maximum health amount set to 100
        maxHealth = 100; 

        maxHealth = currentHealth; 
    }


   public void damageTaken(float damage)
    {
        currentHealth = currentHealth - damage; 

        if ( currentHealth <= 0)
        {
            //Player Dies 
            
            print("Elkan is dead");
        }
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }

}
