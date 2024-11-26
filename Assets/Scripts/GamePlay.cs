using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField, Tooltip("麻雀牌\n生成する際のデータ")]
    private GameObject haiObj;

    [SerializeField, Tooltip("自家の手牌の親オブジェクト\n牌の生成位置")]
    private Transform zityaTehaiParent;
    [SerializeField, Tooltip("下家の手牌の親オブジェクト\n牌の生成位置")]
    private Transform simotyaTehaiParent;
    [SerializeField, Tooltip("対面の手牌の親オブジェクト\n牌の生成位置")]
    private Transform toimenTehaiParent;
    [SerializeField, Tooltip("上家の手牌の親オブジェクト\n牌の生成位置")]
    private Transform kamiTehaiParent;

    void Start()
    {
        //手牌オブジェクトの生成。
        for (int i = 0; i < 13; i++)
        {
            GameObject generateHai = Instantiate(haiObj, new Vector3(0, 0, 0), Quaternion.identity, zityaTehaiParent);
            RectTransform rectTrn = generateHai.GetComponent<RectTransform>();
            //3Dにしないとzが-9000とかになる。不必要だが気持ち。
            rectTrn.anchoredPosition3D = new Vector3(rectTrn.sizeDelta.x, 0, 0) * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
