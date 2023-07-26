
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameChecker : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] GameObject nameInputGO;
    [SerializeField] Button registe_Button;
    [SerializeField] Button submit_Button;

    private void Start()
    {
        GameManager.instance.StarterAssetController(true);
        GameManager.instance.AddStarterInputEvent(registe_Button.onClick.Invoke);
    }
    public void ChangerPlayerName()
    {
        if (nameInputField != null && nameInputField.text != null && nameInputField.text.Length != 0)
        {
            if (nameInputField.text.Length >= 25)
            {
                Debug.Log("over index");
                return;
            }
            GameManager.instance.playerName = nameInputField.text.Trim();
            Time.timeScale = 1;
            GameManager.instance.isNameInputting = false;
            Destroy(this);
        }else
        {
            DefaultName();
        }
    }
    private void DefaultName()
    {
            Time.timeScale = 1;
            GameManager.instance.isNameInputting = false;
            Destroy(this);
    }
    private void OnDestroy()
    {
        GameManager.instance.StarterAssetController(false);
        GameManager.instance.RemoveStarterInputEvent(registe_Button.onClick.Invoke);
        GameManager.instance.AddStarterInputEvent(submit_Button.onClick.Invoke);
        Destroy(nameInputGO);
    }
}
