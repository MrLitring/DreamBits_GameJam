using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
            OnTrigger?.Invoke();
        this.gameObject.SetActive(false);
    }


}
