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
            //other.gameObject.GetComponent<MeshRenderer>().gameObject.SetActive(false);
            Destroy(other.gameObject, 2);
            Destroy(father, 2);
        }
        if (other.gameObject.CompareTag("SilverCoin"))
        {
            father = other.transform.parent.gameObject;
            points++;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject,2);
            Destroy(father,2);
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
