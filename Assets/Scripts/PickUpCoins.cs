using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PickUpCoins : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text lockerScore;

    public int points;
    public int pointsToAccessDarkZone;

    public CoinGenerator coinGoal;

    private GameObject father;

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
        if (other.gameObject.CompareTag("GoldCoin"))
        { 
            father = other.transform.parent.gameObject;
            points += 5;
            Destroy(other.gameObject); 
            Destroy(father);
        }
        if (other.gameObject.CompareTag("SilverCoin"))
        {
            father = other.transform.parent.gameObject;
            points++;
            Destroy(other.gameObject);
            Destroy(father);
        }

        if (points >= pointsToAccessDarkZone)
        {
            coinGoal.coinGoalReached = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = points.ToString();
        lockerScore.text = "Encore " + (pointsToAccessDarkZone - points).ToString() + " pièces pour ouvrir";
    }
}
