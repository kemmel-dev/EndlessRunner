using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public void OnCoinEnter(OnTriggerDelegation delegation)
    {
        if (delegation.Other.CompareTag("Player"))
        {
            StatsHolder.NumCoins++;
            Destroy(this.gameObject);
        }
    }
}
