using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINameController : MonoBehaviour
{
    [SerializeField]Camera _camera;
   // [SerializeField] private float dis;
    [SerializeField] private GameObject text;
    

    void Start()
    {
        if (_camera == null)
        {
            _camera=Camera.main;
        }
        GameManager.instance.DistanceChecker.AddListener(DistanceChange);
    }

    public void DistanceChange()
    {
      var dis=  (transform.position -_camera.transform.position).magnitude;
      if (dis >= 15)
      {
          text.transform.localScale = Vector3.one;
      }
      else
      {
          text.transform.localScale = Mathf.Clamp(dis / 15f,0.05f,0.5f)* Vector3.one;
      }
    }
    private void OnDestroy()
    {
        GameManager.instance.DistanceChecker.RemoveAllListeners();
    }
}
