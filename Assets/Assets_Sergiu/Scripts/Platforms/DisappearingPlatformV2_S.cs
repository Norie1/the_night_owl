using UnityEngine;

public class DisappearingPlatformV2_S : MonoBehaviour
{
    [SerializeField]
    private bool order;
    private bool countdown;
    private bool activePlatform;

    private float time;

    //Platform that appears and disappears every 2 seconds
    private void Update()
    {
        if (time >= 4)
        {
            countdown = true;
        }
        else if (time <= 0)
        {
            countdown = false;
        }

        if (!countdown)
        {
            time += Time.deltaTime;
        }
        else if (countdown)
        {
            time -= Time.deltaTime;
        }        

        if (!order)
        {
            if (Mathf.Round(time) % 4 == 0)
            {
                activePlatform = true;
            }
            else if (Mathf.Round(time) % 4 == 2)
            {
                activePlatform = false;
            }
        }
        else
        {
            if (Mathf.Round(time) % 4 == 0)
            {
                activePlatform = false;
            }
            else if (Mathf.Round(time) % 4 == 2)
            {
                activePlatform = true;
            }
        }

        if (activePlatform)
        {
            ActivatePlatform();
        }
        else
        {
            DisablePlatform();
        }
    }

    private void ActivatePlatform()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DisablePlatform()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
