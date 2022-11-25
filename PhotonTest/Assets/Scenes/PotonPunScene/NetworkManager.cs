using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager _instance;//����
    public InputField nameInputField;//���������
    public InputField RoomnameInputField;//�����������
    public GameObject readyBtn;//׼����ť
    public GameObject startBtn;//��ʼ��Ϸ��ť

    public GameObject NamePanel;//�����������
    public GameObject LobbyPanel;//�������
    public GameObject RoomPanel;//�������
    public GameObject StartInitPanel;//��ʼ��ʼ�����

   
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//��ʼ�����ã����ӷ�����

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)//�ж��Ƿ��Ƿ������Ǿ���ʾ��ʼ��Ϸ��ť�����Ǿ���ʾ׼����ť
        {
            readyBtn.SetActive(false);
            startBtn.SetActive(true);
        }
        else
        {
            readyBtn.SetActive(true);
            startBtn.SetActive(false);
        }

    }
    /// <summary>
    /// �������ð�ť
    /// </summary>
    public void SetNicknameButton()
    {

        if (nameInputField.text.Length < 2)
            return;
        PhotonNetwork.NickName = nameInputField.text;//������������ϴ�������
        if (PhotonNetwork.InLobby)//�ж��Ƿ��ڴ����ڣ��ھ���ʾ�����������������
        {
            LobbyPanel.SetActive(true);
            NamePanel.SetActive(false);
            Debug.Log("�Ѿ��ڴ�����");
        }


    }
    /// <summary>
    /// ��������뷿�䰴ť
    /// </summary>
    public void joinOrCreateRoomBtn()
    {
        if (RoomnameInputField.text.Length <= 2)
        {
            return;
        }
        LobbyPanel.SetActive(false);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };//�����������4��
        PhotonNetwork.JoinOrCreateRoom(RoomnameInputField.text, roomOptions, default);
        RoomPanel.SetActive(true);

    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)//�ж��Ƿ��Ƿ������Ǿ���ʾ��ʼ��Ϸ��ť�����Ǿ���ʾ׼����ť
        {
            readyBtn.SetActive(false);
            startBtn.SetActive(true);
        }
        else
        {
            readyBtn.SetActive(true);
            startBtn.SetActive(false);
        }


    }
    /// <summary>
    /// ���ɹ����Ӹ÷�����
    /// </summary>
    public override void OnConnectedToMaster()
    {
        NamePanel.SetActive(true);//��ʾ�������
        StartInitPanel.SetActive(false);//���ؿ�ʼ��ʼ�����
        PhotonNetwork.JoinLobby();//�������
        Debug.Log("��������ɹ�");
    }
   


}
