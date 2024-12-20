using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSlot : MonoBehaviour
{
    [SerializeField] SOData _data;
    [SerializeField] float _cost=1f;

    void OnMouseDown()
    {
        if(_data.nbCaliCoins.currentValue>0 && !CasinoManager.instance.slotsActivated)
        {
            _data.ReduceStat(_data.nbCaliCoins, _cost);
            CasinoManager.instance.ActivateSlots();
        }
    }
}
