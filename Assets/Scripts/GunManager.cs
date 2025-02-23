using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    private int soundIndex = 6;
    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gun.shoot = true;
        UpdateGun(eventData.position);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (gun.shoot) UpdateGun(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gun.shoot = false;
        SoundManager7.instance.StopSound(soundIndex);
    }

    private void UpdateGun(Vector3 pos)
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(pos);
        spawnPos.z = 0;
        gun.transform.position = spawnPos;

        if (!SoundManager7.instance.IsPlaying(soundIndex))
            SoundManager7.instance.PlaySound(soundIndex);
    }
}
