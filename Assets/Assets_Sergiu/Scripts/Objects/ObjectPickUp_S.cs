using UnityEngine;

public class ObjectPickUp_S : MonoBehaviour
{
    public Inventory_S inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //inventory.addCoins(1);
            Destroy(gameObject);
        }
    }
}
