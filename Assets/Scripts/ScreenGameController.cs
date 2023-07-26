using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ScreenGameController : MonoBehaviour
{
    [SerializeField] GameObject actionUI;
    [SerializeField] AudioClip pressedClip;
    [SerializeField] UnityEvent buttonAction;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] screenGameUIs;
    [SerializeField] PlayerInput playerInput;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        actionUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.gameObject.GetComponent<CharacterController>())
        {
            player = GameManager.instance.rootPlayer;
            playerInput = other.gameObject.GetComponentInParent<PlayerInput>();
            ShowActionUI();
            ActionEventManager._instance.E_ButtonEvent.AddListener(ScreenGameButtonAction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.gameObject.GetComponent<CharacterController>())
        {
            player = null;
            playerInput = null;
            CloseActionUI();
            ActionEventManager._instance.E_ButtonEvent.RemoveListener(ScreenGameButtonAction);
        }
    }

    void ScreenGameButtonAction()
    {
        buttonAction.Invoke();
    }


    void ShowActionUI()
    {
        actionUI.SetActive(true);
    }
    void CloseActionUI()
    {
        actionUI.SetActive(false);
    }
    public void CloseScreen()
    {
        for (var i=0;i< GameManager.instance.screenGameMenu.Count;i++)// in GameManager.instance.screenGameMenu)
        {
            GameManager.instance.screenGameMenu[i]=null;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
       // player.SetActive(true);
        GameManager.instance.CloseScreenGame();
        playerInput.enabled = true;
        playerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
    }
    public void StartScreen()
    {
        //screenGameUIs.CopyTo(GameManager.instance.screenGameMenu,0);
        // GameManager.instance.screenGameMenu = screenGameUIs;
        for (var i = 0; i < screenGameUIs.Length; i++)// in GameManager.instance.screenGameMenu)
        {
            GameManager.instance.screenGameMenu[i] = screenGameUIs[i];
        }
       // GameManager.instance.screenGameMenu = screenGameUIs;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerInput.enabled = false;
       // player.SetActive(false);
        GameManager.instance.StartScreenGame();
    }
}