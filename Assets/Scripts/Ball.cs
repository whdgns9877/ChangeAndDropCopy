using UnityEngine;

public class Ball : MonoBehaviour
{
    private string blueTag = "Blue";
    private string orangeTag = "Orange";
    private bool canSpawn = true;
    private float spawnDelay = 1.0f; // ���� ������ �ð�

    private void OnEnable()
    {
        canSpawn = false;
        Invoke(nameof(ResetSpawn), spawnDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canSpawn && other.CompareTag(blueTag))
        {
            canSpawn = false; // ���� �� ���� �ð� ���� ����� ����
            for (int i = 0; i < other.GetComponent<Obstacle>().MyNum; i++)
            {
                GameObject obj = ObjectPool.SpawnFromPool("Ball", transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.down);
            }
            ObjectPool.ReturnToPool(gameObject);
            CancelInvoke();
            //Invoke(nameof(ResetSpawn), spawnDelay);
        }
    }

    private void ResetSpawn()
    {
        canSpawn = true;
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
        CancelInvoke();
    }
}
