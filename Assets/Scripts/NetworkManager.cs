using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
   
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(String.Format("Player: {0} entered room", newPlayer.NickName));
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(String.Format("Player: {0} leaved room", otherPlayer.NickName));
    }
}