using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);    // �� ��ü�� �ѹ���
        CancelInvoke();
    }
}
