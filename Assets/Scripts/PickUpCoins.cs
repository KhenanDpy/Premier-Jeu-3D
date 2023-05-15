using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/*
 * Allows the player to pick up coins. It increases the score depending of the coin collected.
 */

public class PickUpCoins : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text lockerScore;

    public int points;
    public int pointsToAccessDarkZone;

    public CoinGenerator coinGoal;

    private GameObject father;

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
        if (other.gameObject.CompareTag("GoldCoin")) // add 5 points and make the goldcoin after 2 sec so the sound when picking it up can play
        {
            father = other.transform.parent.gameObject;
            points += 5;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject, 2);
            Destroy(father, 2);
        }
        if (other.gameObject.CompareTag("SilverCoin")) // add 1 point and make the goldcoin after 2 sec so the sound when picking it up can play
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

    void Update()
    {
        score.text = points.ToString();
        lockerScore.text = "Encore " + (pointsToAccessDarkZone - points).ToString() + " pièces pour ouvrir";
    }
}
