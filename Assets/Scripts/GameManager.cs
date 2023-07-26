using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string playerName = "Yakisoba";
    [SerializeField] public bool isInScreenGame;
    [SerializeField] public bool isInScreenGameMenu;

    [Header("Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] public List<GameObject> screenGameMenu; //GameObject[]

    public bool isNameInputting= true;
    public bool inMenu;
    private bool escPressed;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    public GameObject player;
    public GameObject rootPlayer;
    public UnityEvent DistanceChecker;
    public void Awake()
    {
        instance = this;
       
    }
    private void Start()
    {
        Time.timeScale = 0f;

    }
    void Update()
    {
        
        if (!isNameInputting && Keyboard.current.escapeKey.wasPressedThisFrame && (menu != null|| screenGameMenu != null))
        {
            if (isInScreenGame)
            {
                Menu_Switch_ScreenGame();
            }
            else
            {
                Menu_Switch();
            }
            
        }
    }


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void CloseScreenGame()
    {
        isInScreenGameMenu = false;
        isInScreenGame = false;
        escPressed=false;
    }
    public void StartScreenGame()
    {
        isInScreenGameMenu = true;
        isInScreenGame = true;
    }
    public void ScreenGameContinue()
    {
        escPressed = true;
        Menu_Switch_ScreenGame();
        
    }
    void Menu_Switch_ScreenGame()
    {
        
        if (isInScreenGameMenu == false && escPressed ==false)
        {
            escPressed = true;
            isInScreenGameMenu = true;
            if (screenGameMenu != null)
            {
                foreach(var i in screenGameMenu)
                {
                    i.SetActive(true);
                }
                
            }
        }
        else if(isInScreenGameMenu == true && escPressed == true)
        {
            escPressed = false;
            isInScreenGameMenu = false;
            if (screenGameMenu != null)
            {
                foreach (var i in screenGameMenu)
                {
                    i.SetActive(false);
                }
            }
        }
    }
    void Menu_Switch()
    {

        if (inMenu == false && escPressed == false)
        {
            escPressed = true;
            inMenu = true;
            if (menu != null)
            {
                menu.SetActive(true);
            }
            StarterAssetController(true);
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            //starterAssetsInputs.cursorInputForLook = false;
        }
        else if (inMenu == true && escPressed == true)
        {
            escPressed = false;
            inMenu = false;
            if (menu != null)
            {
                menu.SetActive(false);
            }
            StarterAssetController(false);
        }
    }
    public void StarterAssetController(bool locked)
    {
        Cursor.visible = locked;
        if (locked) { Cursor.lockState = CursorLockMode.None; }
        else { Cursor.lockState = CursorLockMode.Locked; }
        if (starterAssetsInputs == null)
            return;
        starterAssetsInputs.cursorInputForLook = !locked;
    }
    public void AddStarterInputEvent(UnityAction action)
    {
        starterAssetsInputs.Add_EnterEvent(action);
    }
    public void RemoveStarterInputEvent(UnityAction action)
    {
        starterAssetsInputs.Add_EnterEvent(action);
    }
}
