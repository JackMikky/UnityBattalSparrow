using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * プレイヤーコントロール用クラス
 */
public class PlayerController : MonoBehaviour
{
    const float MoveSpeed = 3.0f; // 移動速度
    const float RotateSpeed = 200.0f; // 回転角度
    const float AngleMax = -45.0f; // 視点角度上限
    const float AngleMix = 10.0f; // 視点角度下限

    [SerializeField] GameObject headObj; // メインカメラ用オブジェクト

    Rigidbody rb; // プレイヤー移動用

    float v = 0.0f; // 移動速度
    float r = 0.0f; // 回転角度
    float angle = 0.0f; // 視点角度

    void Awake()
    {
        rb = gameObject.transform.GetComponent<Rigidbody>();
    }

    void Start()
    {
        // プレイヤー初期設定
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        angle = headObj.transform.localEulerAngles.x;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        // テキスト入力中はプレイヤー移動を許可しない
        if (EventSystem.current?.currentSelectedGameObject?.GetComponent<InputField>() == null)
        {
            Move();
            Rotate();
            RotatePerspective();
            rb.velocity = Vector3.zero;
        }
    }

    /**
     * プレイヤー移動
     */
    void Move()
    {
        // 前後移動
        if (Input.GetKey(KeyCode.W))
        {
            v = Time.deltaTime * MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            v = -Time.deltaTime * MoveSpeed;
        }
        else
        {
            v = 0.0f;
        }

        transform.position += transform.forward * v;
    }

    /**
     * プレイヤー方向転換
     */
    void Rotate()
    {
        // 左右方向転換
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            r = Time.deltaTime * RotateSpeed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            r = -Time.deltaTime * RotateSpeed;
        }
        else
        {
            r = 0.0f;
        }

        transform.Rotate(Vector3.up * r);
    }

    /**
     * 視点回転
     */
    void RotatePerspective()
    {
        // 上下視点移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            angle += -Time.deltaTime * RotateSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            angle += Time.deltaTime * RotateSpeed;
        }

        angle =  Mathf.Clamp(angle, AngleMax, AngleMix);
        headObj.transform.localEulerAngles = new Vector3(angle, 0.0f, 0.0f);
    }

    /**
     * 衝突判定
     */
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision");
    }
}
