using UnityEngine;

public class PlayerSpawn_S : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        RespawnManager_S.instance.facingRight = true;
    }
}
