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
        // ����������Ϣ��ָ���ͻ���
        PhotonNetwork.RaiseEvent(CustomEventCode, targetViewID, new Photon.Realtime.RaiseEventOptions
        {
            TargetActors = new int[] { PhotonView.Find(targetViewID).Owner.ActorNumber }
        }, SendOptions.SendReliable);
    }



    public void OnAcceptButtonClicked(int senderViewID)
    {
        // ͬ�ⰴť�����������ͬ����Ϣ����������Ŀͻ���
        PhotonNetwork.RaiseEvent(AcceptMessageCode, senderViewID, new Photon.Realtime.RaiseEventOptions
        {
            TargetActors = new int[] { PhotonView.Find(senderViewID).Owner.ActorNumber }
        }, SendOptions.SendReliable);

    }

    public void OnDeclineButtonClicked()
    {
        // �ܾ���ť���������ִ���κβ���
    }
    [PunRPC]

    private void OnAcceptMessageReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == SendMessageCode)
        {
            receiveTxt.text = "�յ���Ϣ"+eventData.CustomData.ToString();
            Debug.Log("�յ���Ϣ" + eventData.CustomData.ToString());
        }
        // �յ�ͬ����Ϣ�Ļص���ִ����Ӧ����
    }


}
