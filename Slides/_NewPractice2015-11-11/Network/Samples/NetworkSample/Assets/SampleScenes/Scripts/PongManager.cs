using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;


namespace UnityStandardAssets.Network
{
    //Manage the game, act as a controller from the server to synchronize the players.
    public class PongManager : NetworkBehaviour
    {
        static public PongManager instance { get { return _instance; } }
        static protected PongManager _instance;

        public PongBall ball;

        [SyncVar]
        public bool GameRunning;

        static public PongPaddle[] Players = new PongPaddle[2];
        
        public Text[] PlayerScoreText;
        public Text[] PlayerNameText;
        public PongScoreZone[] ScoreZones;
        public PongEndPanel EndPanel;

        public void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            //init any player pre added (happen on server hosted by client, were other player can connect before the scene was loaded)
            for (int i = 0; i < PongPaddle.sPaddles.Count; ++i)
            {
                if (PongPaddle.sPaddles[i] == null)
                    continue;

                ScoreZones[PongPaddle.sPaddles[i].number].linkedPlayer = PongPaddle.sPaddles[i];

                //thid is done in the OnStartLocalClient of the paddle too, but in some case, paddle is created before the scene
                //so there is no PongManager instance. Hence we need to do it here too to be sure that thing are setup properly
                PlayerNameText[i].text = PongPaddle.sPaddles[i].playerName;
            }
        }

        public void CheckReady()
        {
            if (GameRunning)
                return;

            bool allReady = true;
            foreach (PongPaddle p in Players)
            {
                if (p == null)
                    continue;

                allReady &= p.isReadyToPlay;
            }

            if (allReady)
            {
                GameRunning = true;

                ball.isFrozen = false;
                RpcToggleBall(true);
                ball.ResetBall(0);

                foreach (PongPaddle p in Players)
                {
                    if (p == null)
                        continue;

                    p.score = 0;
                    p.GetComponent<SimpleController>().enabled = true;
                    p.RpcStartGame();
                }
            }
        }

        public const int winningScore = 5;
        public void CheckScores()
        {
            PongPaddle winningPaddle = null;
            foreach (PongPaddle p in Players)
            {
                if (p == null)
                    continue;

                if (p.score >= winningScore)
                {
                    winningPaddle = p;
                    break;
                }
            }

            if (winningPaddle != null)
            {
                ball.ResetBall(0);
                RpcToggleBall(false);
                ball.isFrozen = true;

                GameRunning = false;

                foreach (PongPaddle p in Players)
                {
                    if (p == null)
                        continue;
                    p.isReadyToPlay = false;
                    p.GetComponent<SimpleController>().enabled = false;
                    p.RpcGameFinished( winningPaddle == p );
                }
            }
        }

        //This disable the ball on client when game is done.
        [ClientRpc]
        void RpcToggleBall(bool value)
        {
            ball.gameObject.SetActive(value);
        }

        //this is used to assign all data per instance (e.g each paddle its score text etc..)
        [Server]
        static public void AddPlayer(PongPaddle paddle)
        {
            Players[paddle.number] = paddle;
            paddle.SpawnAt(paddle.number);

            if (instance != null)
            {
                //add player can be called BEFORE the object is built (i.e. when a client is also server, another client can have
                //loaded its scene faster. So no instance exist yet. All palyer will be init in the awake fonction then
                //if that function is called after the Awake (and so when an instance exist), we still need to init the player, so better do it here.
                _instance.ScoreZones[paddle.number].linkedPlayer = paddle;
            }
        }
    }
}