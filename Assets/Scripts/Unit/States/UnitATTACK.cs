using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitATTACK : UnitFSMState {
	public override void BeginState() {
		
		//manager.SetState(UnitState.MOVE);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (manager.target != null)
        {
            if (Vector3.Distance(this.transform.position, manager.target.transform.position) >= 2.5f)
            {

                manager.SetState(UnitState.MOVE);
            }

        }
	}
}
