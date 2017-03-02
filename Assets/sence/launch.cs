using UnityEngine;
using System.Collections;

public class launch : Photon.PunBehaviour{

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    public byte MaxPlayersPerRoom = 4;

    public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;

    string _gameVersion = "1";

    bool isConnecting;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    public GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    public GameObject progressLabel;

    void Awake()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;

        PhotonNetwork.autoJoinLobby = false;

        PhotonNetwork.automaticallySyncScene = true;
    }

    //// Use this for initialization
    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        isConnecting = true;

        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");

    }

    public override void OnDisconnectedFromPhoton()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        if(PhotonNetwork.room.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");
            PhotonNetwork.LoadLevel("Room for 1");
        }
    }
}
