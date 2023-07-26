using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparrowHP : MonoBehaviour
{
    StarSparrowController starSparrowController;
    [SerializeField] int maxHP = 5;
    [SerializeField] int currentHP = 0;
    [SerializeField] GameObject hpCanvas;
    [SerializeField] GameObject hpBar;
    [SerializeField] List<GameObject> hpBarList;
    [SerializeField] GameObject colliders;

    [SerializeField] float autoHealthTime = 5f;
    float healthTimer;
    int hpbarIndex;
    private void Start()
    {
        starSparrowController=GetComponent<StarSparrowController>();
        InitializeHP();
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu && GameManager.instance.isInScreenGame)
            return;
        Health();
    }
    public void GetDamage(int damage)
    {
        hpbarIndex = currentHP - 1;
        hpBarList[currentHP - 1].gameObject.SetActive(false);
        //hpBarList[currentHP - 1]=null;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    void InitializeHP()
    {
       
        currentHP = maxHP;
        for (int i = 0; i < maxHP; i++)
        {
            hpBarList[i] = (Instantiate(hpBar, hpCanvas.transform));
        }
    }
  public void InitializeHP_Restart()
    {
        starSparrowController.ResetMoveDirection();
        colliders.SetActive(true);
        this.gameObject.SetActive(true);
        CleanHP();
        currentHP = maxHP;
        for (int i = 0; i < maxHP; i++)
        {
            hpBarList[i].SetActive(true);
            //hpBarList[i]=Instantiate(hpBar, hpCanvas.transform);
        }
    }
    public void CleanHP()
    {
        for (var i = 0; i < hpBarList.Count; i++)
        {
            if (hpBarList[i] != null)
            {
                hpBarList[i].SetActive(false);
               // Destroy(hpBarList[i]);
               // hpBarList[i] = null;
                //hpBarList.Remove(hpBarList[i]);
            }
        }
    }
    void Die()
    {
        colliders.SetActive(false);
       this.gameObject.SetActive(false);
    }
    void Health()
    {
        if (currentHP == maxHP) { return; }
        else if (currentHP > 0 && currentHP < maxHP)
        {
            healthTimer += Time.fixedDeltaTime;
            if (healthTimer >= autoHealthTime)
            {
                hpBarList[hpbarIndex].SetActive(true) ;
                //hpBarList[hpbarIndex] =((Instantiate(hpBar, hpCanvas.transform)));
                currentHP += 1;
                healthTimer = 0;
            }
        }
    }
}
