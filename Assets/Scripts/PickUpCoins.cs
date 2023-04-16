using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PickUpCoins : MonoBehaviour
{
    public TMP_Text score;

    int points;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        points = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            points++;
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "x " + points.ToString();
    }
}
