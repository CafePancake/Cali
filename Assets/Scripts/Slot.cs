using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Sprite[] _symbols;
    [SerializeField] SpriteRenderer _slotSymb;
    public SpriteRenderer slotSymb=>_slotSymb;
    [SerializeField] float _spinRate = 0.2f;
    [SerializeField] bool _spin;
    Coroutine _maCoroutine;


    public void Activate()
    {
        _maCoroutine = StartCoroutine(Spin());
    }
    IEnumerator Spin()
    {
        _spin=true;
        Debug.Log("Spin");
        int i=0;
        while (_spin)
        {
            _slotSymb.sprite = _symbols[i];

            yield return new WaitForSeconds(_spinRate);

            if (i < _symbols.Length-1) i++;
            else i=0;

            _slotSymb.sprite= _symbols[i];
        }
    }

    public void Stop()
    {
        _spin = false;
        StopCoroutine(_maCoroutine);
        _slotSymb.sprite = _symbols[Random.Range(0, _symbols.Length)];
        Debug.Log("StopSpin");
    }
}
