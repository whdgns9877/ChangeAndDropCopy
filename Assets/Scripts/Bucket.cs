using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private List<GameObject> childs = new List<GameObject>(6);
    private Transform ObjectPoolTr = null;
    private BallObjectPool ballObjectPool = null;

    private void Start()
    {
        ObjectPoolTr = FindObjectOfType<BallObjectPool>().transform;
        ballObjectPool = BallObjectPool.Instance;
        Invoke(nameof(InitSpawn), 0.1f);
    }

    private void InitSpawn()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = ballObjectPool.SpawnFromPool(transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            childs.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = ballObjectPool.SpawnFromPool(transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            childs.Add(obj);
        }

        PlayerInput.Update();
        TouchState currentState = PlayerInput.state;

        if (currentState == TouchState.NONE)
            return;

        switch (currentState)
        {
            case TouchState.Swipe:
                transform.localPosition = new Vector3(PlayerInput.GetSwipeDistance(), transform.position.y, transform.position.z);
                break;

            case TouchState.TouchEnd:
                StartCoroutine(CO_Pour());
                break;
        }
    }

    private IEnumerator CO_Pour()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        for (int i = 0; i < childs.Count; ++i)
        {
            Rigidbody childRigidbody = childs[i].GetComponent<Rigidbody>();
            childRigidbody.velocity = Vector3.down;
            childs[i].transform.SetParent(ObjectPoolTr);
        }

        childs.Clear();
    }
}
