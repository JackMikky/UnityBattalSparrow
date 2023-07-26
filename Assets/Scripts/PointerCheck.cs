using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerCheck : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    enum Types
    {
        NormalUI,
        ScreenGameUI
    }
    [SerializeField] Types type = Types.NormalUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float moveDistance=-0.1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (type)
        {
            case Types.NormalUI:
                rectTransform.localPosition = new Vector3(0, 0, moveDistance);
                break;
            case Types.ScreenGameUI:
                rectTransform.offsetMax = new Vector2(0, 6f);
                rectTransform.offsetMin = new Vector2(0, 2f);
                break;
        }
        
        audioSource.PlayOneShot(audioClip);
    }

   public void OnPointerExit(PointerEventData eventData)
    {
        switch (type)
        {
            case Types.NormalUI:
                rectTransform.localPosition = Vector3.zero;
                break;
            case Types.ScreenGameUI:
                rectTransform.offsetMax = Vector3.zero;
                rectTransform.offsetMin = Vector3.zero;
                break;
        }
        
    }
   
}
