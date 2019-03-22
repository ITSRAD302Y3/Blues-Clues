using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    public float time = 10f;

    public void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            Destroy(this);
        }
    }
}
