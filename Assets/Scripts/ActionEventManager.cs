using UnityEngine;
using UnityEngine.Events;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager _instance;
    [SerializeField] public UnityEvent E_ButtonEvent;
    [SerializeField] public UnityEvent E_ButtonEvent_ByFixedTime;


    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else { 
            Destroy(this);
        }
    }
    public void E_ButtonAction()
    {
        E_ButtonEvent.Invoke();
    }
    private void FixedUpdate()
    {
            E_ButtonEvent_ByFixedTime.Invoke();
    }
}
