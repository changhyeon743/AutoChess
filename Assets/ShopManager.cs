using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public struct item
    {
        public GameObject obj;
        public UnitStat stat
        {
            get
            {
                return obj.GetComponent<UnitStat>();
            }
        }
        public Sprite image;
    }

    [SerializeField]
    public List<ShopManager.item> items = new List<ShopManager.item>();


    public ShopManager.item currentItem;
    void Awake() { Refresh(); }
    void Start()
    {

    }

    public void Refresh()
    {
        currentItem = items[Random.Range(0, items.Count)];
        GetComponentInChildren<Image>().sprite = currentItem.image;
    }

    public void Buy()
    {
        if (GameFSMManager.instance.currentState != GameState.READY) { return; }

        if (currentItem.stat.price <= GameFSMManager.instance.money)
        {
            //구매
            currentItem.stat.type = UnitType.ALLY;
            currentItem.stat.currentHp = currentItem.stat.hp;
            GameObject obj = Instantiate(currentItem.obj);
            obj.transform.position = Vector3.zero;
            obj.GetComponent<UnitFSMManager>().SetBodyColor();

            GameFSMManager.instance.money -= currentItem.stat.price;

            //--LEVEL UP
            CheckLevelup(obj);


            Refresh();
        }
        else
        {

        }
    }

    public void CheckLevelup(GameObject obj)
    {
        List<GameObject> levelupObjs = new List<GameObject>();
        foreach (GameObject o in GameFSMManager.instance.FindAllies())
        {
            if (o == obj) { continue; } //스스로 무시하고
            UnitStat o_stat = o.GetComponent<UnitStat>();
            if (o_stat.name == currentItem.stat.name && o_stat.level == currentItem.stat.level)
            {
                levelupObjs.Add(o);
            }
        }
        if (levelupObjs.Count >= 2)
        {
            foreach (GameObject o in levelupObjs)
            {
                Destroy(o.GetComponent<UnitStat>().hpBar.gameObject);
                Destroy(o.gameObject);// o.GetComponent<UnitFSMManager>().SetState(UnitState.DEAD);
            }
            obj.GetComponent<UnitStat>().level++;
        } else {
            return;
        }
    }

    public void mouseEnter()
    {
        currentItem.stat.transform.position = this.transform.position;
        GameFSMManager.instance.ui.statManager.Shop_Goto(currentItem.stat);

    }

    public void mouseExit()
    {
        GameFSMManager.instance.ui.statManager.Exit();

    }
}
