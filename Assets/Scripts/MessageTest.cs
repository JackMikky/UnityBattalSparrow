using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTest : MonoBehaviour
{
    [SerializeField] GameObject messageGO;
    [SerializeField] GameObject parentGO;
    public void GenerateText()
    {
      var textGO=  Instantiate(messageGO, parentGO.transform);
    }
}
