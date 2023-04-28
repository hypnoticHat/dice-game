using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;


public class LobbyItems : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private TMP_Text lobbyPlayerText;
    private LobbyList lobbyList;
    private Lobby lobby; 

    public void Initialise(LobbyList lobbyList, Lobby lobby)
    {
        this.lobbyList = lobbyList;
        this.lobby = lobby;

        lobbyNameText.text = lobby.Name;
        lobbyPlayerText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}"; 
    }

    public void Join()
    {
        lobbyList.JoinAsync(lobby);
    }
}
