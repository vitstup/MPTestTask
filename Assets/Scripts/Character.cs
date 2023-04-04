using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IMovable, IShootable, IDieble, ICanCollect, IPunObservable, IOnEventCallback
{
    public class CharacterEvent : UnityEvent<Character> { }
    public static CharacterEvent CharacterDiedEvent = new CharacterEvent();

    private RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
    private SendOptions sendOptions = new SendOptions() { Reliability = true };

    private PhotonView view;
    private CharacterCanvasScript canvas;

    private string bullet = "Bullet";

    private float _health = 1f;
    private int _points = 0;
    
    public float health { get => _health; set => _health = value; }
    public int points { get => _points; set => _points = value; }

    private Vector2 direction;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        canvas = GetComponentInChildren<CharacterCanvasScript>();

        canvas.SetCharacterName(view.Owner.NickName);
    }

    public bool isMine()
    {
        return view.IsMine;
    }

    public void Move(Vector3 direction)
    {
        this.direction = direction;
        transform.position += direction;
        LookAt2D.Look(transform, transform.position + direction);
        canvas.transform.localRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z * -1);
    }
    public void Shoot()
    {
        PhotonNetwork.Instantiate(bullet, transform.position, Quaternion.identity, 0 , new object[] {direction});
    }

    public void TakeDamage(float damage)
    {
        PhotonNetwork.RaiseEvent(100, new Vector2(damage * - 1f, view.Owner.ActorNumber), options, sendOptions);
    }

    private void ChangeHealth(float healthAdded)
    {
        health += healthAdded;
        if (health <= 0) Die();
    }

    public void Die()
    {
        CharacterDiedEvent?.Invoke(this);
        if (view.IsMine && PhotonNetwork.IsConnected) PhotonNetwork.Destroy(view);
    }

    public void Collected(ICollectable collectable)
    {
        points += collectable.points;
        PhotonNetwork.RaiseEvent(100, new Vector2(collectable.health, view.Owner.ActorNumber), options, sendOptions);
        if (health > 1f) health = 1f;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && view.IsMine)
        {
            stream.SendNext(direction);
        }

        if (stream.IsReading && !view.IsMine)
        {
            direction = (Vector2)stream.ReceiveNext();
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 100:
                Vector2 data = (Vector2)photonEvent.CustomData;
                if ((int)data.y == view.Owner.ActorNumber) ChangeHealth(data.x);
                canvas.SetHp(health);
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}