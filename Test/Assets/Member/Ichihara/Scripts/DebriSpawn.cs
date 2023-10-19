using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebriSpawn : SingletonMonoBehaviour<DebriSpawn>
{
    // デブリオブジェクト
    [SerializeField]
    private GameObject _debri = null;
    // デブリをスポーンする座標を決めるカーブ
    [SerializeField]
    private AnimationCurve _debriSpawnCurve = null;
    // デブリの種類を決めるカーブ
    [SerializeField]
    private AnimationCurve _debriTypeCurve = null;

    // デブリがスポーンする間隔
    [SerializeField]
    private float _spawnInterval = 0.0f;
    // デブリ
    //private float[] _debriSpawnCurveTimes = { };

    // Start is called before the first frame update
    void Start()
    {
        CreateKeys();
        CallRandomSpawnDebris();
    }

    /// <summary>
    /// _debriSpawnCurve のキーを設定する
    /// </summary>
    private void CreateKeys()
    {
        _debriSpawnCurve.AddKey(0.0f, 0.0f);
        _debriSpawnCurve.AddKey(1.0f, 1.0f);
    }

    /// <summary>
    /// RandomSpawnDebris を呼び出す
    /// </summary>
    private void CallRandomSpawnDebris()
    {
        RandomSpawnDebris(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// デブリをランダムな位置に配置する
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask RandomSpawnDebris(CancellationToken token = default)
    {
        bool isEndGame = false;
        while (isEndGame == false)
        {
            // TODO: ここにループを止める処理を記述する
            await UniTask.WaitForSeconds(_spawnInterval, cancellationToken: token);
            SpawnDebris();
        }
    }

    /// <summary>
    /// デブリを生成する
    /// </summary>
    private void SpawnDebris()
    {
        // スクリーンのサイズを Unity の座標に置き直す
        Vector2 gameViewScale = new Vector2(Screen.width, Screen.height) / 100f;
        float random = _debriSpawnCurve.Evaluate(Random.value);
        float result = random * gameViewScale.y;
        // result の値を -5.4 ~ 5.4 の間に正規化
        float normalizedResult = Mathf.Repeat(result, gameViewScale.y) - gameViewScale.y / 2;
        Vector3 dummySpawnPosition = Vector3.right * gameViewScale.x + Vector3.up * normalizedResult;
        float dummySpawnPositionY = dummySpawnPosition.y;
        // 限界値を定義
        Vector3 spawnPosition = new Vector3(dummySpawnPosition.x
                                          , Mathf.Clamp(dummySpawnPositionY
                                            , -gameViewScale.y + _debri.transform.localScale.y / 2
                                            , gameViewScale.y - _debri.transform.localScale.y / 2)
                                          , dummySpawnPosition.z);
        Instantiate(_debri, spawnPosition, Quaternion.identity);
    }
}
