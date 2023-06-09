using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //카메라가 바라볼 tartget의 Transform
    private Transform targetTr = null;

    // Update is called once per frame
    private void Update()
    {
        // Player가 터치하여 공들이 떨어진 이후부터 추적
        if (PlayerInput.ballDrop == true)
        {
            targetTr = Utils.FindTarget(); // 타겟을 찾고
            transform.position = new Vector3(transform.position.x, targetTr.position.y, transform.position.z); // y축만 이동시킨다
        }
    }
}
