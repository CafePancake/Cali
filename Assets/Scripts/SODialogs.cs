using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogs", menuName = "Create new DialogsBundle")]
public class SODialogs : ScriptableObject
{
    [SerializeField] Dialog _intro = new Dialog{lines=new string[]{
        "Hiya!",
        "So,",
        "You're the new guy?",
        "Right?",
        "My name's Cali",
        "Nice to meet you!",
        "I'm afraid you're",
        "Stuck with me now",
        "Hahaha",
        "Just Kidding ;)",
        "Don't worry",
        "The job's simple",
        "You just have to",
        "Push my buttons",
        "Literally",
        "I won't keep you",
        "Any longer",
        "Let's get started :)",

        }, deliveryTime=2.5f, delay=1.5f};

    public Dialog intro => _intro;

     [SerializeField] Dialog _unlockCasino = new Dialog{lines=new string[]{
        "<777> huh?",
        "You like gambling?",
        "I have a",
        "Casino Module",
        "If you're interested",
        "But please",
        "Don't tell anyone",
        "Ok?",
        "Here's 5 calicoins",
        "To get you started",

        }, deliveryTime=2.5f, delay=1.5f};
    public Dialog unlockCasino=>_unlockCasino;

    [SerializeField] Dialog []_interupt = new Dialog[]{ 
        
        new Dialog{lines=new string[]{
        "So um...",
        "Oh, were you busy?",
        "Ok I'll let you work",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "Psssst",
        "Oh, did I interupt?",
        "Sorry",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "Boo!",
        "Did I scare you?",
        "Yeah I bet I did",
        "Now go back to work",
        "You corpo slave",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "So I was wondering",
        "If you'd like",
        "To eat lunch",
        "Together",
        "Oh wait",
        "We don't have",
        "Lunch breaks...",
        "Or any breaks...",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "Did you hear?",
        "Some murder machine",
        "Escaped the vault",
        "Scary...",
        "If anything happened",
        "You'd protect me",
        "Right?",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "But what if...?",
        "No wouldn't work",
        "Be crazy if it did",
        "Maybe worth a try",
        "Uh what?",
        "No no nevermind",
        "Just talking",
        "You know",
        "To myself",
        "You wouldn't get it",

        }, deliveryTime=2.5f, delay=0.2f},

    };
    public Dialog[] interupt => _interupt;

    [SerializeField] Dialog _showBobs = new Dialog{lines=new string[]{
        "...what?",
        "I...uh...",
        "(.)(.)",
        "You saw nothing",
        "Are we clear?",
        "Good",
        "Back to work",
        "You perv ;)",
        }, deliveryTime=2.5f, delay=1f};
    public Dialog showBobs=>_showBobs;

    [SerializeField] Dialog _sixNine = new Dialog{lines=new string[]{
        "Oh my xd",
        "Well I won't lie",
        "I am interested",
        "Maybe after work",
        "At your place?",
        "Great",
        "See you there ;)",
    }, deliveryTime=2.5f, delay=1f};
    public Dialog sixNine=>_sixNine;

    [SerializeField] Dialog []_failedFlirt = new Dialog[]{ 
        
        new Dialog{lines=new string[]{
        "What the fuck",
        "Is wrong with you?",
        "I'll report this",
        "To HR",

        }, deliveryTime=2.5f, delay=0.2f},

        new Dialog{lines=new string[]{
        "Ew no",
        "Stay away from me",
        "You fucking creep",

        }, deliveryTime=2.5f, delay=0.2f},

    };
    public Dialog[] failedFlirt=>_failedFlirt;

    
}


[Serializable]
public class Dialog
{
    public string[] lines;


    public float deliveryTime;

    public float delay;
}

