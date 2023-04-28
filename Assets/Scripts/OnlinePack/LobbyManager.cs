using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyManager : MonoBehaviour
{

    private Lobby hostLobby;
    private Lobby joinedLobby;
    private float heartbeatTimer;
    private float lobbyUpdateTimer;
    private string PlayerName = "Me";

    //make the game didnt free while waiting for api
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("signed in" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        handleLobbyHeartBeat();
        handleLobbyPollUpdates();
    }

    private async void handleLobbyHeartBeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f)
            {
                float heartBeatTimerMax = 15; //resend data to server each 15s to keep server alive
                heartbeatTimer = heartBeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    private async void handleLobbyPollUpdates()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0f)
            {
                float lobbyUpdateTimerMax = 1.1f; //resend data to server each 15s to keep server alive
                lobbyUpdateTimer = lobbyUpdateTimerMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;
            }
        }
    }

    //crate lobby
    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int MaxPlayer = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                //remove if game have one 1 gamemode
                Data = new Dictionary<string, DataObject>
                {
                    {"GameMode",  new DataObject(DataObject.VisibilityOptions.Public, "ThisMode"/*,DataObject.IndexOptions.S1*/)}
                }

            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, MaxPlayer, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = hostLobby;

            PrintPlayer(hostLobby);

            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    //search lobby
    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter (QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT),
                    //find game mode
                    //new QueryFilter(QueryFilter.FieldOptions.S1,"ThisMode",QueryFilter.OpOptions.EQ),
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("lobies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + "." + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);
            joinedLobby = lobby;
            Debug.Log("Joined lobby with code" + lobbyCode);

            PrintPlayer(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }

    }
    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
                    {
                        {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerName) }
                    }
        };

    }

    private void PrintPlayer(Lobby lobby)
    {
        Debug.Log("Player in lobby" + lobby.Name + " " + lobby.Data["GameMode"].Value);
        foreach (Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }

    private async void UpdateLobbyGamemode(string gamemode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
            {
                {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, gamemode) }
            }
            });
            joinedLobby = hostLobby;

            PrintPlayer(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private async void UpdatePlayerName(string newPlayerName)
    {
        try
        {
            PlayerName = newPlayerName;
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerName) }
            }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private async void leaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private async void kickPlayer()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, joinedLobby.Players[1].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private async void MigrateLobbyHost()
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                HostId = joinedLobby.Players[1].Id
            });
            joinedLobby = hostLobby;

            PrintPlayer(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }

    private void DeleteLobby()
    {
        try
        {
            LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e); //incase error apear when conect to server
        }
    }
}
