using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    // 服务器端进行子弹和其他物体的碰撞检测及其反应
    void OnTriggerEnter(Collider other)
    {
        if(!isServer)
            return;
        if (other.gameObject.tag != "Player")
            return;
        other.gameObject.GetComponent<Control>().RpcDamage();
        NetworkServer.Destroy(gameObject);
    } 
}
