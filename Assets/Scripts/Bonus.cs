using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{


    // Update is called once per frame
    public void GetBonus()
    {
        scripttroncoaltezza.instance.spawnsezioni();
        Destroy(this.gameObject);

    }
}
