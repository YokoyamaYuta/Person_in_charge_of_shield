using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager_ittei : MonoBehaviour
{
    [SerializeField]
    List<GameObject> items = new List<GameObject>();
    private int Itemsp;
    public int Shieldspmax;
    private int Itemspcount;
    private bool spcooltime;
    public bool endgame;
    // Start is called before the first frame update
    void Start()
    {
        endgame = false;
    }

    // Update is called once per frame
    void Update()
    {
        //シールドバッテリーの生成クールタイム
        if (Itemsp <= Shieldspmax && spcooltime == false)
            {
                spcooltime = true;
                StartCoroutine(Itemspcooltime());
            }
        //シールドバッテリーの生成
        if (Itemsp >= Shieldspmax && Itemspcount < 3)
            {
                Instantiate(items[0], this.transform.position, Quaternion.identity);
                Itemspcount++;
                Itemsp = 0;
            }
        //シールドバッテリーの生成停止
        else if (Itemspcount >= 3)
            {
                spcooltime = true;
            }
        //リアクティブセルの生成
        if (endgame && Itemspcount < 4)
            {
                Instantiate(items[1], this.transform.position, Quaternion.identity);
                Itemspcount++;
                Itemsp = 0;
            }
    }
    //生成の時間をランダムに
    IEnumerator Itemspcooltime()
    {
        Itemsp += 1;
        yield return new WaitForSeconds(0.1f);
        spcooltime = false;
    }
}
