using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //ī�޶� �ٶ� tartget�� Transform
    private Transform targetTr = null;

    private float offsetY = 10f;

    private float minY = Mathf.Infinity; // ù�񱳴� ���� ū���� �Ͽ� �ּڰ��� ��ã������ ������ ����

    // Update is called once per frame
    private void Update()
    {
        // Player�� ��ġ�Ͽ� ������ ������ ���ĺ��� ����
        if (PlayerInput.ballDrop == true)
        {
            targetTr = Utils.FindTarget(); // Ÿ���� ã��
            if (targetTr == null) return;
            // ���� ���� y���� ����س��� �ǽð����� ���ϴ� target���߿��� �ּڰ����� �������� ���ٸ� ����Ǿ��ִ�
            // minY���� �״�� ���
            if(minY > targetTr.transform.position.y)
            {
                minY = targetTr.transform.position.y;
            }
            transform.position = new Vector3(transform.position.x, minY + offsetY, transform.position.z); // y�ุ �̵���Ų��
        }
    }
}
