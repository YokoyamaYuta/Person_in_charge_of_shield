using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Shell : MonoBehaviour
{
    // Shell の移動速度
    [SerializeField, Range(0.0f, 19.2f)]
    private float _shellSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySE(SEName.Shoot);
        MoveShell(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// 弾の動作
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
                transform.Translate(Vector3.up * _shellSpeed * Time.deltaTime);
            }
            catch(MissingReferenceException)
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
