
using UnityEngine;

public class weakSpot : MonoBehaviour

{
    public Gameobject objectToDestroy;

    private void OntriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(ObjectToDestroy);
        }
    }
}
