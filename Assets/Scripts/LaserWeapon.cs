using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public GameObject laserPrefab; // Laser prefab to shoot
    public float fireRate = 0.5f; // Time between shots
    private float nextFire = 0f; // Time when the next shot can be fired

    void Update()
    {
        // Shooting logic (left mouse button or Ctrl)
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity); // Spawn without rotation
        Laser laserScript = laser.GetComponent<Laser>();
        if (laserScript != null)
        {
            Vector2 shootDirection = transform.up; // Get correct shooting direction
            laserScript.SetDirection(shootDirection);

            // Rotate laser to match the shoot direction
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            laser.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // Adjust to match vertical laser
        }
    }

}
