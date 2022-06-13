using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;


    void Update()
    {
            //on déplace la caméra centré sur le joueur avec un déplacement dis "Smooth" ( fluide )
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
