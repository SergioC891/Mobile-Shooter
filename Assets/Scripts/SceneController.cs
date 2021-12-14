using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay = 3.0f;
    [SerializeField] private int maxEnemiesAmount = 100;

    private GameObject _enemy;
    private float spawnTime = 0.0f;
    private bool spawnFlag = false;

    private int enemiesCount = 0;

    private int minX = -59;
    private int maxX = 39;
    private int minY = -49;
    private int maxY = 49;

    void Update()
    {
        if (!spawnFlag && enemiesCount < maxEnemiesAmount)
            {
            _enemy = Instantiate(enemyPrefab) as GameObject;

            // Spawn outside camera view
            float posX, posZ;
            bool posSelectedFlag = false;

            while (!posSelectedFlag)
            {
                posX = Random.Range(minX, maxX);
                posZ = Random.Range(minY, maxY);

                Vector3 viewPos = Camera.main.WorldToViewportPoint(new Vector3(posX, 2.0f, posZ));

                if ((viewPos.x < 0 || viewPos.x > 1.0f))
                {
                    _enemy.transform.position = new Vector3(posX, 2.0f, posZ);
                    posSelectedFlag = true;
                }                
            }

            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);

            spawnFlag = true;
            enemiesCount++;
        }

        if (spawnFlag)
        {
            if (spawnTime < spawnDelay)
            {
                spawnTime += Time.deltaTime;
            }
            else
            {
                spawnTime = 0.0f;
                spawnFlag = false;
            }
        }
    }
}
