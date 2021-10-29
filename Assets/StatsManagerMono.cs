using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManagerMono : MonoBehaviour
{

    public TextUpdater coinsCounter, distanceCounter, sliderUpdater;

    public Slider slider;

    private void Start()
    {
        slider.value = StatsHolder.TouchWindow;
    }

    // Update is called once per frame
    void Update()
    {
        coinsCounter.UpdateText("Coins:", StatsHolder.NumCoins.ToString());
        distanceCounter.UpdateText("Distance:", StatsHolder.Distance.ToString());

        sliderUpdater.UpdateText("Time between touches:", slider.value.ToString());
        StatsHolder.TouchWindow = slider.value;
    }
}
