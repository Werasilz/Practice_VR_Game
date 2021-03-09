using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private float shotPower = 500f;
    [SerializeField] private float ejectPower = 150f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }

        if (!bulletPrefab)
        { return; }

        GameObject cloneBullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        cloneBullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        CasingRelease();
    }

    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab)
        { return; }

        GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
        Destroy(tempCasing, destroyTimer);
    }
}
