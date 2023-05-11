using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Countdown countdown;
    public PickUpCoins resetCoinsValue;
    public CoinGenerator resetCoins;
    public LightingChange resetLighting;

    public int pointsToDZ;

    public void ResetAll()
    {
        countdown.Reset();
        resetCoins.RemoveAll();
        resetCoins.coinGoalReached = true;
        resetCoins.darkZoneGate.SetActive(true);
        resetCoinsValue.Init();
        resetCoinsValue.pointsToDZ = pointsToDZ;
        resetLighting.ResetLight();
    }
}
