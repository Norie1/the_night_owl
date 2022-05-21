using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PlayerHealthN : MonoBehaviour
{
    public int maxHealth = 100 ;
    public int currentHealth ;

    public HealthBar healthBar;

    public static PlayerHealthN instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    private void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //Appuyer sur H retire 20HP au joueur
       if(Input.GetKeyDown(KeyCode.H))
       {
           TakeDamages(20);
       }

       //Appuyer sur J ajoute 20HP au joueur
       if(Input.GetKeyDown(KeyCode.J))
       {
           HealPlayer(20);
       }
    }
   

   //Inflige des dégats au joueur
    void TakeDamages(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
    }

    //Restaure la vie du joueur
    void HealPlayer(int amount)
    {
        if(currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        healthBar.SetHealth(currentHealth);
    }

   
} 