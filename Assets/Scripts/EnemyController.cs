using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    EnemyHP enemyHP;
    enum LaunchType
    {
        Single,
        Double
    }
    [SerializeField]public bool isRock;
    Rigidbody rigidbody;
    Transform enemyTransform;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] Vector3 moveDirection = Vector3.left;
    [SerializeField] GameObject colliders;

    [Header("Attack")]
    [SerializeField] LaunchType launchType = LaunchType.Single;
    [SerializeField] float fireLaggingTime = .25f;
    [SerializeField] float laggingTimeMax = 5f;
    float fireTimer;
    [SerializeField] GameObject missile;
    [SerializeField] Transform[] missileLaunchTransform;
    [SerializeField] List<GameObject> missileCash;
    [SerializeField] public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
           rigidbody = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        if (isRock)
        {
            moveSpeed = Random.Range(2f, 6f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu && GameManager.instance.isInScreenGame )
            return;
        Move();
        FireFunc();
    }

    void Move()
    {
        rigidbody.MovePosition(enemyTransform.position + moveDirection * Time.fixedDeltaTime * moveSpeed);
        // rigidbody.MoveRotation(Quaternion.Euler(180, -90 + moveDirection.x * forwardBack * rotateSpeed * Time.fixedDeltaTime, 90 + moveDirection.y * upDownRotate * rotateSpeed * Time.fixedDeltaTime));// new Vector3(0, moveDirection.x * forwardBack, moveDirection.x * upDownRotate)
    }
    void FireFunc()
    {
        if (isRock || isDead)
            return;
        if (fireTimer >= fireLaggingTime)
        {
            switch (launchType)
            {
                case LaunchType.Single:
                    fireLaggingTime = Random.Range(1.2f, laggingTimeMax);
                    var ms = Instantiate(missile, missileLaunchTransform[0].position, Quaternion.identity);
                    CreateObjectCash(ms);
                    break;
                case LaunchType.Double:
                    fireLaggingTime = Random.Range(1.2f, laggingTimeMax);
                    var ms1 = Instantiate(missile, missileLaunchTransform[0].position, Quaternion.identity);
                    var ms2 = Instantiate(missile, missileLaunchTransform[1].position, Quaternion.identity);
                    CreateObjectCash(ms1);
                    CreateObjectCash(ms2);
                    break;
            }
            fireTimer = 0;
        }
        fireTimer += Time.fixedDeltaTime;
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
        for (var i = 0; i < missileCash.Count - 1; i++)
        {
            if (missileCash[i] != null)
            {
                Destroy(missileCash[i]);
                missileCash[i] = null;
                // missileCash.Remove(missileCash[i]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScreenGameBarrier"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player")&& !isDead)
        {
            
            isDead = true;
            colliders.SetActive(false);
            if (isRock)
            {
                other.gameObject.GetComponentInParent<SparrowHP>().GetDamage(1);
                Destroy(gameObject);
                return;
            }
            other.gameObject.GetComponentInParent<SparrowHP>().GetDamage(1);
            var rendermesh = GetComponent<MeshRenderer>();
            rendermesh.enabled = false;
            enemyHP.enabled = false;
            //this.enabled = false;
             //Destroy(gameObject,4f);
        }
    }

}
