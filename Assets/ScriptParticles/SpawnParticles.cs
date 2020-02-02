using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    public GameObject[] CondizioneAtmo;
    public Transform[] SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Atmo ()
    {
        int index = Random.Range(0, 3);
        if (index == 2)
        {

            GameObject.Instantiate(CondizioneAtmo[index], SpawnPos[Random.Range(0, 2)]);
        }
        else
        {
            GameObject.Instantiate(CondizioneAtmo[index], SpawnPos[Random.Range(0, 3)]);
        }
    }
}
