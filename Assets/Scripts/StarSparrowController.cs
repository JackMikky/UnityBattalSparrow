using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarSparrowController : MonoBehaviour
{

    //Rigidbody myrigidbody;
    // Transform myTransform;
    [SerializeField] SparrowHP sparrowHP;
    CharacterController characterController;
    Animator animator;
   
    [SerializeField] float moveSpeed = 2;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Transform missilesParent;

    // [SerializeField] float upDownRotate = 30f;
    // [SerializeField] float forwardBack = 20f;
    // [SerializeField] float rotateSpeed = 10f;
    [Header("Attack")]
    [SerializeField] GameObject missile;
    [SerializeField] Transform[] missileLaunchTransform;
    [SerializeField] AudioSource attackAudioSource;
    [SerializeField] AudioClip attackClip;
    [SerializeField] List<GameObject> missileCash;
    bool fire;
    [SerializeField] float fireLaggingTime = .25f;
    float fireTimer;

    void Start()
    {
       //myrigidbody = GetComponent<Rigidbody>();
       // myTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        animator= GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu&& GameManager.instance.isInScreenGame)
            return;
        Move_Rotate();
        FireFunc();
    }
    void FireFunc()
    {

        if (fire && fireTimer >= fireLaggingTime)
        {
            attackAudioSource.PlayOneShot(attackClip);
           var ms1= Instantiate(missile, missileLaunchTransform[0].position, Quaternion.identity, missilesParent);
            var ms2=Instantiate(missile, missileLaunchTransform[1].position, Quaternion.identity, missilesParent);
            CreateObjectCash(ms1);
            CreateObjectCash(ms2);
            fireTimer = 0;
        }
        fireTimer += Time.fixedDeltaTime;
    }
    void Move_Rotate()
    {
            animator.SetFloat("X", moveDirection.x);
            animator.SetFloat("Y", moveDirection.y);
        characterController.Move(moveDirection * Time.fixedDeltaTime * moveSpeed);
        //myrigidbody.MovePosition(myTransform.position + moveDirection * Time.fixedDeltaTime * moveSpeed);
        //myrigidbody.MoveRotation(Quaternion.Euler(180, -90 + moveDirection.x * forwardBack * rotateSpeed * Time.fixedDeltaTime, 90 + moveDirection.y * upDownRotate * rotateSpeed * Time.fixedDeltaTime));// new Vector3(0, moveDirection.x * forwardBack, moveDirection.x * upDownRotate)
    }
    void CreateObjectCash(GameObject go)
    {
        for (int a = 0; a <= missileCash.Count - 1; a++)
        {
            if (missileCash[a] == null)
            {
                missileCash[a] = (go);
                break;
            }
            else
            {
                continue;
            }
        }
    }
    public void CleanCash()
    {
       // Debug.Log(missileCash.Count);
        for(var i = 0; i < missileCash.Count - 1; i++)
        {
            if (missileCash[i] != null)
            {

                    Destroy(missileCash[i]);
                    missileCash[i] = null;
            }
        }
        //foreach (var item in missileCash)
        //{
        //    try
        //    {
        //        Destroy(item);
        //        missileCash.Remove(item);
        //    }
        //    catch (Exception e){ Debug.Log(e); }
           
        //}
    }
    public void ResetMoveDirection() { moveDirection = Vector3.zero;}

    #region PlayerInputSys
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    public void OnFire(InputValue value)
    {
        if (value.isPressed) fire = true;
        else fire = false;
    }
    #endregion
}
