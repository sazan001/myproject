using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField, Tooltip("�����v\n��������ۂ̃f�[�^")]
    private GameObject haiObj;

    [SerializeField, Tooltip("���Ƃ̎�v�̐e�I�u�W�F�N�g\n�v�̐����ʒu")]
    private Transform zityaTehaiParent;
    [SerializeField, Tooltip("���Ƃ̎�v�̐e�I�u�W�F�N�g\n�v�̐����ʒu")]
    private Transform simotyaTehaiParent;
    [SerializeField, Tooltip("�Ζʂ̎�v�̐e�I�u�W�F�N�g\n�v�̐����ʒu")]
    private Transform toimenTehaiParent;
    [SerializeField, Tooltip("��Ƃ̎�v�̐e�I�u�W�F�N�g\n�v�̐����ʒu")]
    private Transform kamiTehaiParent;

    void Start()
    {
        //��v�I�u�W�F�N�g�̐����B
        for (int i = 0; i < 13; i++)
        {
            GameObject generateHai = Instantiate(haiObj, new Vector3(0, 0, 0), Quaternion.identity, zityaTehaiParent);
            RectTransform rectTrn = generateHai.GetComponent<RectTransform>();
            //3D�ɂ��Ȃ���z��-9000�Ƃ��ɂȂ�B�s�K�v�����C�����B
            rectTrn.anchoredPosition3D = new Vector3(rectTrn.sizeDelta.x, 0, 0) * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
