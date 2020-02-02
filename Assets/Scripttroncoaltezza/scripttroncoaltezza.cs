using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
public class scripttroncoaltezza : MonoBehaviour
{
    public static scripttroncoaltezza instance;
    public List<GameObject>ramisez;
    public float scaling = 0.99f,scaleref=1f;
    public GameObject tronco;
    public GameObject sezione;
    public float step=0.5f,i=0.08f;
    private void Start()
    {
        instance = this;
        ramisez = new List<GameObject>();
        spawnsezioni();

    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawnsezioni();
        }
    }
    */
        public void spawnsezioni()
        {
            GameObject sez = Instantiate(sezione, new Vector2(0, step+(scaling*step)), Quaternion.identity);
            sez.transform.localScale *= scaling;
            ramisez.Add(sez);
            scaling = scaling - 0.03f;
            step += i;
            TreeController.instance.selectedSezione = sez;
        TreeController.instance.spline = sez.GetComponent<BezierSpline>();
        TreeController.instance.mc = TreeController.instance.selectedSezione.GetComponentInChildren<MeshCollider>();
        TreeController.instance.mf = sez.GetComponent<MeshFilter>();
    }

    } 
