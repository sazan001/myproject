using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class HaiKourituPractise : MonoBehaviour
{
    [Tooltip("手牌\n13牌")]
    private List<MahjongTile> tehaiMahjongTileList = new List<MahjongTile>();

    [SerializeField, Tooltip("手牌\n13牌")]
    private List<Image> tehaiMahjongTileImageList = new List<Image>();

    [Tooltip("ツモ牌\n17牌\n(136-14-13*4)/4=17.5")]
    private List<MahjongTile> tumoMahjongTileList = new List<MahjongTile>();

    [Tooltip("ツモ牌")]
    private MahjongTile tumoMahjongTile;

    [SerializeField, Tooltip("ツモ牌")]
    private Image tumoMahjongTileImage;

    [Tooltip("選択している牌\nおそらく捨てる牌")]
    private MahjongTile selectMahjongTile;

    [SerializeField, Tooltip("選択している牌\nおそらく捨てる牌")]
    private Image selectMahjongTileImage;

    [Tooltip("手牌\n13牌")]
    private List<MahjongTile> aiTehaiMahjongTileList = new List<MahjongTile>();


    [Tooltip("効率\n受け入れの数")]
    private List<int> kourituList = new List<int>();


    [Tooltip("計算時に使用するリスト")]
    private List<MahjongTile> calculationMahjongTileList = new List<MahjongTile>();
    [Tooltip("計算時に使用するHashSet")]
    private HashSet<MahjongTile> calculationMahjongTileHashSet = new HashSet<MahjongTile>();
    [Tooltip("計算時に使用する面子のリスト")]
    private List<List<MahjongTile>> mentuMahjongTileList = new List<List<MahjongTile>>();
    [Tooltip("計算時に使用する面子のHashSet")]
    private HashSet<List<MahjongTile>> mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();

    //翻数
    private int hansuu = 0;
    //役。
    private string yaku = "";
    //最高翻数。
    int highHansuu = 0;
    //最高翻数時の役。
    string highYaku = "";
    //最高符。
    int highHu = 0;
    //最高符内訳。
    string highHuStr = "";

    //刻子の個数。
    int kootuCount = 0;
    //順子の個数。
    int syuntuCount = 0;

    [Tooltip("計算時に使用するリスト")]
    MahjongTile head;

    [Tooltip("役満かどうか")]
    private bool yakuman = false;
    [Tooltip("役満かどうか")]
    private bool yakumanNow = false;


    [SerializeField, Tooltip("符計算をするか")]
    private bool huKeisan = true;
    [Tooltip("符計算に使用する符")]
    private int hu;
    [Tooltip("符計算の内訳")]
    private string huStr;

    [Tooltip("刻子")]
    private List<List<MahjongTile>> kootuMentu = new List<List<MahjongTile>>();
    [Tooltip("順子")]
    private List<List<MahjongTile>> syuntuMentu = new List<List<MahjongTile>>();

    private bool ryanmen = false;
    private bool syanpon = false;
    private bool pentyan = false;
    private bool kantyan = false;
    private bool tanki = false;
    private bool nobetan = false;

    private bool agari = false;

    private bool aiAgari = false;

    [SerializeField, Tooltip("tumoMahjongTileListの個数を表示")]
    private Text tumoMahjongTileListCountText;

    [SerializeField, Tooltip("終わったときに表示するオブジェクト")]
    private GameObject endObj;

    [SerializeField, Tooltip("Content_Play\nプレイした時の手牌の状況")]
    private Transform dataPlayParent;
    [SerializeField, Tooltip("Content_AI\n牌効率に従った時の手牌の状況")]
    private Transform dataAiParent;

    [SerializeField, Tooltip("上がったと表示するテキスト")]
    private GameObject agariTextObj;
    [SerializeField, Tooltip("AIが上がったと表示するテキスト")]
    private GameObject aiAgariTextObj;


    [Tooltip("全ての牌*4が入っているリスト\n決してこれに変更は加えない\nStart時に設定")]
    private List<MahjongTile> allMahjongTileList = new List<MahjongTile>();


    [Tooltip("何巡目の記録を乗せるか\n0～\n1+したのが巡な点に注意")]
    private int zyun = 0;
    [Tooltip("手牌の記録")]
    private List<List<MahjongTile>> tehaiMahjongTileDataList = new List<List<MahjongTile>>();
    [Tooltip("ツモ牌の記録")]
    private List<MahjongTile> tumoMahjongTileDataList = new List<MahjongTile>();
    [Tooltip("捨て牌の記録")]
    private List<MahjongTile> suteMahjongTileDataList = new List<MahjongTile>();
    [Tooltip("向聴数")]
    private List<int> syantenSuuDataList = new List<int>();
    [Tooltip("手牌の記録")]
    private List<List<MahjongTile>> aiTehaiMahjongTileDataList = new List<List<MahjongTile>>();
    [Tooltip("ツモ牌の記録")]
    private List<MahjongTile> aiTumoMahjongTileDataList = new List<MahjongTile>();
    [Tooltip("捨て牌の記録")]
    private List<MahjongTile> aiSuteMahjongTileDataList = new List<MahjongTile>();
    [Tooltip("向聴数")]
    private List<int> aiSyantenSuuDataList = new List<int>();

    [SerializeField, Tooltip("上がった時の点数")]
    private Text agariScore;
    [SerializeField, Tooltip("AIが上がった時の点数")]
    private Text aiAgariScore;


    [SerializeField, Tooltip("AudioSource")]
    private AudioSource audioSource;
    [SerializeField, Tooltip("ゲームクリア時の効果音")]
    private AudioClip gameClearSound;
    [SerializeField, Tooltip("クリック時の効果音")]
    private AudioClip clickSound;
    [SerializeField, Tooltip("花火の効果音")]
    private AudioClip hanabiSound;

    [SerializeField, Tooltip("花火のリスト")]
    private List<GameObject> hanabiList = new List<GameObject>();
    [SerializeField, Tooltip("花火の生成されうる座標")]
    private Vector2 hanabiPos;


    [SerializeField, Tooltip("捨てると表示するテキスト\nツモ時にツモと表示する")]
    private Text suteruText;

    [Tooltip("花火を生成したか")]
    private bool hanabi;

    void Start()
    {
        for (int i = 0; i <= 33; i++)
        {
            allMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            allMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            allMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            allMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
        }

        Reset();
    }

    void Update()
    {
        //左クリック。手持ちの牌。
        if (Input.GetMouseButtonDown(0) && !agari && tumoMahjongTileList.Count > 0)
        {
            PointerEventData pointData = new PointerEventData(EventSystem.current);

            List<RaycastResult> rayResult = new List<RaycastResult>();

            pointData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointData, rayResult);
            foreach (RaycastResult result in rayResult)
            {
                //新たに牌を設定。
                if (result.gameObject.tag == "SelectMahjongTile")
                {
                    int mahjongTileNum = DataManager.mahjongTileImages.IndexOf(result.gameObject.GetComponent<Image>().sprite);
                    selectMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), mahjongTileNum);
                    selectMahjongTileImage.sprite = DataManager.mahjongTileImages[mahjongTileNum];
                }
            }

            audioSource.PlayOneShot(clickSound);
        }
        //Shift+D。ツモ切り。
        else if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.D) && !agari && tumoMahjongTileList.Count > 0)
        {
            int count = tumoMahjongTileList.Count;
            for (int i = 0; i < count; i++)
            {
                selectMahjongTile = tumoMahjongTile;
                selectMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectMahjongTile];
                Suteru();

                if (agari)
                {
                    break;
                }
            }
        }
    }


    void TehaiMahjongTileImageListUpdate()
    {
        //enum型の番号を追加。
        List<int> tehaiMahjongTileNumList = new List<int>();
        foreach (MahjongTile tehaiMahjongTile in tehaiMahjongTileList)
        {
            tehaiMahjongTileNumList.Add((int)tehaiMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newTehaiMahjongTileList = new List<MahjongTile>();
        int count = tehaiMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = tehaiMahjongTileNumList.Min();
            int minNumNum = tehaiMahjongTileNumList.IndexOf(minNum);

            newTehaiMahjongTileList.Add(tehaiMahjongTileList[minNumNum]);
            tehaiMahjongTileList.Remove(tehaiMahjongTileList[minNumNum]);
            tehaiMahjongTileNumList.Remove(minNum);
        }

        tehaiMahjongTileList = new List<MahjongTile>(newTehaiMahjongTileList);

        foreach (Image selectMahjongTileImage in tehaiMahjongTileImageList)
        {
            selectMahjongTileImage.sprite = DataManager.noImage;
        }
        if (tehaiMahjongTileList.Count > 0)
        {
            for (int i = 0; i < tehaiMahjongTileList.Count; i++)
            {
                tehaiMahjongTileImageList[i].sprite = DataManager.mahjongTileImages[(int)tehaiMahjongTileList[i]];
            }
        }
    }

    void AiTehaiMahjongTileImageListUpdate()
    {
        //enum型の番号を追加。
        List<int> tehaiMahjongTileNumList = new List<int>();
        foreach (MahjongTile tehaiMahjongTile in aiTehaiMahjongTileList)
        {
            tehaiMahjongTileNumList.Add((int)tehaiMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newTehaiMahjongTileList = new List<MahjongTile>();
        int count = aiTehaiMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = tehaiMahjongTileNumList.Min();
            int minNumNum = tehaiMahjongTileNumList.IndexOf(minNum);

            newTehaiMahjongTileList.Add(aiTehaiMahjongTileList[minNumNum]);
            aiTehaiMahjongTileList.Remove(aiTehaiMahjongTileList[minNumNum]);
            tehaiMahjongTileNumList.Remove(minNum);
        }

        aiTehaiMahjongTileList = new List<MahjongTile>(newTehaiMahjongTileList);
    }

    void HaiKourituCalculation()
    {
        //リスト
        calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
        calculationMahjongTileList.Add(tumoMahjongTile);
        CalculationListSort();

        kourituList = new List<int>();

        //全ての牌を列挙。
        //旧
        if (0 == 1)
        {
            foreach (MahjongTile mahjongTile in calculationMahjongTileList)
            {
                bool mentu = false;
                //1
                if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        mentu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                //2
                else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        mentu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        mentu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                //9
                else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        mentu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                //8
                else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        mentu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        mentu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                //字牌
                else if ((int)mahjongTile >= 27)
                {
                    //3個以上あれば
                    if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                //その他。3～7
                else
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        mentu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        mentu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        && calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        mentu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
                    {
                        mentu = true;
                    }
                }
                bool taatu = false;
                //1
                if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                {
                    //どちらかがあれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatu = true;
                    }
                    //もしくは2個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        taatu = true;
                    }
                }
                //2
                else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        mentu = true;
                    }
                }
                //9
                else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        taatu = true;
                    }
                }
                //8
                else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        taatu = true;
                    }
                }
                //字牌
                else if ((int)mahjongTile >= 27)
                {
                    //3個以上あれば
                    if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        taatu = true;
                    }
                }
                //その他。3～7
                else
                {
                    //3個並んでいれば
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatu = true;
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                        || calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatu = true;
                    }
                    //もしくは3個以上あれば
                    else if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                    {
                        taatu = true;
                    }
                }

                if (mentu)
                {
                    Debug.Log(mahjongTile + "は面子です。");
                    kourituList.Add(0);
                }
                else if (taatu)
                {
                    Debug.Log(mahjongTile + "は塔子です。");
                    //1
                    if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                    {
                        kourituList.Add(3);
                    }
                    //2
                    else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                    {
                        kourituList.Add(2);
                    }
                    //9
                    else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                    {
                        kourituList.Add(3);
                    }
                    //8
                    else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                    {
                        kourituList.Add(2);
                    }
                    //字牌
                    else if ((int)mahjongTile >= 27)
                    {
                        kourituList.Add(4);
                    }
                    //その他。3～7
                    else
                    {
                        kourituList.Add(1);
                    }
                }
                //孤立牌
                else
                {
                    Debug.Log(mahjongTile + "は孤立牌です。");
                    //1
                    if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                    {
                        kourituList.Add(7);
                    }
                    //2
                    else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                    {
                        kourituList.Add(6);
                    }
                    //9
                    else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                    {
                        kourituList.Add(7);
                    }
                    //8
                    else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                    {
                        kourituList.Add(6);
                    }
                    //字牌
                    else if ((int)mahjongTile >= 27)
                    {
                        kourituList.Add(8);
                    }
                    //その他。3～7
                    else
                    {
                        kourituList.Add(5);
                    }
                }
            }
        }
        //旧。牌効率にしたかったが、作れなかった。牌効率もどき。
        else if (0 == 1)
        {
            //処理を行う前の牌。処理が行われた牌はRemove。
            List<MahjongTile> syoriMaeMahjongTileList = new List<MahjongTile>(calculationMahjongTileList);
            //分類をすでにしている牌。処理が行われた牌はここに入らない。
            List<MahjongTile> bunruiEndMahjongTileList = new List<MahjongTile>();
            //分類をすでにしている牌の優先度。
            List<int> bunruiEndYuusendoList = new List<int>();
            //分類を"した"牌。分類された牌の中でより優先すべき組み合わせが見つかったら、その分類をした牌と結び付いている牌を消去し、更新する。
            List<MahjongTile> bunruiParentMahjongTileList = new List<MahjongTile>();

            //全ての牌を捨てたと仮定して残りの受け入れ枚数を把握。
            foreach (MahjongTile sutehai in calculationMahjongTileList)
            {
                Debug.Log(calculationMahjongTileList.Count / syoriMaeMahjongTileList.Count);
                List<MahjongTile> nokoriMahjongTileList = new List<MahjongTile>(calculationMahjongTileList);
                nokoriMahjongTileList.Remove(sutehai);

                int nokoriUkeireCount = 0;

                foreach (MahjongTile mahjongTile in nokoriMahjongTileList)
                {
                    //1
                    if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 2);
                    }
                    //2
                    else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 2);
                    }
                    //9
                    else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 2);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                    }
                    //8
                    else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 2);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 1);
                    }
                    //字牌
                    else if ((int)mahjongTile >= 27)
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                    }
                    //その他。3～7
                    else
                    {
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 2);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile - 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 1);
                        nokoriUkeireCount += 4 - calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 2);
                    }
                }

                //数字が大きいものを捨てる。
                //孤立牌(字牌>1・9>2・8>3～7)=12～9>対子(字牌>1・9>2・8>3～7)=8～5>ペンチャン=4>カンチャン(1・9・2・8>3～7)=3～2>リャンメン=1>面子=0
                int sutehaiYuusendo = 0;
                //分類が終わったか
                bool bunruiEnd = false;

                //分類をチェック。すでに分類されていた場合どちらを優先すべきかを記述。
                bool bunruiCheck = false;
                if (bunruiEndMahjongTileList.Contains(sutehai))
                {
                    bunruiCheck = true;
                }

                int bunruiEndMahjongTileListNum = bunruiEndMahjongTileList.IndexOf(sutehai);

                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 0 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //面子
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                        //3個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        //もしくは3個以上あれば
                        else if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        //3個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        //もしくは3個以上あれば
                        else if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                        //3個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        //もしくは3個以上あれば
                        else if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        //3個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        //もしくは3個以上あれば
                        else if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                        //3個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //その他。3～7
                    else
                    {
                        //3個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1))
                            && syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        //もしくは3個以上あれば
                        else if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 3)
                        {
                            sutehaiYuusendo = 0;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(0);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                }
                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 1 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //リャンメン
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                    }
                    //その他。3～7
                    else
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 1;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(1);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                }
                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 3 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //カンチャン
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                        //1つとばしであれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 3;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(3);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        //1つとばしであれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 3;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(3);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                        //1つとばしであれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                        {
                            sutehaiYuusendo = 3;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(3);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        //1つとばしであれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                        {
                            sutehaiYuusendo = 3;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(3);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                    }
                    //その他。3～7
                    else
                    {
                        //1つとばしであれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                        {
                            sutehaiYuusendo = 2;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2));
                            bunruiEndYuusendoList.Add(2);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                        else if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                        {
                            sutehaiYuusendo = 2;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2));
                            bunruiEndYuusendoList.Add(2);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                }
                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 4 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //ペンチャン
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 4;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(4);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 4;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(4);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                        {
                            sutehaiYuusendo = 4;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1));
                            bunruiEndYuusendoList.Add(4);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        //2個並んでいれば
                        if (syoriMaeMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                        {
                            sutehaiYuusendo = 4;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1));
                            bunruiEndYuusendoList.Add(4);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                    }
                    //その他。3～7
                    else
                    {
                    }
                }
                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 8 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //対子
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 7;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(7);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 6;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(6);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 7;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(7);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 6;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(6);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 8;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(8);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                    //その他。3～7
                    else
                    {
                        //2個以上あれば
                        if (syoriMaeMahjongTileList.Count(item => item == sutehai) >= 2)
                        {
                            sutehaiYuusendo = 5;
                            bunruiEnd = true;

                            bunruiEndMahjongTileList.Add(sutehai);
                            bunruiEndYuusendoList.Add(5);
                            bunruiParentMahjongTileList.Add(sutehai);
                        }
                    }
                }
                //もしもう分類されたときの方が優先すべきだったら。
                if (bunruiCheck && !bunruiEnd && 12 >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                {
                    //分類しない。
                    bunruiEnd = true;
                }
                //孤立牌
                if (!bunruiEnd)
                {
                    //1
                    if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                    {
                        sutehaiYuusendo = 11;
                        bunruiEnd = true;
                    }
                    //2
                    else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                    {
                        sutehaiYuusendo = 10;
                        bunruiEnd = true;
                    }
                    //9
                    else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                    {
                        sutehaiYuusendo = 11;
                        bunruiEnd = true;
                    }
                    //8
                    else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                    {
                        sutehaiYuusendo = 10;
                        bunruiEnd = true;
                    }
                    //字牌
                    else if ((int)sutehai >= 27)
                    {
                        sutehaiYuusendo = 12;
                        bunruiEnd = true;
                    }
                    //その他。3～7
                    else
                    {
                        sutehaiYuusendo = 9;
                        bunruiEnd = true;
                    }
                }

                //既にほかの牌の時に一緒に分類されている場合。
                if (bunruiCheck)
                {
                    //もしその時の分類の方が優先(数字が小さい)するのなら。
                    if (sutehaiYuusendo >= bunruiEndYuusendoList[bunruiEndMahjongTileListNum])
                    {
                        //上書き
                        bunruiEndMahjongTileList.Remove(sutehai);
                        sutehaiYuusendo = bunruiEndYuusendoList[bunruiEndMahjongTileListNum];
                        bunruiEndYuusendoList.Remove(bunruiEndYuusendoList[bunruiEndMahjongTileListNum]);
                        bunruiParentMahjongTileList.Remove(bunruiParentMahjongTileList[bunruiEndMahjongTileListNum]);
                    }
                    //もし今の方が優先すべきなら。
                    else
                    {
                        //上書き
                        bunruiEndMahjongTileList.Remove(sutehai);

                        MahjongTile bunruiParentMahjongTile = bunruiParentMahjongTileList[bunruiEndMahjongTileListNum];
                        bunruiEndYuusendoList.Remove(bunruiEndYuusendoList[bunruiEndMahjongTileListNum]);
                        bunruiParentMahjongTileList.Remove(bunruiParentMahjongTile);

                        //もし一緒に分類された牌があれば、それも消去。
                        //最大二個までしか一緒にならないため、一回だけ記載。
                        if (bunruiParentMahjongTileList.Contains(bunruiParentMahjongTile))
                        {
                            bunruiEndMahjongTileList.Remove(bunruiEndMahjongTileList[bunruiParentMahjongTileList.IndexOf(bunruiParentMahjongTile)]);
                            bunruiEndYuusendoList.Remove(bunruiEndYuusendoList[bunruiEndMahjongTileListNum]);
                            bunruiParentMahjongTileList.Remove(bunruiParentMahjongTile);
                        }
                    }
                }

                int kouritu = nokoriUkeireCount + sutehaiYuusendo * 1000;
                Debug.Log(sutehai + "を捨てた時の受け入れ枚数は" + nokoriUkeireCount + "/優先度(大きいのを捨てる)は" + sutehaiYuusendo +
                    "総合評価は" + kouritu);
                kourituList.Add(kouritu);

                syoriMaeMahjongTileList.Remove(sutehai);
            }
        }
        //新。独自AI。
        else
        {
            //メモ：その牌の周囲の牌の数を取得し、それが大きいほど捨てにくくする？
            //メモ：向聴数が0の時は、待ちが何個あるかを取得し、その分を引く(足してしまうと、聴牌なのに評価が高くなりかねないため)。
            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

            foreach (MahjongTile sutehai in calculationMahjongTileHashSet)
            {
                //受け入れられる枚数。これが多いということは、この牌を捨てた方が受け入れられる数が多くなる=捨てた方が良いということ。
                int ukeireCount = 0;
                //向聴数。これが多いということは、この牌を捨てると向聴数が多くなる=上がりから遠のく=捨てない方が良いということ。
                int syantenSuu = 0;
                //優先度。これが多いほど捨てた方が良い。字牌>1・9>2・8>3・7>4・6>5の順に小さい数が付けられる。受け入れられる枚数、向聴数共に同じ時の判断基準。
                int yuusendo = 0;
                //周囲の牌の影響度。これが多いほど捨てない方が良い。隣接牌(リャンメン)>同じ牌(対子)>一つ離れている(カンチャン)>ペンチャンの順に大きい数が付けられる。
                int syuui = 0;

                //今の手牌+ツモ牌を設定。
                calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
                calculationMahjongTileList.Add(tumoMahjongTile);

                //今列挙されている牌を捨てる。
                calculationMahjongTileList.Remove(sutehai);

                CalculationListSort();

                HashSet<MahjongTile> nokoriMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

                //受け入れられる牌。
                HashSet<MahjongTile> ukeireMahjongTileHashSet = new HashSet<MahjongTile>();


                foreach (MahjongTile mahjongTile in nokoriMahjongTileHashSet)
                {
                    //1
                    if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                    //2
                    else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                    //9
                    else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                    }
                    //8
                    else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //字牌
                    else if ((int)mahjongTile >= 27)
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                    }
                    //その他。3～7
                    else
                    {
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        ukeireMahjongTileHashSet.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
                foreach (MahjongTile ukeire in ukeireMahjongTileHashSet)
                {
                    ukeireCount += 4 - calculationMahjongTileList.Count(item => item == ukeire);
                }

                syantenSuu += SyantenSuu(true, false);

                //字牌
                if ((int)sutehai >= 27)
                {
                    yuusendo += 5;
                }
                //1or9
                else if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18
                    || (int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                {
                    yuusendo += 4;
                }
                //2or8
                else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19
                    || (int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                {
                    yuusendo += 3;
                }
                //3or7
                else if ((int)sutehai == 2 || (int)sutehai == 11 || (int)sutehai == 20
                    || (int)sutehai == 6 || (int)sutehai == 15 || (int)sutehai == 24)
                {
                    yuusendo += 2;
                }
                //4or6
                else if ((int)sutehai == 3 || (int)sutehai == 12 || (int)sutehai == 21
                    || (int)sutehai == 5 || (int)sutehai == 14 || (int)sutehai == 23)
                {
                    yuusendo += 1;
                }
                //5
                else if ((int)sutehai == 4 || (int)sutehai == 13 || (int)sutehai == 22)
                {
                    yuusendo += 0;
                }

                //向聴数を計算する際にリセットされたため、もう一度設定。
                //今の手牌+ツモ牌を設定。
                calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
                calculationMahjongTileList.Add(tumoMahjongTile);

                //今列挙されている牌を捨てる。
                calculationMahjongTileList.Remove(sutehai);

                //1
                if ((int)sutehai == 0 || (int)sutehai == 9 || (int)sutehai == 18)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                    {
                        syuui += 1 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 1);
                        //Debug.Log(sutehai + "のペンチャンは" + calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 1) + "個あります");
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 2);
                        //Debug.Log(sutehai + "のカンチャンは" + calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 2) + "個あります");
                    }
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                        //Debug.Log(sutehai + "の対子/刻子は" + calculationMahjongTileList.Count(item => (int)item == (int)sutehai) + "個あります");
                    }
                }
                //2
                else if ((int)sutehai == 1 || (int)sutehai == 10 || (int)sutehai == 19)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                    {
                        syuui += 1 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 1);
                    }
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                    {
                        syuui += 4 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 1);
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 2);
                    }
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                    }
                }
                //9
                else if ((int)sutehai == 8 || (int)sutehai == 17 || (int)sutehai == 26)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                    {
                        syuui += 1 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 1);
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 2);
                    }
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                    }
                }
                //8
                else if ((int)sutehai == 7 || (int)sutehai == 16 || (int)sutehai == 25)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                    {
                        syuui += 1 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 1);
                    }
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                    {
                        syuui += 4 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 1);
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 2);
                    }
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                    }
                }
                //字牌
                else if ((int)sutehai >= 27)
                {
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                    }
                }
                //その他。3～7
                else
                {
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 1)))
                    {
                        syuui += 4 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 1);
                    }
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 1)))
                    {
                        syuui += 4 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 1);
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai - 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai - 2);
                    }
                    //カンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)sutehai + 2)))
                    {
                        syuui += 2 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai + 2);
                    }
                    //対子/刻子
                    if (calculationMahjongTileList.Contains(sutehai))
                    {
                        syuui += 3 * calculationMahjongTileList.Count(item => (int)item == (int)sutehai);
                    }
                }


                int kouritu = ukeireCount - syantenSuu * 1000 + yuusendo - syuui * 10;
                //Debug.Log("残りのツモ牌は" + tumoMahjongTileList.Count + sutehai + "を捨てた時の受け入れ枚数は" + ukeireCount +
                //    "/向聴数は" + syantenSuu + "/優先度は" + yuusendo + "/周囲の影響度は" + syuui + "総合評価は" + kouritu);
                kourituList.Add(kouritu);
            }
        }
    }

    void CalculationListSort()
    {
        //enum型の番号を追加。
        List<int> calculationMahjongTileNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in calculationMahjongTileList)
        {
            calculationMahjongTileNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newCalculationMahjongTileList = new List<MahjongTile>();
        int count = calculationMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = calculationMahjongTileNumList.Min();
            int minNumNum = calculationMahjongTileNumList.IndexOf(minNum);

            newCalculationMahjongTileList.Add(calculationMahjongTileList[minNumNum]);
            calculationMahjongTileList.Remove(calculationMahjongTileList[minNumNum]);
            calculationMahjongTileNumList.Remove(minNum);
        }

        calculationMahjongTileList = new List<MahjongTile>(newCalculationMahjongTileList);
    }
    void CalculationListReverseSort()
    {
        //enum型の番号を追加。
        List<int> calculationMahjongTileNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in calculationMahjongTileList)
        {
            calculationMahjongTileNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newCalculationMahjongTileList = new List<MahjongTile>();
        int count = calculationMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int maxNum = calculationMahjongTileNumList.Max();
            int maxNumNum = calculationMahjongTileNumList.IndexOf(maxNum);

            newCalculationMahjongTileList.Add(calculationMahjongTileList[maxNumNum]);
            calculationMahjongTileList.Remove(calculationMahjongTileList[maxNumNum]);
            calculationMahjongTileNumList.Remove(maxNum);
        }

        calculationMahjongTileList = new List<MahjongTile>(newCalculationMahjongTileList);
    }

    //刻子カウント。
    int KootuCount(int maxCount)
    {
        int count = 0;

        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //3個以上あれば
            if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 3)
            {
                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add(mahjongTile);
                mentu.Add(mahjongTile);
                mentu.Add(mahjongTile);
                //面子のリストに追加
                mentuMahjongTileList.Add(mentu);

                kootuMentu.Add(mentu);
                //3個削除。
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                count++;

                if (maxCount > 0 && maxCount == count)
                {
                    break;
                }
            }
        }
        return count;
    }

    int SyuntuCount()
    {
        int count = 0;

        List<MahjongTile> calculationMahjongTileListCopy = new List<MahjongTile>(calculationMahjongTileList);
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            //1~7なら
            if ((int)mahjongTile <= 6 || ((int)mahjongTile >= 9 && (int)mahjongTile <= 15) || ((int)mahjongTile >= 18 && (int)mahjongTile <= 24))
            {
                //3個並んでいれば
                if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 1) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == (int)mahjongTile + 2) >= 1)
                {
                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(mahjongTile);
                    mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    //面子のリストに追加
                    mentuMahjongTileList.Add(mentu);

                    syuntuMentu.Add(mentu);
                    //3個削除。
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));

                    count++;
                }
            }
        }
        return count;
    }

    void HansuuCount(bool ai)
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();
        foreach (List<MahjongTile> mentu in mentuMahjongTileList)
        {
            if (mentuMahjongTileHashSet.Count(item => item.SequenceEqual(mentu)) == 0)
            {
                mentuMahjongTileHashSet.Add(mentu);
            }
        }

        List<MahjongTile> allMahjongTileList = new List<MahjongTile>();
        if (!ai)
        {
            allMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        }
        else
        {
            allMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
        }
        allMahjongTileList.Add(tumoMahjongTile);

        //enum型の番号を追加。
        List<int> allMahjongTileListNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in allMahjongTileList)
        {
            allMahjongTileListNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newAllMahjongTileList = new List<MahjongTile>();
        int allMahjongTileListCount = allMahjongTileList.Count;
        for (int i = 0; i < allMahjongTileListCount; i++)
        {
            int minNum = allMahjongTileListNumList.Min();
            int minNumNum = allMahjongTileListNumList.IndexOf(minNum);

            newAllMahjongTileList.Add(allMahjongTileList[minNumNum]);
            allMahjongTileList.Remove(allMahjongTileList[minNumNum]);
            allMahjongTileListNumList.Remove(minNum);
        }

        allMahjongTileList = new List<MahjongTile>(newAllMahjongTileList);

        yakumanNow = false;

        //１翻
        addHansuu = 1;
        //立直
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        //面前自摸(メンゼンツモ)
        //ツモは確定で入れる
        hansuu += addHansuu;
        yaku += "\n面前自摸：１翻";
        //平和(ピンフ)
        bool pinhu = false;
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        if (syuntuCount == 4)
        {
            //雀頭が役牌でない
            if ((int)head != 31 && (int)head != 32 && (int)head != 33)
            {
                if (ryanmen)
                {
                    //pinhuを有効に。
                    pinhu = true;

                    //平和のみ特殊で、ここで処理は行わない。
                    //平和か三色同順に取れる場合は、三色同順で見ないといけないため、両面がつかない。
                }
            }
        }
        //断么九(タンヤオ)
        //一九時牌抜き。
        if (allMahjongTileList.Count(item => (int)item == 0) == 0 && allMahjongTileList.Count(item => (int)item == 8) == 0
            && allMahjongTileList.Count(item => (int)item == 9) == 0 && allMahjongTileList.Count(item => (int)item == 17) == 0
            && allMahjongTileList.Count(item => (int)item == 18) == 0 && allMahjongTileList.Count(item => (int)item == 26) == 0
            && allMahjongTileList.Count(item => (int)item > 26) == 0)
        {
            hansuu += addHansuu;
            yaku += "\n断么九：１翻";
        }
        //翻牌(ファンパイ)・役牌(ヤクハイ)
        //三元牌/場風牌/自風牌。
        foreach (List<MahjongTile> mentu in mentuMahjongTileList)
        {
            //白
            if (mentu.Count(item => (int)item == 31) == 3)
            {
                hansuu += addHansuu;
                yaku += "\n翻牌/役牌(白)：１翻";
            }
            //發
            if (mentu.Count(item => (int)item == 32) == 3)
            {
                hansuu += addHansuu;
                yaku += "\n翻牌/役牌(發)：１翻";
            }
            //中
            if (mentu.Count(item => (int)item == 33) == 3)
            {
                hansuu += addHansuu;
                yaku += "\n翻牌/役牌(中)：１翻";
            }
            //場風牌
            //場風牌
        }
        //一盃口(イーペーコー)
        if (0 == 0)
        {
            int count = 0;
            //同じメンツが何個あるか。
            foreach (List<MahjongTile> mentu in mentuMahjongTileHashSet)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    //1~7なら
                    if ((int)mahjongTile <= 6 || ((int)mahjongTile >= 9 && (int)mahjongTile <= 15) || ((int)mahjongTile >= 18 && (int)mahjongTile <= 24))
                    {
                        //3個並んでいれば
                        if (mentu.Count(item => item == mahjongTile) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 1) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 2) >= 1)
                        {
                            //同じのが2個以上あれば。
                            if (mentuMahjongTileList.Count(item => mentu.SequenceEqual(item)) >= 2)
                            {
                                count++;
                                break;
                            }
                        }
                    }
                }
            }
            //1個ならば、2個なら二盃口。
            if (count == 1)
            {
                hansuu += addHansuu;
                yaku += "\n一盃口：１翻";
            }
        }
        //嶺上開花(リンシャンカイホー)
        //槍槓(チャンカン)
        //海底(ハイテイ)/河底(ホーテイ)
        //２翻
        addHansuu = 2;
        //ダブル立直
        //三色同順(サンショクドウジュン)
        //順子が３個以上なら
        if (syuntuCount >= 3)
        {
            bool end = false;
            //同じメンツが何個あるか。
            foreach (List<MahjongTile> mentu in mentuMahjongTileHashSet)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    //萬子の1~7なら
                    if ((int)mahjongTile <= 6)
                    {
                        //3個並んでいれば
                        if (mentu.Count(item => item == mahjongTile) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 1) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 2) >= 1)
                        {
                            List<MahjongTile> pinzuSansyoku = new List<MahjongTile>();
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 9));
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 10));
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 11));
                            List<MahjongTile> souzuSansyoku = new List<MahjongTile>();
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 18));
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 19));
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 20));
                            //筒子、索子にて同じ数字の順子があれば
                            if (mentuMahjongTileList.Count(item => pinzuSansyoku.SequenceEqual(item)) >= 1
                                && mentuMahjongTileList.Count(item => souzuSansyoku.SequenceEqual(item)) >= 1)
                            {
                                hansuu += addHansuu;
                                yaku += "\n三色同順：２翻";

                                end = true;
                                break;
                            }
                        }
                    }
                }
                if (end)
                {
                    break;
                }
            }
            //三色同順がついたら
            if (end)
            {
                if (pinhu && kantyan)
                {
                    ryanmen = false;
                    Debug.Log("三色同順がついたため、リャンメンは待ちではなくなります。");
                }
                else if (pinhu)
                {
                    hansuu += 1;
                    yaku += "\n平和：１翻";

                    pentyan = false;
                    kantyan = false;
                    tanki = false;
                    Debug.Log("平和がついたため、ペンチャン、カンチャン、単騎は待ちではなくなります。");
                }
            }
            else
            {
                if (pinhu)
                {
                    hansuu += 1;
                    yaku += "\n平和：１翻";

                    pentyan = false;
                    kantyan = false;
                    tanki = false;
                    Debug.Log("平和がついたため、ペンチャン、カンチャン、単騎は待ちではなくなります。");
                }
            }
        }
        //三色同刻(サンショクドウコウ)
        //刻子が３個以上なら
        if (kootuCount >= 3)
        {
            bool end = false;
            //同じメンツが何個あるか。
            foreach (List<MahjongTile> mentu in mentuMahjongTileHashSet)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    //萬子の1~9なら
                    if ((int)mahjongTile <= 8)
                    {
                        //3個あれば
                        if (mentu.Count(item => item == mahjongTile) >= 3)
                        {
                            List<MahjongTile> pinzuSansyoku = new List<MahjongTile>();
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 9));
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 9));
                            pinzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 9));
                            List<MahjongTile> souzuSansyoku = new List<MahjongTile>();
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 18));
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 18));
                            souzuSansyoku.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 18));
                            //筒子、索子にて同じ数字の刻子があれば
                            if (mentuMahjongTileList.Count(item => pinzuSansyoku.SequenceEqual(item)) >= 1
                                && mentuMahjongTileList.Count(item => souzuSansyoku.SequenceEqual(item)) >= 1)
                            {
                                hansuu += addHansuu;
                                yaku += "\n三色同刻：２翻";
                                end = true;
                                break;
                            }
                        }
                    }
                }
                if (end)
                {
                    break;
                }
            }
        }
        //一気通貫(イッキツウカン)
        //順子が３個以上なら
        if (syuntuCount >= 3)
        {
            List<MahjongTile> ittuu = new List<MahjongTile>();
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));
            //萬子の1、2、3が一つ以上あったら。
            if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
            {
                ittuu = new List<MahjongTile>();
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 3));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 4));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 5));
                //萬子の4、5、6が一つ以上あったら。
                if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                {
                    ittuu = new List<MahjongTile>();
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));
                    //萬子の7、8、9が一つ以上あったら。
                    if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                    {
                        hansuu += addHansuu;
                        yaku += "\n一気通貫：２翻";
                    }
                }
            }
            ittuu = new List<MahjongTile>();
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));
            //筒子の1、2、3が一つ以上あったら。
            if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
            {
                ittuu = new List<MahjongTile>();
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 12));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 13));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 14));
                //筒子の4、5、6が一つ以上あったら。
                if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                {
                    ittuu = new List<MahjongTile>();
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));
                    //筒子の7、8、9が一つ以上あったら。
                    if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                    {
                        hansuu += addHansuu;
                        yaku += "\n一気通貫：２翻";
                    }
                }
            }
            ittuu = new List<MahjongTile>();
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
            ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));
            //索子の1、2、3が一つ以上あったら。
            if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
            {
                ittuu = new List<MahjongTile>();
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 21));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 22));
                ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 23));
                //索子の4、5、6が一つ以上あったら。
                if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                {
                    ittuu = new List<MahjongTile>();
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                    ittuu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));
                    //索子の7、8、9が一つ以上あったら。
                    if (mentuMahjongTileList.Count(item => item.SequenceEqual(ittuu)) >= 1)
                    {
                        hansuu += addHansuu;
                        yaku += "\n一気通貫：２翻";
                    }
                }
            }
        }
        //対々和(トイトイホー)
        //4個の刻子があったら
        if (kootuCount >= 4)
        {
            hansuu += addHansuu;
            yaku += "\n対々和：２翻";
        }
        //三暗刻(サンアンコー)
        if (kootuCount >= 3)
        {
            hansuu += addHansuu;
            yaku += "\n三暗刻：２翻";
        }
        //三槓子(サンカンツ)
        //判定無し。
        //全帯么(チャンタ)
        //一九字牌絡め。
        //頭が一九字牌なら
        if ((int)head == 0 || (int)head == 8 || (int)head == 9 || (int)head == 17 || (int)head == 18 || (int)head == 26 || (int)head >= 27)
        {
            bool chanta = true;
            //面子を列挙。
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                if (mentu.Count(item => (int)item == 0) >= 1 || mentu.Count(item => (int)item == 8) >= 1
                    || mentu.Count(item => (int)item == 9) >= 1 || mentu.Count(item => (int)item == 17) >= 1
                    || mentu.Count(item => (int)item == 18) >= 1 || mentu.Count(item => (int)item == 26) >= 1
                    || mentu.Count(item => (int)item >= 27) >= 1)
                {
                    continue;
                }
                else
                {
                    chanta = false;
                }
            }
            //字牌があったら。
            //字牌がなかったら、純全帯么になるため。処理しない。
            //刻子が4つだったら、混老頭になる。
            if (chanta && kootuCount < 4 && allMahjongTileList.Count(item => (int)item >= 27) >= 1)
            {
                hansuu += addHansuu;
                yaku += "\n全帯么：２翻";
            }
        }
        //混老頭(ホンロウトウ)
        //全て一九字牌。
        //頭が一九字牌なら
        if ((int)head == 0 || (int)head == 8 || (int)head == 9 || (int)head == 17 || (int)head == 18 || (int)head == 26 || (int)head >= 27)
        {
            if (kootuCount >= 4)
            {
                bool honroutou = true;
                foreach (List<MahjongTile> mentu in mentuMahjongTileList)
                {
                    if (mentu.Count(item => (int)item == 0) >= 3 || mentu.Count(item => (int)item == 8) >= 3
                        || mentu.Count(item => (int)item == 9) >= 3 || mentu.Count(item => (int)item == 17) >= 3
                        || mentu.Count(item => (int)item == 18) >= 3 || mentu.Count(item => (int)item == 26) >= 3
                        || mentu.Count(item => (int)item > 26) >= 3)
                    {
                        continue;
                    }
                    else
                    {
                        honroutou = false;
                    }
                }
                //字牌があったら。
                //字牌がなかったら、清老頭になるため。処理しない。
                if (honroutou && allMahjongTileList.Count(item => (int)item >= 27) >= 1)
                {
                    hansuu += addHansuu;
                    yaku += "\n混老頭：２翻";
                }
            }
        }
        //小三元(ショウサンゲン)
        if (kootuCount >= 2)
        {
            bool hakuKootu = false;
            bool hatuKootu = false;
            bool tyunKootu = false;
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                if (mentu.Count(item => (int)item == 31) >= 3)
                {
                    hakuKootu = true;
                }
                else if (mentu.Count(item => (int)item == 32) >= 3)
                {
                    hatuKootu = true;
                }
                else if (mentu.Count(item => (int)item == 33) >= 3)
                {
                    tyunKootu = true;
                }
            }
            if (hakuKootu && hatuKootu && (int)head == 33)
            {
                hansuu += addHansuu;
                yaku += "\n小三元：２翻";
            }
            else if (hakuKootu && tyunKootu && (int)head == 32)
            {
                hansuu += addHansuu;
                yaku += "\n小三元：２翻";
            }
            else if (hatuKootu && tyunKootu && (int)head == 31)
            {
                hansuu += addHansuu;
                yaku += "\n小三元：２翻";
            }
        }
        //七対子
        //別に判定。
        //３翻
        addHansuu = 3;
        //二盃口(リャンペーコー)
        if (0 == 0)
        {
            int count = 0;
            //同じメンツが何個あるか。
            foreach (List<MahjongTile> mentu in mentuMahjongTileHashSet)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    //1~7なら
                    if ((int)mahjongTile <= 6 || ((int)mahjongTile >= 9 && (int)mahjongTile <= 15) || ((int)mahjongTile >= 18 && (int)mahjongTile <= 24))
                    {
                        //3個並んでいれば
                        if (mentu.Count(item => item == mahjongTile) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 1) >= 1
                            && mentu.Count(item => (int)item == (int)mahjongTile + 2) >= 1)
                        {
                            //同じのが2個以上あれば。
                            if (mentuMahjongTileList.Count(item => mentu.SequenceEqual(item)) >= 2)
                            {
                                //一個見つかれば終了。二個あるのは二盃口。二盃口では、一盃口の分を削除する。
                                count++;
                                break;
                            }
                        }
                    }
                }
            }
            if (count == 2)
            {
                hansuu += addHansuu;
                yaku += "\n二盃口：３翻";
            }
        }
        //混一色(ホンイーソー、ホンイツ)
        //一色+字牌。
        if (allMahjongTileList.Count(item => (int)item <= 8) + allMahjongTileList.Count(item => (int)item >= 27) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 17) + allMahjongTileList.Count(item => (int)item >= 27)
            - allMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 26) + allMahjongTileList.Count(item => (int)item >= 27)
            - allMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            //字牌があったら。
            //字牌がなかったら、清一色になるため。処理しない。
            if (allMahjongTileList.Count(item => (int)item >= 27) >= 1)
            {
                hansuu += addHansuu;
                yaku += "\n混一色：３翻";
            }
        }
        //純全帯么(ジュンチャンタ)
        //一九絡め。
        //頭が一九なら
        if ((int)head == 0 || (int)head == 8 || (int)head == 9 || (int)head == 17 || (int)head == 18 || (int)head == 26)
        {
            //面子を列挙。
            bool zyunChanta = true;
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                if (mentu.Count(item => (int)item == 0) >= 1 || mentu.Count(item => (int)item == 8) >= 1
                    || mentu.Count(item => (int)item == 9) >= 1 || mentu.Count(item => (int)item == 17) >= 1
                    || mentu.Count(item => (int)item == 18) >= 1 || mentu.Count(item => (int)item == 26) >= 1)
                {
                    continue;
                }
                else
                {
                    zyunChanta = false;
                }
            }
            if (zyunChanta)
            {
                hansuu += addHansuu;
                yaku += "\n純全帯么：３翻";
            }
        }
        //６翻
        addHansuu = 6;
        //清一色(チンイーソー、チンイツ)
        //一色。
        if (allMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 17) - tehaiMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 26) - tehaiMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            hansuu += addHansuu;
            yaku += "\n清一色：６翻";
        }
        //役満。yakumanNow=true
        //四暗刻(スーアンコー)
        if (kootuCount >= 4)
        {
            yakumanNow = true;
            yaku += "\n四暗刻：役満";
        }
        //大三元(ダイサンゲン)
        if (kootuCount >= 3)
        {
            bool hakuKootu = false;
            bool hatuKootu = false;
            bool tyunKootu = false;
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                if (mentu.Count(item => (int)item == 31) >= 3)
                {
                    hakuKootu = true;
                }
                else if (mentu.Count(item => (int)item == 32) >= 3)
                {
                    hatuKootu = true;
                }
                else if (mentu.Count(item => (int)item == 33) >= 3)
                {
                    tyunKootu = true;
                }
            }
            if (hakuKootu && hatuKootu && tyunKootu)
            {
                yakumanNow = true;
                yaku += "\n大三元：役満";
            }
        }
        //国士無双
        //別に判定。
        //四喜和(スーシーホー)
        if (kootuCount >= 3)
        {
            bool tonKootu = false;
            bool nanKootu = false;
            bool syaaKootu = false;
            bool peeKootu = false;
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                if (mentu.Count(item => (int)item == 27) >= 3)
                {
                    tonKootu = true;
                }
                else if (mentu.Count(item => (int)item == 28) >= 3)
                {
                    nanKootu = true;
                }
                else if (mentu.Count(item => (int)item == 29) >= 3)
                {
                    syaaKootu = true;
                }
                else if (mentu.Count(item => (int)item == 30) >= 3)
                {
                    peeKootu = true;
                }
            }
            if (tonKootu && nanKootu && syaaKootu && peeKootu)
            {
                yakumanNow = true;
                yaku += "\n四喜和(大四喜)：役満";
            }
            else if (tonKootu && nanKootu && syaaKootu && (int)head == 30)
            {
                yakumanNow = true;
                yaku += "\n四喜和(小四喜)：役満";
            }
            else if (tonKootu && nanKootu && (int)head == 29 && peeKootu)
            {
                yakumanNow = true;
                yaku += "\n四喜和(小四喜)：役満";
            }
            else if (tonKootu && (int)head == 28 && syaaKootu && peeKootu)
            {
                yakumanNow = true;
                yaku += "\n四喜和(小四喜)：役満";
            }
            else if ((int)head == 27 && nanKootu && syaaKootu && peeKootu)
            {
                yakumanNow = true;
                yaku += "\n四喜和(小四喜)：役満";
            }
        }
        //字一色(ツーイーソー)
        //字牌。
        if (allMahjongTileList.Count(item => (int)item >= 27) == 14)
        {
            yakumanNow = true;
            yaku += "\n字一色：役満";
        }
        //九連宝燈(チューレンポートン)
        //一色。1、9が各3牌。2~8が各1牌以上。
        //鳴いていないか、ポン、チー、カン(暗槓も)をしていないか。
        if (allMahjongTileList.Count == 14)
        {
            //萬子一色か。
            if (allMahjongTileList.Count(item => (int)item <= 8) == 14)
            {
                if (allMahjongTileList.Count(item => (int)item == 0) >= 3 && allMahjongTileList.Count(item => (int)item == 1) >= 1 && allMahjongTileList.Count(item => (int)item == 2) >= 1
                && allMahjongTileList.Count(item => (int)item == 3) >= 1 && allMahjongTileList.Count(item => (int)item == 4) >= 1 && allMahjongTileList.Count(item => (int)item == 5) >= 1
                && allMahjongTileList.Count(item => (int)item == 6) >= 1 && allMahjongTileList.Count(item => (int)item == 7) >= 1 && allMahjongTileList.Count(item => (int)item == 8) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
            //筒子一色か。
            else if (allMahjongTileList.Count(item => (int)item <= 17) - allMahjongTileList.Count(item => (int)item <= 8) == 14)
            {
                if (allMahjongTileList.Count(item => (int)item == 9) >= 3 && allMahjongTileList.Count(item => (int)item == 10) >= 1 && allMahjongTileList.Count(item => (int)item == 11) >= 1
                && allMahjongTileList.Count(item => (int)item == 12) >= 1 && allMahjongTileList.Count(item => (int)item == 14) >= 1 && allMahjongTileList.Count(item => (int)item == 16) >= 1
                && allMahjongTileList.Count(item => (int)item == 15) >= 1 && allMahjongTileList.Count(item => (int)item == 17) >= 1 && allMahjongTileList.Count(item => (int)item == 17) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
            //索子一色か。
            else if (allMahjongTileList.Count(item => (int)item <= 26) - allMahjongTileList.Count(item => (int)item <= 8) == 14)
            {
                if (allMahjongTileList.Count(item => (int)item == 18) >= 3 && allMahjongTileList.Count(item => (int)item == 19) >= 1 && allMahjongTileList.Count(item => (int)item == 20) >= 1
                && allMahjongTileList.Count(item => (int)item == 21) >= 1 && allMahjongTileList.Count(item => (int)item == 22) >= 1 && allMahjongTileList.Count(item => (int)item == 23) >= 1
                && allMahjongTileList.Count(item => (int)item == 24) >= 1 && allMahjongTileList.Count(item => (int)item == 25) >= 1 && allMahjongTileList.Count(item => (int)item == 26) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
        }
        //緑一色(リューイーソー)
        //緑一色。日本プロ麻雀連盟公式ルール改訂により、2023年度以降發無しでも緑一色となる。
        if (allMahjongTileList.Count(item => (int)item == 19) + allMahjongTileList.Count(item => (int)item == 20)
        + allMahjongTileList.Count(item => (int)item == 21) + allMahjongTileList.Count(item => (int)item == 23)
        + allMahjongTileList.Count(item => (int)item == 28) + allMahjongTileList.Count(item => (int)item == 32) == 14)
        {
            yakumanNow = true;
            yaku += "\n緑一色：役満";
        }
        //清老頭(チンロートー)
        //一九のみ。
        if (allMahjongTileList.Count(item => (int)item == 0) + allMahjongTileList.Count(item => (int)item == 8)
        + allMahjongTileList.Count(item => (int)item == 9) + allMahjongTileList.Count(item => (int)item == 17)
        + allMahjongTileList.Count(item => (int)item == 18) + allMahjongTileList.Count(item => (int)item == 26) == 14)
        {
            yakumanNow = true;
            yaku += "\n清老頭：役満";
        }
        //四槓子(スーカンツ)
        //判定無し。
        //天和(テンホー)/地和(チーホー)
        if (tehaiMahjongTileDataList.Count == 1)
        {
            yakumanNow = true;
            yaku += "\n地和：役満";
        }
    }

    void TiitoituHansuuCount(bool ai)
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();
        foreach (List<MahjongTile> mentu in mentuMahjongTileList)
        {
            if (mentuMahjongTileHashSet.Count(item => item.SequenceEqual(mentu)) == 0)
            {
                mentuMahjongTileHashSet.Add(mentu);
            }
        }

        List<MahjongTile> allMahjongTileList = new List<MahjongTile>();
        if (!ai)
        {
            allMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        }
        else
        {
            allMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
        }
        allMahjongTileList.Add(tumoMahjongTile);

        //enum型の番号を追加。
        List<int> allMahjongTileListNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in allMahjongTileList)
        {
            allMahjongTileListNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newAllMahjongTileList = new List<MahjongTile>();
        int allMahjongTileListCount = allMahjongTileList.Count;
        for (int i = 0; i < allMahjongTileListCount; i++)
        {
            int minNum = allMahjongTileListNumList.Min();
            int minNumNum = allMahjongTileListNumList.IndexOf(minNum);

            newAllMahjongTileList.Add(allMahjongTileList[minNumNum]);
            allMahjongTileList.Remove(allMahjongTileList[minNumNum]);
            allMahjongTileListNumList.Remove(minNum);
        }

        allMahjongTileList = new List<MahjongTile>(newAllMahjongTileList);

        yakumanNow = false;

        //１翻
        addHansuu = 1;
        //立直
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        //面前自摸(メンゼンツモ)
        hansuu += addHansuu;
        yaku += "\n面前自摸：１翻";
        //平和(ピンフ)
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        //断么九(タンヤオ)
        //一九時牌抜き。
        if (allMahjongTileList.Count(item => (int)item == 0) == 0 && allMahjongTileList.Count(item => (int)item == 8) == 0
            && allMahjongTileList.Count(item => (int)item == 9) == 0 && allMahjongTileList.Count(item => (int)item == 17) == 0
            && allMahjongTileList.Count(item => (int)item == 18) == 0 && allMahjongTileList.Count(item => (int)item == 26) == 0
            && allMahjongTileList.Count(item => (int)item > 26) == 0)
        {
            hansuu += addHansuu;
            yaku += "\n断么九：１翻";
        }
        //翻牌(ファンパイ)・役牌(ヤクハイ)
        //三元牌/場風牌/自風牌。
        //一盃口(イーペーコー)
        //嶺上開花(リンシャンカイホー)
        //槍槓(チャンカン)
        //海底(ハイテイ)/河底(ホーテイ)
        //２翻
        addHansuu = 2;
        //ダブル立直
        //三色同順(サンショクドウジュン)
        //順子が３個以上なら
        //三色同刻(サンショクドウコウ)
        //刻子が３個以上なら
        //一気通貫(イッキツウカン)
        //順子が３個以上なら
        //対々和(トイトイホー)
        //4個の刻子があったら
        //三暗刻(サンアンコー)
        //三槓子(サンカンツ)
        //全帯么(チャンタ)
        //一九字牌絡め。
        if (allMahjongTileList.Count(item => (int)item == 0) + allMahjongTileList.Count(item => (int)item == 8)
            + allMahjongTileList.Count(item => (int)item == 9) + allMahjongTileList.Count(item => (int)item == 17)
            + allMahjongTileList.Count(item => (int)item == 18) + allMahjongTileList.Count(item => (int)item == 26)
            + allMahjongTileList.Count(item => (int)item >= 27) == 14)
        {
            hansuu += addHansuu;
            yaku += "\n全帯么：２翻";
        }
        //混老頭(ホンロウトウ)
        //全て一九字牌。
        if (allMahjongTileList.Count(item => (int)item == 0) + allMahjongTileList.Count(item => (int)item == 8)
            + allMahjongTileList.Count(item => (int)item == 9) + allMahjongTileList.Count(item => (int)item == 17)
            + allMahjongTileList.Count(item => (int)item == 18) + allMahjongTileList.Count(item => (int)item >= 26) == 14)
        {
            hansuu += addHansuu;
            yaku += "\n混老頭：２翻";
        }
        //小三元(ショウサンゲン)
        //七対子
        //この処理をしている時点で判定済み。
        hansuu += 2;
        yaku += "\n七対子：２翻";
        //３翻
        addHansuu = 3;
        //二盃口(リャンペーコー)
        //混一色(ホンイーソー、ホンイツ)
        //一色+字牌。
        if (allMahjongTileList.Count(item => (int)item <= 8) + allMahjongTileList.Count(item => (int)item >= 27) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 17) + allMahjongTileList.Count(item => (int)item >= 27)
            - allMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 26) + allMahjongTileList.Count(item => (int)item >= 27)
            - allMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            //字牌があったら。
            //字牌がなかったら、清一色になるため。処理しない。
            if (allMahjongTileList.Count(item => (int)item >= 27) >= 1)
            {
                hansuu += addHansuu;
                yaku += "\n混一色：３翻";
            }
        }
        //純全帯么(ジュンチャンタ)
        //一九絡め。一九が六個しかないため不可。
        //６翻
        addHansuu = 6;
        //清一色(チンイーソー、チンイツ)
        //一色。
        if (allMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 17) - allMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            allMahjongTileList.Count(item => (int)item <= 26) - allMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            hansuu += addHansuu;
            yaku += "\n清一色：６翻";
        }
        //役満。yakumanNow=true
        //四暗刻(スーアンコー)
        //大三元(ダイサンゲン)
        //国士無双
        //別に判定。
        //四喜和(スーシーホー)
        //字一色(ツーイーソー)
        //字牌。
        if (allMahjongTileList.Count(item => (int)item >= 27) == 14)
        {
            yakumanNow = true;
            yaku += "\n字一色：役満";
        }
        //九連宝燈(チューレンポートン)
        //一色。1、9が各3牌。2~8が各1牌以上。
        //鳴いていないか、ポン、チー、カン(暗槓も)をしていないか。
        //緑一色(リューイーソー)
        //緑一色。日本プロ麻雀連盟公式ルール改訂により、2023年度以降發無しでも緑一色となる。六種なため不可。
        //清老頭(チンロートー)
        //一九のみ。六種なため不可。
        //四槓子(スーカンツ)
        //天和(テンホー)/地和(チーホー)
        if (tehaiMahjongTileDataList.Count == 1)
        {
            yakumanNow = true;
            yaku += "\n地和：役満";
        }
    }

    void KokusiHansuuCount(bool ai)
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();
        foreach (List<MahjongTile> mentu in mentuMahjongTileList)
        {
            if (mentuMahjongTileHashSet.Count(item => item.SequenceEqual(mentu)) == 0)
            {
                mentuMahjongTileHashSet.Add(mentu);
            }
        }

        List<MahjongTile> allMahjongTileList = new List<MahjongTile>();
        if (!ai)
        {
            allMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        }
        else
        {
            allMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
        }
        allMahjongTileList.Add(tumoMahjongTile);

        //enum型の番号を追加。
        List<int> allMahjongTileListNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in allMahjongTileList)
        {
            allMahjongTileListNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newAllMahjongTileList = new List<MahjongTile>();
        int allMahjongTileListCount = allMahjongTileList.Count;
        for (int i = 0; i < allMahjongTileListCount; i++)
        {
            int minNum = allMahjongTileListNumList.Min();
            int minNumNum = allMahjongTileListNumList.IndexOf(minNum);

            newAllMahjongTileList.Add(allMahjongTileList[minNumNum]);
            allMahjongTileList.Remove(allMahjongTileList[minNumNum]);
            allMahjongTileListNumList.Remove(minNum);
        }

        allMahjongTileList = new List<MahjongTile>(newAllMahjongTileList);

        yakumanNow = false;

        //１翻
        addHansuu = 1;
        //立直
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        //面前自摸(メンゼンツモ)
        hansuu += addHansuu;
        yaku += "\n面前自摸：１翻";
        //平和(ピンフ)
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        //断么九(タンヤオ)
        //一九時牌抜き。国士無双は一九字牌のみ。
        //翻牌(ファンパイ)・役牌(ヤクハイ)
        //三元牌/場風牌/自風牌。
        //一盃口(イーペーコー)
        //嶺上開花(リンシャンカイホー)
        //槍槓(チャンカン)
        //海底(ハイテイ)/河底(ホーテイ)
        //２翻
        addHansuu = 2;
        //ダブル立直
        //三色同順(サンショクドウジュン)
        //順子が３個以上なら
        //三色同刻(サンショクドウコウ)
        //刻子が３個以上なら
        //一気通貫(イッキツウカン)
        //順子が３個以上なら
        //対々和(トイトイホー)
        //4個の刻子があったら
        //三暗刻(サンアンコー)
        //三槓子(サンカンツ)
        //全帯么(チャンタ)
        //一九字牌絡め。処理しなくても国士無双だから一九字牌。
        hansuu += addHansuu;
        yaku += "\n全帯么：２翻";
        //混老頭(ホンロウトウ)
        //全て一九字牌。処理しなくても国士無双だから一九字牌。
        hansuu += addHansuu;
        yaku += "\n混老頭：２翻";
        //小三元(ショウサンゲン)
        //七対子
        //別に判定。
        //３翻
        addHansuu = 3;
        //二盃口(リャンペーコー)
        //混一色(ホンイーソー、ホンイツ)
        //一色+字牌。
        //純全帯么(ジュンチャンタ)
        //一九絡め。
        //頭が一九なら
        //６翻
        addHansuu = 6;
        //清一色(チンイーソー、チンイツ)
        //一色。
        //役満。yakumanNow=true
        //四暗刻(スーアンコー)
        //大三元(ダイサンゲン)
        //国士無双
        //この処理をしている時点で判定済み。
        yakumanNow = true;
        yaku += "\n国士無双：役満";
        //四喜和(スーシーホー)
        //字一色(ツーイーソー)
        //字牌。
        //九連宝燈(チューレンポートン)
        //一色。1、9が各3牌。2~8が各1牌以上。
        //鳴いていないか、ポン、チー、カン(暗槓も)をしていないか。
        //緑一色(リューイーソー)
        //緑一色。日本プロ麻雀連盟公式ルール改訂により、2023年度以降發無しでも緑一色となる。
        //清老頭(チンロートー)
        //一九のみ。
        //四槓子(スーカンツ)
        //天和(テンホー)/地和(チーホー)
        if (tehaiMahjongTileDataList.Count == 1)
        {
            yakumanNow = true;
            yaku += "\n地和：役満";
        }
    }

    void HuKeisan()
    {
        //符計算　麻雀豆腐　を参考に。
        //点数早見表　麻雀の雀流　を参考に。
        huStr = "\n符の内訳";
        //ピンフツモは一律20符。
        //平和(ピンフ)
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。ツモ。
        if (syuntuCount == 4 && (int)head != 31 && (int)head != 32 && (int)head != 33 && ryanmen)
        {
            hu = 20;
            huStr += "\nピンフツモ：一律２０符";
        }
        //鳴いている、順子が4つ、雀頭が役牌でない、リャンメン待ち。ロン。
        else
        {
            //副底(フーテイ)
            hu = 20;
            huStr += "\n副底：２０符";


            //メンゼンロン
            //ツモ
            hu += 2;
            huStr += "\nツモ：２符";


            //順子は0符。
            //明刻(ミンコ)
            //暗刻(アンコ)
            foreach (List<MahjongTile> mentu in kootuMentu)
            {
                //2~8なら
                if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7) || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16) || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                {
                    hu += 4;
                    huStr += "\n２～８の暗刻：４符";
                }
            }
            //明槓(ミンカン)
            //暗槓(アンカン)

            //明刻(ミンコ)
            //暗刻(アンコ)
            foreach (List<MahjongTile> mentu in kootuMentu)
            {
                //一九字牌なら
                if ((int)mentu[0] == 0 || (int)mentu[0] == 8 || (int)mentu[0] == 9 || (int)mentu[0] == 17 || (int)mentu[0] == 18 || (int)mentu[0] == 26
                    || (int)mentu[0] >= 27)
                {
                    hu += 8;
                    huStr += "\n１・９・字牌の暗刻：８符";
                }
            }
            //明槓(ミンカン)
            //暗槓(アンカン)

            if ((int)head == 31 || (int)head == 32 || (int)head == 33)
            {
                hu += 2;
                huStr += "\n雀頭が役牌：２符";
            }


            if (pentyan)
            {
                hu += 2;
                huStr += "\n辺張待ち：２符";
            }
            else if (kantyan)
            {
                hu += 2;
                huStr += "\n嵌張待ち：２符";
            }
            else if (tanki)
            {
                hu += 2;
                huStr += "\n単騎待ち：２符";
            }

            float huFloat = hu / 10f;
            hu = Mathf.CeilToInt(huFloat);
            hu *= 10;
            huStr += "\n最終：" + hu + "符";
        }
    }

    void MatiCheck()
    {
        ryanmen = false;
        //非対応。
        syanpon = false;
        pentyan = false;
        kantyan = false;
        tanki = false;
        //ノべタンの場合、単騎になると思うため、非対応。
        nobetan = false;

        //新
        //リャンメン。(旧：平和)
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            List<MahjongTile> ryanmenCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            ryanmenCalculationMahjongTileList.Insert(0, tumoMahjongTile);
            List<MahjongTile> ryanmenCalculationMahjongTileListCopy = new List<MahjongTile>(ryanmenCalculationMahjongTileList);
            List<List<MahjongTile>> ryanmenCalculationMentuList = new List<List<MahjongTile>>();

            //頭にならない。
            if (ryanmenCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
            {
                continue;
            }

            //2個削除。頭の分。
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            //萬子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 0) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 1) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 2) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //萬子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 6) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 7) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 8) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //筒子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 9) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 10) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 11) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //筒子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 15) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 16) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 17) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //索子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 18) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 19) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 20) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //索子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 24) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 25) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 26) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));
                ryanmenCalculationMentuList.Add(mentu);
            }

            int count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    ryanmenCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        ryanmenCalculationMentuList.Add(mentu);
                    }
                }
            }
            if (ryanmenCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in ryanmenCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //各2~8だったら。
                            if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7)
                                || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16)
                                || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                            {
                                //各2~8だったら。
                                if (((int)mentu[1] >= 1 && (int)mentu[1] <= 7)
                                    || ((int)mentu[1] >= 10 && (int)mentu[1] <= 16)
                                    || ((int)mentu[1] >= 19 && (int)mentu[1] <= 25))
                                {
                                    ryanmen = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //リスト
            ryanmenCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            //萬子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 0) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 1) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 2) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //萬子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 6) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 7) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 8) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //筒子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 9) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 10) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 11) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //筒子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 15) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 16) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 17) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //索子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 18) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 19) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 20) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));
                ryanmenCalculationMentuList.Add(mentu);
            }
            //索子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 24) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 25) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 26) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));

                List<MahjongTile> mentu = new List<MahjongTile>();
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));
                ryanmenCalculationMentuList.Add(mentu);
            }

            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        ryanmenCalculationMentuList.Add(mentu);
                    }
                }
            }
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    ryanmenCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (ryanmenCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in ryanmenCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //各2~8だったら。
                            if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7)
                                || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16)
                                || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                            {
                                //各2~8だったら。
                                if (((int)mentu[1] >= 1 && (int)mentu[1] <= 7)
                                    || ((int)mentu[1] >= 10 && (int)mentu[1] <= 16)
                                    || ((int)mentu[1] >= 19 && (int)mentu[1] <= 25))
                                {
                                    ryanmen = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //リスト
            ryanmenCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            ryanmenCalculationMahjongTileList.Remove(mahjongTile);
            //萬子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 0) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 1) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 2) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 1));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 2));
            }
            //萬子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 6) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 7) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 8) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 6));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 7));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8));
            }
            //筒子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 9) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 10) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 11) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 10));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 11));
            }
            //筒子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 15) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 16) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 17) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 15));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 16));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17));
            }
            //索子の一、二、三があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 18) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 19) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 20) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 19));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 20));
            }
            //索子の七、八、九があるかぎり
            while (ryanmenCalculationMahjongTileList.Count(item => (int)item == 24) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 25) >= 1
                && ryanmenCalculationMahjongTileList.Count(item => (int)item == 26) >= 1)
            {
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 24));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 25));
                ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26));
            }

            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    ryanmenCalculationMentuList.Add(mentu);

                    count++;

                    if (1 > 0 && 1 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && ryanmenCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        ryanmenCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        ryanmenCalculationMentuList.Add(mentu);
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in ryanmenCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                    ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    ryanmenCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (ryanmenCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in ryanmenCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //各2~8だったら。
                            if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7)
                                || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16)
                                || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                            {
                                //各2~8だったら。
                                if (((int)mentu[1] >= 1 && (int)mentu[1] <= 7)
                                    || ((int)mentu[1] >= 10 && (int)mentu[1] <= 16)
                                    || ((int)mentu[1] >= 19 && (int)ryanmenCalculationMahjongTileList[1] <= 25))
                                {
                                    ryanmen = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        //シャンポン。
        //ペンチャン。
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            List<MahjongTile> pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            pentyanCalculationMahjongTileList.Insert(0, tumoMahjongTile);
            List<MahjongTile> pentyanCalculationMahjongTileListCopy = new List<MahjongTile>(pentyanCalculationMahjongTileList);
            List<List<MahjongTile>> pentyanCalculationMentuList = new List<List<MahjongTile>>();

            //頭にならない。
            if (pentyanCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
            {
                continue;
            }

            //2個削除。頭の分。
            pentyanCalculationMahjongTileList.Remove(mahjongTile);
            pentyanCalculationMahjongTileList.Remove(mahjongTile);

            int count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (pentyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    pentyanCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (pentyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        pentyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        pentyanCalculationMentuList.Add(mentu);
                    }
                }
            }
            if (pentyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in pentyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //どちらかがいずれかの1、9だったら。
                            if (((int)mentu[0] == 0 || (int)mentu[0] == 8
                                || (int)mentu[0] == 9 || (int)mentu[0] == 17
                                || (int)mentu[0] == 18 || (int)mentu[0] == 26)
                                ||
                                ((int)mentu[1] == 0 || (int)mentu[1] == 8
                                || (int)mentu[1] == 9 || (int)mentu[1] == 17
                                || (int)mentu[1] == 18 || (int)mentu[1] == 26))
                            {
                                pentyan = true;
                                break;
                            }
                        }
                    }
                }
            }

            //リスト
            pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            pentyanCalculationMahjongTileList.Remove(mahjongTile);
            pentyanCalculationMahjongTileList.Remove(mahjongTile);

            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (pentyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        pentyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (pentyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (pentyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in pentyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //どちらかがいずれかの1、9だったら。
                            if (((int)mentu[0] == 0 || (int)mentu[0] == 8
                                || (int)mentu[0] == 9 || (int)mentu[0] == 17
                                || (int)mentu[0] == 18 || (int)mentu[0] == 26)
                                ||
                                ((int)mentu[1] == 0 || (int)mentu[1] == 8
                                || (int)mentu[1] == 9 || (int)mentu[1] == 17
                                || (int)mentu[1] == 18 || (int)mentu[1] == 26))
                            {
                                pentyan = true;
                                break;
                            }
                        }
                    }
                }
            }

            //リスト
            pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            pentyanCalculationMahjongTileList.Remove(mahjongTile);
            pentyanCalculationMahjongTileList.Remove(mahjongTile);

            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (pentyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (1 > 0 && 1 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (pentyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && pentyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        pentyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        pentyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in pentyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (pentyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    pentyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (pentyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in pentyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //隣接していたら
                        if ((int)mentu[0] - (int)mentu[1] == 1
                            || (int)mentu[0] - (int)mentu[1] == -1)
                        {
                            //どちらかがいずれかの1、9だったら。
                            if (((int)mentu[0] == 0 || (int)mentu[0] == 8
                                || (int)mentu[0] == 9 || (int)mentu[0] == 17
                                || (int)mentu[0] == 18 || (int)mentu[0] == 26)
                                ||
                                ((int)mentu[1] == 0 || (int)mentu[1] == 8
                                || (int)mentu[1] == 9 || (int)mentu[1] == 17
                                || (int)mentu[1] == 18 || (int)mentu[1] == 26))
                            {
                                pentyan = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //カンチャン。
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            List<MahjongTile> kantyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            kantyanCalculationMahjongTileList.Insert(0, tumoMahjongTile);
            List<MahjongTile> kantyanCalculationMahjongTileListCopy = new List<MahjongTile>(kantyanCalculationMahjongTileList);
            List<List<MahjongTile>> kantyanCalculationMentuList = new List<List<MahjongTile>>();

            //頭にならない。
            if (kantyanCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
            {
                continue;
            }

            //2個削除。頭の分。
            kantyanCalculationMahjongTileList.Remove(mahjongTile);
            kantyanCalculationMahjongTileList.Remove(mahjongTile);

            int count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    kantyanCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        kantyanCalculationMentuList.Add(mentu);
                    }
                }
            }
            if (kantyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in kantyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //一個飛ばしだったら
                        if ((int)mentu[0] - (int)mentu[1] == 2
                            || (int)mentu[0] - (int)mentu[1] == -2)
                        {
                            //上がり牌が二つの牌の間だったら
                            if (((int)mentu[0] - (int)tumoMahjongTile == 1
                                || (int)mentu[0] - (int)tumoMahjongTile == -1)
                                &&
                                ((int)mentu[1] - (int)tumoMahjongTile == 1
                                || (int)mentu[1] - (int)tumoMahjongTile == -1))
                            {
                                kantyan = true;
                                break;
                            }
                        }
                    }
                }
            }

            //リスト
            kantyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            kantyanCalculationMahjongTileList.Remove(mahjongTile);
            kantyanCalculationMahjongTileList.Remove(mahjongTile);

            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        kantyanCalculationMentuList.Add(mentu);
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    kantyanCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (kantyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in kantyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //一個飛ばしだったら
                        if ((int)mentu[0] - (int)mentu[1] == 2
                            || (int)mentu[0] - (int)mentu[1] == -2)
                        {
                            //上がり牌が二つの牌の間だったら
                            if (((int)mentu[0] - (int)tumoMahjongTile == 1
                                || (int)mentu[0] - (int)tumoMahjongTile == -1)
                                &&
                                ((int)mentu[1] - (int)tumoMahjongTile == 1
                                || (int)mentu[1] - (int)tumoMahjongTile == -1))
                            {
                                kantyan = true;
                                break;
                            }
                        }
                    }
                }
            }

            //リスト
            kantyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

            //2個削除。頭の分。
            kantyanCalculationMahjongTileList.Remove(mahjongTile);
            kantyanCalculationMahjongTileList.Remove(mahjongTile);

            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    kantyanCalculationMentuList.Add(mentu);

                    count++;

                    if (1 > 0 && 1 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && kantyanCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        kantyanCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));

                        List<MahjongTile> mentu = new List<MahjongTile>();
                        mentu.Add(syuntuMahjongTile);
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                        kantyanCalculationMentuList.Add(mentu);
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in kantyanCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                    kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                    List<MahjongTile> mentu = new List<MahjongTile>();
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    mentu.Add(kootuMahjongTile);
                    kantyanCalculationMentuList.Add(mentu);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (kantyanCalculationMahjongTileList.Count == 0)
            {
                foreach (List<MahjongTile> mentu in kantyanCalculationMentuList)
                {
                    //刻子の可能性を除去。
                    if (mentu.Count(item => item == tumoMahjongTile) == 1)
                    {
                        mentu.Remove(tumoMahjongTile);
                        //一個飛ばしだったら
                        if ((int)mentu[0] - (int)mentu[1] == 2
                            || (int)mentu[0] - (int)mentu[1] == -2)
                        {
                            //上がり牌が二つの牌の間だったら
                            if (((int)mentu[0] - (int)tumoMahjongTile == 1
                                || (int)mentu[0] - (int)tumoMahjongTile == -1)
                                &&
                                ((int)mentu[1] - (int)tumoMahjongTile == 1
                                || (int)mentu[1] - (int)tumoMahjongTile == -1))
                            {
                                kantyan = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //タンキ
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            List<MahjongTile> tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            tankiCalculationMahjongTileList.Insert(0, tumoMahjongTile);
            List<MahjongTile> tankiCalculationMahjongTileListCopy = new List<MahjongTile>(tankiCalculationMahjongTileList);

            //頭にならない。
            if (tankiCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
            {
                continue;
            }


            if (mahjongTile != tumoMahjongTile)
            {
                continue;
            }

            if (tankiCalculationMahjongTileList.Count(item => item == tumoMahjongTile) >= 2)
            {
                //2個削除。頭の分。
                tankiCalculationMahjongTileList.Remove(mahjongTile);
                tankiCalculationMahjongTileList.Remove(mahjongTile);
            }
            else
            {
                continue;
            }

            int count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (tankiCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (tankiCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        tankiCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                    }
                }
            }
            if (tankiCalculationMahjongTileList.Count == 0)
            {
                tanki = true;
                break;
            }

            //リスト
            tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            tankiCalculationMahjongTileList.Insert(0, tumoMahjongTile);

            //2個削除。頭の分。
            tankiCalculationMahjongTileList.Remove(mahjongTile);
            tankiCalculationMahjongTileList.Remove(mahjongTile);

            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (tankiCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        tankiCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (tankiCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (tankiCalculationMahjongTileList.Count == 0)
            {
                tanki = true;
                break;
            }

            //リスト
            tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            tankiCalculationMahjongTileList.Insert(0, tumoMahjongTile);

            //2個削除。頭の分。
            tankiCalculationMahjongTileList.Remove(mahjongTile);
            tankiCalculationMahjongTileList.Remove(mahjongTile);

            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (tankiCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (1 > 0 && 1 == count)
                    {
                        break;
                    }
                }
            }
            //順子の削除。
            foreach (MahjongTile syuntuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //1~7なら
                if ((int)syuntuMahjongTile <= 6 || ((int)syuntuMahjongTile >= 9 && (int)syuntuMahjongTile <= 15)
                    || ((int)syuntuMahjongTile >= 18 && (int)syuntuMahjongTile <= 24))
                {
                    //3個並んでいれば
                    if (tankiCalculationMahjongTileList.Count(item => item == syuntuMahjongTile) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 1) >= 1
                        && tankiCalculationMahjongTileList.Count(item => (int)item == (int)syuntuMahjongTile + 2) >= 1)
                    {
                        //3個削除。
                        tankiCalculationMahjongTileList.Remove(syuntuMahjongTile);
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 1));
                        tankiCalculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)syuntuMahjongTile + 2));
                    }
                }
            }
            count = 0;
            //刻子の削除。
            foreach (MahjongTile kootuMahjongTile in tankiCalculationMahjongTileListCopy)
            {
                //3個以上あれば
                if (tankiCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                {
                    //3個削除。
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);
                    tankiCalculationMahjongTileList.Remove(kootuMahjongTile);

                    count++;

                    if (0 > 0 && 0 == count)
                    {
                        break;
                    }
                }
            }
            if (tankiCalculationMahjongTileList.Count == 0)
            {
                tanki = true;
                break;
            }
        }
    }

    public void ScoreCalculation(bool ai)
    {
        //得点計算開始。
        //最高翻数。
        highHansuu = 0;
        //最高翻数時の役。
        highYaku = "";
        //最高符。
        highHu = 0;
        //最高符内訳。
        highHuStr = "";

        //リスト
        if (!ai)
        {
            calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        }
        else
        {
            calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
        }
        calculationMahjongTileList.Add(tumoMahjongTile);
        CalculationListSort();
        mentuMahjongTileList = new List<List<MahjongTile>>();
        //HashSetを作成。HashSetは重複しない。
        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        //HashSetを作成。HashSetは重複しない。
        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();

        yakuman = false;

        //全ての役を頭ととらえ考える。
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            if (!ai)
            {
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            }
            else
            {
                calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
            }
            calculationMahjongTileList.Add(tumoMahjongTile);
            CalculationListSort();
            //HashSetを作成。HashSetは重複しない。
            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();

            kootuMentu = new List<List<MahjongTile>>();
            syuntuMentu = new List<List<MahjongTile>>();

            //現在の牌が2個以上あれば
            if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
            {
                head = mahjongTile;

                //2個削除。頭の分。
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                //刻子の個数。同時に使った牌の削除。
                kootuCount = KootuCount(0);
                //順子の個数。同時に使った牌の削除。
                syuntuCount = SyuntuCount();
                if (calculationMahjongTileList.Count == 0)
                {
                    MatiCheck();

                    //翻数カウント
                    HansuuCount(ai);

                    //符計算
                    HuKeisan();

                    //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                    //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                    if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                        || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                    {
                        //役満の設定。
                        yakuman = yakumanNow;
                        //最高翻数を更新。
                        highHansuu = hansuu;
                        //最高翻数時の役を更新。
                        highYaku = yaku;

                        highHu = hu;
                        highHuStr = huStr;
                    }
                }

                //リスト
                if (!ai)
                {
                    calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                }
                else
                {
                    calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
                }
                calculationMahjongTileList.Add(tumoMahjongTile);
                CalculationListSort();
                mentuMahjongTileList = new List<List<MahjongTile>>();

                kootuMentu = new List<List<MahjongTile>>();
                syuntuMentu = new List<List<MahjongTile>>();

                //2個削除。頭の分。
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                //順子の個数。同時に使った牌の削除。
                syuntuCount = SyuntuCount();
                //刻子の個数。同時に使った牌の削除。
                kootuCount = KootuCount(0);
                if (calculationMahjongTileList.Count == 0)
                {
                    MatiCheck();

                    //翻数カウント
                    HansuuCount(ai);

                    //符計算
                    HuKeisan();

                    //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                    //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                    if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                        || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                    {
                        //役満の設定。
                        yakuman = yakumanNow;
                        //最高翻数を更新。
                        highHansuu = hansuu;
                        //最高翻数時の役を更新。
                        highYaku = yaku;

                        highHu = hu;
                        highHuStr = huStr;
                    }
                }

                //リスト
                if (!ai)
                {
                    calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                }
                else
                {
                    calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
                }
                calculationMahjongTileList.Add(tumoMahjongTile);
                CalculationListSort();
                mentuMahjongTileList = new List<List<MahjongTile>>();

                kootuMentu = new List<List<MahjongTile>>();
                syuntuMentu = new List<List<MahjongTile>>();

                //2個削除。頭の分。
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                //刻子の個数。同時に使った牌の削除。1個だけ取り出す。
                kootuCount = KootuCount(1);
                //順子の個数。同時に使った牌の削除。
                syuntuCount = SyuntuCount();
                //刻子の個数。同時に使った牌の削除。
                kootuCount += KootuCount(0);
                if (calculationMahjongTileList.Count == 0)
                {
                    MatiCheck();

                    //翻数カウント
                    HansuuCount(ai);

                    //符計算
                    HuKeisan();

                    //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                    //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                    if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                        || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                    {
                        //役満の設定。
                        yakuman = yakumanNow;
                        //最高翻数を更新。
                        highHansuu = hansuu;
                        //最高翻数時の役を更新。
                        highYaku = yaku;

                        highHu = hu;
                        highHuStr = huStr;
                    }
                }
            }
        }

        if (highHansuu < 1)
        {
            bool tiitoitu = false;

            //リスト
            if (!ai)
            {
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            }
            else
            {
                calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
            }
            calculationMahjongTileList.Add(tumoMahjongTile);
            CalculationListSort();
            //HashSetを作成。HashSetは重複しない。
            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

            //全ての役を頭ととらえ考える。
            foreach (MahjongTile toitu in calculationMahjongTileHashSet)
            {
                if (calculationMahjongTileList.Count(item => item == toitu) >= 2)
                {
                    //2個削除。
                    calculationMahjongTileList.Remove(toitu);
                    calculationMahjongTileList.Remove(toitu);
                }
            }

            //全ての牌が2こずつあったら。
            if (calculationMahjongTileList.Count == 0)
            {
                tiitoitu = true;
            }

            if (tiitoitu)
            {
                TiitoituHansuuCount(ai);

                //役満の設定。
                yakuman = yakumanNow;
                //最高翻数を更新。
                highHansuu = hansuu;
                //最高翻数時の役を更新。
                highYaku = yaku;

                //七対子は一律25符。
                highHu = 25;

                huStr = "\n符の内訳";
                huStr += "\n七対子：一律２５符";
            }
            else
            {
                bool kokusi = false;

                //リスト
                if (!ai)
                {
                    calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                }
                else
                {
                    calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
                }
                calculationMahjongTileList.Add(tumoMahjongTile);
                CalculationListSort();
                //HashSetを作成。HashSetは重複しない。
                calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

                //一九字牌が各一個以上。
                if (calculationMahjongTileList.Count(item => (int)item == 0) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 8) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 9) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 17) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 18) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 26) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 27) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 28) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 29) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 30) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 31) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 32) >= 1
                    && calculationMahjongTileList.Count(item => (int)item == 33) >= 1)
                {
                    //全て足したのが14なら
                    if (calculationMahjongTileList.Count(item => (int)item == 0)
                        + calculationMahjongTileList.Count(item => (int)item == 8)
                        + calculationMahjongTileList.Count(item => (int)item == 9)
                        + calculationMahjongTileList.Count(item => (int)item == 17)
                        + calculationMahjongTileList.Count(item => (int)item == 18)
                        + calculationMahjongTileList.Count(item => (int)item == 26)
                        + calculationMahjongTileList.Count(item => (int)item == 27)
                        + calculationMahjongTileList.Count(item => (int)item == 28)
                        + calculationMahjongTileList.Count(item => (int)item == 29)
                        + calculationMahjongTileList.Count(item => (int)item == 30)
                        + calculationMahjongTileList.Count(item => (int)item == 31)
                        + calculationMahjongTileList.Count(item => (int)item == 32)
                        + calculationMahjongTileList.Count(item => (int)item == 33) == 14)
                    {
                        kokusi = true;
                    }
                }

                if (kokusi)
                {
                    KokusiHansuuCount(ai);

                    //役満の設定。
                    yakuman = yakumanNow;
                    //最高翻数を更新。
                    highHansuu = hansuu;
                    //最高翻数時の役を更新。
                    highYaku = yaku;
                }
            }
        }
    }

    //牌を拾う方
    void Tumo()
    {
        tumoMahjongTile = tumoMahjongTileList[0];
        tumoMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)tumoMahjongTile];
        tumoMahjongTileList.Remove(tumoMahjongTileList[0]);
        tumoMahjongTileListCountText.text = "残り" + tumoMahjongTileList.Count.ToString() + "牌";

        if (tumoMahjongTileList.Count == 0)
        {
            DataSet();
            endObj.SetActive(true);
        }

        //コピーしないと、tehaiMahjongTileListを常に参照してしまうため消えてしまう。
        List<MahjongTile> tehaiMahjongTileListCopy = new List<MahjongTile>(tehaiMahjongTileList);
        tehaiMahjongTileDataList.Add(tehaiMahjongTileListCopy);
        tumoMahjongTileDataList.Add(tumoMahjongTile);
        syantenSuuDataList.Add(SyantenSuu(false, true));

        if (!aiAgari)
        {
            //コピーしないと、tehaiMahjongTileListを常に参照してしまうため消えてしまう。
            tehaiMahjongTileListCopy = new List<MahjongTile>(aiTehaiMahjongTileList);
            aiTehaiMahjongTileDataList.Add(tehaiMahjongTileListCopy);
            aiTumoMahjongTileDataList.Add(tumoMahjongTile);
            aiSyantenSuuDataList.Add(SyantenSuu(true, true));
        }


        ScoreCalculation(false);
        if (highHansuu > 0)
        {
            Agari();
        }
        ScoreCalculation(true);
        if (highHansuu > 0)
        {
            AiAgari();
        }
    }

    public void Suteru()
    {
        if (selectMahjongTile != MahjongTile.NoSelect && !agari && tumoMahjongTileList.Count > 0)
        {
            suteMahjongTileDataList.Add(selectMahjongTile);

            if (tehaiMahjongTileList.Contains(selectMahjongTile))
            {
                tehaiMahjongTileList.Remove(selectMahjongTile);
                tehaiMahjongTileList.Add(tumoMahjongTile);

                TehaiMahjongTileImageListUpdate();
            }

            AiSuteru();

            selectMahjongTile = MahjongTile.NoSelect;
            selectMahjongTileImage.sprite = DataManager.noImage;

            Tumo();
        }
        //!hanabi=一回目だけ反応したい。
        else if (agari && !hanabi)
        {
            AgariKakutei();
        }
    }

    void AiSuteru()
    {
        if (!aiAgari)
        {
            HaiKourituCalculation();

            //今の手牌+ツモ牌を設定。
            calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
            calculationMahjongTileList.Add(tumoMahjongTile);
            CalculationListSort();
            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileHashSet);

            int maxNum = kourituList.Max();
            int maxNumNum = kourituList.IndexOf(maxNum);

            aiSuteMahjongTileDataList.Add(calculationMahjongTileList[maxNumNum]);

            if (calculationMahjongTileList[maxNumNum] != tumoMahjongTile)
            {
                aiTehaiMahjongTileList.Remove(calculationMahjongTileList[maxNumNum]);
                aiTehaiMahjongTileList.Add(tumoMahjongTile);
            }

            AiTehaiMahjongTileImageListUpdate();
        }
    }

    //おそらくまだ不十分。要修正。
    int SyantenSuu(bool ai, bool autoListSet)
    {
        if (autoListSet)
        {
            //リスト
            if (!ai)
            {
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            }
            else
            {
                calculationMahjongTileList = new List<MahjongTile>(aiTehaiMahjongTileList);
            }
            calculationMahjongTileList.Add(tumoMahjongTile);
            //Debug.Log("calculationMahjongTileList.Count" + calculationMahjongTileList.Count + "tumoMahjongTile" + tumoMahjongTile);
            CalculationListSort();
        }
        //Copyを作成。
        List<MahjongTile> calculationMahjongTileListCopy = new List<MahjongTile>(calculationMahjongTileList);
        //Copyを作成。完全に変更しない。
        List<MahjongTile> calculationMahjongTileListCopyCopy = new List<MahjongTile>(calculationMahjongTileList);
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            //面子
            //1
            if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
            {
                //孤立牌なら
                if (!calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2))
                    && calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //2
            else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
            {
                //孤立牌なら
                if (!calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2))
                    && calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //9
            else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
            {
                //孤立牌なら
                if (!calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                    && calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //8
            else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
            {
                //孤立牌なら
                if (!calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                    && calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //字牌
            else if ((int)mahjongTile >= 27)
            {
                //孤立牌なら
                if (calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //その他。3～7
            else
            {
                //孤立牌なら
                if (!calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1))
                    && !calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2))
                    && calculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    //Debug.Log(mahjongTile + "は孤立牌です");
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
        }
        //Debug.Log("calculationMahjongTileListの個数を" + calculationMahjongTileListCopy.Count + "から" + calculationMahjongTileList.Count + "まで減らしました");
        //Copyを作成。
        calculationMahjongTileListCopy = new List<MahjongTile>(calculationMahjongTileList);
        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();

        int highMentuCount = 0;
        List<List<MahjongTile>> highMentu = new List<List<MahjongTile>>();
        int toituCount = 0;
        List<MahjongTile> toituMahjongTileList = new List<MahjongTile>();
        int taatuCount = 0;
        List<MahjongTile> taatuMahjongTileList = new List<MahjongTile>();

        //雀頭があるか。
        bool headSet = false;

        int syantensuu = 0;

        int highSyantensuu = 100;

        //4面子1雀頭。
        //雀頭を取る。面子を取れるだけ取る。対子を取れるだけ取る。塔子を取れるだけ取る。
        foreach (MahjongTile headKatei in calculationMahjongTileListCopy)
        {
            highMentuCount = 0;
            highMentu = new List<List<MahjongTile>>();
            toituCount = 0;
            toituMahjongTileList = new List<MahjongTile>();
            taatuCount = 0;
            taatuMahjongTileList = new List<MahjongTile>();

            //雀頭があるか。
            headSet = false;

            syantensuu = 0;

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            if (calculationMahjongTileList.Count(item => item == headKatei) <= 1)
            {
                continue;
            }
            else
            {
                headSet = true;
                toituCount++;
                toituMahjongTileList.Add(headKatei);
                toituMahjongTileList.Add(headKatei);
            }
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();

            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);
            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();

            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();
            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();


            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(1);
            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();
            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }


            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);
            //面子に使った牌の削除。
            foreach (List<MahjongTile> mentu in highMentu)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //対子。
            foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
            {
                if (highMentuCount + toituCount == 5)
                {
                    break;
                }
                //自身がいなければcontinue
                if (!calculationMahjongTileList.Contains(mahjongTile))
                {
                    continue;
                }

                if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                {
                    toituCount++;
                    toituMahjongTileList.Add(mahjongTile);
                    toituMahjongTileList.Add(mahjongTile);
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove(mahjongTile);

                    headSet = true;
                }
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            //面子に使った牌の削除。
            foreach (List<MahjongTile> mentu in highMentu)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //対子に使った牌の削除。
            //ここで雀頭も削除。
            foreach (MahjongTile mahjongTile in toituMahjongTileList)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
            //塔子。
            foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
            {
                if (highMentuCount + toituCount + taatuCount == 5)
                {
                    break;
                }
                //自身がいなければcontinue
                if (!calculationMahjongTileList.Contains(mahjongTile))
                {
                    continue;
                }

                //1
                if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
                //2
                else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //リャンメン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
                //9
                else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                }
                //8
                else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //リャンメン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                }
                //字牌
                else if ((int)mahjongTile >= 27)
                {
                }
                //その他。3～7
                else
                {
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
            }

            syantensuu = 8 - (highMentuCount * 2 + toituCount + taatuCount);
            if (highMentuCount + toituCount + taatuCount == 5 && !headSet)
            {
                syantensuu++;
            }
            //Debug.Log("面子数" + highMentuCount + "/対子数" + toituCount + "/塔子数" + taatuCount + "/向聴数" + syantensuu);
            if (syantensuu < highSyantensuu)
            {
                highSyantensuu = syantensuu;
            }
        }

        //雀頭を取る。面子を取れるだけ取る。対子を取れるだけ取る。塔子を取れるだけ取る。
        highMentuCount = 0;
        highMentu = new List<List<MahjongTile>>();
        toituCount = 0;
        toituMahjongTileList = new List<MahjongTile>();
        taatuCount = 0;
        taatuMahjongTileList = new List<MahjongTile>();

        //雀頭があるか。
        headSet = false;

        syantensuu = 0;

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();

        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);
        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();

        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();
        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();


        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(1);
        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();
        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }


        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
        //面子に使った牌の削除。
        foreach (List<MahjongTile> mentu in highMentu)
        {
            foreach (MahjongTile mahjongTile in mentu)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
        }
        //対子。
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            if (highMentuCount + toituCount == 5)
            {
                break;
            }
            //自身がいなければcontinue
            if (!calculationMahjongTileList.Contains(mahjongTile))
            {
                continue;
            }

            if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
            {
                toituCount++;
                toituMahjongTileList.Add(mahjongTile);
                toituMahjongTileList.Add(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                headSet = true;
            }
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
        //面子に使った牌の削除。
        foreach (List<MahjongTile> mentu in highMentu)
        {
            foreach (MahjongTile mahjongTile in mentu)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
        }
        //対子に使った牌の削除。
        foreach (MahjongTile mahjongTile in toituMahjongTileList)
        {
            calculationMahjongTileList.Remove(mahjongTile);
        }
        //塔子。
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            if (highMentuCount + toituCount + taatuCount == 5)
            {
                break;
            }
            //自身がいなければcontinue
            if (!calculationMahjongTileList.Contains(mahjongTile))
            {
                continue;
            }

            //1
            if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
            //2
            else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //リャンメン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
            //9
            else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
            }
            //8
            else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //リャンメン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
            }
            //字牌
            else if ((int)mahjongTile >= 27)
            {
            }
            //その他。3～7
            else
            {
                //リャンメン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
        }

        syantensuu = 8 - (highMentuCount * 2 + toituCount + taatuCount);
        if (highMentuCount + toituCount + taatuCount == 5 && !headSet)
        {
            syantensuu++;
        }
        //Debug.Log("面子数" + highMentuCount + "/対子数" + toituCount + "/塔子数" + taatuCount + "/向聴数" + syantensuu);
        if (syantensuu < highSyantensuu)
        {
            highSyantensuu = syantensuu;
        }



        //逆向きにSort
        //孤立牌はもうすでに除去されている。
        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
        CalculationListReverseSort();
        calculationMahjongTileListCopy = new List<MahjongTile>(calculationMahjongTileList);
        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileListCopy);

        //4面子1雀頭。
        //雀頭を取る。面子を取れるだけ取る。対子を取れるだけ取る。塔子を取れるだけ取る。
        foreach (MahjongTile headKatei in calculationMahjongTileListCopy)
        {
            highMentuCount = 0;
            highMentu = new List<List<MahjongTile>>();
            toituCount = 0;
            toituMahjongTileList = new List<MahjongTile>();
            taatuCount = 0;
            taatuMahjongTileList = new List<MahjongTile>();

            //雀頭があるか。
            headSet = false;

            syantensuu = 0;

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            if (calculationMahjongTileList.Count(item => item == headKatei) <= 1)
            {
                continue;
            }
            else
            {
                headSet = true;
                toituCount++;
                toituMahjongTileList.Add(headKatei);
                toituMahjongTileList.Add(headKatei);
            }
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();

            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);
            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();

            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();
            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);

            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            mentuMahjongTileList = new List<List<MahjongTile>>();


            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(1);
            //順子の個数。同時に使った牌の削除。
            syuntuCount = SyuntuCount();
            //刻子の個数。同時に使った牌の削除。
            kootuCount = KootuCount(0);

            if (mentuMahjongTileList.Count > highMentuCount)
            {
                highMentuCount = mentuMahjongTileList.Count;
                highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
            }


            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            calculationMahjongTileList.Remove(headKatei);
            calculationMahjongTileList.Remove(headKatei);
            //面子に使った牌の削除。
            foreach (List<MahjongTile> mentu in highMentu)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //対子。
            foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
            {
                if (highMentuCount + toituCount == 5)
                {
                    break;
                }
                //自身がいなければcontinue
                if (!calculationMahjongTileList.Contains(mahjongTile))
                {
                    continue;
                }

                if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                {
                    toituCount++;
                    toituMahjongTileList.Add(mahjongTile);
                    toituMahjongTileList.Add(mahjongTile);
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove(mahjongTile);

                    headSet = true;
                }
            }

            calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
            //面子に使った牌の削除。
            foreach (List<MahjongTile> mentu in highMentu)
            {
                foreach (MahjongTile mahjongTile in mentu)
                {
                    calculationMahjongTileList.Remove(mahjongTile);
                }
            }
            //対子に使った牌の削除。
            //ここで雀頭も削除。
            foreach (MahjongTile mahjongTile in toituMahjongTileList)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
            //塔子。
            foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
            {
                if (highMentuCount + toituCount + taatuCount == 5)
                {
                    break;
                }
                //自身がいなければcontinue
                if (!calculationMahjongTileList.Contains(mahjongTile))
                {
                    continue;
                }

                //1
                if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
                //2
                else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //リャンメン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
                //9
                else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                }
                //8
                else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
                {
                    //ペンチャン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //リャンメン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                }
                //字牌
                else if ((int)mahjongTile >= 27)
                {
                }
                //その他。3～7
                else
                {
                    //リャンメン
                    if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //カンチャン
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    }
                    else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                    {
                        taatuCount++;
                        taatuMahjongTileList.Add(mahjongTile);
                        taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                        calculationMahjongTileList.Remove(mahjongTile);
                        calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
            }

            syantensuu = 8 - (highMentuCount * 2 + toituCount + taatuCount);
            if (highMentuCount + toituCount + taatuCount == 5 && !headSet)
            {
                syantensuu++;
            }
            //Debug.Log("面子数" + highMentuCount + "/対子数" + toituCount + "/塔子数" + taatuCount + "/向聴数" + syantensuu);
            if (syantensuu < highSyantensuu)
            {
                highSyantensuu = syantensuu;
            }
        }

        //雀頭を取る。面子を取れるだけ取る。対子を取れるだけ取る。塔子を取れるだけ取る。
        highMentuCount = 0;
        highMentu = new List<List<MahjongTile>>();
        toituCount = 0;
        toituMahjongTileList = new List<MahjongTile>();
        taatuCount = 0;
        taatuMahjongTileList = new List<MahjongTile>();

        //雀頭があるか。
        headSet = false;

        syantensuu = 0;

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();

        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);
        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();

        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();
        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);

        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        mentuMahjongTileList = new List<List<MahjongTile>>();


        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(1);
        //順子の個数。同時に使った牌の削除。
        syuntuCount = SyuntuCount();
        //刻子の個数。同時に使った牌の削除。
        kootuCount = KootuCount(0);

        if (mentuMahjongTileList.Count > highMentuCount)
        {
            highMentuCount = mentuMahjongTileList.Count;
            highMentu = new List<List<MahjongTile>>(mentuMahjongTileList);
        }


        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
        //面子に使った牌の削除。
        foreach (List<MahjongTile> mentu in highMentu)
        {
            foreach (MahjongTile mahjongTile in mentu)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
        }
        //対子。
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            if (highMentuCount + toituCount == 5)
            {
                break;
            }
            //自身がいなければcontinue
            if (!calculationMahjongTileList.Contains(mahjongTile))
            {
                continue;
            }

            if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
            {
                toituCount++;
                toituMahjongTileList.Add(mahjongTile);
                toituMahjongTileList.Add(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);
                calculationMahjongTileList.Remove(mahjongTile);

                headSet = true;
            }
        }

        calculationMahjongTileList = new List<MahjongTile>(calculationMahjongTileListCopy);
        //面子に使った牌の削除。
        foreach (List<MahjongTile> mentu in highMentu)
        {
            foreach (MahjongTile mahjongTile in mentu)
            {
                calculationMahjongTileList.Remove(mahjongTile);
            }
        }
        //対子に使った牌の削除。
        foreach (MahjongTile mahjongTile in toituMahjongTileList)
        {
            calculationMahjongTileList.Remove(mahjongTile);
        }
        //塔子。
        foreach (MahjongTile mahjongTile in calculationMahjongTileListCopy)
        {
            if (highMentuCount + toituCount + taatuCount == 5)
            {
                break;
            }
            //自身がいなければcontinue
            if (!calculationMahjongTileList.Contains(mahjongTile))
            {
                continue;
            }

            //1
            if ((int)mahjongTile == 0 || (int)mahjongTile == 9 || (int)mahjongTile == 18)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
            //2
            else if ((int)mahjongTile == 1 || (int)mahjongTile == 10 || (int)mahjongTile == 19)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //リャンメン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
            //9
            else if ((int)mahjongTile == 8 || (int)mahjongTile == 17 || (int)mahjongTile == 26)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
            }
            //8
            else if ((int)mahjongTile == 7 || (int)mahjongTile == 16 || (int)mahjongTile == 25)
            {
                //ペンチャン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //リャンメン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
            }
            //字牌
            else if ((int)mahjongTile >= 27)
            {
            }
            //その他。3～7
            else
            {
                //リャンメン
                if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 1));
                }
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //カンチャン
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile - 2));
                }
                else if (calculationMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2)))
                {
                    taatuCount++;
                    taatuMahjongTileList.Add(mahjongTile);
                    taatuMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    calculationMahjongTileList.Remove(mahjongTile);
                    calculationMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
        }

        syantensuu = 8 - (highMentuCount * 2 + toituCount + taatuCount);
        if (highMentuCount + toituCount + taatuCount == 5 && !headSet)
        {
            syantensuu++;
        }
        //Debug.Log("面子数" + highMentuCount + "/対子数" + toituCount + "/塔子数" + taatuCount + "/向聴数" + syantensuu);
        if (syantensuu < highSyantensuu)
        {
            highSyantensuu = syantensuu;
        }

        //国士無双
        bool kokusiHeadSet = false;
        syantensuu = 13;

        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 0)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 0) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 8)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 8) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 9)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 9) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 17)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 17) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 18)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 18) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 26)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 26) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 27)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 27) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 28)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 28) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 29)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 29) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 30)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 30) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 31)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 31) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 32)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 32) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }
        if (calculationMahjongTileListCopyCopy.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), 33)))
        {
            syantensuu--;
            if (!kokusiHeadSet && calculationMahjongTileListCopyCopy.Count(item => (int)item == 33) >= 2)
            {
                kokusiHeadSet = true;
                syantensuu--;
            }
        }

        if (syantensuu < highSyantensuu)
        {
            highSyantensuu = syantensuu;
        }

        //七対子
        syantensuu = 6;
        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileListCopyCopy);

        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            if (calculationMahjongTileListCopyCopy.Count(item => item == mahjongTile) >= 2)
            {
                syantensuu--;
            }
        }

        if (syantensuu < highSyantensuu)
        {
            highSyantensuu = syantensuu;
        }
        return highSyantensuu;
    }

    public void Reset()
    {
        selectMahjongTile = MahjongTile.NoSelect;
        selectMahjongTileImage.sprite = DataManager.noImage;

        endObj.SetActive(false);

        List<MahjongTile> mahjongTileList = new List<MahjongTile>(allMahjongTileList);
        List<MahjongTile> mahjongTileRndList = new List<MahjongTile>();


        tehaiMahjongTileList = new List<MahjongTile>();
        aiTehaiMahjongTileList = new List<MahjongTile>();
        //Debug用。使わない時は0==1にする。手牌を直接設定する。
        if (0 == 1)
        {
            //1
            MahjongTile mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 0);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //2
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 1);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //3
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 1);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //4
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 2);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //5
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 2);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //6
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 3);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //7
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 4);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //8
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 4);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //9
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 4);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //10
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 6);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //11
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 7);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //12
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 8);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
            //13
            mahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), 8);
            tehaiMahjongTileList.Add(mahjongTile);
            aiTehaiMahjongTileList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
        }
        else
        {
            //手牌設定。
            for (int i = 0; i < 13; i++)
            {
                MahjongTile mahjongTile = mahjongTileList[UnityEngine.Random.Range(0, mahjongTileList.Count)];
                tehaiMahjongTileList.Add(mahjongTile);
                aiTehaiMahjongTileList.Add(mahjongTile);
                mahjongTileList.Remove(mahjongTile);
            }
        }
        TehaiMahjongTileImageListUpdate();
        AiTehaiMahjongTileImageListUpdate();

        //ツモ牌設定
        for (int i = 0; i < 17; i++)
        {
            MahjongTile mahjongTile = mahjongTileList[UnityEngine.Random.Range(0, mahjongTileList.Count)];
            mahjongTileRndList.Add(mahjongTile);
            mahjongTileList.Remove(mahjongTile);
        }

        tumoMahjongTileList = new List<MahjongTile>(mahjongTileRndList);

        agari = false;
        aiAgari = false;

        agariTextObj.SetActive(false);
        aiAgariTextObj.SetActive(false);

        agariScore.text = "";
        aiAgariScore.text = "";

        zyun = 0;
        tehaiMahjongTileDataList = new List<List<MahjongTile>>();
        tumoMahjongTileDataList = new List<MahjongTile>();
        suteMahjongTileDataList = new List<MahjongTile>();
        syantenSuuDataList = new List<int>();
        aiTehaiMahjongTileDataList = new List<List<MahjongTile>>();
        aiTumoMahjongTileDataList = new List<MahjongTile>();
        aiSuteMahjongTileDataList = new List<MahjongTile>();
        aiSyantenSuuDataList = new List<int>();

        suteruText.text = "捨てる";
        hanabi = false;

        Tumo();
    }

    void Agari()
    {
        agari = true;

        suteruText.text = "ツモ";

        audioSource.PlayOneShot(gameClearSound);
    }

    void AgariKakutei()
    {
        agariTextObj.SetActive(true);

        ScoreCalculation(false);
        if (!yakuman)
        {
            agariScore.text = highHansuu.ToString() + "翻" + highHu.ToString() + "符";
        }
        else
        {
            agariScore.text = "役満";
        }
        DataSet();
        endObj.SetActive(true);

        StartCoroutine(Hanabi());
    }

    void AiAgari()
    {
        aiAgari = true;

        if (!yakuman)
        {
            aiAgariScore.text = highHansuu.ToString() + "翻" + highHu.ToString() + "符";
        }
        else
        {
            aiAgariScore.text = "役満";
        }
        aiAgariTextObj.SetActive(true);
    }

    void DataSet()
    {
        //現在のdataPlayParent.childCount=5
        for (int i = 0; i < 5; i++)
        {
            int zyunCopy = zyun + i;
            //Play
            if (zyunCopy < tehaiMahjongTileDataList.Count)
            {
                GameObject nowData = dataPlayParent.GetChild(i).gameObject;
                HaiKourituData haiKourituData = nowData.GetComponent<HaiKourituData>();
                for (int j = 0; j < 13; j++)
                {
                    haiKourituData.images[j].sprite = DataManager.mahjongTileImages[(int)tehaiMahjongTileDataList[zyunCopy][j]];
                }
                haiKourituData.tumohaiImage.sprite = DataManager.mahjongTileImages[(int)tumoMahjongTileDataList[zyunCopy]];
                haiKourituData.zyunText.text = (zyunCopy + 1).ToString() + "巡";
                if (syantenSuuDataList[zyunCopy] == -1)
                {
                    haiKourituData.text.text = "和了";
                }
                else if (syantenSuuDataList[zyunCopy] == 0)
                {
                    haiKourituData.text.text = "聴牌";
                }
                else
                {
                    haiKourituData.text.text = syantenSuuDataList[zyunCopy].ToString() + "向聴";
                }
                if (zyunCopy < tehaiMahjongTileDataList.Count - 1)
                {
                    haiKourituData.sutehaiImage.sprite = DataManager.mahjongTileImages[(int)suteMahjongTileDataList[zyunCopy]];
                }
                else
                {
                    haiKourituData.sutehaiImage.sprite = DataManager.noImage;
                }
            }
            else
            {
                GameObject nowData = dataPlayParent.GetChild(i).gameObject;
                HaiKourituData haiKourituData = nowData.GetComponent<HaiKourituData>();
                for (int j = 0; j < 13; j++)
                {
                    haiKourituData.images[j].sprite = DataManager.noImage;
                }
                haiKourituData.tumohaiImage.sprite = DataManager.noImage;
                haiKourituData.zyunText.text = (zyunCopy + 1).ToString() + "巡";
                haiKourituData.text.text = "";
                haiKourituData.sutehaiImage.sprite = DataManager.noImage;
            }
            //Ai
            if (zyunCopy < aiTehaiMahjongTileDataList.Count)
            {
                GameObject nowData = dataAiParent.GetChild(i).gameObject;
                HaiKourituData haiKourituData = nowData.GetComponent<HaiKourituData>();
                for (int j = 0; j < 13; j++)
                {
                    haiKourituData.images[j].sprite = DataManager.mahjongTileImages[(int)aiTehaiMahjongTileDataList[zyunCopy][j]];
                }
                haiKourituData.tumohaiImage.sprite = DataManager.mahjongTileImages[(int)aiTumoMahjongTileDataList[zyunCopy]];
                haiKourituData.zyunText.text = (zyunCopy + 1).ToString() + "巡";
                if (aiSyantenSuuDataList[zyunCopy] == -1)
                {
                    haiKourituData.text.text = "和了";
                }
                else if (aiSyantenSuuDataList[zyunCopy] == 0)
                {
                    haiKourituData.text.text = "聴牌";
                }
                else
                {
                    haiKourituData.text.text = aiSyantenSuuDataList[zyunCopy].ToString() + "向聴";
                }
                if (zyunCopy < aiTehaiMahjongTileDataList.Count - 1)
                {
                    haiKourituData.sutehaiImage.sprite = DataManager.mahjongTileImages[(int)aiSuteMahjongTileDataList[zyunCopy]];
                }
                else
                {
                    haiKourituData.sutehaiImage.sprite = DataManager.noImage;
                }
            }
            else
            {
                GameObject nowData = dataAiParent.GetChild(i).gameObject;
                HaiKourituData haiKourituData = nowData.GetComponent<HaiKourituData>();
                for (int j = 0; j < 13; j++)
                {
                    haiKourituData.images[j].sprite = DataManager.noImage;
                }
                haiKourituData.tumohaiImage.sprite = DataManager.noImage;
                haiKourituData.zyunText.text = (zyunCopy + 1).ToString() + "巡";
                haiKourituData.text.text = "";
                haiKourituData.sutehaiImage.sprite = DataManager.noImage;
            }
        }
    }

    public void Down()
    {
        zyun -= 1;
        if (zyun < 0)
        {
            zyun = 0;
        }
        DataSet();
    }
    public void Up()
    {
        zyun += 1;
        //現在のdataPlayParent.childCount=5
        if (zyun > tehaiMahjongTileDataList.Count - 5)
        {
            zyun = tehaiMahjongTileDataList.Count - 5;
        }
        DataSet();
    }


    IEnumerator Hanabi()
    {
        hanabi = true;

        int hanabiCount = 0;
        if (yakuman)
        {
            hanabiCount = 35;
        }
        else if (highHansuu == 1)
        {
            hanabiCount = 10;
        }
        else if (highHansuu == 2)
        {
            hanabiCount = 15;
        }
        else if (highHansuu == 3)
        {
            hanabiCount = 17;
        }
        else if (highHansuu == 4 || highHansuu == 5)
        {
            hanabiCount = 18;
        }
        else if (highHansuu == 6 || highHansuu == 7)
        {
            hanabiCount = 22;
        }
        else if (highHansuu == 8 || highHansuu == 9 || highHansuu == 10)
        {
            hanabiCount = 25;
        }
        else if (highHansuu == 11 || highHansuu == 12)
        {
            hanabiCount = 28;
        }
        else
        {
            hanabiCount = 35;
        }

        for (int i = 0; i < hanabiCount; i++)
        {
            Vector3 generatePos = new Vector3(UnityEngine.Random.Range(-hanabiPos.x, hanabiPos.x), UnityEngine.Random.Range(-hanabiPos.y, hanabiPos.y), 0);
            GameObject hanabi = Instantiate(hanabiList[UnityEngine.Random.Range(0, hanabiList.Count)], generatePos, Quaternion.Euler(-90, 0, 0));
            Destroy(hanabi, 2f);
            audioSource.PlayOneShot(hanabiSound);

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.3f));
        }
    }
}