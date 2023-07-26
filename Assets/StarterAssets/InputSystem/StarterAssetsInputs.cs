using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.UI;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField]UnityEvent enter_Button;
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
        public void OnE_Action(InputValue value)
        {
            if(GameManager.instance.inMenu)
            {
                return;
            }
            ActionEventManager._instance.E_ButtonAction();
            //ActionEventManager._instance.E_ButtonDown = value.isPressed;
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        //public void JumpInput(bool newJumpState)
        //{
        //	jump = newJumpState;
        //}

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
        public void OnEnter_Action(InputValue value)
        {
            if (GameManager.instance.inMenu || GameManager.instance.isNameInputting && value.isPressed)
            {
                enter_Button.Invoke();
               // enter_Button.onClick.Invoke();
            }
        }
        public void Add_EnterEvent(UnityAction action)
        {
            enter_Button.AddListener(action);
        }
        public void Remove_EnterEvent(UnityAction action)
        {
            enter_Button.RemoveListener(action);
        }
    }

}