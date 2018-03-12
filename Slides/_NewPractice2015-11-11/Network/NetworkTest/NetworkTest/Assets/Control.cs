using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Control : NetworkBehaviour{
    [SyncVar]
    public Color color;
    [SyncVar]
    public Vector3 pos;

    public float health = 100;

    public float speed = 2;
    private Rigidbody rb;
    public GameObject bulletPrefab;
    // 依据创建时的参数为物体中的数据赋值
    void Start() {
        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = color;
        transform.position = pos;
    }
    // 必须使用command，在服务器端调用此函数，才能通知所有客户端完成实例化
    [Command]
    void CmdDoFire(float lifeTime, Vector3 Dir)
    {
        GameObject bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + Dir.normalized,
            Quaternion.identity);

        var bulletR = bullet.GetComponent<Rigidbody>();
        bulletR.velocity = Dir.normalized * 3;
        Destroy(bullet, lifeTime);
       
        NetworkServer.Spawn(bullet);
    }
    // 进行必要的交互
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 dir = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - dir;
            CmdDoFire(3.0f, dir);
        }
        Vector3 m = new Vector3(Input.GetAxis("Horizontal") *speed,  Input.GetAxis("Vertical") * speed, 0);
        m *= Time.deltaTime;
        rb.MovePosition(transform.position + m);
    }

    // 由服务器端调用，运行于客户端，这样才能更新客户端的信息
    [ClientRpc]
    public void RpcDamage()
    {
        health -= 30;
        if(health < 0)
            CmdKillMyself();
    }

    // 服务器端负责管理物体
    [Command]
    void CmdKillMyself()
    {
        NetworkServer.Destroy(gameObject);

    }
}
