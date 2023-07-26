using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float yAixMax = 7.5f;
    [SerializeField] float yAixMin = -7.5f;

    [SerializeField] float spawnMinTime = 1f;
    [SerializeField] float spawnMaxTime = 5f;
    float spawnTime = 2f;
    float spawnTimer;
    [SerializeField] List<GameObject> cashGameObjects;
    private void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu && GameManager.instance.isInScreenGame)
            return;
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime)
        {
            spawnTime = Random.Range(spawnMinTime, spawnMaxTime);
            spawnTimer = 0;
            var r = Random.Range(yAixMin, yAixMax);
            var pos = new Vector3(0, r, 0);
            var i = Random.Range(0, enemies.Length);
            var ob = Instantiate(enemies[i], transform);
            CreateObjectCash(ob);
            ob.transform.position += pos;
        }
    }
    void CreateObjectCash(GameObject go)
    {
        for (int a = 0; a <= cashGameObjects.Count - 1; a++)
        {
            if (cashGameObjects[a] == null)
            {
                cashGameObjects[a] = (go);
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
        for (var i = 0; i < cashGameObjects.Count - 1; i++)
        {
            if (cashGameObjects[i] != null)
            {
                if(cashGameObjects[i].GetComponent<EnemyController>()!=null)
                cashGameObjects[i].GetComponent<EnemyController>().CleanCash();
                Destroy(cashGameObjects[i]);
                cashGameObjects[i] = null;
                //cashGameObjects.Remove(cashGameObjects[i]);
            }
        }
        //foreach (var item in cashGameObjects)
        //{
        //    if(item.GetComponent<EnemyController>()!=null)
        //    item.GetComponent<EnemyController>().CleanCash(); 

        //    Destroy(item);
        //    cashGameObjects.Remove(item);
        //}
    }
}
