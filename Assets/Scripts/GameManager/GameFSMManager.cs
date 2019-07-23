using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSMManager : MonoBehaviour {
	//플레이어의 상태
    public GameState currentState;

    //시작할 때 초기화해줄 상태
    public GameState startState;

	Dictionary<GameState,GameFSMState> states = new Dictionary<GameState,GameFSMState>();

	public static GameFSMManager instance;

    public List<GameObject> units;

	void Awake() {
		instance = this;
        states.Add(GameState.READY, GetComponent<GameREADY>());
        states.Add(GameState.RUN, GetComponent<GameRUN>());
	}

	void Start () {
        units = FindUnits();
		SetState(startState);

	}
    
    

	public void SetState(GameState newState)
    {
        foreach (GameFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        currentState = newState;
        states[newState].enabled = true;
        states[newState].BeginState();
        //anim.SetInteger("CurrentState", (int)currentState);
        
    }

	// Update is called once per frame
	void Update () {
		
	}

    public List<GameObject> FindUnits() {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        return gameObjects;
    }
}
