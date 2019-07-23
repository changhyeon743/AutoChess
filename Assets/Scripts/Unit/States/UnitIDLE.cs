using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIDLE : UnitFSMState {
	public override void BeginState() {
		

        if (manager.stat.origin != Vector3.zero)
        {
            this.transform.position = manager.stat.origin;
        }
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
