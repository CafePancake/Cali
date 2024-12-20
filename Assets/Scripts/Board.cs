using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class Board : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] float _num1;
    [SerializeField] float _num2;
    [SerializeField] float _result;
    [SerializeField] float _assignedNumber;
    [SerializeField] int _streak;
    [SerializeField] int _correctEquationReward=1;
    [SerializeField] int _assignedNumberLimit=99;
    [SerializeField] bool _writingNum1;
    [SerializeField] bool _writingNum2;
    [SerializeField] bool _usingResult;
    [SerializeField] string _numString;
    [SerializeField] bool _lastInputOperator;
    Bouton.InputType _operation;


    [Header("Initial Values")]
    [SerializeField] float _num1Ini=0;
    [SerializeField] float _num2Ini=0;
    [SerializeField] float _resultIni=0;
    [SerializeField] int _streakIni=0;
    [SerializeField] bool _writingNum1Ini=true;
    [SerializeField] bool _writingNum2Ini=false;
    [SerializeField] bool _usingResultIni=false;
    [SerializeField] bool _lastInputOperatorIni=false;
    [SerializeField] string _numStringIni="";
    Bouton.InputType _operationIni = Bouton.InputType.None;

    [Header("Parts")]
    [SerializeField] TextMeshProUGUI _displayText;
    [SerializeField] Bouton _boutonEgal;
    [SerializeField] Bouton _boutonDecimal;
    [SerializeField] Bouton[] _operators;
    public static Board instance;

    [Header("SOSystems")]
    [SerializeField] SODialogs _dialogs;
    [SerializeField] SOData _data;
    [SerializeField] SOEvents _events;

    [Header ("Miscelanious")]
    [SerializeField] float _interuptProbability = 1f; 
    public float interuptProbability =>_interuptProbability; 
    

    void Awake()
    {
        SetInstance();
    }

    void SetInstance(){
        if (instance == null) instance = this;
        else Destroy(this);
    }


    void Start()
    {
        Clear();
        PickNewAssignment();
    }

    public void InputNumber(string number)
    {
        if(_lastInputOperator)
        {
            ClearDisplay();
            _lastInputOperator=false;
        }

        if(_usingResult && !_writingNum2)
        {
            Clear();
        }

        if(_writingNum1)
        {   
            _numString += number;
            ToggleOperators(true);
        }
        else
        {
            _numString+=number;
            ToggleOperators(true);
        }

        if(number==".")
        {
            ToggleOperators(false);
        }

        UpdateDisplay(_numString);
    }

    void UpdateDisplay(string textDisplayed)
    {
        _displayText.text = textDisplayed;
    }

    void ClearDisplay()
    {
        _numString=_numStringIni;
        _displayText.text = "";
    }

    public void Clear()
    {
        _num1=_num1Ini;
        _num2=_num2Ini;
        _writingNum1=_writingNum1Ini;
        _writingNum2=_writingNum2Ini;
        _usingResult=_usingResultIni;
        _numString=_numStringIni;
        _result=_resultIni;
        _operation=_operationIni;
        _lastInputOperator = _lastInputOperatorIni;
        EndStreak();
        _boutonDecimal.Enable();
        ToggleOperators(false);
        ClearDisplay();
    }

    public void OperatorInput(Bouton.InputType operation)
    {
        ToggleOperators(false);
        _boutonDecimal.Enable();
        _operation = operation;
        _lastInputOperator=true;
        UpdateDisplay(""+_operation);

        if(_writingNum2) Equalize();
        else if(_writingNum1) _num1 = float.Parse(_numString);

        _writingNum1=false;
        _writingNum2=true;
    }

    public void Equalize()
    {
        if(_writingNum2) _num2 = float.Parse(_numString);

        if(_usingResult) _num1=_result;
        else _num2 = float.Parse(_numString);

        _writingNum2=false;

        if (_operation == Bouton.InputType.Add) _result=_num1 + _num2;
        else if (_operation == Bouton.InputType.Substract) _result=_num1 - _num2;
        else if (_operation == Bouton.InputType.Multiply) _result=_num1 * _num2;
        else if (_operation == Bouton.InputType.Divide) _result =_num1 / _num2;
        else _result = _num2;

        _numString = "" + _result;
        UpdateDisplay(_numString);
        ProcessResult();

        _data.IncrementStat(_data.nbCalculations);

        _boutonDecimal.Enable();
        _usingResult=true;
    }

    void ToggleOperators(bool state)
    {
        if(state==true) for (int i = 0; i < _operators.Length; i++) _operators[i].Enable();
        else for (int i = 0; i < _operators.Length; i++) _operators[i].Disable();
    }
    
    IEnumerator DisableEnableInputs(float time)
    {
        Clear();

        _events.disableAllInputs.Invoke();
        
        yield return new WaitForSeconds(time);

        _events.enableAllInputs.Invoke();

        Clear();
    }
    public IEnumerator PlayCutscene(Dialog dialog)
    {
        yield return new WaitForSeconds(dialog.delay);

        float totalDialogTime = dialog.deliveryTime*dialog.lines.Length;
        StartCoroutine(DisableEnableInputs(totalDialogTime));

        for (int i = 0; i < dialog.lines.Length; i++)
        {
            UpdateDisplay(dialog.lines[i]);
            yield return new WaitForSeconds(dialog.deliveryTime);
        }

        ClearDisplay();
    }

    void ProcessResult()
    {
        if(_result==_assignedNumber)
        {
           CompleteAssignment();
        }
        else
        {
            Debug.Log("result not assigned number");
            EndStreak();
        }

        if(CheckForNumberEvents(_result)) TriggerNumberEvent(_result);
    }

    void CompleteAssignment()
    {
        _data.IncrementStat(_data.nbSolvedAssignments);
        _data.AugmentStat(_data.nbCaliCoins, _correctEquationReward);
        _data.IncrementStat(_data.workplaceReputation);
        PickNewAssignment();
        _streak++;
        Debug.Log("streak="+_streak);
        _data.CheckRank();
    }

    void EndStreak()
    {
        Debug.Log("endstreakstart");
        if(_streak>_data.longestStreak.currentValue)
        {
            Debug.Log("streak is bigger than longest");
            _data.SetStat(_data.longestStreak, _streak);
            _data.AugmentStat(_data.workplaceReputation, _streak);
            _data.CheckRank();
            _data.AugmentStat(_data.nbCaliCoins, _streak);
        }
        else{
            Debug.Log("streak"+_streak+" is not bigger than hs: "+_data.longestStreak.currentValue);
        }
        ResetStreak();
    }
    void ResetStreak()
    {
        Debug.Log("streak reset");
        _streak = _streakIni;
    }
    void PickNewAssignment()
    {
        _assignedNumber = Random.Range(0,_assignedNumberLimit);
        if(CheckForNumberEvents(_assignedNumber))
        {
            PickNewAssignment();
        }
        else
        {
            Debug.Log(_assignedNumber);
        }
    }

    bool CheckForNumberEvents(float number)
    {
        for (int i = 0; i < _events.numberEvents.Length; i++)
        {
            if(number==_events.numberEvents[i].number)
            {
                return true;
            }
        }
        return false;
    }

    void TriggerNumberEvent(float number)
    {
        for (int i = 0; i < _events.numberEvents.Length; i++)
        {
            if(number==_events.numberEvents[i].number)
            {
                _events.numberEvents[i].relatedEvent.Invoke();
            }
        }
    }
}


[Serializable]
public class NumberEvent
{
    public float number;
    public UnityEvent relatedEvent;
}
