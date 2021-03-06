using System.Collections;
using UnityEngine;

public class DisappearingPlatforms : MonoBehaviour
{
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            yield return new WaitForSeconds(0.35f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(3f);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

