using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagerMono : MonoBehaviour
{

    public TextUpdater coinsCounter, distanceCounter;

    // Update is called once per frame
    void Update()
    {
        coinsCounter.UpdateText("Coins:", StatsHolder.NumCoins.ToString());
        distanceCounter.UpdateText("Distance:", StatsHolder.Distance.ToString());
    }
}
