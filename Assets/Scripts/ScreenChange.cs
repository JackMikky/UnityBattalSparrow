using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    [SerializeField] Texture tv_texture;
    [SerializeField] Texture gameScreen_texture;
    [SerializeField]Material tv_material;
    public void ChangeToTextTexture()
    {
        tv_material.SetTexture("_MainTex", tv_texture);
    }
    public void ChangeToGameTexture()
    {
        tv_material.SetTexture("_MainTex", gameScreen_texture);
    }
}
