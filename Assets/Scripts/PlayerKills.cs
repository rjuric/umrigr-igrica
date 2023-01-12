using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using TMPro;

public class PlayerKills : NetworkBehaviour
{
    [SyncVar] public int kills = 0;
    private TextMeshProUGUI killsText;

    private void Start()
    {
        killsText = GameObject.FindGameObjectWithTag("KillsText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!base.IsOwner)
            return;

        killsText.text = "Kills : " + kills.ToString();
    }
}
