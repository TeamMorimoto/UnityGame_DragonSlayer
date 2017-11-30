﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleManager : MonoBehaviour
{
    [SerializeField]
    List<Character> characterList;

    private void Awake()
    {
        bool flag = false;
        if (characterList.Count != 2) flag = true;

        if (!flag)
        {
            foreach (Character ch in characterList)
            {
                if (ch == null) flag = true;
            }
        }

        if(flag)
        {
           this.enabled = false;
        }
    }

    private void Start()
    {
        characterList[0].ActionSequencer.AddEventOnChangeMode(OnChangeEventCharacter0);
        characterList[1].ActionSequencer.AddEventOnChangeMode(OnChangeEventCharacter1);
    }

    private void Update()
    {
        
    }

    void OnChangeEventCharacter0(ActionSequencer.Mode newMode, ActionParamater ap)
    {
        OnActionSequencerChangeMode(0, newMode, ap);
    }

    void OnChangeEventCharacter1(ActionSequencer.Mode newMode, ActionParamater ap)
    {
        OnActionSequencerChangeMode(1, newMode, ap);
    }

    void OnActionSequencerChangeMode(int characterNum,ActionSequencer.Mode newMode, ActionParamater ap)
    {
      

        Character me = characterList[characterNum];

        int otherCharacterNum = (characterNum == 0) ? 1 : 0;
        Character other = characterList[otherCharacterNum];

        if (newMode == ActionSequencer.Mode.MAIN)
        {
       

            if (ap.Type == ActionParamater.TYPE.ATTACK)
            {
                bool AttackHitFlag = true;

                ActionSequencer otherCharacterActSeq = other.ActionSequencer;

                if (otherCharacterActSeq != null)
                {
                  
                    if (otherCharacterActSeq.Active)
                    {

                        if (otherCharacterActSeq.CurrentMode == ActionSequencer.Mode.MAIN)
                        {
                            ActionParamater otherActParam = otherCharacterActSeq.CurrentActionParamater;

                            if (otherActParam != null)
                            {
                               switch(otherActParam.Type)
                               {
                                    case ActionParamater.TYPE.DODGE:
                                        AttackHitFlag = false;
                                        break;
                                    case ActionParamater.TYPE.GUARD:
                                        AttackHitFlag = false;
                                        break;
                                    default:                                        
                                        break; 
                               }
                            }
                        }
                    }
                }

                if(AttackHitFlag)
                {
                    other.OnAttacked(me);
                }

            }
        }
    }
}
