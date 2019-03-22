using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTimer : MonoBehaviour
{
    public float Timer;

    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("open");
            Destroy(this);
        }
    }
}
