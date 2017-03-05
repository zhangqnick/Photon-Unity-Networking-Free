﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour {

    static string playerNamePrefKey = "PlayerName";

	// Use this for initialization
	void Start () {
        string defaultName = "";
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.playerName = defaultName;
	}
	
    public void SetPlayerName(string value)
    {
        PhotonNetwork.playerName = value + " ";
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
