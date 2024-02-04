using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

   [SerializeField] public HealthSystem playerHealth;
   [SerializeField] public float damage;
  
   void Start()
    {

    }

    void Update()
    {

    }

     void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {

            other.gameObject.GetComponent<HealthSystem>().currentHealth -= damage;

        }
    }
}
