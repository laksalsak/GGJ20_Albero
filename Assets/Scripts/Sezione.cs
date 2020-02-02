using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sezione : MonoBehaviour
{
    public GameObject[] attachs;
    public int index=0;
    public Material red, grey;
    // Start is called before the first frame update
    void Start()
    {
        Highlight(0);
    }

    public GameObject GetAttach()
    {
        return attachs[index];
    }
    // Update is called once per frame
    void Update()
    {
        if (TreeController.instance.selectedSezione == null) return;
        if (TreeController.instance.selectedSezione== this.gameObject)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                index = (index + 1) % 4;
                Highlight(index);
            }
          
        }
    }

    void Highlight(int index)
    {
        for (int i=0; i< attachs.Length; i++)
        {
            if (i==index)
                attachs[i].GetComponent<MeshRenderer>().material= red;
            else
               attachs[i].GetComponent<MeshRenderer>().material = grey;
        }
    }
}
