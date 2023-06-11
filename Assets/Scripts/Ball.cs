using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0f;
    private bool canSpawn = true; // ���� ������ ball�� ���� ��ų�� �ִ���
    private float spawnDelay = 1.0f; // ���� ������ �ð�
    private Rigidbody myRb = null;

    // 5���� Point�迭
    private Vector3[] spawnPoints = new Vector3[5]
    {
        new Vector3(-0.5f, -0.1f, 0),
        new Vector3(-0.25f, -0.1f, 0),
        new Vector3(0f, -0.1f, 0),
        new Vector3(0.25f, -0.1f, 0),
        new Vector3(0.5f, -0.1f, 0)
    };

    private void OnEnable()
    {
        // ������Ʈ�� Ȱ��ȭ �� ���Ŀ��� �����Ҽ� ���»��·� �����
        canSpawn = false;
        // �����ð� ���Ŀ� �����Ҽ� �ִ»��·� �ٲ��ش�
        Invoke(nameof(ResetSpawn), spawnDelay);
        //// RigidBody������Ʈ�� ��� 
        myRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // �ִ� ȸ�� �ӵ��� 10���� ����
        myRb.maxAngularVelocity = 0.1f;

        // �浹 �ذ� �ӵ��� 1�� ����
        myRb.maxDepenetrationVelocity = 1f;
    }

    private void FixedUpdate()
    {  
        if (myRb.velocity.magnitude > maxSpeed)
        {
            myRb.velocity *= 0.9f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.EndLineTag))
        {
            // z�� ��ǥ ������ Ǯ���ش�
            myRb.constraints = ~RigidbodyConstraints.FreezePosition;
        }

        // ������ �Ķ��� �����϶�
        if(PlayerInput.isBallBlue == true)
        {
            // ���� �����ϰ� �浹ü�� �Ķ� �±׶��
            if(canSpawn && other.CompareTag(Utils.BLUETAG))
            {
                canSpawn = false; // ���� �� ���� �ð� ���� ����� ����
                int createNum = other.GetComponent<ObstacleBlue>().MyNum; // ������ �浹�� ��ü�� MyNum���� � �������� ����

                int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount�Լ��� ���� (0,4) ,(0,2,4), (0,1,2,3,4) ����

                int idx = 0;

                for (int i = 0; i < createNum; ++i)
                {
                    // ObjectPool���� ball �� 1�� ������
                    Ball childBall = BallObjectPool.Instance.SpawnFromPool(transform.position + spawnPoints[idx], Utils.QI).GetComponent<Ball>();
                    childBall.GoDown(myRb.velocity);
                    idx += increase;
                }

                // ���� ����������ٸ� ������Ų ��ü�� Pool�� ��ȯ��Ŵ
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }

            // �浹ü�� ������ �±׶��
            if (other.CompareTag(Utils.ORANGETAG))
            {
                // Pool�� ��ȯ
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }
        }
        // ������ ��Ȳ�� �����϶�
        else
        {
            // ���� �����ϰ� �浹ü�� �Ķ� �±׶��
            if (canSpawn && other.CompareTag(Utils.ORANGETAG))
            {
                canSpawn = false; // ���� �� ���� �ð� ���� ����� ����
                int createNum = other.GetComponent<ObstacleOrange>().MyNum; // ������ �浹�� ��ü�� MyNum���� � �������� ����

                int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount�Լ��� ���� (0,4) ,(0,2,4), (0,1,2,3,4) ����

                int idx = 0;

                for (int i = 0; i < createNum; ++i)
                {
                    // ObjectPool���� ball �� 1�� ������
                    BallObjectPool.Instance.SpawnFromPool(transform.position + spawnPoints[idx], Utils.QI);
                    idx += increase;
                }

                // ���� ����������ٸ� ������Ų ��ü�� Pool�� ��ȯ��Ŵ
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }

            // �浹ü�� ������ �±׶��
            if (other.CompareTag(Utils.BLUETAG))
            {
                // Pool�� ��ȯ
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }
        }
    }

    // �Ʒ��������� ������������ ���� ���Ѵ�
    public void GoDown(Vector3 velocity)
    {
        myRb.AddForce(velocity, ForceMode.Force);
    }

    private int GetIncreaseAmount(int num)
    {
        switch (num)
        {
            // 2���϶��� 4�� ������Ű���� (0,4)
            case 2:
                num = 4;
                break;
            // 3���϶��� 2�� ������Ű���� (0,2,4)
            case 3:
                num = 2;
                break;
            // 4���϶��� 1�� ������Ű���� (0,1,2,3,4)
            case 4:
                num = 1;
                break;
        }

        return num;
    }

    // ���������ѻ��·� �ٲ��ִ��Լ�
    private void ResetSpawn() => canSpawn = true;
}
