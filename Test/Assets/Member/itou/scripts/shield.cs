using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shield : MonoBehaviour
{
    private int shieldecount;
    [SerializeField]
    public List<Image> _Battery = new List<Image>();
    public int _shields;
    public bool _shieldget;
    public bool _reactiveget;
    public bool _shieldgetmax;
    [SerializeField]
    public List<Sprite> _battenumber = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        _shields = -1;
        _shieldget = false;
        _shieldgetmax = false;
        _Battery[0].color = Color.black;
        _Battery[1].color = Color.black;
        _Battery[2].color = Color.black;
        _Battery[3].color = Color.black;
        shieldecount = 1;
        _Battery[0].sprite = _battenumber[0];
        _Battery[1].sprite = _battenumber[0];
        _Battery[2].sprite = _battenumber[0];
        _Battery[3].sprite = _battenumber[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(_shields == 2)
        {
            _shieldgetmax = true;
        }
        if (_shieldget && _shields != -1)
        {
            //シールドバッテリーを見やすく
            if (_Battery[_shields].color != Color.white)
            {
                _Battery[_shields].color = Color.white;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _shieldget = false;
                    //シールドバッテリーの使用
                    if (shieldecount == 1)
                    {
                        //シールドバッテリーの見た目変更
                        _Battery[_shields].sprite = _battenumber[2];
                        //連続使用しないようのクールタイム
                        StartCoroutine(shielduse());
                        shieldecount--;
                    }
                }
                //値の初期化
                else if (shieldecount == 0)
                {
                    _shieldget = false;
                    shieldecount = 1;
                }
            }
        }
        //シールドバッテリーの所持数が最大かの確認
        if (_shieldgetmax && _shields >= 0)
        {
            //現在のシールドバッテリーの使用したどうかの確認
            if (_Battery[_shields].sprite == _battenumber[2])
            {
                //現在使用してないシールドバッテリーの計算
                _shields--;
                _shieldget = true;
            }
        }
        //アイテムの使い切りを確認後動作停止
        else if(_shields == -1)
        {
            _shieldget = false;
            _shieldgetmax = false;
        }
        if (_reactiveget)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _reactiveget = false;
                StartCoroutine(Reactive());
                _Battery[3].sprite = _battenumber[2];
            }
        }
    }

    IEnumerator shielduse()
    {
        yield return new WaitForSeconds(1);
        _shieldget = true;
    }

    IEnumerator Reactive()
    {
        yield return new WaitForSeconds(10);
        _Battery[3].sprite = _battenumber[1];
        _reactiveget = true;
    }
}
