using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_behaviour enemyParent;

    private void Awake() {
        enemyParent = GetComponentInParent<Enemy_behaviour>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enemyParent.target = other.transform; //Target the player
            enemyParent.isInRange = true; //Player is in range
            enemyParent.hotZone.SetActive(true); //Activate hotZone
            gameObject.SetActive(false); //Deactivate trigger area object
        }
    }
}
