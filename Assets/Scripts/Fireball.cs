using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public AudioClip shootSound;
    public string playerName = "Player";
    public string enemyName = "Enemy(Clone)";
    public string cubeName = "Cube(Clone)";
    public float speed = 10.0f;
    public int damage = 1;
    public int playerDamage = -5;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(shootSound);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == playerName)
        {
            Managers.Player.ChangeHealth(playerDamage);
        }

        if (other.name == enemyName)
        {
            other.GetComponent<WanderingAI>().ChangeHealth(damage);
        }

        if (other.name != cubeName)
        {
            Destroy(this.gameObject);
        }
    }
}
