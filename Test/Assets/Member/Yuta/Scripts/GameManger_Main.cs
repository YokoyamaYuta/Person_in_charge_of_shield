using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger_Main : MonoBehaviour
{
    //リスト化して、ゲームオブジェクトをまとめられる
    [SerializeField]
    List<GameObject> _Object = new List<GameObject>();

    GameObject shild_UI1;
    GameObject shild_UI2;
    GameObject shild_UI3;
    GameObject shild_UI4;
    GameObject Parcent_text;
    GameObject Score_text;
    Text parcent_text;
    Text score_text;

    public static int Shild_Count = 0;
    public static int score = 0;
    public static bool Reactive = false;
    public static bool _GameOver = false;

    private float time;
    private float enemy_delta = 0;
    private int distance;
    private int spawn_number = 0;
    private bool item;
    private bool gole;
    private bool gole_Obj;

    // Start is called before the first frame update
    void Start()
    {
        Shild_Count = 0;
        Reactive = false;
        _GameOver = false;
        time = 0;
        distance = 0;
        enemy_delta = 0;
        spawn_number = 0;
        score = 0;
        item = false;
        gole = false;
        gole_Obj = false;

        shild_UI1 = GameObject.FindGameObjectWithTag("Shild_UI1");
        shild_UI1.GetComponent<Image>().fillAmount = 0f;
        shild_UI2 = GameObject.FindGameObjectWithTag("Shild_UI2");
        shild_UI2.GetComponent<Image>().fillAmount = 0f;
        shild_UI3 = GameObject.FindGameObjectWithTag("Shild_UI3");
        shild_UI3.GetComponent<Image>().fillAmount = 0f;
        shild_UI4 = GameObject.FindGameObjectWithTag("Shild_UI4");
        shild_UI4.GetComponent<Image>().fillAmount = 0f;

        Parcent_text = GameObject.FindGameObjectWithTag("Parcent");
        parcent_text = Parcent_text.GetComponent<Text>();

        Score_text = GameObject.FindGameObjectWithTag("Score");
        score_text = Score_text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = score.ToString();

        if (!_GameOver && !gole)
        {
            //1分を想定して動かす
            time += Time.deltaTime;

            if (time > 1f && distance < 100)
            {
                time = 0f;
                item = false;
                distance++;
                parcent_text.text = (distance + "%").ToString();
            }

            if (distance == 10 && spawn_number == 0)
            {
                item = true;
                spawn_number++;
                Instantiate(_Object[5], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            if (distance > 30 && spawn_number == 1)
            {
                item = true;
                spawn_number++;
                Instantiate(_Object[5], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            if (distance > 50 && spawn_number == 2)
            {
                item = true;
                spawn_number++;
                Instantiate(_Object[5], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            if (distance > 65 && spawn_number == 3)
            {
                item = true;
                spawn_number++;
                Instantiate(_Object[6], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            if (distance > 68 && spawn_number == 4)
            {
                spawn_number++;
                Instantiate(_Object[7], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            if (distance > 84 && spawn_number == 5)
            {
                spawn_number++;
                Instantiate(_Object[7], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }

            enemy_delta += Time.deltaTime;

            if (enemy_delta > 0.5f && !item)
            {
                enemy_delta = 0f;
                DebriSpawn();
            }
        }

        if (distance == 100)
        {
            //ゴール処理を書く
            gole = true;
            StartCoroutine("Gole");
            if (gole_Obj && spawn_number == 6)
            {
                spawn_number++;
                Instantiate(_Object[8], new Vector3(11.14f, -0.77f, 0), Quaternion.identity);
                Instantiate(_Object[9], new Vector3(11.14f, -1.15f, 0), Quaternion.identity);
            }
        }
    }

    IEnumerator Gole()
    {
        yield return new WaitForSeconds(2);

        gole_Obj = true;
    }

    private void DebriSpawn()
    {
        int dice = Random.Range(1, 6);

        int dice2 = Random.Range(-4, 4);

        switch (dice)
        {
            case 1:
                Instantiate(_Object[0], new Vector3(11.14f, dice2, 0), Quaternion.Euler(0, 0, -30));
                break;
            case 2:
                Instantiate(_Object[1], new Vector3(11.14f, dice2, 0), Quaternion.Euler(0, 0, -60));
                break;
            case 3:
                Instantiate(_Object[2], new Vector3(11.14f, dice2, 0), Quaternion.Euler(0, 0, 36));
                break;
            case 4:
                Instantiate(_Object[3], new Vector3(11.14f, dice2, 0), Quaternion.Euler(0, 0, 25));
                break;
            case 5:
                Instantiate(_Object[4], new Vector3(11.14f, dice2, 0), Quaternion.identity);
                break;
        }
    }

    public void Item_UI(int number)
    {
        switch (number)
        {
            case 1:
                shild_UI1.GetComponent<Image>().fillAmount += 0.34f;
                Shild_Count++;
                break;
            case 2:
                shild_UI3.GetComponent<Image>().fillAmount = 1f;
                Reactive = true;
                break;
            case 3:
                shild_UI2.GetComponent<Image>().fillAmount += 0.34f;
                break;
            case 4:
                shild_UI4.GetComponent<Image>().fillAmount = 1f;
                Reactive = false;
                break;
            case 5:
                shild_UI4.GetComponent<Image>().fillAmount = 0f;
                Reactive = true;
                break;
        }
    }

    public void Score_Up(int number)
    {
        switch (number)
        {
            case 1:
                score += 100;
                break;
            case 2:
                score += 10;
                break;
            case 3:
                score += 300;
                break;
        }
    }

    public static int Finish_Score()
    {
        return score;
    }
}
