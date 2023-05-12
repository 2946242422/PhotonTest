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
        // ע�������Ϣ�Ļص�����
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    private void OnDestroy()
    {
        // ȡ��ע�������Ϣ�Ļص�����
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }

    private void OnEventReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == CustomEventCode)
        {
            // �յ�������Ϣ����ȡ�����ߵ� ViewID
            senderViewID = (int)eventData.CustomData;
            isRequestReceived = true;
            panel.gameObject.SetActive(true);
            text.text = senderViewID.ToString()+"���յ���Ϣ";
            PhotonNetwork.RaiseEvent(SendMessageCode, 1001, new Photon.Realtime.RaiseEventOptions
            {
                TargetActors = new int[] { PhotonView.Find(1001).Owner.ActorNumber }
            }, SendOptions.SendReliable);

        }
        else if (eventData.Code == AcceptMessageCode)
        {
            // �յ�ͬ����Ϣ��ִ����Ӧ����
            int acceptedViewID = (int)eventData.CustomData;
            if (acceptedViewID == senderViewID)
            {
                // ִ��ͬ�����
            }
        }
    }


}