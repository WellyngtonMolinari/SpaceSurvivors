using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0f;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}