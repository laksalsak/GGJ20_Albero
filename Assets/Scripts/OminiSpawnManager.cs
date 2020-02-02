using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OminiSpawnManager : MonoBehaviour
{
    public GameObject Omino;
    public Transform OminiLeftSpawnTransform, OminiRightSpawnTransform;
    public int  SpawnDelay;
    private int NOmini, BestieIndex;
    private bool CoroutinePlaying;
    public GameObject[] Bestie;

    void Start()
    {
    CoroutinePlaying=false;
        //Bestie = new GameObject[8];   

    }


void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !CoroutinePlaying)
        {
            NOmini = Random.Range(1, 10);
            StartCoroutine(SpawnOmini(NOmini));
        }
    }

    IEnumerator SpawnOmini(int N)
    {
        CoroutinePlaying = true; 
        for (int i = 0; i <= N; i++)
        {
            Transform SpawnPos;
            int r = Random.Range(0, 2);
            switch (r)
                {
                case 0: SpawnPos = OminiLeftSpawnTransform;
                    break;
                default : SpawnPos = OminiRightSpawnTransform;
                    break;
                
                }
            BestieIndex = Random.Range(0, Bestie.Length);
            Instantiate(Bestie[BestieIndex], SpawnPos.position, SpawnPos.rotation);
            OminoController.Instance.SetTarget();
            yield return new WaitForSeconds(SpawnDelay);
        }
        CoroutinePlaying = false;
        yield return null;
       
    }

 
}


        
            
        
        

        

    

