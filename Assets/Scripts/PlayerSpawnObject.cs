using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerSpawnObject : NetworkBehaviour
{

    public GameObject objToSpawn;
    [HideInInspector] public GameObject spawnedObject;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            
        }
        else
        {
            GetComponent<PlayerSpawnObject>().enabled = false;
        }
    }

    private void Update()
    {
        if(spawnedObject == null && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnObject(objToSpawn, transform, this);
        }

        if(spawnedObject != null && Input.GetKeyDown(KeyCode.Alpha1))
        {
            DespawnObject(spawnedObject);
        }
    }

    [ServerRpc]
    public void SpawnObject(GameObject gameObject, Transform player, PlayerSpawnObject script) 
    {
        GameObject spawned = Instantiate(gameObject, player.position + player.forward, Quaternion.identity);
        ServerManager.Spawn(spawned);
        SetSpawnedObject(spawned, script);
    }

    [ObserversRpc]
    public void SetSpawnedObject(GameObject gameObject, PlayerSpawnObject script) 
    {
        script.spawnedObject = gameObject;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnObject(GameObject gameObject)
    {
        ServerManager.Despawn(gameObject);
    }
}
