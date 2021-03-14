using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerPlanetWars : NetworkManager
{
    public Transform playerOneSpawn;
    public Transform playerTwoSpawn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? playerOneSpawn : playerTwoSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

       
    }
}
