using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMOVE : UnitFSMState
{
    List<GameObject> gameObjects;


    

    public override void BeginState()
    {
       

        gameObjects = new List<GameObject>();
        if (gameObject.tag.Equals("Enemy"))
        {
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        }
        else
        {
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Unit"));
        }
        
        gameObjects.Remove(this.gameObject);

        StartCoroutine(SearchTarget());
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.target != null)
        {
            print(Vector3.Distance(this.transform.position, manager.target.transform.position));
            if (Vector3.Distance(this.transform.position, manager.target.transform.position) <= 2.5f)
            {
				
                manager.SetState(UnitState.ATTACK);
            }
            else
            {
                
                SRUtil.SRMove(manager.cc, this.transform, manager.target.transform.position, manager.moveSpeed, manager.rotateSpeed, manager.fallSpeed);
            }

        }
    }

   



    IEnumerator SearchTarget()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.2f);

            float closestDist = Mathf.Infinity;
            GameObject closest = null;

            foreach (GameObject i in gameObjects)
            {

                Vector3 objectPos = i.transform.position;
                float dist = (objectPos - transform.position).sqrMagnitude;

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = i;
                }
            }
            manager.target = closest;
        }

    }
}
