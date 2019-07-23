using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitFSMManager : MonoBehaviour
{
    //플레이어의 상태
    public UnitState currentState;

    //시작할 때 초기화해줄 상태
    public UnitState startState;

	public int hp;

    Dictionary<UnitState, UnitFSMState> states = new Dictionary<UnitState, UnitFSMState>();

    public static UnitFSMManager instance;

    public CharacterController cc;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;

	public GameObject target;
	private Animator anim;

    public float moveSpeed;
    public float rotateSpeed;
    public float fallSpeed;

    public Vector3 origin;

    void Awake()
    {
        instance = this;
        states.Add(UnitState.IDLE, GetComponent<UnitIDLE>());
        states.Add(UnitState.MOVE, GetComponent<UnitMOVE>());
        states.Add(UnitState.ATTACK, GetComponent<UnitATTACK>());
        states.Add(UnitState.DEAD, GetComponent<UnitDEAD>());

        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    void Start()
    {
		SetState(startState);
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
        if (GameFSMManager.instance.currentState == GameState.READY)
        {

        }
    }


    Vector3 dist;
    float posX;
    float posY;

    void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

    }

    void OnMouseDrag()
    {
        Vector3 curPos =
                  new Vector3(Input.mousePosition.x - posX,
                  Input.mousePosition.y - posY, dist.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }

    public void Punch()
    {
        target.GetComponent<UnitFSMManager>().hp--;
    }
}
