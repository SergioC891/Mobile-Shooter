using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    private string sceneName = "MFPCTest";

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        health = 100;
        maxHealth = 100;

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
            SceneManager.LoadScene(sceneName);
        }

        Debug.Log("Health: " + health + "/" + maxHealth);
    }

    public int getHealth()
    {
        return health;
    }
}
