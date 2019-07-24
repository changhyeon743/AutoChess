using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRUN : GameFSMState {

	public Color backgroundColor;

	public override void BeginState() {
		base.BeginState();
		foreach ( GameObject obj in manager.FindUnits()) {
			UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            unit.SetState(UnitState.READY);
            unit.stat.origin = unit.transform.position;
		}

		Camera.main.backgroundColor = backgroundColor;

	}
	void Start() {}

    void Update()
    {
        if (manager.FindEnemies().Count == 0)
        {
            //아군 승
            manager.SetState(GameState.WIN);
        }
        else if (manager.FindAllies().Count == 0)
        {
            //적 승
            manager.SetState(GameState.LOSE);
        }
    }
}
