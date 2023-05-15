using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * To set any parameters from editor and reset everything when restarting a run
 */
public class GameController : MonoBehaviour
{
    [SerializeField] Countdown countdown;
    public PickUpCoins resetCoinsValue;
    public CoinGenerator resetCoins;
    public LightingChange resetLighting;
    public Life lifePoints;
    public Attack resetMonsters;

    public int setLifePoints;
    public float setCountdownDuration;
    public int pointsToAccessDarkZone; // points to get until the dark zone is accessible

    public void ResetAll()
    {
        lifePoints.lifePoints = setLifePoints;
        countdown.countdownDuration = setCountdownDuration;
        countdown.Reset();
        resetCoins.RemoveAll();
        resetCoins.coinGoalReached = false;
        resetCoins.darkZoneGate.SetActive(true);
        resetCoinsValue.Init();
        resetCoinsValue.pointsToAccessDarkZone = pointsToAccessDarkZone;
        resetLighting.ResetLight();
        resetMonsters.ResetMonsters();
    }
}
