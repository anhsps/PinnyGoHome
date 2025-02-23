using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootTime = 0.05f, timeLife = 0.5f;
    private float timer;
    private Transform player;
    private Transform gunTip;
    private int soundIndex = 6;
    public bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gunTip = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Time.timeScale == 0f) SoundManager7.instance.StopSound(soundIndex);
        if (!player || GameManager7.instance.isPaused) return;

        GunHandler();
    }

    private void GunHandler()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = shootTime;
            if (shoot) Shoot();
        }
    }

    private void Shoot()
    {
        float randomY = gunTip.position.y + Random.Range(-0.3f, 0.3f);
        Vector3 randomPos = new Vector3(gunTip.position.x, randomY);
        GameObject bullet = Instantiate(bulletPrefab, randomPos, transform.rotation);
        bullet.GetComponent<Bullet>().SetDirection((player.position - transform.position).normalized);
        Destroy(bullet, timeLife);
    }
}
