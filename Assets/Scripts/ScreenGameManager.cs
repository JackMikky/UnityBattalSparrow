using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameManager : MonoBehaviour
{
    [SerializeField] bool firstGame=true;
    [SerializeField] GameObject player;
    [SerializeField] EnemySpawner[] enemySpawners;
    public void CleanStage()
    {
        foreach (var i in enemySpawners)
        {
            i.CleanCash();
            i.enabled = false;
        }
    }
    public void InitializeSpawner()
    {
        foreach (var i in enemySpawners)
        {
            i.enabled = true;
        }
    }
   public void ResetPlayer() {
        if (!firstGame)
        {
            return;
        }
        firstGame = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.localPosition = new Vector3(-7,0,12);
        player.GetComponent<CharacterController>().enabled = true;
        
    }

    public void ResetGameToFirstGame()
    {
        firstGame=true;
    }
    public void ScreenGameContinue()
    {
        GameManager.instance.ScreenGameContinue();
    }
}
