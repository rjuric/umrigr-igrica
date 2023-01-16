using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnClientScript : MonoBehaviour
{

    private NetworkManager _networkManager;
    private LocalConnectionState _clientState = LocalConnectionState.Stopped;
    // Start is called before the first frame update
    void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
            return;

        if (_clientState != LocalConnectionState.Stopped)
            _networkManager.ClientManager.StopConnection();
        else
            _networkManager.ClientManager.StartConnection();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
