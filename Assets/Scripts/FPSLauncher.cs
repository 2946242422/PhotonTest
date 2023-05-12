using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class FPSLauncher : MonoBehaviourPunCallbacks
{
    public string playerName;
    public InputField roomName;
    public bool coonectToMaster;
    private bool joinRoom;
    public PhotonView photonView;
    public void ConnectToMaster()
    {
       photonView = GetComponent<PhotonView>();
        //������������
        PhotonNetwork.ConnectUsingSettings();
        //�汾��
        PhotonNetwork.GameVersion = "1";
    }
    public void CreateRoom()
    {
        print("cjain");
        if (!coonectToMaster || joinRoom) return;

        //�������� ������� λ����һ����������
        print(coonectToMaster);
        PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { MaxPlayers = 15 }, TypedLobby.Default);


    }
    public void JoinRoom()
    {

        if (!coonectToMaster || joinRoom)
            return;


        PhotonNetwork.JoinRoom(roomName.text);





    }
    /// <summary>
    /// ���ӳɹ��ص�
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        coonectToMaster = true;
        print(coonectToMaster);
    }
    /// <summary>
    /// ��������ص�
    /// </summary>
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("�����ɹ�");
        joinRoom = true;
    }
    /// <summary>
    ///���뷿��ص�
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("���뷿��");
        PhotonNetwork.Instantiate(playerName, Vector3.zero, Quaternion.identity);
    }
    [PunRPC]
    public void SayHello()
    {
        Debug.Log("���");
    }

    public void Test()
    {

        photonView.RPC("SayHello", RpcTarget.All);

    }

}
