using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Main : MonoBehaviour
{
    [SerializeField] private GameObject _shield;
    public Animator P_Anima;

    GameObject Gage;

    public static bool invincible = false;

    private float shMater = 100f;
    private float shCostTime = 0f;
    private float shRecoveryTime = 0f;
    private bool shildActivation = false;
    //private bool invincible = false;
    private bool shild_Stop = false;

    // Start is called before the first frame update
    void Start()
    {
        _shield.SetActive(false);
        shMater = 100f;
        shCostTime = 0f;
        shRecoveryTime = 0f;
        shildActivation = false;
        invincible = false;
        shild_Stop = false;

        Gage = GameObject.FindGameObjectWithTag("Shild_Mater");
        Gage.GetComponent<Image>().fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManger_Main.Shild_Count >= 1)
            {
                shMater = 100f;
                Gage.GetComponent<Image>().fillAmount = 1f;
                GameManger_Main.Shild_Count--;
                GameObject manger = GameObject.FindGameObjectWithTag("GameManger");
                manger.GetComponent<GameManger_Main>().Item_UI(3);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (GameManger_Main.Reactive)
            {
                invincible = true;
                GameObject manger = GameObject.FindGameObjectWithTag("GameManger");
                manger.GetComponent<GameManger_Main>().Item_UI(4);

                StartCoroutine("Shild");
            }
        }

        if (!shild_Stop && !GameManger_Main._GameOver)
        {
            if (shMater > 0f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _shield.SetActive(true);
                    shildActivation = true;
                    AudioManager.Instance.PlaySE(SEName.Shild);
                }
            }
            else if (shMater == 0f)
            {
                _shield.SetActive(false);
                shildActivation = false;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _shield.SetActive(false);
                shildActivation = false;
                AudioManager.Instance.PlaySE(SEName.Shild);
            }
        }

        if (shildActivation)
        {
            if (!invincible)
            {
                shCostTime += Time.deltaTime;
                if (shCostTime > 0.5f)
                {
                    if (shMater > 0f)
                    {
                        shMater -= 5f;
                        Gage.GetComponent<Image>().fillAmount -= 0.05f;
                    }
                    else
                    {
                        shMater = 0f;
                        Gage.GetComponent<Image>().fillAmount = 0f;
                    }
                    shCostTime = 0f;
                }
            }
        }
        else
        {
            shRecoveryTime += Time.deltaTime;
            if (shRecoveryTime > 0.5f)
            {
                if (shMater < 100f)
                {
                    shMater += 1f;
                    Gage.GetComponent<Image>().fillAmount += 0.01f;
                }
                else
                {
                    shMater = 100f;
                    Gage.GetComponent<Image>().fillAmount = 1f;
                }
                shRecoveryTime = 0f;
            }
        }

        if (GameManger_Main._GameOver)
        {
            P_Anima.SetTrigger("GameOver");
        }
    }

    IEnumerator Shild()
    {
        yield return new WaitForSeconds(5);

        shild_Stop = true;
        _shield.SetActive(false);

        yield return new WaitForSeconds(2);

        shild_Stop = false;
        invincible = false;
        GameObject manger = GameObject.FindGameObjectWithTag("GameManger");
        manger.GetComponent<GameManger_Main>().Item_UI(5);
    }

    private void GameOver()
    {
        AudioManager.Instance.PlaySE(SEName.Bom);
    }

    private void SceneLoad1()
    {
        SceneLoader.Instance.LoadScene("GameOverscene");
    }

    public void GameClear()
    {
        P_Anima.SetTrigger("GameClear");
    }

    private void SceneLoad2()
    {
        SceneLoader.Instance.LoadScene("ClearScene");
    }
}
