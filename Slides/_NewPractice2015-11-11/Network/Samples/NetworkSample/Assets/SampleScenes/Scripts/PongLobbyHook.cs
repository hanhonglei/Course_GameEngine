using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Network
{
    public class PongLobbyHook : LobbyHook
    {
        public override void OnLobbyServerSceneLoadedForPlayer(UnityEngine.Networking.NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            LobbyPlayer l = lobbyPlayer.GetComponent<LobbyPlayer>();

            PongPaddle paddle = gamePlayer.GetComponent<PongPaddle>();
            paddle.number = l.slot;
            paddle.color = l.playerColor;
            paddle.playerName = l.playerName;

            PongManager.AddPlayer(paddle);
        }
    }
}