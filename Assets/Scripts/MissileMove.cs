using UnityEngine;

public class MissileMove : MonoBehaviour
{
   // MissileController missileController;
  public enum MissileType
    {
        Enemy=0,
        Player=1
    }
    enum MissileDirections
    {
        right=1,
        left=-1
    }

    new Rigidbody rigidbody;
    MissileType missileType = MissileType.Player;
    public MissileType SetMissileType { set { missileType = value; } get { return missileType; } }
    [SerializeField] MissileDirections missileDirection = MissileDirections.right;

    [SerializeField] float moveSpeed = 1.5f;
     Vector3 moveDirection;
    Transform mytransform;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mytransform = GetComponent<Transform>();
       MissileInitialize(missileDirection.GetHashCode());
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isInScreenGameMenu && GameManager.instance.isInScreenGame)
            return;
        rigidbody.MovePosition(mytransform.position+moveDirection * Time.fixedDeltaTime*moveSpeed);
    }
    void MissileInitialize(int xDir)
    {
        switch (missileType)
        {
            case MissileType.Enemy:
                moveDirection = new Vector3(xDir,0,0);
                transform.localEulerAngles = new Vector3(0,-90,90);
                break;
            case MissileType.Player:
                moveDirection = new Vector3(xDir, 0, 0);
                transform.localEulerAngles = new Vector3(180, -90, 90);
                break;
        }
    }
}
