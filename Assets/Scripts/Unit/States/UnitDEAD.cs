using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDEAD : UnitFSMState {
	public override void BeginState() 
	{
		gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
