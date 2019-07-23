using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameREADY : GameFSMState {
	public int time = 15;
	private int timeLeft;

	public Color backgroundColor;
	public override void BeginState() {
		base.BeginState();
		timeLeft = time;
		Invoke("DecreaseTime",0f);
		
        foreach (GameObject obj in manager.units)
        {
            UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
			unit.gameObject.SetActive(true);
			unit.target = null;
			unit.stat.currentHp = unit.stat.hp;
            unit.SetState(UnitState.IDLE);
            
        }
		manager.units = manager.FindUnits();
		Camera.main.backgroundColor = backgroundColor;
	}

	void Start() {}

	public void DecreaseTime() {
		if (timeLeft > 0) {
			timeLeft--;
			Invoke("DecreaseTime",1f);
		} else { //끝
			timeLeft = time;
			manager.SetState(GameState.RUN);
		}
	}
}
