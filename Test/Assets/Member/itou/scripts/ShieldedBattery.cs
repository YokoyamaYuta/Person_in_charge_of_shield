using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedBattery : MonoBehaviour
{
    private GameObject _battecanvas;
    private shield _shield;
    // Start is called before the first frame update
    void Start()
    {
        _battecanvas = GameObject.Find("ItemCanvas");
        _shield = _battecanvas.GetComponent<shield>();
    }

    // Update is called once per frame
    void Update()
    {
        //自分の位置を移動
        this.transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーに当たった際自身を消してプレイヤーにシールドバッテリー付与
        if (other.gameObject.CompareTag("Player"))
        {
            //シールドバッテリーの増加
            _shield._shields++;
            //シールドバッテリーの見た目変更
            _shield._Battery[_shield._shields].sprite = _shield._battenumber[1];
            //シールドバッテリー獲得をtrueへ
            _shield._shieldget = true;
            Destroy(this.gameObject);
        }
    }
}
