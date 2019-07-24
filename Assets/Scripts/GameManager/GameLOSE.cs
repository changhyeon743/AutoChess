﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLOSE : GameFSMState
{
    public int time = 5;
    private int timeLeft;

    public Color backgroundColor;


    public override void BeginState()
    {
        base.BeginState();

        timeLeft = time;
        DecreaseTime();

        foreach (GameObject obj in manager.FindAllies())
        {
            UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            //unit.gameObject.SetActive(true);
            unit.target = null;
            //unit.stat.currentHp = unit.stat.hp;
            unit.SetState(UnitState.IDLE);
        }

        Camera.main.backgroundColor = backgroundColor;

    }
    void Start() { }

    public void DecreaseTime()
    {
        if (timeLeft > 0)
        {
            timeLeft--;
            Invoke("DecreaseTime", 1f);
        }
        else
        { //끝
            timeLeft = time;
            manager.SetState(GameState.READY);
        }
    }
}
