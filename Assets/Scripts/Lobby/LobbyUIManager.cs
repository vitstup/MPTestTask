using UnityEngine;
using TMPro;

public class LobbyUIManager : Lobby
{
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TMP_InputField roomNameInput;

    private string playerName;
    private string roomName;

    private void Start()
    {
        SetBasePlayerName();
        SetBaseRoomName();
    }

    private void SetBasePlayerName()
    {
        playerNameInput.text = "Player" + Random.Range(0, 1000);
    }

    private void SetBaseRoomName()
    {
        roomNameInput.text = "Room" + Random.Range(0, 1000);
    }

    public void ChangePlayerName(string value)
    {
        playerName = value;
    }

    public void ChangeRoomName(string value)
    {
        roomName = value;
    }

    public override string GetPlayerName()
    {
        return playerName;
    }

    public override string GetRoomName()
    {
        return roomName;
    }
}