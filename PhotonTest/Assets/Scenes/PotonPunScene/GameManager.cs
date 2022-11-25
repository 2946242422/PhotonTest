using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager _insatnce;

    // Start is called before the first frame update
    void Awake()
    {
        _insatnce = this;

        int index = 0;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (var item in players)
        {
            if (item.NickName == PhotonNetwork.NickName)
            {
                index = item.ActorNumber - 1;
                //  PhotonNetwork.Instantiate("Player", m_spawns.mySpawns[index].spawnPos.position, Quaternion.identity);
                PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
                Debug.Log("userid:" + item.ActorNumber);
            }
        }
    }
}

