using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    // Bucket���������� ������ ����ٴϰ� �ؾ��ϹǷ� �ʿ��� List
    private List<GameObject> childs = new List<GameObject>();
    // ��� ���Ŀ��� Pool�� �־��ֱ����� ObjectPool�� ������ �����ִ´�
    private Transform ObjectPoolTr = null;

    private void Start()
    {
        ObjectPoolTr = FindObjectOfType<ObjectPool>().transform; //ObjectPool �� transfrom �Ҵ�
        Invoke(nameof(Test),0.1f);
    }

    private void Test()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = ObjectPool.SpawnFromPool(Utils.BALLTAG, transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0), Utils.QI);
            obj.transform.SetParent(transform);
            childs.Add(obj); 
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = ObjectPool.SpawnFromPool(Utils.BALLTAG, transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0), Utils.QI);
            obj.transform.SetParent(transform);
            childs.Add(obj);
        }
        // ��ġ �Է��� ���ٸ� ���������ʴ´�
        if (Input.touchCount == 0)
            return;

        // ��ġ �Է��� �ִٸ� �÷��̾��� �Է»��¸� ������Ʈ�Ѵ�
        PlayerInput.Update();

        // �÷��̾��� �Է»��¿� ���� ó���Ѵ�
        switch (PlayerInput.state)
        {
            // �������� �ÿ���
            case TouchState.Swipe:
                // bucket�� Swipe�� ���� �����δ�. GetSwipeDistance �Լ��� �̿��� x���� �ּ�, �ִ밪�� Clamping�Ѵ�
                transform.localPosition = new Vector3(PlayerInput.GetSwipeDistance(), transform.position.y, transform.position.z);
                break;

            // ��ġ�� �����ٸ�
            case TouchState.TouchEnd:
                // Bucket�� ������ ������ ������
                StartCoroutine(CO_Pour());
                break;
        }
    }

    // 180�� ȸ����Ű�� �Լ�
    private IEnumerator CO_Pour()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        float elapsedTime = 0f;
        float duration = 1f;

        // Lerp �Լ��� �̿��Ͽ� 180�� ��ŭ �ε巴�� ȸ��
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        // ȸ�� �Ϸ� �� bucket������Ʈ�� �ڽ����� �ִ� ball���� �θ�� ObejctPool������Ʈ�� ����
        for (int i = 0; i < childs.Count; ++i)
        {
            childs[i].GetComponent<Rigidbody>().velocity = Vector3.down;
            childs[i].transform.SetParent(ObjectPoolTr);
        }

        yield break;
    }

    // Bucket���� ball���� �����ϴ��Լ�
    private void ClearList() => childs.Clear();
}
