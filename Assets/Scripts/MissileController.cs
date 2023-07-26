using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    MissileMove missileMove;
     enum MissileType
    {
        Enemy=0,
        Player=1
    }
    enum MissileHitType
    {
        Enemy,
        Player,
        Everything
    }
    [SerializeField] int missileDamage = 1;
    [SerializeField] MissileHitType missileHitType = MissileHitType.Enemy;
    [SerializeField] MissileType missileType = MissileType.Player;
    [SerializeField] AudioSource missileAS;
    [SerializeField] AudioClip missileClip;
    [SerializeField] GameObject vfx;
    Collider collider;
    private void Awake()
    {
        missileMove = GetComponent<MissileMove>();
        MissileInitialize();
    }
    void Start()
    {
        missileAS = GameObject.FindGameObjectWithTag("MissileSFX").GetComponent<AudioSource>();
        collider = GetComponentInChildren<Collider>();

    }
    void MissileInitialize()
    {
        switch (missileType)
        {
            case MissileType.Enemy:
                missileMove.SetMissileType = (MissileMove.MissileType.Enemy);
                break;
            case MissileType.Player:
                missileMove.SetMissileType = (MissileMove.MissileType.Player);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScreenGameBarrier"))
        {
            Destroy(this.gameObject);
        }else if (other.gameObject.CompareTag("Player") && missileHitType==MissileHitType.Player|| missileHitType == MissileHitType.Everything)
        {
            collider.enabled = false;
            other.gameObject.GetComponentInParent<SparrowHP>().GetDamage(missileDamage);
            HitAction();
        }
        else if (other.gameObject.CompareTag("Enemy") && missileHitType == MissileHitType.Enemy || missileHitType == MissileHitType.Everything)
        {
            collider.enabled = false;
            other.gameObject.GetComponentInParent<EnemyHP>().GetDamage(missileDamage);
            HitAction();
        }else if (other.gameObject.CompareTag("Rock") && missileHitType == MissileHitType.Enemy|| missileHitType == MissileHitType.Everything)
        {
            collider.enabled = false;
            other.gameObject.GetComponentInParent<EnemyHP>().GetDamage(missileDamage);
            HitAction();
        }
    }
    void TargetCheck()
    {
        switch (missileHitType)
        {
            case MissileHitType.Enemy:
                break;
            case MissileHitType.Player:
                break;
            case MissileHitType.Everything:
                break;
        }
    }
    void HitAction()
    {
        Instantiate(vfx,transform.position,Quaternion.identity);
        missileAS.PlayOneShot(missileClip);
        Destroy(this.gameObject);
    }
}
