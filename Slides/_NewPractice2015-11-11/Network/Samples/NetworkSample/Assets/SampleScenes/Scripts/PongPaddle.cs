using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


namespace UnityStandardAssets.Network
{
    //This script handle everything related to the panel (apart movement, handled by
    //the network standard assets SimpleController)
    public class PongPaddle : NetworkBehaviour
    {
        //NOTE : Prefer to stock any list of a specific Script inside that script, as player can be created before the transition
        //from the lobbyscene to the gamescene, so no manager existing in the gamesceme will be loaded yet.
        static public List<PongPaddle> sPaddles = new List<PongPaddle>();

        //Color is synced across clients so that each client color all the panels properly
        [SyncVar]
        public Color color;

        [SyncVar]
        public string playerName;

        //this is the "player number" (1 or 2) of that panel
        [SyncVar]
        public byte number;

        //Used when a game is finished to wait for the player to get ready again or leave.
        [SyncVar]
        public bool isReadyToPlay;

        [SyncVar(hook = "OnScore")]
        public int score;

        public PongBall attachedBall = null;

        public override void OnStartClient()
        {
            //When the client start, we set that panel to the proper color
            //as color is synced, all panels will have the proper color on all clients.
            GetComponent<Renderer>().material.color = color;

            sPaddles.Add(this);

            //sometime instance isn't yet define at that point (network synchronisation) so we check its existence first
            if(PongManager.instance != null)
                PongManager.instance.PlayerNameText[number].text = playerName;
        }

        public override void OnNetworkDestroy()
        {
            sPaddles.Remove(this);
        }

        [ClientRpc]
        public void RpcGameFinished(bool winner)
        {
            GetComponent<SimpleController>().enabled = false;
            if(isLocalPlayer)
                PongManager.instance.EndPanel.Display(winner ? "YOU WON" : "YOU LOST", SetReady, ExitToLobby);
        }

        [ClientRpc]
        public void RpcStartGame()
        {
            GetComponent<SimpleController>().enabled = true;
            PongManager.instance.EndPanel.Hide();
        }

        public void ExitToLobby()
        {
            CmdExitToLobby();
        }

        [Command]
        public void CmdExitToLobby()
        {
            var lobby = NetworkManager.singleton as UnityStandardAssets.Network.LobbyManager;
            if (lobby != null)
            {
                lobby.ServerReturnToLobby();
            }
        }

        [ServerCallback]
        public void FixedUpdate()
        {
            Vector3 pos = transform.position;
            if (pos.y > 2.4f)
                pos.y = 2.4f;
            else if (pos.y < -2.4f)
                pos.y = -2.4f;

            if (attachedBall != null)
            {
                attachedBall.MoveBall(pos + (number == 0 ? Vector3.right : Vector3.left) * 0.3f);
            }

            transform.position = pos;
        }

        public void SetReady()
        {
            CmdSetReady();
        }

        public void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            float val = Input.GetAxis("Jump");

            if (val > 0.1f) 
            {
                CmdFireBall();
            }
        }

        [Command]
        public void CmdSetReady()
        {
            isReadyToPlay = true;
            PongManager.instance.CheckReady();
        }

        [Command]
        public void CmdFireBall()
        {
            if (attachedBall == null)
                return;

            attachedBall.Fire(Vector3.Normalize(new Vector3(number == 0? 1.0f : -1.0f, 1.0f, 0.0f)));
            attachedBall = null;
        }
       
        // will be called on the client when score changes on the server
        public void OnScore(int newScore)
        {
            PongManager.instance.PlayerScoreText[number].text = newScore.ToString();
            score = newScore;
        }

        public void SpawnAt(byte slot)
        {
            //ineffective, but easier here. Real world scenario would cache the spawn points instead of finding them by name.
            transform.position = GameObject.Find("Spawn" + (slot == 0 ? "Left" : "Right")).transform.position;

            number = slot;
            isReadyToPlay = true;
        }
    }
}
