using UnityEngine;

public class ObjectPickup_S : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory_S.instance.addCoins(1);
            //gameObject = object that contains this script
            Destroy(gameObject);
        }
    }
}