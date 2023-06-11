using TMPro;
using UnityEngine;

public class ObstacleOrange : MonoBehaviour, IObstacle
{
    // �տ� ���� Text
    [SerializeField] private TextMeshProUGUI myText = null;
    // ��� ���Ұ�����
    private int myNum;
    public int MyNum { get { return myNum; } }
    [SerializeField] private string tag = "";

    // Start is called before the first frame update
    private void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>(); // TextMeshPro ������ ���
        int rand = Random.Range(2, 5); // 2~4 ��������
        myText.text = $"X {rand}"; // text�� �������� ��� �ְ�
        myNum = rand; // myNum�� ����
        tag = gameObject.tag;

        Process();
    }

    public void Process()
    {
    }
}
