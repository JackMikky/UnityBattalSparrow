using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

/**
 * チャット用コントローラー
 */
public class ChatController : MonoBehaviour
{
    ////const string SendMessageURL = "http://192.168.64.2/vria_api/message.php"; // メッセージ送信用APIのURL
    //const string MessageTextPrefabPath = "Prefabs/MessageT"; // メッセージ用Prefabのパス

    //[SerializeField] string hostName = "192.168.20.107"; // ホスト名
    //[SerializeField] string apiPath = "/Unity_Meeting/message.php"; // メッセージ送信用APIのパス
    //[SerializeField] bool useWeb = false; // Webサーバーの使用有無
    [SerializeField] TMP_InputField inputField; // テキスト入力
   // [SerializeField] GameObject messageRootObj; // メッセージ用ルートオブジェクト

    [Header("SceneMessage")]
    [SerializeField] GameObject scrollview;
    [SerializeField] GameObject messageParent;
    [SerializeField] GameObject messageGO;

   [SerializeField] GameObject messageTextPrefab;

    [HideInInspector] GameObject textcash;
    // メッセージデータ
    //[System.Serializable]
    //public class MessageData
    //{
    //    public string user_name;
    //    public string message;
    //}

    void Start()
    {
        // Prefabロード
        //if (messageTextPrefab != null)
        //    messageTextPrefab = (GameObject)Resources.Load(MessageTextPrefabPath);
        if (messageParent != null)
            messageParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(scrollview.GetComponent<RectTransform>().rect.width - 20, 30);
    }

    /**
     * テキスト入力時処理
     */
    //public void OnChangeInputField(string message)
    //{
    //    // Enterキーが押された場合のみ処理継続
    //    if (message.IndexOf("\n") == -1)
    //    {
    //        return;
    //    }

    //    // メッセージ送信
    //    message = message.TrimEnd();
    //    //Send(message);

    //    // 入力テキスト初期化
    //    inputField.text = "";
    //    inputField.ActivateInputField();
    //}

    /**
     *　送信ボタン押下時処理
     */
    public void OnClickSendButton()
    {
        var textGO = Instantiate(messageGO, messageParent.transform);
        textcash = textGO;
        string message;
        // 入力テキスト取得
        message = inputField.text;


        textGO.GetComponent<TMP_Text>().text = GameManager.instance.playerName + ":" + message + "[" + WorldTimeAPI._instance.worldRealTime + "]";
        // メッセージ送信
       // Send(message);

        // 入力テキスト初期化
        inputField.text = "";
        inputField.ActivateInputField();
    }

    /**
     * サーバーへ書き込んだメッセージを送信
     */
    //void Send(string message)
    //{
    //    //Debug.Log(message); 

    //    //if (useWeb)
    //    //{
    //    //    // Webサーバーを使用する場合は、サーバーからのレスポンスを表示

    //    //    // サーバーにメッセージ送信
    //    //    StartCoroutine(RequestSendMessage(message));
    //    //}
    //    //else
    //    //{
    //        // Webサーバーを使用しない場合は、入力したテキストをそのまま表示
    //        ShowMessage(message);
    //    //}
    //}

    /**
     * メッセージ表示
     */
    //void ShowMessage(string message)
    //{
    //    if (messageTextPrefab == null)
    //        return;
    //    // メッセージ用オブジェクト作成
    //    GameObject textObj = (GameObject)Instantiate(messageTextPrefab);
    //    textObj.GetComponent<TMP_Text>().text = message;

    //    // メッセージ用オブジェクト設定
    //    Vector3 pos = new Vector3(800.0f, Random.Range(-200.0f, 200.0f), 0.0f);
    //   // textObj.transform.SetParent(messageRootObj.transform);
    //    textObj.GetComponent<RectTransform>().localPosition = pos;
    //    textObj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f); ;
    //}

    /**
     * メッセージ送信
     */
    //IEnumerator RequestSendMessage(string message)
    //{
    //    // リクエスト送信
    //    string url_send = "http://" + hostName + apiPath + "?message=" + message + "&user_name=" + GameManager.instance.playerName;
    //    UnityWebRequest request = new UnityWebRequest(url_send, UnityWebRequest.kHttpVerbGET);
    //    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

    //    yield return request.SendWebRequest();

    //    // レスポンス処理
    //    if (request.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.LogError(request.error);
    //    }
    //    else
    //    {
    //        // Debug.Log(request.result);
    //        // Debug.Log(request.downloadHandler.text);

    //        try
    //        {// データ受信
    //            MessageData data = JsonUtility.FromJson<MessageData>(request.downloadHandler.text);

    //            // メッセージ表示
    //            ShowMessage(data.user_name + data.message + "[" + WorldTimeAPI._instance.worldRealTime + "]");//
    //        }
    //        catch
    //        {
    //            ShowMessage(textcash.GetComponent<TMP_Text>().text);
    //        }

    //    }
    //}
}
