using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemypatrol : MonoBehaviour
{
    public float rapide;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    private Transform target;
    private int desPoint; 


    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translater(dir.normalizedn * rapide * Time.deltaTime, Space.word);
        // si l'ennemi arrive a sa destination
        if(vector3.Distance(transform.position) < 0.3f)
        {
            desPoint = (desPoint + 1) % waypoints.Length;
            target = waypoints[desPoint];
            graphics.flipX = !graphics.flipX
        }
    }
}
