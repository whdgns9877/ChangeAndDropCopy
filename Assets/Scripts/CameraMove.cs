using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //ī�޶� �ٶ� tartget�� Transform
    private Transform targetTr = null;

    // Update is called once per frame
    private void Update()
    {
        // Player�� ��ġ�Ͽ� ������ ������ ���ĺ��� ����
        if (PlayerInput.ballDrop == true)
        {
            targetTr = Utils.FindTarget(); // Ÿ���� ã��
            transform.position = new Vector3(transform.position.x, targetTr.position.y, transform.position.z); // y�ุ �̵���Ų��
        }
    }
}
