using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageManager : MonoBehaviour
{
    [SerializeField]
    private int _currentStage;

    public int currentStage
    {
        get
        {
            return _currentStage;
        }
        set
        {
            _currentStage = value;
            
        }
    }

    public List<Stage> stages;

    void Start() { }

    public void CreateUnits(int index)
    {
        Stage current = stages[index];
        for (int i = 0; i < current.unit.Count; i++)
        {
            GameObject obj = Instantiate(current.unit[i]);
            obj.transform.position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            UnitStat stat = obj.GetComponent<UnitStat>();
            stat.type = UnitType.ENEMY;
            stat.tag = stat.type.ToString();
            stat.manager.SetBodyColor();
            stat.currentHp = stat.hp;
            
        }
        
    }
}
