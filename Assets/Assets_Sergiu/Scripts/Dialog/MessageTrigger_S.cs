using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageTrigger_S : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DisplayMessage());
        }  
    }

    private IEnumerator DisplayMessage()
    {
        text.enabled = true;
        yield return new WaitForSeconds(3f);
        text.enabled = false;
    }
}
