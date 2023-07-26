using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
   // public bool eButton_pressed { get; private set; }
    //private void Update()
    //{
    //   // E_action();
    //}
    public void OnE_action(InputValue value)
    {
        #region OldAction
        //if (Keyboard.current.eKey.wasPressedThisFrame)
        //{Debug.Log("E Button Pressed");

        // eButton_pressed = true;
        //Debug.Log("E start");
        //}
        //else if (Keyboard.current.eKey.wasReleasedThisFrame)
        //{
        //    //Debug.Log("E_end");
        //   // eButton_pressed = false;
        //}
        #endregion
        ActionEventManager._instance.E_ButtonAction();
    }
}
