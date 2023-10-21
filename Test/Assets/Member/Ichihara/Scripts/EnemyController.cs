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
    [SerializeField, Range(0.0f, 5.4f)]
    private float _enemySpeed = 0.0f;
    [SerializeField, Range(0.0f, 19.2f)]
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
        if (_enemyType == EnemyType.Enamy && _shell == null)
        {
            // _shell にオブジェクト参照がない場合、Resources フォルダから直接読み込む
            _shell = Resources.Load("Prefabs/Shell") as GameObject;
        }
        switch (_enemyType)
        {
            case EnemyType.Enamy:
                CallEnemyBehaviour();
                break;
            case EnemyType.Debri:
                CallDebrisBehaviour();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// EnemyBehaviour を呼び出す
    /// </summary>
    private void CallEnemyBehaviour()
    {
        EnemyBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// DebrisBehaviour を呼び出す
    /// </summary>
    private void CallDebrisBehaviour()
    {
        DebrisBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// エネミーの挙動
    /// </summary>
    /// <param name="token">キャンセル処理用のトークン</param>
    /// <returns></returns>
    private async UniTask EnemyBehaviour(CancellationToken token = default)
    {
        Shell shellSpript = _shell.GetComponent<Shell>();
        for (int i = 0; i < 3; i++)
        {
            await UniTask.WaitForSeconds(5, cancellationToken: token);
            shellSpript.GenerateShell(transform);
        }
    }

    /// <summary>
    /// デブリ類の挙動
    /// </summary>
    /// <param name="token">キャンセル処理用のトークン</param>
    /// <returns></returns>
    private async UniTask DebrisBehaviour(CancellationToken token = default)
    {
        bool isMovedDebri = false;
        while (isMovedDebri == false)
        {
            try
            {
                transform.Translate(Vector3.left * _debriSpeed * Time.deltaTime);
                await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: token);
            }
            catch(MissingReferenceException)
            {
                isMovedDebri = true;
                continue;
            }
        }
    }

    /// <summary>
    /// デブリ類の破壊
    /// 外部から呼び出す
    /// </summary>
    public void BreakDebris()
    {
        AudioManager.Instance.PlaySE(SEName.BreakDebris);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
