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
        int rand = Random.Range(2, 5); // 2~4 랜덤숫자
        myText.text = $"X {rand}"; // text에 랜덤숫자 결과 넣고
        myNum = rand; // myNum에 대입
        tag = gameObject.tag;
    }

    public void ThornActive(bool active) => Thorns.SetActive(active);
}
