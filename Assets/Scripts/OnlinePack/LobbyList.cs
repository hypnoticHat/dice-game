using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;



public class LobbyList : MonoBehaviour
{
    private bool isRefeshing;
    private bool isJoining;


    private void OnEnable()
    {
        RefreshList();
    }

    private async void RefreshList()
    {
        if (isRefeshing)
        {
            return;
        }
        isRefeshing = true;
        try 
        { 
            var option = new QueryLobbiesOptions();
            option.Count = 25;

            option.Filters = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0"),
                new QueryFilter(
                    field: QueryFilter.FieldOptions.IsLocked,
                    op: QueryFilter.OpOptions.EQ,
                    value: "0"),
            };

            var lobbies = await Lobbies.Instance.QueryLobbiesAsync(option);
        }
        catch { }
        isRefeshing = false;
    }

    public async void JoinAsync(Lobby lobby)
    {
        if (isJoining) { return; }
        isJoining = true;

        try 
        {
            var joiningLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            string joinCode = joiningLobby.Data["joinCode"].Value;
        }
        catch { }

        isJoining = false;
    }
}
