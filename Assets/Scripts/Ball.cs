using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool canSpawn = true; // ���� ������ ball�� ���� ��ų�� �ִ���
    private float spawnDelay = 1.0f; // ���� ������ �ð�
    private Rigidbody myRb = null;

    // 5���� Point�迭
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
        // ������Ʈ�� Ȱ��ȭ �� ���Ŀ��� �����Ҽ� ���»��·� �����
        canSpawn = false;
        // �����ð� ���Ŀ� �����Ҽ� �ִ»��·� �ٲ��ش�
        Invoke(nameof(ResetSpawn), spawnDelay);
        // RigidBody������Ʈ�� ��� 
        myRb = GetComponent<Rigidbody>();
        // Ȱ��ȭ�Ǹ� �켱 �Ʒ��� �߻�
        GoDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ������ �����̰� 
        if (canSpawn && other.CompareTag(Utils.BLUETAG))
        {
            canSpawn = false; // ���� �� ���� �ð� ���� ����� ����
            int createNum = other.GetComponent<Obstacle>().MyNum; // ������ �浹�� ��ü�� MyNum���� � �������� ����

            int increase = GetIncreaseAmount(createNum); // GetIncreaseAmount�Լ��� ���� (0,4) ,(0,2,4), (0,1,2,3,4) ����

            int idx = 0;

            for (int i = 0; i < createNum; ++i)
            {
                // ObjectPool���� ������Ʈ Ȱ��ȭ (������ ������ ������)
                ObjectPool.SpawnFromPool(Utils.BALLTAG, transform.position + spawnPoints[idx], Quaternion.identity);
                idx += increase;
            }

            // ���� ����������ٸ� ������Ų ��ü�� Pool�� ��ȯ��Ŵ
            ObjectPool.ReturnToPool(gameObject);
            // �����ϰ� �ִ� ��� ���� ����
            CancelInvoke();
        }
    }

    // �Ʒ��������� ������������ ���� ���Ѵ�
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

    // ��Ȱ��ȭ �ɶ��� Pool�� ��ȯ
    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
        CancelInvoke();
    }
}
