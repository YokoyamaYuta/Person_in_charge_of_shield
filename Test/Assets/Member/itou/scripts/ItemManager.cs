using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject item;
    private int Itemsp;
    public int Itemspmax;
    private int Itemspcount;
    private bool spcooltime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Itemsp <= Itemspmax && spcooltime == false)
        {
            spcooltime = true;
            StartCoroutine(Itemspcooltime());
        }
        if(Itemsp >= Itemspmax && Itemspcount < 3) 
        { 
            Instantiate(item,this.transform.position,Quaternion.identity);
            Itemspcount++;
            Itemsp = 0;
        }
        else if(Itemspcount >= 3)
        {
            spcooltime = true;
        }
    }

    IEnumerator Itemspcooltime()
    {
        Itemsp += Random.Range(1, 5);
        yield return new WaitForSeconds(0.1f);
        spcooltime = false;
    }
}
