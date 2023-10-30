using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// �G�l�~�[�̎��
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
    #region �F���C�������˂���e
    // �F���C�������˂���e
    [SerializeField]
    private GameObject _shell = null;
    // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g
    private GameObject _obj = null;
    #endregion
    #region �X�s�[�h
    [SerializeField, Range(0.0f, 10.0f)]
    private float _enemySpeed = 0.0f;
    [SerializeField, Range(0.0f, 19.2f)]
    private float _debriSpeed = 0.0f;
    #endregion
    #region ���
    // �I�u�W�F�N�g�̃G�l�~�[�̎��
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
            // _shell�ɃI�u�W�F�N�g�Q�Ƃ��Ȃ��ꍇ�AResources�t�H���_�[���璼�ړǂݍ���
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
    ///EnemyBehaviour ���Ăяo��
    /// </summary>
    private void CallEnemyBehaviour()
    {
        //�g�[�N������
        EnemyBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    ///<summary>
    ///DebrisBehaviour ���Ăяo��
    /// </summary>
    private void CallDebrisBehaviour()
    {
        //�g�[�N������
        DebrisBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    ///<summary>
    ///ItemBehaviour ���Ăяo��
    /// </summary>
    private void CallItemBehaviour()
    {
        //�g�[�N������
        ItemBehaviour(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// �G�l�~�[�̋���
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
    /// �f�u���ނ̋���
    /// </summary>
    /// <param name="token">�L�����Z�������p�̃g�[�N��</param>
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
    /// �A�C�e���ނ̋���
    /// </summary>
    /// <param name="token">�L�����Z�������p�̃g�[�N��</param>
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
