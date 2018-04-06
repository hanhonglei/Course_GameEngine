using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    public GameObject effect;
    public void HitMe(GameObject g)
    {
        switch(g.tag)
        {
            case "Bullet":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
    void OnDestroy()
    {
        Debug.Log(name + " destroyed!");
        if (effect)
            Instantiate(effect, transform.position, transform.rotation);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
