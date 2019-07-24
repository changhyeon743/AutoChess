using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    NONE = 0,
    ALLY,
    ENEMY
}

public class UnitFSMManager : MonoBehaviour
{
    //플레이어의 상태
    public UnitState currentState;

    //시작할 때 초기화해줄 상태
    public UnitState startState;

    Dictionary<UnitState, UnitFSMState> states = new Dictionary<UnitState, UnitFSMState>();

    public static UnitFSMManager instance;

    public CharacterController cc;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;

    public GameObject target;
    public Animator anim;

    public UnitStat stat;

    public UnitType GetUnitType()
    {
        if (gameObject.CompareTag("ENEMY"))
        {
            return UnitType.ENEMY;
        }
        else if (gameObject.CompareTag("ALLY"))
        {
            return UnitType.ALLY;
        }
        return UnitType.NONE;
    }

    public Material GetMaterial(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.ENEMY:
                return Resources.Load("Materials/Female", typeof(Material)) as Material;
            case UnitType.ALLY:
                return Resources.Load("Materials/Male", typeof(Material)) as Material;
            default:
                return Resources.Load("Materials/Heavy", typeof(Material)) as Material;
        }
    }

    void Awake()
    {
        instance = this;
        states.Add(UnitState.IDLE, GetComponent<UnitIDLE>());
        states.Add(UnitState.MOVE, GetComponent<UnitMOVE>());
        states.Add(UnitState.ATTACK, GetComponent<UnitATTACK>());
        states.Add(UnitState.DEAD, GetComponent<UnitDEAD>());
        states.Add(UnitState.READY, GetComponent<UnitREADY>());

        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<UnitStat>();
        tag = stat.type.ToString();


    }


    void Start()
    {
        SetState(startState);
        GameFSMManager.instance.ui.MakeHPBar(stat);
        SetBodyColor();
    }

    public void SetBodyColor()
    {
        foreach (SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = GetMaterial(GetUnitType());
        }
    }

    public void SetState(UnitState newState)
    {
        foreach (UnitFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        currentState = newState;
        states[newState].enabled = true;
        states[newState].BeginState();
        anim.SetInteger("CurrentState", (int)currentState);

    }

    // Update is called once per frame
    void Update()
    {


        if (target != null)
        {
            if (target.activeSelf == false)
            {
                target = null;
            }
        }

    }


    //--DRAG
    Vector3 dist;
    Vector3 startPos;
    float posX;
    float posZ;
    float posY;
    void OnMouseDown()
    {
        if (GetUnitType() == UnitType.ENEMY) { return; }
        if (GameFSMManager.instance.currentState == GameState.RUN) { return; }

        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseOver()
    {
        GameFSMManager.instance.ui.statManager.Goto(stat);
    }

    void OnMouseExit()
    {
        GameFSMManager.instance.ui.statManager.Exit();
    }

    void OnMouseDrag()
    {
        if (GetUnitType() == UnitType.ENEMY) { return; }
        if (GameFSMManager.instance.currentState == GameState.RUN) { return; }

        float planeY = 0;
        Transform draggingObject = transform;

        Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance; // the distance from the ray origin to the ray intersection of the plane
        if (plane.Raycast(ray, out distance))
        {
            draggingObject.position = ray.GetPoint(distance); // distance along the ray
        }
    }
    //--PUNCH

    public void Punch()
    {
        if (target != null)
        {
            if (target.GetComponent<UnitStat>().ApplyDamageReturnDead(stat.power))
            { //DEAD
                //target = null;
            }
            else
            { //LIVE

            }
        }

        SetState(UnitState.READY);
    }

    public void Dead()
    {
        //Destroy(this);
        SetState(UnitState.DEAD);
    }

}
