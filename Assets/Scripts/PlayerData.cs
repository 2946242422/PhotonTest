using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviour, IPunObservable
{
    public string playerName;
    public int playerLevel;
    public int playerExp;
    public int playerCoins;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerName);
            stream.SendNext(playerLevel);
            stream.SendNext(playerExp);
            stream.SendNext(playerCoins);
        }
        else
        {
            playerName = (string)stream.ReceiveNext();
            playerLevel = (int)stream.ReceiveNext();
            playerExp = (int)stream.ReceiveNext();
            playerCoins = (int)stream.ReceiveNext();
        }
    }
}


