using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveCell : MonoBehaviour
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
        this.transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーに当たった際自身を消してプレイヤーにシールドバッテリー付与
        if (other.gameObject.CompareTag("Player"))
        {
            _shield._reactiveget = true;
            _shield._Battery[3].color = Color.white;
            _shield._Battery[3].sprite = _shield._battenumber[2];
            Destroy(this.gameObject);
        }
    }
}
