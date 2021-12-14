using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip collectSound;
    public string playerName = "Player";
    public string weaponName = "WeaponExample";
    
    private void Start()
    {
        GameObject player = GameObject.Find(playerName);
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == playerName)
        {
            Managers.Inventory.AddItem(this.gameObject.tag);
            audioSource.PlayOneShot(collectSound);

            GameObject weapon = GameObject.Find(weaponName);
            weapon.GetComponent<FireBallShot>().setLastCubeTag(this.gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
