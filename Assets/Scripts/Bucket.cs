using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bucket : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPool.SpawnFromPool("Ball", transform.position, Quaternion.identity, transform);
        }
        if (Input.touchCount == 0)
            return;

        PlayerInput.Update();

        switch (PlayerInput.state)
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
            yield return new WaitForFixedUpdate();
        }

        // 회전 완료 후 추가적인 동작 수행
        // ...

        yield break;
    }

}
