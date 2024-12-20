using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameData", menuName = "Create New GameData")]
public class SOData : ScriptableObject
{
    [SerializeField] SOEvents _events;
    [SerializeField] SOSauvegarde _sauvegarde;

    [Header("Statistics")]
    Stat[] allStats;
    public Stat nbCalculations = new Stat(){initialValue=0, currentValue=0};
    public Stat nbCaliCoins = new Stat(){initialValue=0, currentValue=0};
    public Stat nbDialolgs = new Stat(){initialValue=0, currentValue=0};
    public Stat nbSolvedAssignments = new Stat(){initialValue=0, currentValue=0};
    public Stat nbComplaints = new Stat(){initialValue=0, currentValue=0};
    public Stat affectionMeter = new Stat(){initialValue=0, currentValue=0};
    public Stat workplaceReputation = new Stat(){initialValue=0, currentValue=0};
    public Stat longestStreak = new Stat(){initialValue=0, currentValue=0};


    [Header("Flags")]
    Flag[] allFlags;
    public Flag introFlag = new Flag(){initialState=false, currentState=false};
    public Flag unlockedCasinoFlag = new Flag(){initialState=false, currentState=false};
    public Flag isFriend = new Flag(){initialState=false, currentState=false};
    public Flag isFWB = new Flag(){initialState=false, currentState=false};
    public Flag hasSeenBoba = new Flag(){initialState=false, currentState=false};
    public Flag hadSixNineEvent = new Flag(){initialState=false, currentState=false};

    [Header("Ranks")]
    public Rank currentRank= new Rank{lvl=-1, tresHold=0, rankTitle="The New Guy"};
    public Rank[] ranks = new Rank[]{
        new Rank{lvl=-1, tresHold=0, rankTitle="The New Guy"},
        new Rank {lvl=0, tresHold=50, rankTitle="Corporate Slave"}, 
        new Rank {lvl=1, tresHold=100, rankTitle="Abused Intern"}, 
        new Rank {lvl=2, tresHold=250, rankTitle="Restless Employee"}, 
        new Rank {lvl=3, tresHold=500, rankTitle="Employee of the Month"}, 
        new Rank {lvl=4, tresHold=750, rankTitle="Assistant to the Manager"}, 
        new Rank {lvl=5, tresHold=1250, rankTitle="Branch Manager"}, 
        new Rank {lvl=6, tresHold=2500, rankTitle="Board Member"}, 
        new Rank {lvl=7, tresHold=5000, rankTitle="CEO of Evil"}, 
        new Rank {lvl=8, tresHold=10000, rankTitle="Tyranical Grand Overlord"}, 
        new Rank {lvl=9, tresHold=25000, rankTitle="The Anthichrist"}, 
        new Rank {lvl=10, tresHold=50000, rankTitle="Dark Diety"}, 
        new Rank {lvl=11, tresHold=100000, rankTitle="Sinister God of Malice"}, 
        new Rank {lvl=12, tresHold=999999, rankTitle="That Which Lurks Withing the Dark"}, 

        };

        [Header("Affection Requirements")]
        public float boobsRequirement = 500;
        public float sixNineRequirement = 690;

        [Header("Affection Modifiers")]
        public float NegligibleAffection=1; 
        public float smallAffection=5; 
        public float mediumAffection=10; 
        public float bigAffection=50;
        public float EnornousAffection=100;

    public void CreateDataArray()
    {
        allStats = new Stat[]{nbCalculations, nbCaliCoins, nbDialolgs, nbSolvedAssignments, nbComplaints, affectionMeter, workplaceReputation, longestStreak};
        allFlags = new Flag[]{introFlag, unlockedCasinoFlag, isFriend, isFWB};
    }


    public void IncrementStat(Stat stat)
    {
        stat.currentValue++;
        _events.updateUI.Invoke();
    }
    public void AugmentStat(Stat stat, float addAmount)
    {
        stat.currentValue+=addAmount;
        _events.updateUI.Invoke();
    }
    public void DecrementStat(Stat stat)
    {
        stat.currentValue--;
        _events.updateUI.Invoke();
    }
    public void ReduceStat(Stat stat, float reduceAmount)
    {
        stat.currentValue-=reduceAmount;
        _events.updateUI.Invoke();
    }
    public void SetStat(Stat stat, float newValue)
    {
        stat.currentValue = newValue;
        _events.updateUI.Invoke();
    }

    public void ResetStat(Stat stat)
    {
        stat.currentValue=stat.initialValue;
    }

    [ContextMenu ("Reset all stats")]
    public void ResetAllStats()
    {
        for (int i = 0; i < allStats.Length; i++)
        {
            ResetStat(allStats[i]);
        }
        _events.updateUI.Invoke();
    }


    public void SetFlag(Flag flag, bool newState)
    {
        flag.currentState=newState;
        _events.updateUI.Invoke();
    }

    public void ResetFlag(Flag flag)
    {
        flag.currentState=flag.initialState;
    }

    [ContextMenu ("Reset all flags")]
    public void ResetAllFlags()
    {
        for (int i = 0; i < allFlags.Length; i++)
        {
            ResetFlag(allFlags[i]);
        }

        Debug.Log("All flags reset");
        _events.updateUI.Invoke();
    }

    public void SetRank(Rank newRank)
    {
        currentRank=newRank;
    }

    [ContextMenu ("Reset rank")]
    public void resetRank()
    {
        currentRank=ranks[0];
    }
    public void CheckRank()
    {
        for (int i = currentRank.lvl+1; i < ranks.Length; i++)
        {
            if(workplaceReputation.currentValue>=ranks[i].tresHold)
            {
                SetRank(ranks[i]);
            }
            else
            {
                _events.updateUI.Invoke();
                return;
            }
        }
    }
    void OnValidate()
    {
        _sauvegarde.EcrireFichier();
    }
}

[Serializable]
public class Flag
{
    public bool currentState;
    public bool initialState;
}

[Serializable]
public class Stat
{
    public float currentValue;
    public float initialValue;
}

[Serializable]
public class Rank
{
    public int lvl;
    public int tresHold;
    public string rankTitle;
}
