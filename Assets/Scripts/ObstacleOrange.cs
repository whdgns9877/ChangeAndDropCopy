using TMPro;
using UnityEngine;

public class ObstacleOrange : MonoBehaviour, IObstacle
{
    // 앞에 보일 Text
    [SerializeField] private TextMeshProUGUI myText = null;
    // 몇개를 곱할것인지
    private int myNum;
    public int MyNum { get { return myNum; } }
    [SerializeField] private string tag = "";

    // Start is called before the first frame update
    private void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>(); // TextMeshPro 참조를 얻고
        int rand = Random.Range(2, 5); // 2~4 랜덤숫자
        myText.text = $"X {rand}"; // text에 랜덤숫자 결과 넣고
        myNum = rand; // myNum에 대입
        tag = gameObject.tag;

        Process();
    }

    public void Process()
    {
    }
}
