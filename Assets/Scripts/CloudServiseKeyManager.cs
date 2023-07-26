using UnityEngine;

public class CloudServiseKeyManager : MonoBehaviour
{
    public static CloudServiseKeyManager Instance;
    private const string SubscriptionKey = "2d9885fcedb440c2a7a1b4d486475e58";
    private const string translateKey = "a0d9d09e5c744462b7cf2b44f3491724";
    private const string Region = "japanwest";
    public string Get_Key { get {return SubscriptionKey;} }
    public string Get_Key_Translator { get { return translateKey; } }
    public string Get_Region { get { return Region;} }
    private void Awake()
    {
        Instance = this;
    }
}
