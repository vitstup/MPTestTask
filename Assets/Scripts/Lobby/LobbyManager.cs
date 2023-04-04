using Photon.Pun;
using System;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Lobby lobby;

    private void Start()
    {
        SetBaseInfo();
        lobby = FindObjectOfType<LobbyUIManager>();
    }

    private void SetBaseInfo()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void SetUserName()
    {
        PhotonNetwork.NickName = lobby.GetPlayerName();
    }

    public void CreateRoom()
    {
        SetUserName();
        PhotonNetwork.CreateRoom(lobby.GetRoomName(), new Photon.Realtime.RoomOptions { MaxPlayers = 4 }) ;
    }

    public void JoinRoom()
    {
        SetUserName();
        PhotonNetwork.JoinRoom(lobby.GetRoomName());
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning(String.Format("Joining failed, return code: {0}, message: {1}", returnCode, message));
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }
}
