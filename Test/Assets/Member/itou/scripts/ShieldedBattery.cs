using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedBattery : MonoBehaviour
{
    GameObject _battecanvas;
    shield _shield;
    // Start is called before the first frame update
    void Start()
    {
        _battecanvas = GameObject.Find("ItemCanvas");
        _shield = _battecanvas.GetComponent<shield>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           _shield._batte.sprite = _shield._battenumber[1];
            _shield.itemget = true;
            Destroy(this.gameObject);
        }
    }
}
