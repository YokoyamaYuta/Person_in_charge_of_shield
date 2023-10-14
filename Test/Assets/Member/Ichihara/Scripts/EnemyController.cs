using Cysharp.Threading.Tasks;
using System;
using System.Threading;
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
    #region Refarences
    // 宇宙海賊が発射する弾
    [SerializeField]
    private GameObject _shell = null;
    // このスクリプトがアタッチされているオブジェクト
    private GameObject _obj = null;
    #endregion
    #region Speeds
    [SerializeField, Range(0.0f, 100.0f)]
    private float _enemySpeed = 0.0f;
    [SerializeField, Range(0.0f, 500.0f)]
    private float _debriSpeed = 0.0f;
    #endregion
    #region EnemyType
    // オブジェクトのエネミーの種類
    private EnemyType _enemyType = EnemyType.None;
    public EnemyType EnemyType => _enemyType;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _obj = GameObject.Find(name);
        if (tag == "Enemy") { _enemyType = EnemyType.Enamy; }
        else if (tag == "Debri") { _enemyType = EnemyType.Debri; }

        switch (_enemyType)
        {
            case EnemyType.Enamy:
                //gameObject.transform.Translate(Vector3.up * _enemySpeed * Time.deltaTime);
                break;
            case EnemyType.Debri:
                DebriBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// エネミーの挙動
    /// </summary>
    private async UniTask EnemyBehaviour(CancellationToken token = default)
    {
        bool destroy = false;
        while (destroy == false)
        {
            await UniTask.WaitForSeconds(5, cancellationToken: token);
            // TODO:弾を発射する処理を記述する
        }
    }

    /// <summary>
    /// デブリ類の挙動
    /// </summary>
    private async UniTask DebriBehaviour(CancellationToken token = default)
    {
        if (transform.position.y > 0.0f) { Destroy(gameObject); }
        gameObject.transform.Translate(Vector3.left * _debriSpeed * Time.deltaTime);
        await UniTask.Yield(cancellationToken:token);
    }
}
