using Photon.Pun;
using UI_InputSystem.Base;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Character[] playersPrefabs;

    private Character player;

    private void Start()
    {
        Vector2 pos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        PhotonNetwork.Instantiate(playersPrefabs[PhotonNetwork.LocalPlayer.ActorNumber - 1].name, pos, Quaternion.identity);

        var players = FindObjectsOfType<Character>();
        foreach (var p in players) { if (p.isMine()) player = p; }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (player == null) return;
        player.Move(PlayerMovementDirection());
    }

    private Vector3 PlayerMovementDirection()
    {
        var baseDirection = transform.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) +
                            transform.up * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);

        baseDirection *= 5f * Time.deltaTime;
        return baseDirection;
    }

    public void Shoot()
    {
        player.Shoot();
    }
}