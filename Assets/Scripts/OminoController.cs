using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OminoController : MonoBehaviour
{
    public static OminoController Instance;
    private Transform OminoStartPos, OminoPos;
    private Transform TargetPos, LeftSpawn, RightSpawn;
    public float WalkSpeed, MinDistance, Sec;
    
    public bool Return;
    private float TargetX;

    private bool flag = false;

    public float timer = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OminoStartPos = this.GetComponent<Transform>();
        OminoPos = this.GetComponent<Transform>();
        Return = false;
       
    }


    void FixedUpdate()
    {
        
           OminoArrival();
         
        if (Return)
            OminoReturn();
    }


    void OminoArrival()
    {
           
            timer += Time.deltaTime;
            if((OminoPos.position - new Vector3(TargetX, OminoPos.position.y, 0)).magnitude >= 0.01f)
            {
            float ratio = timer / Sec;
            OminoPos.position = Vector3.Lerp(OminoPos.position, new Vector3(TargetX, OminoPos.position.y, 0), ratio);
            }
     
    }                                           
                    
    void OminoReturn()
    {
        if (!flag)
        {
            timer = 0;
            flag = true;
        }
        timer = Time.deltaTime;
        if (OminoPos.position.x > 0)
        {

            TargetX = 50;

        }
        else if (OminoPos.position.x < 0)
        {
            TargetX = -50;

        }
        if ((OminoPos.position - new Vector3(TargetX, OminoPos.position.y, 0)).magnitude >= 0.01f)
        {
            float ratio = timer / Sec;
            OminoPos.position = Vector3.Lerp(OminoPos.position, new Vector3(TargetX, OminoPos.position.y, 0), ratio);
        }

        
            
            

    }
   public void SetTarget()
    { if (OminoPos == null) { OminoPos = this.GetComponent<Transform>(); }
        if (OminoPos.position.x > 0)
        {
            TargetX = MinDistance * Random.Range(1, 11);

        }
        else if (OminoPos.position.x < 0)
        {
            TargetX = -MinDistance * Random.Range(1, 11);

        }
    }
            
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
