using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun, IPunInstantiateMagicCallback, IPunObservable, IMovable
{
    private Vector3 direction;
    private const float speed = 10f;
    private const float damage = 0.25f;

    private bool alreadyHittedSomething;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Envaironment")) DestroyBullet();

            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
            if (damagable != null && !IsBulletOwner(hit.collider)) Damage(damagable);
        }

        Move(direction);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void Damage(IDamagable damagable)
    {
        if (alreadyHittedSomething) return;
        if (!PhotonNetwork.IsMasterClient) return;
        alreadyHittedSomething = true;
        Debug.Log("Damaged");
        damagable.TakeDamage(damage);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        if (GetComponent<PhotonView>().IsMine && PhotonNetwork.IsConnected) PhotonNetwork.Destroy(GetComponent<PhotonView>());
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        direction = (Vector2) info.photonView.InstantiationData[0];
        LookAt2D.Look(transform, transform.position + direction);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    private bool IsBulletOwner(Collider2D coll)
    {
        return GetComponent<PhotonView>().Owner.ActorNumber == coll.GetComponent<PhotonView>().Owner.ActorNumber;
    }
}