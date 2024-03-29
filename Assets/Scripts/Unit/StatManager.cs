﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public UnitStat stat;
    public Text info;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Goto(UnitStat stat)
    {
        this.gameObject.SetActive(true);
        info.text = stat.name+" "+(stat.level+1).ToString()+"성"+
                    "\n공격속도: " + stat.attackSpeed.ToString()+
                    "\n공격력: " + stat.power.ToString()+
                    "\n이동속도:"+ stat.moveSpeed.ToString();
        Vector3 pos = stat.transform.position;
        transform.position = Camera.main.WorldToScreenPoint(pos);
    }

    public void Shop_Goto(UnitStat stat)
    {
        this.gameObject.SetActive(true);
        info.text = stat.name+
                    "\n공격속도: " + stat.attackSpeed.ToString()+
                    "\n공격력: " + stat.power.ToString()+
                    "\n이동속도:"+ stat.moveSpeed.ToString()+
                    "\n가격: " + stat.price.ToString();
        Vector3 pos = stat.transform.position;
        transform.position = pos;
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
