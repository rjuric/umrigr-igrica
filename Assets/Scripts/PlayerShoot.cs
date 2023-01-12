using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float fireRate = 0.3f;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] LayerMask playerLayer;

    bool canShoot = true;
    WaitForSeconds shootWait;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
            return;

        shootWait = new WaitForSeconds(fireRate);
    }

    private void Update()
    {
        if (!base.IsOwner)
            return;

        //

        if (Input.GetKey(shootKey) && canShoot)
            Shoot();
    }

    void Shoot()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, playerLayer))
        {
            HitPlayer(hit.transform.gameObject);
        }

        StartCoroutine(CanShootUpdater());
    }

    [ServerRpc(RequireOwnership = false)]
    void HitPlayer(GameObject playerHit)
    {
        PlayerManager.instance.DamagePlayer(playerHit.GetInstanceID(), damage, gameObject.GetInstanceID());
    }

    IEnumerator CanShootUpdater()
    {
        canShoot = false;

        yield return shootWait;

        canShoot = true;
    }
}
