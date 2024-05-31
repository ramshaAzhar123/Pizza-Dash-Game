using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomButton : MonoBehaviour
{
    public TMP_Text buttonText;

    private RoomInfo info;


    public void setButtonDetails(RoomInfo InputInfo)
    {
        info = InputInfo;

        buttonText.text = info.Name;
    }
    public void OpenRoom()
    {
        launcher.instance.JoinRoom(info);
    }
}
