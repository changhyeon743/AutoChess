using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRUN : GameFSMState {
	public int time = 15;
	private int timeLeft;

	public Color backgroundColor;

	public override void BeginState() {
		base.BeginState();
		timeLeft = time;
		Invoke("DecreaseTime",0f);

		foreach ( GameObject obj in manager.FindUnits()) {
			UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            unit.SetState(UnitState.READY);
            unit.stat.origin = unit.transform.position;
		}

		Camera.main.backgroundColor = backgroundColor;

	}
	void Start() {}
	public void DecreaseTime() {
		if (timeLeft > 0) {
			timeLeft--;
			Invoke("DecreaseTime",1f);
		} else { //끝
			manager.SetState(GameState.READY);
		}
	}
}
