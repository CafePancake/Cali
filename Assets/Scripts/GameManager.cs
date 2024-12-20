using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] SOSauvegarde _sauvegarde;
    [SerializeField] SOData _data;
    [SerializeField] SODialogs _dialogs;
    [SerializeField] SOEvents _events;
    [SerializeField] SOSounds _soundBank;
    [SerializeField]SoundManager _soundManager;
    [SerializeField] GameObject _casinoModule;
    public static GameManager instance;
    Board _board;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI _coinsText;
    [SerializeField] TextMeshProUGUI _rankText;
    float _coinsDisplayLImit = 999999999999;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        _sauvegarde.LireFichier();
        _data.CreateDataArray();
        _events.CreateNumberEventArray();
        _events.updateUI.AddListener(UpdateUI);
        _events.intro.AddListener(PlayIntro);
        _events.casino.AddListener(UnlockCasino);
        _events.sixNine.AddListener(RespondToSixNine);
        _events.boobs.AddListener(RespondToBoobs);
    }
    void Start()
    {
        _board = Board.instance;
        _soundManager = SoundManager.instance;

        if(_data.introFlag.currentState!=true)
        {
            _events.intro.Invoke();
            _data.introFlag.currentState=true;
        }
        SetModules();
        UpdateUI();
    }

    void SetModules()
    {
            _casinoModule.SetActive(_data.unlockedCasinoFlag.currentState);
    }

    void PlayIntro()
    {
        StartCoroutine(_board.PlayCutscene(_dialogs.intro));
    }

    void UnlockCasino()
    {
        if(!_data.unlockedCasinoFlag.currentState)
        {
            Debug.Log("You have unlocked: Casino");
            _data.SetFlag(_data.unlockedCasinoFlag, true);
            _casinoModule.SetActive(true);
            _soundManager.PlayClip(_soundBank.casinoJackpot);
            _data.AugmentStat(_data.nbCaliCoins, 5);
            StartCoroutine(_board.PlayCutscene(_dialogs.unlockCasino));
        }
    }

    void Interupt()
    {
        StartCoroutine(_board.PlayCutscene(_dialogs.interupt[Random.Range(0,_dialogs.interupt.Length)]));
        Debug.Log("dialogue interupt");
    }

    void RespondToBoobs()
    {
        if(!_data.hasSeenBoba.currentState)
        {
            if(_data.affectionMeter.currentValue>_data.boobsRequirement)
            {   
                _data.AugmentStat(_data.affectionMeter, _data.bigAffection);
                StartCoroutine(_board.PlayCutscene(_dialogs.showBobs));
                _data.SetFlag(_data.hasSeenBoba, true);
            }
            else FailFlirt();
        }
    }
    void RespondToSixNine()
    {
        if(!_data.hadSixNineEvent.currentState)
        {
            if(_data.affectionMeter.currentValue>_data.sixNineRequirement)
            {
               _data.AugmentStat(_data.affectionMeter, _data.EnornousAffection);
                StartCoroutine(_board.PlayCutscene(_dialogs.sixNine));
                _data.SetFlag(_data.hadSixNineEvent, true); 
            }
            else FailFlirt();
        }
    }

    void FailFlirt()
    {
        _data.ReduceStat(_data.affectionMeter, _data.EnornousAffection);
        StartCoroutine(_board.PlayCutscene(_dialogs.failedFlirt[Random.Range(0,_dialogs.failedFlirt.Length)]));
        _data.IncrementStat(_data.nbComplaints);
    }

    void UpdateUI()
    {
        //MAKE ARRAY OF ALL UI ELEMENTS ????
        _coinsText.text=""+Mathf.Clamp(_data.nbCaliCoins.currentValue, -_coinsDisplayLImit, _coinsDisplayLImit);
        _rankText.text=_data.currentRank.rankTitle;

        //UPDATE REST OF UI (FUTURE)
    }

    void OnDestroy()
    {
        _sauvegarde.EcrireFichier();
    }
    void OnApplicationQuit()
    {
        _sauvegarde.EcrireFichier();
    }
}
