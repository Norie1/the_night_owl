using UnityEngine;

public class ObjectPickup_S : MonoBehaviour
{
    public static ObjectPickup_S instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("ObjectPickup already initiliazed");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (PlayerHealth_S.instance.playerDeath)
        {
            RestoreObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory_S.instance.AddCoins(1);
            RemoveObject();
        }
    }

    //Disables object collider and graphics
    [HideInInspector]
    public void RemoveObject()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Restores object collider and graphics
    [HideInInspector]
    public void RestoreObject()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}