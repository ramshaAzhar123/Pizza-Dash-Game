using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class launcher : MonoBehaviourPunCallbacks
{
    public static launcher instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject loadingScreen;
    public TMP_Text loadingText;

    public GameObject menuButtons;

    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;

    public GameObject roomScreen;
    public TMP_Text roomNameText, PLayerNameLabel;
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton theRoomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    public GameObject nameInputScreem;
    public TMP_InputField nameInput;
    private bool hasSetNick;

    public string levelToPlay;
    public GameObject startButton;

    public GameObject TestRoomButton;

    void Start()
    {
        CloseMenues();

        loadingScreen.SetActive(true);
        loadingText.text = "Connecting To Network....";
        SetAllTextToWhite(); // Set all text components to white

        PhotonNetwork.ConnectUsingSettings();

#if UNITY_EDITOR
        TestRoomButton.SetActive(true);
#endif 
    }

    void CloseMenues()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreem.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;

        loadingText.text = "Joining Lobby....";
    }

    public override void OnJoinedLobby()
    {
        CloseMenues();
        menuButtons.SetActive(true);

        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();
        if (!hasSetNick)
        {
            CloseMenues();
            nameInputScreem.SetActive(true);

            if (PlayerPrefs.HasKey("playerName"))
            {
                nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }

    public void OpenRoomCreate()
    {
        CloseMenues();
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;

            PhotonNetwork.CreateRoom(roomNameInput.text, options);

            CloseMenues();
            loadingText.text = "Creating Room....";
            loadingScreen.SetActive(true);
        }
    }

    public override void OnJoinedRoom()
    {
        CloseMenues();
        roomScreen.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    private void ListAllPlayers()
    {
        foreach (TMP_Text player in allPlayerNames)
        {
            Destroy(player.gameObject);
        }
        allPlayerNames.Clear();

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            TMP_Text newPLayerLabel = Instantiate(PLayerNameLabel, PLayerNameLabel.transform.parent);
            newPLayerLabel.text = players[i].NickName;
            newPLayerLabel.gameObject.SetActive(true);

            allPlayerNames.Add(newPLayerLabel);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        TMP_Text newPLayerLabel = Instantiate(PLayerNameLabel, PLayerNameLabel.transform.parent);
        newPLayerLabel.text = newPlayer.NickName;
        newPLayerLabel.gameObject.SetActive(true);

        allPlayerNames.Add(newPLayerLabel);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Failed to Create Room: " + message;
        CloseMenues();
        errorScreen.SetActive(true);
    }

    public void CloseErrorScreen()
    {
        CloseMenues();
        menuButtons.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseMenues();
        loadingText.text = "Leaving Room";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        CloseMenues();
        menuButtons.SetActive(true);
    }

    public void OpenRoomBrowser()
    {
        CloseMenues();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        CloseMenues();
        menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }
        allRoomButtons.Clear();

        theRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
            {
                RoomButton newButton = Instantiate(theRoomButton, theRoomButton.transform.parent);
                newButton.setButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);

                allRoomButtons.Add(newButton);
            }
        }
    }

    public void JoinRoom(RoomInfo inputinfo)
    {
        PhotonNetwork.JoinRoom(inputinfo.Name);

        CloseMenues();
        loadingText.text = "Joining Room";
        loadingScreen.SetActive(true);
    }

    public void SetNickName()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;

            CloseMenues();
            menuButtons.SetActive(true);

            hasSetNick = true;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void QuickJoin()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;

        PhotonNetwork.CreateRoom("Test");
        CloseMenues();
        loadingText.text = "Creating Room";
        loadingScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetAllTextToWhite()
    {
        loadingText.color = Color.white;
        roomNameText.color = Color.white;
        PLayerNameLabel.color = Color.white;
        errorText.color = Color.white;
    }
}
