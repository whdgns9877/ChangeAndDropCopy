using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);    // 한 객체에 한번만
        CancelInvoke();
    }
}
