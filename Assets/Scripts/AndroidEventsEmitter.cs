using UnityEngine;
using UnityEngine.Events;

public class AndroidEventsEmitter : MonoBehaviour
{
    [HideInInspector] public UnityEvent androidEscape;

    void Update()
    {
        CheckSystemBack();
    }

    public void InitEvents()
    {
        androidEscape ??= new UnityEvent();
    }
    
    private void CheckSystemBack()
    {
        if (Application.platform != RuntimePlatform.Android) 
            return;

        if (Input.GetKey(KeyCode.Escape))
            androidEscape.Invoke();
    }
}
