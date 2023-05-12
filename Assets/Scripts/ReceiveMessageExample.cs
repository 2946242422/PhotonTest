using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveMessageExample : MonoBehaviourPunCallbacks
{
    private const byte CustomEventCode = 1;
    private const byte AcceptMessageCode = 2;
    private const byte ReceiveMessageCode = 3;
    private const byte SendMessageCode = 4;

    private bool isRequestReceived = false;
    private int senderViewID;
    public Text text;
    public GameObject panel;
    private void Awake()
    {
        // 注册接收消息的回调方法
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    private void OnDestroy()
    {
        // 取消注册接收消息的回调方法
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }

    private void OnEventReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == CustomEventCode)
        {
            // 收到请求消息，获取发送者的 ViewID
            senderViewID = (int)eventData.CustomData;
            isRequestReceived = true;
            panel.gameObject.SetActive(true);
            text.text = senderViewID.ToString()+"接收到消息";
            PhotonNetwork.RaiseEvent(SendMessageCode, 1001, new Photon.Realtime.RaiseEventOptions
            {
                TargetActors = new int[] { PhotonView.Find(1001).Owner.ActorNumber }
            }, SendOptions.SendReliable);

        }
        else if (eventData.Code == AcceptMessageCode)
        {
            // 收到同意消息，执行相应操作
            int acceptedViewID = (int)eventData.CustomData;
            if (acceptedViewID == senderViewID)
            {
                // 执行同意操作
            }
        }
    }


}