using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float rotateDelay = 3.0f;
    [SerializeField] private GameObject cubePrefab;
    private GameObject _fireball;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public string playerName = "Player";

    public int health = 2;

    private GameObject player;

    private bool rotateDelayFlag = false;
    private float rotateTime = 0.0f;
    private Dictionary<string, Color32> cubeColors = new Dictionary<string, Color32>();

    private void Start()
    {
        player = GameObject.Find(playerName);

        cubeColors.Add("Red", Color.red);
        cubeColors.Add("Yellow", Color.yellow);
        cubeColors.Add("Green", Color.green);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        if (!rotateDelayFlag)
        {
            transform.LookAt(player.transform);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<CharacterController>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
                rotateDelayFlag = true;
            }
        }

        if (rotateDelayFlag)
        {
            if (rotateTime < rotateDelay)
            {
                rotateTime += Time.deltaTime;
            }
            else
            {
                rotateTime = 0.0f;
                rotateDelayFlag = false;
            }
        }
    }

    public void ChangeHealth(int value)
    {
        health -= value;
        if (health <= 0)
        {
            throwCube();
            Destroy(this.gameObject);
        }
    }

    public void throwCube()
    {
        GameObject _cube = Instantiate(cubePrefab) as GameObject;

        _cube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        string color = generateColor();
        _cube.GetComponent<Renderer>().material.SetColor("_Color", cubeColors[color]);
        _cube.tag = color + "Cube";
    }

    private string generateColor()
    {
        int colorN = Random.Range(1, 4);
        string color = "Red";

        if (colorN == 2)
        {
            color = "Yellow";
        }
        else if (colorN == 3)
        {
            color = "Green";
        }

        return color;
    }
}
