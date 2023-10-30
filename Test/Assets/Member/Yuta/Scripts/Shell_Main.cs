using Cysharp.Threading.Tasks;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Shell_Main : MonoBehaviour
{
    // Shell の移動速度
    [SerializeField, Range(0.0f, 19.2f)]
    private float _shellSpeed = 0.0f;

    private bool reflection;
    private float dis;

    // Start is called before the first frame update
    void Start()
    {
        MoveShell(this.GetCancellationTokenOnDestroy()).Forget();
        reflection = false;
    }

    void Update()
    {
        Vector2 posi = transform.position;
        if (posi.x <= -12)
        {
            Object_DY();
        }
        if (posi.x >= 13)
        {
            Object_DY();
            reflection = false;
        }

        if (GameManger_Main._GameOver)
        {
            _shellSpeed = 0.0f;
        }

        Vector3 targetPos = transform.position;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        dis = Vector3.Distance(targetPos, playerPos);
    }

    ///<summary>
    ///弾の動作
    /// </summary>
    /// <param name="token">キャンセル処理用のトークン</param>
    /// <returns></returns>
    private async UniTask MoveShell(CancellationToken token)
    {
        bool isCanceled = false;
        while (isCanceled == false)
        {
            try
            {
                await UniTask.WaitForSeconds(Time.deltaTime);
                if (reflection)
                {
                    transform.position += new Vector3(_shellSpeed * Time.deltaTime, _shellSpeed * Time.deltaTime, 0);
                    transform.localEulerAngles = new Vector3(0, 0, -55f);
                }
                else
                {
                    transform.position += Vector3.left * (_shellSpeed * Time.deltaTime);
                }
            }
            catch (MissingReferenceException)
            {
                isCanceled = true;
                continue;
            }
        }
    }

    /// <summary>
    /// 弾を生成する
    /// </summary>
    /// <param name="parent">生成する弾の親オブジェクト</param>
    public void GenerateShell(Transform parent)
    {
        Instantiate(gameObject, parent);
    }

    public void Object_DY()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Shild")
        {
            if (other.gameObject.tag == "Player")
            {
                Object_DY();
                GameManger_Main._GameOver = true;
            }

            if (other.gameObject.tag == "Shild")
            {
                GameObject manger = GameObject.FindGameObjectWithTag("GameManger");

                if (Player_Main.invincible)
                {
                    reflection = true;
                    manger.GetComponent<GameManger_Main>().Score_Up(3);
                }
                else
                {
                    Object_DY();

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
