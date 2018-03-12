using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Manager : NetworkManager {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // called when a new player is added for a client
    override public void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // 随机位置和颜色创建联网的玩家角色
        var player = (GameObject)GameObject.Instantiate(playerPrefab, startPositions[0].position, Quaternion.identity);
        player.GetComponent<Control>().color = new Color(Random.value, 0, 0);
        player.GetComponent<Control>().pos = startPositions[0].position + Vector3.right * Random.value * 4;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
