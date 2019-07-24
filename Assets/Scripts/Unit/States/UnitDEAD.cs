using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDEAD : UnitFSMState {
	public override void BeginState() 
	{
        gameObject.SetActive(false);
        return;
        if (manager.stat.type == UnitType.ENEMY)
        {
            Destroy(this.manager.stat.hpBar.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            
        }
        
        
        //gameObject.SetActive(false);
	}


    void Start() { }

}
