using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float fireRate = 2f;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] LayerMask playerLayer;

    bool canShoot = true;
    WaitForSeconds shootWait;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
            return;

        shootWait = new WaitForSeconds(1/fireRate);
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
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, playerLayer) && hit.transform.TryGetComponent(out PlayerHealth enemyHealth))
        {
            HitPlayer(hit.transform.gameObject, enemyHealth);
        }

        StartCoroutine(CanShootUpdater());
    }

    [ServerRpc(RequireOwnership = false)]
    void HitPlayer(GameObject playerHit, PlayerHealth enemyHealth)
    {
        PlayerManager.instance.DamagePlayer(playerHit.GetInstanceID(), damage, gameObject.GetInstanceID(), enemyHealth, gameObject.GetComponent<PlayerKills>());
    }

    IEnumerator CanShootUpdater()
    {
        canShoot = false;

        yield return shootWait;

        canShoot = true;
    }
}
