using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
public class TreeController : MonoBehaviour
{
    public static TreeController instance;

    public GameObject selectedSezione;
    public int indexSelected = -1;
    // Start is called before the first frame update
    public BezierSpline spline;


    public MeshCollider mc;
    public MeshFilter mf;
    public Vector3 dir;
    public float step=0.5f;

    public LayerMask bonus;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (spline != null)
        {
            float direzione = Input.GetAxis("Vertical");

            //dir = new Vector3(1,direzione, 0);
            Vector3 fwd = (spline[spline.Count - 1].position - spline[spline.Count - 2].position).normalized;
            dir = Quaternion.Euler(0, 0, 40 * direzione) * fwd;
            if (Input.GetButtonDown("Fire1"))
            {

                    NewBranchPiece();
            }
          


        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
                    Sezione sez = selectedSezione.GetComponent<Sezione>();
                    Debug.Log(sez);
                    spline = sez.GetAttach().GetComponentInChildren<BezierSpline>();
                    mc = selectedSezione.GetComponentInChildren<MeshCollider>();
                    mf = mc.gameObject.GetComponentInChildren<MeshFilter>();
                    // selectedSezione.GetComponent<Sezione>()
                
            }
        }
      
    }

    void NewBranchPiece()
    {
        spline.InsertNewPointAt(spline.Count);
        Vector3 target = spline[spline.Count - 2].position + dir * step;

        spline[spline.Count - 1].position = target ;

        Collider[] collider = Physics.OverlapSphere(target, 0.5f, bonus);
        Debug.Log(collider.Length);
        if (collider.Length>0)
        {
            Bonus bon = collider[0].gameObject.GetComponentInChildren<Bonus>();
            if (bon != null)
                bon.GetBonus();
        }
        spline.ConstructLinearPath();
        spline.Refresh();
        mc.sharedMesh = mf.sharedMesh;

    }
}
