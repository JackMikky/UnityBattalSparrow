using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCon : MonoBehaviour
{
    [SerializeField] float selfDestroyTime = 5f;
    [SerializeField] CanvasGroup CanvasGroup;
    [SerializeField] float duration = 0.1f;
    void Start()
    {
        Destroy(this.gameObject, selfDestroyTime);
    }
    private void Update()
    {
        TextFadeOut();
    }
    void TextFadeOut()
    {
        if (CanvasGroup != null)
            CanvasGroup.alpha -= Time.deltaTime * duration;

        if (CanvasGroup.alpha <= 0)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
