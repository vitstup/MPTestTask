using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WaitForPlayers : MonoBehaviour
{
    [SerializeField] private GameObject waitingCanvas;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient) StartCoroutine(WaitForPlayersToJoin());
    }

    private IEnumerator WaitForPlayersToJoin()
    {
        waitingCanvas.SetActive(true);
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount >= 2);
        waitingCanvas.SetActive(false);
    }
}