using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PickUpCoins : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text lockerScore;

    int points;
    public int pointsToDZ;

    public CoinGenerator coinGoal;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        points = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject father = other.transform.parent.gameObject;

        if (other.gameObject.CompareTag("GoldCoin"))
        {
            points += 5;
            Destroy(other.gameObject); 
            Destroy(father);
            if (points >= pointsToDZ)
            {
                coinGoal.coinGoalReached = false;
            }
        }
        if (other.gameObject.CompareTag("SilverCoin"))
        {
            points++;
            Destroy(other.gameObject);
            Destroy(father);
            if (points >= pointsToDZ)
            {
                coinGoal.coinGoalReached = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = points.ToString() + " / " + pointsToDZ.ToString();
        lockerScore.text = "Encore " + (pointsToDZ-points).ToString() + " pièces pour ouvrir";
    }
}
