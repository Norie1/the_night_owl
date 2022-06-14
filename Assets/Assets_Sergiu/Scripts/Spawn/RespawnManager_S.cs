using UnityEngine;

public class RespawnManager_S : MonoBehaviour
{
    [SerializeField]
    private int nbOfCheckpoints;

    //Array used to determine which checkpoints have been reached (false = not yet reached)
    [HideInInspector]
    public bool[] checkpoints;

    public bool facingRight;

    public static RespawnManager_S instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Respawn manager already intialized.");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        checkpoints = new bool[nbOfCheckpoints];
    }

    public void checkpointReached(int checkpointID)
    {
        checkpoints[checkpointID] = true;
    }
}
