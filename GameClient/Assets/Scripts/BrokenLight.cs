using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLight : MonoBehaviour
{
    private Light roomLight;
    public float time = 10f;

    public void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            roomLight = GetComponent<Light>();
            roomLight.intensity = 0.75f;

            Destroy(this);
        }
    }
}
