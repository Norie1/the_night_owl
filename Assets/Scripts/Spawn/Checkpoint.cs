using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Checkpoint : MonoBehaviour
{
    private Transform respawnPoint;

    [SerializeField]
    private int checkpointID;

    public Text checkpointText;

    private void Awake()
    {
        //Initializing respawnPoint to the position PlayerSpawn (initial player spawn position)
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Moving respawnPoint to the next checkpoint
            respawnPoint.position = transform.position;

            //Update of the respawnPoint attribute from PlayerMovement_S script
            PlayerMovement.instance.respawnPoint = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            //Update of reached checkpoints in RespawnManager_S script (enemy and object respawn related)
            RespawnManager.instance.checkpointReached(checkpointID);

            //Displaying checkpoint indicator
            checkpointText.enabled = true;
            yield return new WaitForSeconds(3f);
            checkpointText.enabled = false;           
        }
    }
}
