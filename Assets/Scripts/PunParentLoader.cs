using Photon.Pun;
using UnityEngine;

public class PunParentLoader : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    GameObject parent;
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        parent = PhotonView.Find((int)info.photonView.InstantiationData[0]).gameObject;
        transform.SetParent(parent.transform);
    }
}