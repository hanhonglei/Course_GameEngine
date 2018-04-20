using UnityEngine;
using System.Collections;

public class NavigationTest : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent agent;
    public Transform pos;
	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(pos.position);     
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
	
	}
}
