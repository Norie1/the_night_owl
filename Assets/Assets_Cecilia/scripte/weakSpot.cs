
using UnityEngine;

public class weakSpot : MonoBehaviour
{
    private void OntriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
