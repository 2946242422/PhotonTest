using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;

public class SendMessageExample : MonoBehaviourPunCallbacks
{
    private const byte CustomEventCode = 1;
    private const byte AcceptMessageCode = 2;
    private const byte ReceiveMessageCode = 3;
    private const byte SendMessageCode = 4;
    public Text receiveTxt;
    private void Awake()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnAcceptMessageReceived;
    }

    private void Start()
    {

    }
    public void SendRequestMessage(int targetViewID)
    {
        //
        // 发送请求消息给指定客户端
        PhotonNetwork.RaiseEvent(CustomEventCode, targetViewID, new Photon.Realtime.RaiseEventOptions
        {
            TargetActors = new int[] { PhotonView.Find(targetViewID).Owner.ActorNumber }
        }, SendOptions.SendReliable);
    }



    public void OnAcceptButtonClicked(int senderViewID)
    {
        // 同意按钮被点击，发送同意消息给发送请求的客户端
        PhotonNetwork.RaiseEvent(AcceptMessageCode, senderViewID, new Photon.Realtime.RaiseEventOptions
        {
            TargetActors = new int[] { PhotonView.Find(senderViewID).Owner.ActorNumber }
        }, SendOptions.SendReliable);

    }

    public void OnDeclineButtonClicked()
    {
        // 拒绝按钮被点击，不执行任何操作
    }
    [PunRPC]

    private void OnAcceptMessageReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == SendMessageCode)
        {
            receiveTxt.text = "收到消息"+eventData.CustomData.ToString();
            Debug.Log("收到消息" + eventData.CustomData.ToString());
        }
        // 收到同意消息的回调，执行相应操作
    }


}
