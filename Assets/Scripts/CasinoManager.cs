using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoManager : MonoBehaviour
{
    public static CasinoManager instance;
    [SerializeField] SOData _data;
    [SerializeField] Slot[] _slots;
    [SerializeField] float _slotDelay = 0.2f;
    [SerializeField] float _spintime = 2.5f;
    [SerializeField] float _winPrize = 25f;
    [SerializeField] bool _slotsActivated=false;
    public bool slotsActivated=>_slotsActivated;
    [SerializeField] bool _win=false;
    Coroutine _macoroutine;


    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void ActivateSlots()
    {
        if(!_slotsActivated) _macoroutine=StartCoroutine(SpinSlotMachine());
        _slotsActivated=true;
    }

    IEnumerator SpinSlotMachine()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Activate();
            yield return new WaitForSeconds(_slotDelay);
        }

        yield return new WaitForSeconds(_spintime);

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Stop();
            yield return new WaitForSeconds(_slotDelay);
        }
        _slotsActivated=false;
        CheckResult();
        StopCoroutine(_macoroutine);
    }

    void CheckResult()
    {
        _win=true;
        for (int i = 0; i < _slots.Length; i++)
        {
            if(_slots[i].slotSymb.sprite!=_slots[0].slotSymb.sprite)_win=false;
        }
        if(_win) _data.AugmentStat(_data.nbCaliCoins, _winPrize);
    }


}
