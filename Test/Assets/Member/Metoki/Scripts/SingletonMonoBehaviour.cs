using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " �����̃V�[����ɑ��݂��܂���");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // �����̂��������m�F����
        // ���������玩�g�����
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (instance != null)
        {
            instance = this as T;
            //return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        //Destroy(this);
        return false;
    }
}
