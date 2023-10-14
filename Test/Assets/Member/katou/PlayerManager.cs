using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _shield;


    public Slider slider;

    private void Start()
    {
        float Maxshield = 1f;
        float MinShield = 0f;

        slider.maxValue = Maxshield;
        slider.minValue = MinShield;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("aaa");private void Start()
            if (slider.value > 0)
            {
                slider.value = Mathf.Lerp(slider.value, 0, 0.01f);
                _shield.SetActive(true);
                Debug.Log(slider.value);


                if (slider.value < 0.001f)
                {
                    _shield.SetActive(false);
                    slider.value = 0;

                }
            }
        }
        else
        {
            slider.value++;
            _shield.SetActive(false);
        }
    }
}
