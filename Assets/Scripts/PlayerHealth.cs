using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100 ;
    public int currentHealth ;
    public HealthBar_J healthBar;
   void Update()
   {
       if(Input.GetKeyDown(KeyCode.H))
       {
           TakeDamages(20);
       }
   }
   
    void TakeDamages(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
    }

   
} 