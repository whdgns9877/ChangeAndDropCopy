using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //카메라가 바라볼 tartget의 Transform
    private Transform targetTr = null;

    private float offsetY = 10f;

    private float minY = Mathf.Infinity; // 첫비교는 가장 큰수로 하여 최솟값을 못찾는일이 없도록 설정

    // Update is called once per frame
    private void Update()
    {
        // Player가 터치하여 공들이 떨어진 이후부터 추적
        if (PlayerInput.ballDrop == true)
        {
            targetTr = Utils.FindTarget(); // 타겟을 찾고
            if (targetTr == null) return;
            // 제일 작은 y값을 기억해놓고 실시간으로 변하는 target들중에서 최솟값보다 작은값이 없다면 저장되어있던
            // minY값을 그대로 사용
            if(minY > targetTr.transform.position.y)
            {
                minY = targetTr.transform.position.y;
            }
            transform.position = new Vector3(transform.position.x, minY + offsetY, transform.position.z); // y축만 이동시킨다
        }
    }
}
