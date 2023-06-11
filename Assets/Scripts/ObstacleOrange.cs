using UnityEngine;

public class ObstacleOrange : Obstacle
{
    [SerializeField] private GameObject Thorns = null;

    public int MyNum { get { return myNum; } }

    private void OnEnable()
    {
        Thorns.SetActive(false);
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        int rand = Random.Range(2, 5); // 2~4 ��������
        myText.text = $"X {rand}"; // text�� �������� ��� �ְ�
        myNum = rand; // myNum�� ����
        tag = gameObject.tag;
    }

    public void ThornActive(bool active) => Thorns.SetActive(active);
}
