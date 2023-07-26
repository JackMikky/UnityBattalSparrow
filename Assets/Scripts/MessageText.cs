using UnityEngine;
using System.Collections;

public class MessageText : MonoBehaviour
{
    const float MoveSpeed = 2.0f; // 移動速度
    const float Lifetime = 10.0f; // 破棄されるまでの時間

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        transform.position += new Vector3(-MoveSpeed, 0.0f, 0.0f);
    }
    
}
