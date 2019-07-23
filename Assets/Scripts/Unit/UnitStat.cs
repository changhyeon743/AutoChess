using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat : MonoBehaviour
{
    public float moveSpeed;
    public float fallSpeed;
    public float rotateSpeed;
    public float attackRange;
    public float power;

    [SerializeField]
    private float _attackSpeed = 1;

    
    public float attackSpeed {
        get {
            return _attackSpeed;
        }
        set {
            _attackSpeed = value;
            manager.anim.SetFloat("AttackSpeed",value);
        }
    }

    public float hp;
    public float currentHp;

    public UnitFSMManager manager;

    public Vector3 origin;

    private void Awake()
    {
        manager = GetComponent<UnitFSMManager>();
    }

	// Use this for initialization
	void Start () {
        currentHp = hp;
        manager.anim.SetFloat("AttackSpeed",attackSpeed);
	}

    public bool ApplyDamageReturnDead(float amount)
    {
        currentHp -= amount;
        Debug.Log("[" + name + "] took damage: " + amount);
        if (currentHp <= 0)
        {
            manager.Dead();
            return true;
        }

        return false;
    }
    
}
