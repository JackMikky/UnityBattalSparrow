using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropTextChange : MonoBehaviour
{
    [SerializeField] TMP_Text labelText;
    [SerializeField] TMP_Text toptext;
    // Start is called before the first frame update
 public void TextChanger()
    {
        toptext.text= labelText.text;
    }
}
