using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public string playerName;
    public InputField roomName;
    public bool coonectToMaster;
    private bool joinRoom;
    public List<GameObject> playerlist=new List<GameObject>();
    public Text text;
    public void ConnectToMaster()
    {
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
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { MaxPlayers = 15 },TypedLobby.Default);
          
        
    }
    public void JoinRoom()
    {

        if (!coonectToMaster || joinRoom)
        return;
       
     
            PhotonNetwork.JoinRoom(roomName.text);
            
        
      
     

    }
    private void Update()
    {
        // 例如，按下空格键发送消息
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 调用发送消息的方法
            SendMessageToPlayer(roomName.text, playerlist[0] );
        }
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
        text.text = "创建成功";
        joinRoom = true;
    }
    /// <summary>
    ///加入房间回调
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("加入房间");
        text.text = "加入房间";
       //GameObject play= PhotonNetwork.Instantiate(playerName,Vector3.zero,Quaternion.identity);
       // playerlist.Add(play);
        // 将当前玩家关联到物体上
        GameObject playerObject = InstantiatePlayerObject();
        AssignPlayerToGameObject(playerObject);
    }
    private void SendMessageToPlayer(string message, GameObject player)
    {
        // 获取接收消息玩家的 PhotonView 组件
        PhotonView playerPhotonView = player.GetComponent<PhotonView>();

        // 通过RPC调用接收消息的方法，将消息发送给指定的玩家
        playerPhotonView.RPC("ReceiveMessage", playerPhotonView.Owner, message);
    }

    // 接收消息的方法，将在指定玩家上被调用
    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        // 获取发送消息的玩家的信息
        string senderName = info.Sender.NickName;

        // 在控制台输出消息
        Debug.Log("Received message from " + senderName + ": " + message);
        text.text = message;
        // 在此处执行其他操作...
    }
    // 实例化玩家对象
    private GameObject InstantiatePlayerObject()
    {
        // 在适当的位置和朝向实例化玩家对象
        return PhotonNetwork.Instantiate(playerName, Vector3.zero, Quaternion.identity, 0);
    }

    // 将玩家关联到物体上
    private void AssignPlayerToGameObject(GameObject playerObject)
    {
        // 获取当前玩家的 PhotonPlayer 对象
        //Player currentPlayer = PhotonNetwork.LocalPlayer;

        // 将当前玩家关联到物体上
        //playerObject.GetComponent<PlayerController>().SetPlayer(currentPlayer);
    }
}
