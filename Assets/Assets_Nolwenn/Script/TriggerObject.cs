using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{   
    public UnityEvent myEvents;
    bool activated;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !activated)
        {
            activated = true;
            myEvents.Invoke();
        }
    }
}
