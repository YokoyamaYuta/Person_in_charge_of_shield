using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shield : MonoBehaviour
{
    private int shieldecount;
    [SerializeField]
    public Image _batte;
    public bool itemget;
    [SerializeField]
    public List<Sprite> _battenumber = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        itemget = false;
        _batte.color = Color.black;
        shieldecount = 3;
        _batte.sprite = _battenumber[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (itemget)
        {
            _batte.color = Color.white;
            if (Input.GetMouseButtonDown(0))
            {
                itemget = false;
                if (shieldecount == 3)
                {
                    _batte.sprite = _battenumber[2];
                    StartCoroutine(shielduse());
                }
                else if (shieldecount == 2)
                {
                    _batte.sprite = _battenumber[3];
                    StartCoroutine(shielduse());
                }
                else if (shieldecount == 1)
                {
                    _batte.sprite = _battenumber[4];
                    StartCoroutine(shielduse());
                }
            }
            else if (shieldecount == 0)
            {
                itemget = false;
                shieldecount = 3;
            }
        }
    }

    IEnumerator shielduse()
    {
        yield return new WaitForSeconds(1);
        itemget = true;
        shieldecount--;
    }
}
