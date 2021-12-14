using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallShot : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    public FP_Input playerInput;

    public float shootRate = 0.15F;
    
    private float delay;
    private string lastCubeTag = "";
    private Dictionary<string, Color32> cubeColors = new Dictionary<string, Color32>();

    private void Start()
    {
        cubeColors.Add("Red", Color.red);
        cubeColors.Add("Yellow", Color.yellow);
        cubeColors.Add("Green", Color.green);
    }

    void Update()
    {
        if (playerInput.Shoot())
        {
            if (Time.time > delay)
                Shoot();
        }
    }

    void Shoot()
    {
        delay = Time.time + shootRate;
        _fireball = Instantiate(fireballPrefab) as GameObject;
        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _fireball.transform.rotation = transform.rotation;

        if (lastCubeTag != "")
        {
            string color = getFireballColor();
            _fireball.GetComponent<Renderer>().material.SetColor("_Color", cubeColors[color]);
        }
    }

    public void setLastCubeTag(string tag)
    {
        lastCubeTag = tag;
    }

    private string getFireballColor()
    {
        string color = "";

        if (lastCubeTag == "RedCube")
        {
            color = "Red";
        }
        else if (lastCubeTag == "YellowCube")
        {
            color = "Yellow";
        }
        else if (lastCubeTag == "GreenCube")
        {
            color = "Green";
        }

        return color;
    }
}
