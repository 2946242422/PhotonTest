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
        //链接至服务器
        PhotonNetwork.ConnectUsingSettings();
        //版本号
        PhotonNetwork.GameVersion = "1";
    }
    public void CreateRoom()
    {
        print("cjain");
        if (!coonectToMaster || joinRoom) return;

        //创建房间 最大人数 位于哪一个大厅底下
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
    /// 连接成功回调
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        coonectToMaster = true;
        print(coonectToMaster);
    }
    /// <summary>
    /// 创建房间回调
    /// </summary>
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("创建成功");
        joinRoom = true;
    }
    /// <summary>
    ///加入房间回调
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("加入房间");
        PhotonNetwork.Instantiate(playerName, Vector3.zero, Quaternion.identity);
    }
    [PunRPC]
    public void SayHello()
    {
        Debug.Log("你好");
    }

    public void Test()
    {

        photonView.RPC("SayHello", RpcTarget.All);

    }

}
