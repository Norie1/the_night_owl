using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Health_J : MonoBehaviour
{

    public int maxHealth = 100 ;
    public int currentHealth ;
   
    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.15f;
    public bool isInvissible = false;

    public SpriteRenderer graphics;
    public HealthBar_J healthBar;
   
   void Update()
   {
    
       if(Input.GetKeyDown(KeyCode.H))
       {
           TakeDamages(20);
       }
   }
   
    public void TakeDamages(int damage)
    {
        if(!isInvissible)
        {
            currentHealth = currentHealth - damage;
            healthBar.SetHealth(currentHealth);
            isInvissible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }
    //"clignottement" du joueur
    public IEnumerator InvicibilityFlash()
    {
        while(isInvissible)
        {
            //float r , g , b , a
            graphics.color = new Color(1f,1f,1f,0f);
            
            // wait n seconds
            yield return new WaitForSeconds(invicibilityFlashDelay);

            //float r , g , b , a
            graphics.color = new Color(1f,1f,1f,1f);

            yield return new WaitForSeconds(invicibilityFlashDelay);


        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvissible = false;
    }

   
} 