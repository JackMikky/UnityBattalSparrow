using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HyperpodControllor : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] UnityEvent openEvent;
    [SerializeField] UnityEvent closeEvent;
    [SerializeField] GameObject uiObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioSource uiAS;
    [SerializeField] private AudioClip audioClip;
    private float dis = 1.2f;
    [SerializeField] private bool isOpening;
    private bool actionAdded;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiObject.SetActive(false);
        animator = GetComponent<Animator>();

    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(sfxClip);
    }
    public void OpenAnima()
    {
        openEvent.Invoke();
    }

    public void CloseAnima()
    {
        closeEvent.Invoke();
    }

    void DisPlayeUI()
    {
        uiObject.SetActive(true);
    }

    void CloseUI()
    {
        uiObject.SetActive(false);
    }

    void ObjectStateCheck()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            return;
        if (isOpening)
        {
            uiAS.PlayOneShot(audioClip);
            CloseAnima();
            isOpening = false;
        }
        else
        {
            uiAS.PlayOneShot(audioClip);
            OpenAnima();
            isOpening = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisPlayeUI();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var distance = (uiObject.transform.position - (other.transform.position + new Vector3(0, 0.5f, 0))).magnitude;
            if (distance > dis)
            {
                actionAdded = false;
                ActionEventManager._instance.E_ButtonEvent.RemoveListener(ObjectStateCheck);
                // ObjectStateCheck();
            }
            else
            {
                if (!actionAdded)
                {
                    actionAdded=true;
                    ActionEventManager._instance.E_ButtonEvent.AddListener(ObjectStateCheck);
                }
                // ObjectStateCheck(); 
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActionEventManager._instance.E_ButtonEvent.RemoveListener(ObjectStateCheck);
            CloseUI();
        }
    }
}
