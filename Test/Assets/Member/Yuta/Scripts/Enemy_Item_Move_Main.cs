using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// エネミーの種類
/// </summary>
public enum EnemyType2
{
    None,
    Enamy,
    Debri,
    Item,
}

public class Enemy_Item_Move_Main : MonoBehaviour
{
    #region 宇宙海賊が発射する弾
    // 宇宙海賊が発射する弾
    [SerializeField]
    private GameObject _shell = null;
    // このスクリプトがアタッチされているオブジェクト
    private GameObject _obj = null;
    #endregion
    #region スピード
    [SerializeField, Range(0.0f, 10.0f)]
    private float _enemySpeed = 0.0f;
    [SerializeField, Range(0.0f, 19.2f)]
    private float _debriSpeed = 0.0f;
    #endregion
    #region 種類
    // オブジェクトのエネミーの種類
    private EnemyType2 _Type = EnemyType2.None;
    public EnemyType2 EnemyType2 => _Type;
    #endregion

    private float dis;

    // Start is called before the first frame update
    void Start()
    {
        _obj = GameObject.Find(name);
        if (tag == "Enemy") { _Type = EnemyType2.Enamy; }
        else if (tag == "Debri") { _Type = EnemyType2.Debri; }
        else if (tag == "Item") { _Type = EnemyType2.Item; }
        if (_Type == EnemyType2.Enamy && _shell == null)
        {
            // _shellにオブジェクト参照がない場合、Resourcesフォルダーから直接読み込む
            _shell = Resources.Load("Prefabs/Shell") as GameObject;
        }
        switch (_Type)
        {
            case EnemyType2.Enamy:
                CallEnemyBehaviour();
                break;
            case EnemyType2.Debri:
                CallDebrisBehaviour();
                break;
            case EnemyType2.Item:
                CallItemBehaviour();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        Vector2 posi = transform.position;
        if (posi.x <= -12)
        {
            Object_DY();
        }

        if (GameManger_Main._GameOver)
        {
            _enemySpeed = 0f;
            _debriSpeed = 0f;
        }

        if (gameObject.tag == "Debri")
        {
            Vector3 targetPos = transform.position;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPos = player.transform.position;

            dis = Vector3.Distance(targetPos, playerPos);
        }
    }

    ///<summary>
    ///EnemyBehaviour を呼び出す
    /// </summary>
    private void CallEnemyBehaviour()
    {
        //トークン消去
        EnemyBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    ///<summary>
    ///DebrisBehaviour を呼び出す
    /// </summary>
    private void CallDebrisBehaviour()
    {
        //トークン消去
        DebrisBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    ///<summary>
    ///ItemBehaviour を呼び出す
    /// </summary>
    private void CallItemBehaviour()
    {
        //トークン消去
        ItemBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// エネミーの挙動
    /// </summary>
    private async UniTask EnemyBehaviour(CancellationToken token = default)
    {
        bool isMovedDebri = false;
        while (isMovedDebri == false)
        {
            try
            {
                Vector2 posi = transform.position;
                if (posi.x >= 7f)
                {
                    transform.position += Vector3.left * (_enemySpeed * Time.deltaTime);
                    await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: token);
                }
                else
                {
                    isMovedDebri = true;
                }
            }
            catch (MissingReferenceException)
            {
                isMovedDebri = true;
                continue;
            }
        }

        Shell shellSpript = _shell.GetComponent<Shell>();
        for (int i = 0; i < 3; i++)
        {
            await UniTask.WaitForSeconds(4, cancellationToken: token);
            shellSpript.GenerateShell(transform);
        }

        await UniTask.WaitForSeconds(3, cancellationToken: token);

        bool isMovedDebri2 = false;
        while (isMovedDebri2 == false)
        {
            try
            {
                Vector2 posi = transform.position;
                if (posi.y <= 8)
                {
                    transform.position += Vector3.up * (_enemySpeed * Time.deltaTime);
                    await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: token);
                }
                else
                {
                    isMovedDebri2 = true;
                }
            }
            catch (MissingReferenceException)
            {
                isMovedDebri2 = true;
                continue;
            }
        }

        Object_DY();
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
                transform.position += Vector3.left * (_debriSpeed * Time.deltaTime);
                await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: token);
            }
            catch (MissingReferenceException)
            {
                isMovedDebri = true;
                continue;
            }
        }
    }

    /// <summary>
    /// アイテム類の挙動
    /// </summary>
    /// <param name="token">キャンセル処理用のトークン</param>
    /// <returns></returns>
    private async UniTask ItemBehaviour(CancellationToken token = default)
    {
        bool isMovedDebri = false;
        while (isMovedDebri == false)
        {
            try
            {
                transform.position += Vector3.left * (_debriSpeed * Time.deltaTime);
                await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: token);
            }
            catch (MissingReferenceException)
            {
                isMovedDebri = true;
                continue;
            }
        }
    }

    private void Object_DY()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Shild")
        {
            Object_DY();

            GameObject manger = GameObject.FindGameObjectWithTag("GameManger");

            if (gameObject.name == "Gole(Clone)")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Player_Main>().GameClear();
            }

            if (other.gameObject.tag == "Player")
            {
                if (gameObject.tag == "Debri")
                {
                    GameManger_Main._GameOver = true;
                }
                if (gameObject.name == "ShildBatery(Clone)")
                {
                    manger.GetComponent<GameManger_Main>().Item_UI(1);
                    AudioManager.Instance.PlaySE(SEName.Get);
                }
                if (gameObject.name == "ReactiveBatery(Clone)")
                {
                    manger.GetComponent<GameManger_Main>().Item_UI(2);
                    AudioManager.Instance.PlaySE(SEName.Get);
                }
            }

            if (other.gameObject.tag == "Shild")
            {
                if (gameObject.tag == "Debri")
                {
                    if (dis < 5f)
                    {
                        manger.GetComponent<GameManger_Main>().Score_Up(1);
                    }
                    else
                    {
                        manger.GetComponent<GameManger_Main>().Score_Up(2);
                    }
                }
            }
        }
    }
}
