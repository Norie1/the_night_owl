using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_behavior enemyParent;

    private void Awake() {
        enemyParent = GetComponentInParent<Enemy_behavior>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false); //Deactivate trigger area object
            enemyParent.target = other.transform;
            enemyParent.isInRange = true;
            enemyParent.hotZone.SetActive(true);

        }
    }
}
