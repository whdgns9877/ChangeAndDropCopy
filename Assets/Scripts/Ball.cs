using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool canSpawn = true; // 현재 스스로 ball을 생성 시킬수 있는지
    private float spawnDelay = 1.0f; // 생성 딜레이 시간
    private Rigidbody myRb = null;

    // 5개의 Point배열
    private Vector3[] spawnPoints = new Vector3[5]
    {
        new Vector3(-0.7f, -0.3f, 0),
        new Vector3(-0.35f, -0.3f, 0),
        new Vector3(0f, -0.3f, 0),
        new Vector3(0.35f, -0.3f, 0),
        new Vector3(0.7f, -0.3f, 0)
    };

    private void OnEnable()
    {
        // 오브젝트가 활성화 된 직후에는 생성할수 없는상태로 만들고
        canSpawn = false;
        // 일정시간 이후에 생성할수 있는상태로 바꿔준다
        Invoke(nameof(ResetSpawn), spawnDelay);
        // RigidBody컴포넌트를 얻고 
        myRb = GetComponent<Rigidbody>();
        // 활성화되면 우선 아래로 발사
        GoDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 현재 스폰이 가능한 상태이고 
        if (canSpawn && other.CompareTag(Utils.BLUETAG))
        {
            canSpawn = false; // 생성 후 일정 시간 동안 재생성 방지
            int createNum = other.GetComponent<Obstacle>().MyNum; // 본인이 충돌한 물체의 MyNum으로 몇개 생성할지 결정

            int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount함수를 통해 (0,4) ,(0,2,4), (0,1,2,3,4) 선택

            int idx = 0;

            for (int i = 0; i < createNum; ++i)
            {
                // ObjectPool에서 오브젝트 활성화 (꺼낼게 없으면 생성함)
                ObjectPool.SpawnFromPool(Utils.BALLTAG, transform.position + spawnPoints[idx], Quaternion.identity);
                idx += increase;
            }

            // 전부 생성시켜줬다면 생성시킨 주체는 Pool에 반환시킴
            ObjectPool.ReturnToPool(gameObject);
            // 실행하고 있던 모든 로직 종료
            CancelInvoke();
        }
    }

    // 아랫방향으로 랜덤방향으로 힘을 가한다
    private void GoDown()
    {
        myRb.velocity = Vector3.down;
        Vector3 dir = new Vector3(Random.Range(-1, 1f), -1f, 0f).normalized;
        myRb.AddForce(dir, ForceMode.Impulse);
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

    // 비활성화 될때는 Pool에 반환
    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
        CancelInvoke();
    }
}
