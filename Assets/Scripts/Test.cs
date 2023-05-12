using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;
using static System.Net.Mime.MediaTypeNames;

public class Test : MonoBehaviourPunCallbacks
{
    public InputField inputField;
    public UnityEngine.UI.Text text;
    // Start is called before the first frame update
    void Start()
    {
        // 获取本地玩家
        Player localPlayer = PhotonNetwork.LocalPlayer;

        // 获取本地玩家的 ActorNumber（唯一标识符）
        int actorNumber = localPlayer.ActorNumber;

        // 获取本地玩家的昵称
        string nickname = localPlayer.NickName;
        Debug.Log("Online player1: " + actorNumber+nickname);
        text = GameObject.Find("zText").GetComponent<UnityEngine.UI.Text>();
        // 检查是否连接到了 Photon 服务器
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // 获取当前房间中的所有玩家
            Player[] players = PhotonNetwork.PlayerList;
            Debug.Log("Online player: " + players.Length);
            // 遍历玩家列表
            foreach (Player player in players)
            {
                // 获取玩家的昵称
                string playerName = player.NickName;

                // 输出玩家昵称到控制台
                Debug.Log("Online player: " + playerName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 接收消息的方法，将在所有玩家上被调用
    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        // 获取发送消息的玩家的信息
        string senderName = info.Sender.NickName;

        // 在控制台输出消息
        Debug.Log("Received message from " + senderName + ": " + message);

        // 在此处执行其他操作...
        text.text = message;
    }

}
