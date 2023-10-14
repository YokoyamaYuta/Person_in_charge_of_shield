using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebriSpawn : SingletonMonoBehaviour<DebriSpawn>
{
    [SerializeField]
    private GameObject _debri = null;
    [SerializeField]
    private AnimationCurve _debriSpawnCurve = null;
    [SerializeField]
    private AnimationCurve _debriTypeCurve = null;

    private float[] _debriSpawnCurveTimes = { };

    // Start is called before the first frame update
    void Start()
    {
        CreateKeys();
        _debriSpawnCurveTimes = new float[_debriSpawnCurve.keys.Length];
        for (int i = 0; i < _debriSpawnCurve.keys.Length; i++)
        {
            _debriSpawnCurveTimes[i] = _debriSpawnCurve.keys[i].time;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// AnimationCurve のキーを設定する
    /// </summary>
    private void CreateKeys()
    {
        _debriSpawnCurve.AddKey(0.0f, 0.0f);
        _debriSpawnCurve.AddKey(1.0f, 1.0f);
    }

    private async UniTask RandomSpawnDebris(CancellationToken token)
    {

    }

    private void SpawnDebris()
    {
        float random = _debriSpawnCurve.Evaluate(Random.value);
        float result = random * (Screen.height / 100.0f);
        Vector3 spawnPosition = Vector3.right * (Screen.width / 100.0f) + Vector3.up * result;
        Instantiate(_debri, spawnPosition, Quaternion.identity);
    }
}
