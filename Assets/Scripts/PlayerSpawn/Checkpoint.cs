using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform respawnPoint;

    private void Awake()
    {
        //Initializing respawnPoint to the position PlayerSpawn (initial player spawn position)
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Moving respawnPoint to the next checkpoint
            respawnPoint.position = transform.position;
            //Update of the respawnPoint attribute from PlayerMovement_S script          
            PlayerMovement.instance.respawnPoint = transform.position;
            Destroy(gameObject);
        }
    }
}
