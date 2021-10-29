using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatsHolder
{

    public static int Distance { get; set; }

    public static int NumCoins { get; set; }

    internal static void Reset()
    {
        Distance = 0;
        NumCoins = 0;
    }
}
