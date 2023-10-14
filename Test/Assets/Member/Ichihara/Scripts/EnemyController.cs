using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの種類
/// </summary>
public enum EnemyType
{
    None,
    Enamy,
    Debri,
}

public class EnemyController : MonoBehaviour
{
    // このスクリプトがアタッチされているオブジェクト
    private GameObject _obj = null;
    // オブジェクトのエネミーの種類
    private EnemyType Type = EnemyType.None;

    // Start is called before the first frame update
    void Start()
    {
        _obj = GameObject.Find(name);
        if (tag == "Enemy") { Type = EnemyType.Enamy; }
        else if (tag == "Debri") { Type = EnemyType.Debri; }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
