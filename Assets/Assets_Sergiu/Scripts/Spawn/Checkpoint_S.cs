using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Checkpoint_S : MonoBehaviour
{
    private Transform respawnPoint;

    [SerializeField]
    private int checkpointID;

    [HideInInspector]
    public bool disableCheckpointMsg;

    public bool facingRight;

    private RespawnManager_S respawnManager;

    public Text checkpointText;

    private void Awake()
    {
        //Initializing respawnPoint to the position PlayerSpawn (initial player spawn position)
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;

        respawnManager = RespawnManager_S.instance;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Moving respawnPoint to the next checkpoint
            respawnPoint.position = transform.position;

            //Update of the respawnPoint attribute from PlayerMovement_S script
            PlayerMovement_S.instance.respawnPoint = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            //Update of reached checkpoints in RespawnManager_S script (enemy and object respawn related)
            respawnManager.checkpointReached(checkpointID);

            //Bool used by PlayerMovement script to establish the direction of the player when respawning
            respawnManager.facingRight = facingRight;

            //Checkpoint message disabled by the Platforms_S script (Triggers)
            if (!disableCheckpointMsg)
            {
                //Displaying checkpoint indicator
                checkpointText.enabled = true;
                yield return new WaitForSeconds(3f);
                checkpointText.enabled = false;
            }            
        }
    }
}
