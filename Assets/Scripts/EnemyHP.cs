using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    EnemyController enemyController;
    [SerializeField] float maxHP = 5;
    [SerializeField] float currentHP = 0;
    [SerializeField] int point=200;
    [SerializeField] GameObject colliders;
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        InitializeHP();
    }
    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            AddScore();
            Die();
        }
    }
    void InitializeHP()
    {
        currentHP = maxHP;
    }
    void Die()
    {
        
        enemyController.isDead = true;
        colliders.SetActive(false);
        if (enemyController.isRock)
        {
            Destroy(gameObject);
            return;
        }
        var rendermesh = GetComponent<MeshRenderer>();
        rendermesh.enabled = false;
        this.enabled = false;
        //Destroy(this.gameObject,4f);
    }
    void AddScore()
    {
        var ob = GameObject.FindGameObjectWithTag("Score");
        if (ob != null)
        {
            ob.GetComponent<ScoreCounter>().Score += point;
            ob.GetComponent<ScoreCounter>().ChangeScore();
        }
    }
}
