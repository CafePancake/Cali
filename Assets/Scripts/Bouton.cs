using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class Bouton : MonoBehaviour
{
    [SerializeField] string value;
    [SerializeField] InputType _inputType;
    [SerializeField] SOEvents _events;
    [SerializeField] AudioClip _pressSound;

    Board _board;
    SoundManager _soundManager;
    Button _button;
    
    void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _events.disableAllInputs.AddListener(Disable);
        _events.enableAllInputs.AddListener(Enable);
    }

    void Start()
    {
        _board = Board.instance;
        _soundManager = SoundManager.instance;
    }
    public void Input()
    {
        if(Random.Range(0f,100f)<_board.interuptProbability) _events.interupt.Invoke();

        _soundManager.PlayClip(_pressSound);

        if(_inputType==InputType.Number)
        {
            _board.InputNumber(value);
        }
        else if(_inputType==InputType.Add||_inputType==InputType.Substract||_inputType==InputType.Multiply||_inputType==InputType.Divide)
        {
            _board.OperatorInput(_inputType);
        }
        else if(_inputType==InputType.Equalize)
        {
            _board.Equalize();
        }
        else if(_inputType==InputType.Clear)
        {
            _board.Clear();
        }
    }
    public void Disable()
    {
        _button.interactable = false;
    }
    public void Enable()
    {
        _button.interactable = true;
    }
    public enum InputType{Number,Add,Substract,Multiply,Divide,Equalize,Clear,Random, None};
}
