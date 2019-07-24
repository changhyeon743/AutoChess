using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameREADY : GameFSMState {
	public int time = 15;
	public int timeLeft;

	public Color backgroundColor;
	public override void BeginState() {
		base.BeginState();
		timeLeft = time;
        DecreaseTime();

        foreach (GameObject obj in manager.FindEnemies())
        {
            Destroy(obj.GetComponent<UnitFSMManager>().stat.hpBar.gameObject);
            Destroy(obj);
        }

        foreach (GameObject obj in manager.units)
        {
            UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            unit.gameObject.SetActive(true);
            unit.stat.currentHp = unit.stat.hp;

            if (unit.stat.type == UnitType.ENEMY)
            {
                Destroy(unit.gameObject);
            }
        }

        manager.stageManager.currentStage++;

        manager.units = manager.FindAllies();


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
