using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0f;
    private bool canSpawn = true; // 현재 스스로 ball을 생성 시킬수 있는지
    private float spawnDelay = 1.0f; // 생성 딜레이 시간
    private Rigidbody myRb = null;

    // 5개의 Point배열
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
        // 오브젝트가 활성화 된 직후에는 생성할수 없는상태로 만들고
        canSpawn = false;
        // 일정시간 이후에 생성할수 있는상태로 바꿔준다
        Invoke(nameof(ResetSpawn), spawnDelay);
        //// RigidBody컴포넌트를 얻고 
        myRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 최대 회전 속도를 10으로 설정
        myRb.maxAngularVelocity = 0.1f;

        // 충돌 해결 속도를 1로 설정
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
            // z축 좌표 고정을 풀어준다
            myRb.constraints = ~RigidbodyConstraints.FreezePosition;
        }

        // 공들이 파란색 상태일때
        if(PlayerInput.isBallBlue == true)
        {
            // 스폰 가능하고 충돌체가 파란 태그라면
            if(canSpawn && other.CompareTag(Utils.BLUETAG))
            {
                canSpawn = false; // 생성 후 일정 시간 동안 재생성 방지
                int createNum = other.GetComponent<ObstacleBlue>().MyNum; // 본인이 충돌한 물체의 MyNum으로 몇개 생성할지 결정

                int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount함수를 통해 (0,4) ,(0,2,4), (0,1,2,3,4) 선택

                int idx = 0;

                for (int i = 0; i < createNum; ++i)
                {
                    // ObjectPool에서 ball 을 1개 꺼낸다
                    Ball childBall = BallObjectPool.Instance.SpawnFromPool(transform.position + spawnPoints[idx], Utils.QI).GetComponent<Ball>();
                    childBall.GoDown(myRb.velocity);
                    idx += increase;
                }

                // 전부 생성시켜줬다면 생성시킨 주체는 Pool에 반환시킴
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }

            // 충돌체가 오랜지 태그라면
            if (other.CompareTag(Utils.ORANGETAG))
            {
                // Pool에 반환
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }
        }
        // 공들이 주황색 상태일때
        else
        {
            // 스폰 가능하고 충돌체가 파란 태그라면
            if (canSpawn && other.CompareTag(Utils.ORANGETAG))
            {
                canSpawn = false; // 생성 후 일정 시간 동안 재생성 방지
                int createNum = other.GetComponent<ObstacleOrange>().MyNum; // 본인이 충돌한 물체의 MyNum으로 몇개 생성할지 결정

                int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount함수를 통해 (0,4) ,(0,2,4), (0,1,2,3,4) 선택

                int idx = 0;

                for (int i = 0; i < createNum; ++i)
                {
                    // ObjectPool에서 ball 을 1개 꺼낸다
                    BallObjectPool.Instance.SpawnFromPool(transform.position + spawnPoints[idx], Utils.QI);
                    idx += increase;
                }

                // 전부 생성시켜줬다면 생성시킨 주체는 Pool에 반환시킴
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }

            // 충돌체가 오랜지 태그라면
            if (other.CompareTag(Utils.BLUETAG))
            {
                // Pool에 반환
                BallObjectPool.Instance.ReturnToPool(gameObject);
            }
        }
    }

    // 아랫방향으로 랜덤방향으로 힘을 가한다
    public void GoDown(Vector3 velocity)
    {
        myRb.AddForce(velocity, ForceMode.Force);
    }

    private int GetIncreaseAmount(int num)
    {
        switch (num)
        {
            // 2개일때는 4씩 증가시키게함 (0,4)
            case 2:
                num = 4;
                break;
            // 3개일때는 2씩 증가시키게함 (0,2,4)
            case 3:
                num = 2;
                break;
            // 4개일때는 1씩 증가시키게함 (0,1,2,3,4)
            case 4:
                num = 1;
                break;
        }

        return num;
    }

    // 스폰가능한상태로 바꿔주는함수
    private void ResetSpawn() => canSpawn = true;
}
