using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    // Bucket스와이프시 공들이 따라다니게 해야하므로 필요한 List
    private List<GameObject> childs = new List<GameObject>();
    // 드랍 이후에는 Pool에 넣어주기위해 ObjectPool의 참조를 갖고있는다
    private Transform ObjectPoolTr = null;

    private void Start()
    {
        ObjectPoolTr = FindObjectOfType<ObjectPool>().transform; //ObjectPool 의 transfrom 할당
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
        // 터치 입력이 없다면 연산하지않는다
        if (Input.touchCount == 0)
            return;

        // 터치 입력이 있다면 플레이어의 입력상태를 업데이트한다
        PlayerInput.Update();

        // 플레이어의 입력상태에 따라 처리한다
        switch (PlayerInput.state)
        {
            // 스와이프 시에는
            case TouchState.Swipe:
                // bucket을 Swipe에 따라 움직인다. GetSwipeDistance 함수를 이용해 x축의 최소, 최대값을 Clamping한다
                transform.localPosition = new Vector3(PlayerInput.GetSwipeDistance(), transform.position.y, transform.position.z);
                break;

            // 터치가 끝났다면
            case TouchState.TouchEnd:
                // Bucket을 뒤집어 공들을 떨군다
                StartCoroutine(CO_Pour());
                break;
        }
    }

    // 180도 회전시키는 함수
    private IEnumerator CO_Pour()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        float elapsedTime = 0f;
        float duration = 1f;

        // Lerp 함수를 이용하여 180도 만큼 부드럽게 회전
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        // 회전 완료 후 bucket오브젝트의 자식으로 있던 ball들의 부모로 ObejctPool오브젝트를 설정
        for (int i = 0; i < childs.Count; ++i)
        {
            childs[i].GetComponent<Rigidbody>().velocity = Vector3.down;
            childs[i].transform.SetParent(ObjectPoolTr);
        }

        yield break;
    }

    // Bucket안의 ball들을 정리하는함수
    private void ClearList() => childs.Clear();
}
