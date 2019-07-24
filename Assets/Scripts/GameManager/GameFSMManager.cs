﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSMManager : MonoBehaviour {
	//플레이어의 상태
    public GameState currentState;

    //시작할 때 초기화해줄 상태
    public GameState startState;

    public int money;

	Dictionary<GameState,GameFSMState> states = new Dictionary<GameState,GameFSMState>();

	public static GameFSMManager instance;

    public List<GameObject> allies;
    public List<GameObject> enemies;

    public GameUIManager ui;
    public StageManager stageManager;
    public ShopManager shop;


	void Awake() {
		instance = this;
        states.Add(GameState.READY, GetComponent<GameREADY>());
        states.Add(GameState.RUN, GetComponent<GameRUN>());
        states.Add(GameState.WIN, GetComponent<GameWIN>());
        states.Add(GameState.LOSE, GetComponent<GameLOSE>());

        ui = GetComponent<GameUIManager>();
        stageManager = GetComponentInChildren<StageManager>();

	}

	void Start () {
        allies = FindAllies();
        enemies = FindEnemies();
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
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("ALLY"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("ENEMY"));
        return gameObjects;
    }

    public List<GameObject> FindEnemies()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("ENEMY"));
        return gameObjects;
    }

    public List<GameObject> FindAllies()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("ALLY"));
        return gameObjects;
    }
}
