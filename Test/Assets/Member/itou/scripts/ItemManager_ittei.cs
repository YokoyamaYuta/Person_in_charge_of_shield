using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager_ittei : MonoBehaviour
{
    [SerializeField]
    List<GameObject> items = new List<GameObject>();
    private int Itemsp;
    public int Shieldspmax;
    private int Itemspcount;
    private bool spcooltime;
    public bool endgame;
    // Start is called before the first frame update
    void Start()
    {
        endgame = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���h�o�b�e���[�̐����N�[���^�C��
        if (Itemsp <= Shieldspmax && spcooltime == false)
            {
                spcooltime = true;
                StartCoroutine(Itemspcooltime());
            }
        //�V�[���h�o�b�e���[�̐���
        if (Itemsp >= Shieldspmax && Itemspcount < 3)
            {
                Instantiate(items[0], this.transform.position, Quaternion.identity);
                Itemspcount++;
                Itemsp = 0;
            }
        //�V�[���h�o�b�e���[�̐�����~
        else if (Itemspcount >= 3)
            {
                spcooltime = true;
            }
        //���A�N�e�B�u�Z���̐���
        if (endgame && Itemspcount < 4)
            {
                Instantiate(items[1], this.transform.position, Quaternion.identity);
                Itemspcount++;
                Itemsp = 0;
            }
    }
    //�����̎��Ԃ������_����
    IEnumerator Itemspcooltime()
    {
        Itemsp += 1;
        yield return new WaitForSeconds(0.1f);
        spcooltime = false;
    }
}
