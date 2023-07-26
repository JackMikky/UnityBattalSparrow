using UnityEngine;

public class NormalRockController : MonoBehaviour
{
    Rigidbody rigidbody;
    Transform enemyTransform;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] Vector3 moveDirection = Vector3.left;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        var rx=Random.Range(0f, 180f);
        var ry = Random.Range(0f, 180f);
        var rz = Random.Range(0f, 180f);
        enemyTransform.eulerAngles= new Vector3(rx, ry, rz);
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu && GameManager.instance.isInScreenGame)
            return;
        Move();
    }

    void Move()
    {
        rigidbody.MovePosition(enemyTransform.position + moveDirection * Time.fixedDeltaTime * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScreenGameBarrier"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<SparrowHP>().GetDamage(1);
            Destroy(gameObject);
        }
    }
}
