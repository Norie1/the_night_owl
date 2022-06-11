using UnityEngine;

public class CameraFollow_S : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 positionOffset;

    private Vector3 velocity;

    void Update()
    {
        //Move camera to the player when he is not dancing
        //Keep the camera static when the player is dancing
        if (!Moonwalk_S.instance.isDancing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset, ref velocity, timeOffset);
        }
    }
}
