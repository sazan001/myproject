using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ScoreCalculation : MonoBehaviour
{
    [Tooltip("選択された牌\n点数を計算する牌\n13牌")]
    private List<MahjongTile> selectMahjongTileList = new List<MahjongTile>();

    [Tooltip("選択された牌\n点数を計算する牌\n13牌")]
    private List<Image> selectMahjongTileImageList = new List<Image>();

    [Tooltip("選択された牌\n点数を計算する牌\n上がり牌")]
    private MahjongTile selectEndMahjongTile = MahjongTile.NoSelect;

    [SerializeField, Tooltip("選択された牌\n点数を計算する牌\n上がり牌")]
    private Image selectEndMahjongTileImage;


    [Tooltip("ドラ\nめくった牌の次の牌\n10牌")]
    private List<MahjongTile> doraMahjongTileList = new List<MahjongTile>();

    [Tooltip("ドラ表示牌\nめくった牌の次の牌\n10牌")]
    private List<MahjongTile> doraHyouziMahjongTileList = new List<MahjongTile>();

    [SerializeField, Tooltip("ドラ表示牌\nめくった牌の次の牌\n10牌")]
    private List<Image> doraMahjongTileImageList = new List<Image>();


    [Tooltip("場の風\n東南西北")]
    private MahjongTile baMahjongTile = MahjongTile.NoSelect;

    [SerializeField, Tooltip("場の風\n東南西北")]
    private Image baMahjongTileImage;

    [Tooltip("自分の風\n東南西北")]
    private MahjongTile ziMahjongTile = MahjongTile.NoSelect;

    [SerializeField, Tooltip("自分の風\n東南西北")]
    private Image ziMahjongTileImage;


    [Tooltip("親")]
    private bool parent;
    [SerializeField, Tooltip("親")]
    private Image parentCheckBoxImage;

    [Tooltip("立直")]
    private bool riiti;
    [SerializeField, Tooltip("立直")]
    private Image riitiCheckBoxImage;

    [Tooltip("ツモ")]
    private bool tumo;
    [SerializeField, Tooltip("ツモ")]
    private Image tumoCheckBoxImage;

    [Tooltip("鳴いたか")]
    private bool naki;
    [SerializeField, Tooltip("鳴いたか")]
    private Image nakiCheckBoxImage;

    [Tooltip("一発")]
    private bool ippatu;
    [SerializeField, Tooltip("一発")]
    private Image ippatuCheckBoxImage;

    [Tooltip("槍槓(チャンカン)\n他人がポンしている牌に一つ付けたして\nカンした牌で上がる")]
    private bool tyankan;
    [SerializeField, Tooltip("槍槓(チャンカン)\n他人がポンしている牌に一つ付けたして\nカンした牌で上がる")]
    private Image tyankanCheckBoxImage;

    [Tooltip("嶺上開花(リンシャンカイホー)\n嶺上牌で上がること\n嶺上牌はカンした時につもる牌")]
    private bool rinsyankaihoo;
    [SerializeField, Tooltip("嶺上開花(リンシャンカイホー)\n嶺上牌で上がること\n嶺上牌はカンした時につもる牌")]
    private Image rinsyankaihooCheckBoxImage;

    [Tooltip("海底(ハイテイ)/河底(ホーテイ)\n牌山の最後の牌で上がると、海底。\n要するに最後の最後にツモ" +
        "\n局の最後の牌で上がると、河底。\n要するに最後の最後にロン")]
    private bool haitei;
    [SerializeField, Tooltip("海底(ハイテイ)/河底(ホーテイ)\n牌山の最後の牌で上がると、海底。\n要するに最後の最後にツモ" +
        "\n局の最後の牌で上がると、河底。\n要するに最後の最後にロン")]
    private Image haiteiCheckBoxImage;

    [Tooltip("ダブル立直\n一巡目に立直")]
    private bool doubleRiiti;
    [SerializeField, Tooltip("ダブル立直\n一巡目に立直")]
    private Image doubleRiitiCheckBoxImage;

    [Tooltip("天和/地和\n一巡目のツモで上がる")]
    private bool tenhoo;
    [SerializeField, Tooltip("天和/地和\n一巡目のツモで上がる")]
    private Image tenhooCheckBoxImage;


    [SerializeField, Tooltip("スコアを表示する際に有効に\nスコア関連の親オブジェクト")]
    private GameObject scoreObj;

    [Tooltip("スコアを表示中か")]
    private bool scoreDisplay;

    [SerializeField, Tooltip("上がった時の役の一覧")]
    private Text yakuText;

    [SerializeField, Tooltip("受け取る点数")]
    private Text scoreText;
    [SerializeField, Tooltip("払う点数")]
    private Text scorePayText;

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

    //刻子の個数。
    int kootuCount = 0;
    //順子の個数。
    int syuntuCount = 0;

    [SerializeField, Tooltip("SelectMahjongTiles\nBackGround")]
    private Transform selectMahjongTilesParent;
    [SerializeField, Tooltip("MahjongTile_Image\nPrefabs")]
    private GameObject mahjongTileImageObj;
    [SerializeField, Tooltip("Pon\nPrefabs")]
    private GameObject ponObj;
    [SerializeField, Tooltip("MinKann\n明槓\nPrefabs")]
    private GameObject minKannObj;
    [SerializeField, Tooltip("AnKann\n暗槓\nPrefabs")]
    private GameObject anKannObj;

    [Tooltip("確定している面子")]
    private List<List<MahjongTile>> kakuteiMentuMahjongTileList = new List<List<MahjongTile>>();
    [Tooltip("合計の牌\n13牌")]
    private List<MahjongTile> totalMahjongTileList = new List<MahjongTile>();

    [Tooltip("確定している刻子の数")]
    private int kakuteiKootuCount = 0;
    [Tooltip("確定している順子の数")]
    private int kakuteiSyuntuCount = 0;
    [Tooltip("確定しているカンの数")]
    private int kakuteiKannCount = 0;
    [Tooltip("確定しているアンカンの数")]
    private int anKannCount = 0;
    [Tooltip("カンした牌\n13牌")]
    private List<MahjongTile> kannMahjongTileList = new List<MahjongTile>();

    [Tooltip("計算時に使用するリスト")]
    MahjongTile head;

    [Tooltip("役満かどうか")]
    private bool yakuman = false;
    [Tooltip("役満かどうか")]
    private bool yakumanNow = false;


    [Tooltip("符計算をするか")]
    private bool huKeisan = false;
    [SerializeField, Tooltip("符計算をするか")]
    private Image huKeisanCheckBoxImage;
    [Tooltip("符計算に使用する符")]
    private int hu;
    [Tooltip("符計算の内訳")]
    private string huStr;

    [Tooltip("刻子")]
    private List<List<MahjongTile>> kootuMentu = new List<List<MahjongTile>>();
    [Tooltip("順子")]
    private List<List<MahjongTile>> syuntuMentu = new List<List<MahjongTile>>();
    [Tooltip("確定している刻子")]
    private List<List<MahjongTile>> kakuteiKootuMentu = new List<List<MahjongTile>>();
    [Tooltip("確定している順子")]
    private List<List<MahjongTile>> kakuteiSyuntuMentu = new List<List<MahjongTile>>();
    [Tooltip("確定しているミンカン")]
    private List<List<MahjongTile>> kakuteiMinkanMentu = new List<List<MahjongTile>>();
    [Tooltip("確定しているアンカン")]
    private List<List<MahjongTile>> kakuteiAnkanMentu = new List<List<MahjongTile>>();

    private bool ryanmen = false;
    private bool syanpon = false;
    private bool pentyan = false;
    private bool kantyan = false;
    private bool tanki = false;
    private bool nobetan = false;

    [SerializeField, Tooltip("連風牌(リェンフォンパイ)を4符とするか\n2符とするならfalse\nフリーの雀荘では2符\nプロの団体や競技麻雀では4符が多い")]
    private bool ryenfonpaiYonHu = false;
    [SerializeField, Tooltip("切り上げ満貫")]
    private bool kiriageMangan = false;


    [SerializeField, Tooltip("待ちを表示するか\n完全Debug用")]
    private bool matiHyouzi = false;


    [SerializeField, Tooltip("選択情報を表示するテキスト。")]
    private Text selectInformationText;

    void Start()
    {
        selectEndMahjongTileImage.sprite = DataManager.noImage;
        foreach (Image doraMahjongTileImage in doraMahjongTileImageList)
        {
            doraMahjongTileImage.sprite = DataManager.noImage;
        }
        baMahjongTileImage.sprite = DataManager.noImage;
        ziMahjongTileImage.sprite = DataManager.noImage;

        ParentCheckBoxUpdate();
        RiitiCheckBoxUpdate();
        TumoCheckBoxUpdate();
        NakiCheckBoxUpdate();
        IppatuCheckBoxUpdate();
        TyankanCheckBoxUpdate();
        RinsyankaihooCheckBoxUpdate();
        HaiteiCheckBoxUpdate();
        DoubleRiitiCheckBoxUpdate();
        TenhooCheckBoxUpdate();
        HuKeisanCheckBoxUpdate();

        SelectInformationTextUpdate();

        ScoreHide();
    }

    void Update()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            //Shift+D。Debug。
            if ((Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.D))
            {
                DebugMahjongTileGenerate();
            }
            //左クリック。手持ちの牌。
            else if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //13牌以上は入れない。
                        if (totalMahjongTileList.Count < 13)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //同じ牌は4つまで。
                            if (count < 4 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                            {
                                GameObject generateMahjongTileImageObj =
                                    Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                                selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                                MahjongTileSelect(mahjongTileManager.mahjongTile);
                            }

                            SelectInformationTextUpdate();
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectMahjongTile")
                    {
                        MahjongTileRemove(selectMahjongTileList[selectMahjongTileImageList.IndexOf(result.gameObject.GetComponent<Image>())], result.gameObject);

                        SelectInformationTextUpdate();
                    }
                }
            }
            //右クリック。上がり牌。
            else if (Input.GetMouseButtonDown(1))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //上書きも可。
                        MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                        //同じ牌が何個あるか計算。
                        int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                            + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                        if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                        {
                            count++;
                        }
                        //同じ牌は4つまで。
                        if (count < 4 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                        {
                            selectEndMahjongTile = mahjongTileManager.mahjongTile;
                            selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];
                        }

                        SelectInformationTextUpdate();
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectEndMahjongTile")
                    {
                        //画像が設定されていたら
                        if (result.gameObject.GetComponent<Image>().sprite != DataManager.noImage)
                        {
                            selectEndMahjongTile = MahjongTile.NoSelect;
                            selectEndMahjongTileImage.sprite = DataManager.noImage;

                            SelectInformationTextUpdate();
                        }
                    }
                }
            }
            //D。ドラの牌。
            else if (Input.GetKeyDown(KeyCode.D))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //10牌以上は入れない。
                        if (doraMahjongTileList.Count < 10)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //同じ牌は4つまで。
                            if (count < 4 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                            {
                                DoraMahjongTileSelect(mahjongTileManager.mahjongTile);
                            }

                            SelectInformationTextUpdate();
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "DoraMahjongTile")
                    {
                        //画像が設定されていたら
                        if (result.gameObject.GetComponent<Image>().sprite != DataManager.noImage)
                        {
                            DoraMahjongTileRemove(doraHyouziMahjongTileList[doraMahjongTileImageList.IndexOf(result.gameObject.GetComponent<Image>())]);

                            SelectInformationTextUpdate();
                        }
                    }
                }
            }
            //B。場の風。東南西北のみ。
            else if (Input.GetKeyDown(KeyCode.B))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //上書きも可。
                        MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                        //東南西北のどれかなら。
                        if (27 <= (int)mahjongTileManager.mahjongTile && (int)mahjongTileManager.mahjongTile <= 30)
                        {
                            baMahjongTile = mahjongTileManager.mahjongTile;
                            baMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)baMahjongTile];
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "BaMahjongTile")
                    {
                        //画像が設定されていたら
                        if (baMahjongTileImage.sprite != DataManager.noImage)
                        {
                            baMahjongTile = MahjongTile.NoSelect;
                            baMahjongTileImage.sprite = DataManager.noImage;
                        }
                    }
                }
            }
            //Z。自分の風。東南西北のみ。
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //上書きも可。
                        MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                        //東南西北のどれかなら。
                        if (27 <= (int)mahjongTileManager.mahjongTile && (int)mahjongTileManager.mahjongTile <= 30)
                        {
                            ziMahjongTile = mahjongTileManager.mahjongTile;
                            ziMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)ziMahjongTile];
                        }

                        ParentCheck();
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "ZiMahjongTile")
                    {
                        //画像が設定されていたら
                        if (ziMahjongTileImage.sprite != DataManager.noImage)
                        {
                            ziMahjongTile = MahjongTile.NoSelect;
                            ziMahjongTileImage.sprite = DataManager.noImage;
                        }

                        ParentCheck();
                    }
                }
            }
            //P。ポン。
            else if (Input.GetKeyDown(KeyCode.P))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //13牌以上は入れない。
                        if (totalMahjongTileList.Count <= 10)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //同じ牌は4つまで。ポンで3つ加算するため、1以下ならOK。
                            if (count <= 1 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                            {
                                GameObject generatePonObj = Instantiate(ponObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                                PonMahjongTileSelect(mahjongTileManager.mahjongTile, generatePonObj);

                                SelectInformationTextUpdate();
                            }
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectMahjongTileNaki")
                    {
                        MahjongTile mahjongTile = MahjongTile.NoSelect;
                        foreach (Transform image in result.gameObject.transform)
                        {
                            mahjongTile =
                                (MahjongTile)Enum.ToObject(typeof(MahjongTile), DataManager.mahjongTileImages.IndexOf(image.GetComponent<Image>().sprite));
                            Debug.Log(mahjongTile + "のポンを削除。");
                            break;
                        }
                        //MahjongTileを削除。
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        List<MahjongTile> removeMentu = new List<MahjongTile>();
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        foreach (List<MahjongTile> mentu in kakuteiMentuMahjongTileList)
                        {
                            if (mentu.SequenceEqual(removeMentu))
                            {
                                //確定した面子から削除。
                                kakuteiMentuMahjongTileList.Remove(mentu);

                                kakuteiKootuMentu.Remove(mentu);
                                break;
                            }
                        }
                        //確定した順子の数から1減算。
                        kakuteiKootuCount--;

                        //鳴き確認。
                        NakiCheck();

                        SelectInformationTextUpdate();

                        Destroy(result.gameObject);
                    }
                }
            }
            //C。チー。
            else if (Input.GetKeyDown(KeyCode.C))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //13牌以上は入れない。
                        if (totalMahjongTileList.Count <= 10)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //1~7なら
                            if ((int)mahjongTileManager.mahjongTile <= 6
                                || ((int)mahjongTileManager.mahjongTile >= 9 && (int)mahjongTileManager.mahjongTile <= 15)
                                || ((int)mahjongTileManager.mahjongTile >= 18 && (int)mahjongTileManager.mahjongTile <= 24))
                            {
                                //同じ牌は4つまで。
                                if (count <= 3 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                                {
                                    //同じ牌が何個あるか計算。
                                    count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile + 1)
                                        + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile + 1);
                                    if (selectEndMahjongTile == (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTileManager.mahjongTile + 1))
                                    {
                                        count++;
                                    }
                                    //同じ牌は4つまで。
                                    if (count <= 3 &&
                                        !kannMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTileManager.mahjongTile + 1)))
                                    {
                                        //同じ牌が何個あるか計算。
                                        count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile + 2)
                                            + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile + 2);
                                        if (selectEndMahjongTile == (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTileManager.mahjongTile + 2))
                                        {
                                            count++;
                                        }
                                        //同じ牌は4つまで。
                                        if (count <= 3 &&
                                            !kannMahjongTileList.Contains((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTileManager.mahjongTile + 2)))
                                        {
                                            GameObject generateChiiObj = Instantiate(ponObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                                            ChiiMahjongTileSelect(mahjongTileManager.mahjongTile, generateChiiObj);

                                            SelectInformationTextUpdate();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectMahjongTileNaki")
                    {
                        MahjongTile mahjongTile = MahjongTile.NoSelect;
                        foreach (Transform image in result.gameObject.transform)
                        {
                            mahjongTile =
                                (MahjongTile)Enum.ToObject(typeof(MahjongTile), DataManager.mahjongTileImages.IndexOf(image.GetComponent<Image>().sprite));
                            Debug.Log(mahjongTile + "のチーを削除。");
                            break;
                        }
                        //MahjongTileを削除。
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 1);
                        totalMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 2);
                        List<MahjongTile> removeMentu = new List<MahjongTile>();
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 1);
                        removeMentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 2);
                        foreach (List<MahjongTile> mentu in kakuteiMentuMahjongTileList)
                        {
                            if (mentu.SequenceEqual(removeMentu))
                            {
                                //確定した面子から削除。
                                kakuteiMentuMahjongTileList.Remove(mentu);

                                kakuteiSyuntuMentu.Remove(mentu);
                                break;
                            }
                        }
                        //確定した順子の数から1減算。
                        kakuteiSyuntuCount--;

                        //鳴き確認。
                        NakiCheck();

                        SelectInformationTextUpdate();

                        Destroy(result.gameObject);
                    }
                }
            }
            //M。明槓。
            else if (Input.GetKeyDown(KeyCode.M))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //13牌以上は入れない。
                        if (totalMahjongTileList.Count <= 10)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //同じ牌は4つまで。カンで4つ加算するため、0ならOK。
                            if (count == 0 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                            {
                                GameObject generateMinkannObj = Instantiate(minKannObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                                KannMahjongTileSelect(mahjongTileManager.mahjongTile, generateMinkannObj, false);

                                SelectInformationTextUpdate();
                            }
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectMahjongTileKann")
                    {
                        MahjongTile mahjongTile = MahjongTile.NoSelect;
                        int i = 0;
                        foreach (Transform image in result.gameObject.transform)
                        {
                            if (i == 1)
                            {
                                mahjongTile =
                                    (MahjongTile)Enum.ToObject(typeof(MahjongTile), DataManager.mahjongTileImages.IndexOf(image.GetComponent<Image>().sprite));
                                Debug.Log(mahjongTile + "のカンを削除。");
                                break;
                            }
                            i++;
                        }
                        //MahjongTileを削除。
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        List<MahjongTile> removeMentu = new List<MahjongTile>();
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        foreach (List<MahjongTile> mentu in kakuteiMentuMahjongTileList)
                        {
                            if (mentu.SequenceEqual(removeMentu))
                            {
                                //確定した面子から削除。
                                kakuteiMentuMahjongTileList.Remove(mentu);

                                kakuteiMinkanMentu.Remove(mentu);
                                break;
                            }
                        }
                        //確定したカンの数から1減算。
                        kakuteiKannCount--;

                        kannMahjongTileList.Remove(mahjongTile);

                        //鳴き確認。
                        NakiCheck();

                        SelectInformationTextUpdate();

                        Destroy(result.gameObject);
                    }
                }
            }
            //A。暗槓。
            else if (Input.GetKeyDown(KeyCode.A))
            {
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                List<RaycastResult> rayResult = new List<RaycastResult>();

                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayResult);
                foreach (RaycastResult result in rayResult)
                {
                    //新たに牌を設定。
                    if (result.gameObject.tag == "MahjongTile")
                    {
                        //13牌以上は入れない。
                        if (totalMahjongTileList.Count <= 10)
                        {
                            MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                            //同じ牌が何個あるか計算。
                            int count = totalMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile)
                                + doraHyouziMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                            if (selectEndMahjongTile == mahjongTileManager.mahjongTile)
                            {
                                count++;
                            }
                            //同じ牌は4つまで。カンで4つ加算するため、0ならOK。
                            if (count == 0 && !kannMahjongTileList.Contains(mahjongTileManager.mahjongTile))
                            {
                                GameObject generateMinkannObj = Instantiate(anKannObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                                KannMahjongTileSelect(mahjongTileManager.mahjongTile, generateMinkannObj, true);

                                SelectInformationTextUpdate();
                            }
                        }
                    }
                    //牌を消去。
                    else if (result.gameObject.tag == "SelectMahjongTileKann")
                    {
                        MahjongTile mahjongTile = MahjongTile.NoSelect;
                        int i = 0;
                        foreach (Transform image in result.gameObject.transform)
                        {
                            if (i == 1)
                            {
                                mahjongTile =
                                    (MahjongTile)Enum.ToObject(typeof(MahjongTile), DataManager.mahjongTileImages.IndexOf(image.GetComponent<Image>().sprite));
                                Debug.Log(mahjongTile + "のカンを削除。");
                                break;
                            }
                            i++;
                        }
                        //MahjongTileを削除。
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        totalMahjongTileList.Remove(mahjongTile);
                        List<MahjongTile> removeMentu = new List<MahjongTile>();
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        removeMentu.Add(mahjongTile);
                        foreach (List<MahjongTile> mentu in kakuteiMentuMahjongTileList)
                        {
                            if (mentu.SequenceEqual(removeMentu))
                            {
                                //確定した面子から削除。
                                kakuteiMentuMahjongTileList.Remove(mentu);

                                kakuteiAnkanMentu.Remove(mentu);
                                break;
                            }
                        }
                        //確定したカンの数から1減算。
                        kakuteiKannCount--;
                        //暗槓の数-1
                        anKannCount--;

                        kannMahjongTileList.Remove(mahjongTile);

                        //鳴き確認。
                        NakiCheck();

                        SelectInformationTextUpdate();

                        Destroy(result.gameObject);
                    }
                }
            }
            //C。Clear。
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Clear();
            }
        }
    }

    void MahjongTileSelect(MahjongTile mahjongTile)
    {
        //MahjongTileを追加。
        selectMahjongTileList.Add(mahjongTile);
        //MahjongTileを追加。
        totalMahjongTileList.Add(mahjongTile);

        //enum型の番号を追加。
        List<int> selectMahjongTileNumList = new List<int>();
        foreach(MahjongTile selectMahjongTile in selectMahjongTileList)
        {
            selectMahjongTileNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newSelectMahjongTileList = new List<MahjongTile>();
        int count = selectMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = selectMahjongTileNumList.Min();
            int minNumNum = selectMahjongTileNumList.IndexOf(minNum);

            newSelectMahjongTileList.Add(selectMahjongTileList[minNumNum]);
            selectMahjongTileList.Remove(selectMahjongTileList[minNumNum]);
            selectMahjongTileNumList.Remove(minNum);
        }

        selectMahjongTileList = new List<MahjongTile>(newSelectMahjongTileList);
        SelectMahjongTileImageListUpdate();
    }

    void MahjongTileRemove(MahjongTile mahjongTile, GameObject image)
    {
        //MahjongTileを削除。
        selectMahjongTileList.Remove(mahjongTile);
        //MahjongTileを削除。
        totalMahjongTileList.Remove(mahjongTile);

        selectMahjongTileImageList.Remove(image.GetComponent<Image>());
        Destroy(image);

        SelectMahjongTileImageListUpdate();
    }

    void SelectMahjongTileImageListUpdate()
    {
        foreach (Image selectMahjongTileImage in selectMahjongTileImageList)
        {
            selectMahjongTileImage.sprite = DataManager.noImage;
        }
        for (int i = 0; i < selectMahjongTileList.Count; i++)
        {
            selectMahjongTileImageList[i].sprite = DataManager.mahjongTileImages[(int)selectMahjongTileList[i]];
        }

        //ポン、チー、カンの位置替え。
        List<Transform> items = new List<Transform>();
        foreach (Transform item in selectMahjongTilesParent)
        {
            items.Add(item);
        }
        foreach (Transform item in items)
        {
            if (item.tag == "SelectMahjongTileNaki" || item.tag == "SelectMahjongTileKann")
            {
                item.SetAsLastSibling();
            }
        }
    }

    void DoraMahjongTileSelect(MahjongTile mahjongTile)
    {
        //MahjongTileを追加。
        doraHyouziMahjongTileList.Add(mahjongTile);

        //MahjongTileを追加。これは内部的にはドラを扱う。
        doraMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), Dora((int)mahjongTile)));

        DoraMahjongTileImageListUpdate();
    }

    void DoraMahjongTileRemove(MahjongTile mahjongTile)
    {
        //MahjongTileを削除。
        doraMahjongTileList.Remove(doraMahjongTileList[doraHyouziMahjongTileList.IndexOf(mahjongTile)]);

        //MahjongTileを削除。
        doraHyouziMahjongTileList.Remove(mahjongTile);

        DoraMahjongTileImageListUpdate();
    }

    void DoraMahjongTileImageListUpdate()
    {
        //画像の設定。
        foreach (Image doraMahjongTileImage in doraMahjongTileImageList)
        {
            doraMahjongTileImage.sprite = DataManager.noImage;
        }
        for (int i = 0; i < doraHyouziMahjongTileList.Count; i++)
        {
            doraMahjongTileImageList[i].sprite = DataManager.mahjongTileImages[(int)doraHyouziMahjongTileList[i]];
        }
    }

    int Dora(int dorahyouzihaiNum)
    {
        if (dorahyouzihaiNum == 8)
        {
            return 0;
        }
        else if (dorahyouzihaiNum == 17)
        {
            return 9;
        }
        else if (dorahyouzihaiNum == 26)
        {
            return 18;
        }
        else if (dorahyouzihaiNum == 30)
        {
            return 27;
        }
        else if (dorahyouzihaiNum == 33)
        {
            return 31;
        }
        else
        {
            return ++dorahyouzihaiNum;
        }
    }
    void PonMahjongTileSelect(MahjongTile mahjongTile, GameObject ponObj)
    {
        //MahjongTileを追加。
        totalMahjongTileList.Add(mahjongTile);
        totalMahjongTileList.Add(mahjongTile);
        totalMahjongTileList.Add(mahjongTile);

        List<MahjongTile> mentu = new List<MahjongTile>();
        mentu.Add(mahjongTile);
        mentu.Add(mahjongTile);
        mentu.Add(mahjongTile);
        //確定した面子に追加。
        kakuteiMentuMahjongTileList.Add(mentu);
        //確定した刻子の数に1追加。
        kakuteiKootuCount++;

        //確定した刻子に追加。
        kakuteiKootuMentu.Add(mentu);

        //鳴き確認。
        NakiCheck();

        foreach (Transform image in ponObj.transform)
        {
            image.GetComponent<Image>().sprite = DataManager.mahjongTileImages[(int)mahjongTile];
        }
    }
    void ChiiMahjongTileSelect(MahjongTile mahjongTile, GameObject chiiObj)
    {
        //MahjongTileを追加。
        totalMahjongTileList.Add(mahjongTile);
        totalMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 1);
        totalMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 2);

        List<MahjongTile> mentu = new List<MahjongTile>();
        mentu.Add(mahjongTile);
        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 1);
        mentu.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile) + 2);
        //確定した面子に追加。
        kakuteiMentuMahjongTileList.Add(mentu);
        //確定した順子の数に1追加。
        kakuteiSyuntuCount++;

        //確定した順子に追加。
        kakuteiSyuntuMentu.Add(mentu);

        //鳴き確認。
        NakiCheck();

        int i = 0;
        foreach (Transform image in chiiObj.transform)
        {
            image.GetComponent<Image>().sprite = DataManager.mahjongTileImages[(int)mahjongTile + i];
            i++;
        }
    }
    void KannMahjongTileSelect(MahjongTile mahjongTile, GameObject kannObj, bool anKann)
    {
        //MahjongTileを追加。
        totalMahjongTileList.Add(mahjongTile);
        totalMahjongTileList.Add(mahjongTile);
        totalMahjongTileList.Add(mahjongTile);

        List<MahjongTile> mentu = new List<MahjongTile>();
        mentu.Add(mahjongTile);
        mentu.Add(mahjongTile);
        mentu.Add(mahjongTile);
        //確定した面子に追加。
        kakuteiMentuMahjongTileList.Add(mentu);
        //確定したカンの数に1追加。
        kakuteiKannCount++;
        if (anKann)
        {
            //暗槓の数+1
            anKannCount++;
        }

        if (anKann)
        {
            //確定した暗槓に追加。
            kakuteiAnkanMentu.Add(mentu);
        }
        else
        {
            //確定した明槓に追加。
            kakuteiMinkanMentu.Add(mentu);
        }

        kannMahjongTileList.Add(mahjongTile);

        //鳴き確認。
        NakiCheck();

        int i = 0;
        foreach (Transform image in kannObj.transform)
        {
            if (anKann)
            {
                if (i == 0 || i == 3)
                {
                    image.GetComponent<Image>().sprite = DataManager.uraImage;
                }
                else
                {
                    image.GetComponent<Image>().sprite = DataManager.mahjongTileImages[(int)mahjongTile];
                }
            }
            else
            {
                image.GetComponent<Image>().sprite = DataManager.mahjongTileImages[(int)mahjongTile];
            }
            i++;
        }
    }

    void SelectInformationTextUpdate()
    {
        string str = "#選択情報#";
        str += "\n[手牌]";
        str += "\n" + totalMahjongTileList.Count + "牌";
        str += "\n[上がり牌]";
        if(selectEndMahjongTile!= MahjongTile.NoSelect)
        {
            str += "\n選択済み";
        }
        else
        {
            str += "\n未選択";
        }
        str += "\n[ドラ表示牌]";
        str += "\n" + doraHyouziMahjongTileList.Count + "牌";
        selectInformationText.text = str;
    }

    public void Clear()
    {
        selectMahjongTileList = new List<MahjongTile>();

        foreach (Transform trn in selectMahjongTilesParent)
        {
            Destroy(trn.gameObject);
        }

        selectMahjongTileImageList = new List<Image>();

        selectEndMahjongTile = MahjongTile.NoSelect;

        selectEndMahjongTileImage.sprite = DataManager.noImage;


        doraMahjongTileList = new List<MahjongTile>();
        
        doraHyouziMahjongTileList = new List<MahjongTile>();

        DoraMahjongTileImageListUpdate();

        //場風牌はリセットしない。
        //baMahjongTile = MahjongTile.NoSelect;
        //baMahjongTileImage.sprite = DataManager.noImage;
        //自風牌はリセットしない。
        //ziMahjongTile = MahjongTile.NoSelect;
        //ziMahjongTileImage.sprite = DataManager.noImage;

        parent = false;
        ParentCheckBoxUpdate();
        riiti = false;
        RiitiCheckBoxUpdate();
        tumo = false;
        TumoCheckBoxUpdate();
        naki = false;
        NakiCheckBoxUpdate();
        ippatu = false;
        IppatuCheckBoxUpdate();
        tyankan = false;
        TyankanCheckBoxUpdate();
        rinsyankaihoo = false;
        RinsyankaihooCheckBoxUpdate();
        haitei = false;
        HaiteiCheckBoxUpdate();
        doubleRiiti = false;
        DoubleRiitiCheckBoxUpdate();
        tenhoo = false;
        TenhooCheckBoxUpdate();

        kakuteiMentuMahjongTileList = new List<List<MahjongTile>>();
        totalMahjongTileList = new List<MahjongTile>();

        SelectInformationTextUpdate();

        kakuteiKootuCount = 0;
        kakuteiSyuntuCount = 0;
        kakuteiKannCount = 0;
        anKannCount = 0;
        kannMahjongTileList = new List<MahjongTile>();

        kakuteiKootuMentu = new List<List<MahjongTile>>();
        kakuteiSyuntuMentu = new List<List<MahjongTile>>();
        kakuteiMinkanMentu = new List<List<MahjongTile>>();
        kakuteiAnkanMentu = new List<List<MahjongTile>>();

        ParentCheck();
        NakiCheck();
    }


    public void ParentCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            parent = !parent;

            ParentCheckBoxUpdate();
        }
    }
    void ParentCheck()
    {
        if ((int)ziMahjongTile==27)
        {
            parent = true;

            ParentCheckBoxUpdate();
        }
        else
        {
            parent = false;

            ParentCheckBoxUpdate();
        }
    }
    void ParentCheckBoxUpdate()
    {
        if (parent)
        {
            parentCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            parentCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void RiitiCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay && kakuteiKootuCount + kakuteiSyuntuCount + kakuteiKannCount - anKannCount == 0)
        {
            riiti = !riiti;

            RiitiCheckBoxUpdate();

            //立直がなかったら一発はない。
            if (!riiti)
            {
                if (ippatu)
                {
                    IppatuCheckChange();
                }
            }
            //立直があったら
            else
            {
                //鳴きは無い。
                if (naki)
                {
                    NakiCheckChange();
                }

                //ダブル立直はない。
                if (doubleRiiti)
                {
                    DoubleRiitiCheckChange();
                }
            }
        }
    }
    void RiitiCheckBoxUpdate()
    {
        if (riiti)
        {
            riitiCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            riitiCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void TumoCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            tumo = !tumo;

            TumoCheckBoxUpdate();
        }
    }
    void TumoCheckBoxUpdate()
    {
        if (tumo)
        {
            tumoCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            tumoCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    //使わない。
    public void NakiCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            if (kakuteiKootuCount + kakuteiSyuntuCount + kakuteiKannCount - anKannCount == 0)
            {
                naki = !naki;

                NakiCheckBoxUpdate();

                //鳴きがあったら。
                if (naki)
                {
                    //立直はない。
                    riiti = false;

                    RiitiCheckBoxUpdate();

                    //立直がなければ一発はない。
                    ippatu = false;

                    IppatuCheckBoxUpdate();

                    //ダブル立直はない。
                    doubleRiiti = false;

                    DoubleRiitiCheckBoxUpdate();
                }
            }
            else
            {
                naki = true;

                NakiCheckBoxUpdate();

                //立直はない。
                riiti = false;

                RiitiCheckBoxUpdate();

                //立直がなければ一発はない。
                ippatu = false;

                IppatuCheckBoxUpdate();

                //ダブル立直はない。
                doubleRiiti = false;

                DoubleRiitiCheckBoxUpdate();
            }
        }
    }
    void NakiCheck()
    {
        if (kakuteiKootuCount + kakuteiSyuntuCount + kakuteiKannCount - anKannCount == 0)
        {
            naki = false;

            NakiCheckBoxUpdate();
        }
        else
        {
            naki = true;

            NakiCheckBoxUpdate();

            if (riiti)
            {
                RiitiCheckChange();
            }
            else if (doubleRiiti)
            {
                DoubleRiitiCheckChange();
            }
        }
    }
    void NakiCheckBoxUpdate()
    {
        if (naki)
        {
            nakiCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            nakiCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void IppatuCheckChange()
    {
        //得点表示中は操作を受け入れない。鳴きがなければ。
        if (!scoreDisplay && kakuteiKootuCount + kakuteiSyuntuCount + kakuteiKannCount - anKannCount == 0)
        {
            ippatu = !ippatu;

            IppatuCheckBoxUpdate();

            //立直とダブル立直がなければ。
            if (!riiti && !doubleRiiti)
            {
                //立直を有効に。ダブル立直でもいいが、出現率的に立直を有効に。
                RiitiCheckChange();
            }
        }
    }
    void IppatuCheckBoxUpdate()
    {
        if (ippatu)
        {
            ippatuCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            ippatuCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void TyankanCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            tyankan = !tyankan;

            TyankanCheckBoxUpdate();

            if (tyankan)
            {
                if (tumo)
                {
                    TumoCheckChange();
                }
            }
        }
    }
    void TyankanCheckBoxUpdate()
    {
        if (tyankan)
        {
            tyankanCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            tyankanCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void RinsyankaihooCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            rinsyankaihoo = !rinsyankaihoo;

            RinsyankaihooCheckBoxUpdate();

            if (rinsyankaihoo)
            {
                if (!tumo)
                {
                    TumoCheckChange();
                }
            }
        }
    }
    void RinsyankaihooCheckBoxUpdate()
    {
        if (rinsyankaihoo)
        {
            rinsyankaihooCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            rinsyankaihooCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void HaiteiCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            haitei = !haitei;

            HaiteiCheckBoxUpdate();
        }
    }
    void HaiteiCheckBoxUpdate()
    {
        if (haitei)
        {
            haiteiCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            haiteiCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void DoubleRiitiCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay && kakuteiKootuCount + kakuteiSyuntuCount + kakuteiKannCount - anKannCount == 0)
        {
            doubleRiiti = !doubleRiiti;

            DoubleRiitiCheckBoxUpdate();

            //ダブル立直があったら
            if (doubleRiiti)
            {
                if (riiti)
                {
                    RiitiCheckChange();
                }
                //鳴きは無い。
                if (naki)
                {
                    NakiCheckChange();
                }
            }
        }
    }
    void DoubleRiitiCheckBoxUpdate()
    {
        if (doubleRiiti)
        {
            doubleRiitiCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            doubleRiitiCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void TenhooCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            tenhoo = !tenhoo;

            TenhooCheckBoxUpdate();

            if (tenhoo)
            {
                if (!tumo)
                {
                    TumoCheckChange();
                }
            }
        }
    }
    void TenhooCheckBoxUpdate()
    {
        if (tenhoo)
        {
            tenhooCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            tenhooCheckBoxImage.sprite = DataManager.noImage;
        }
    }
    public void HuKeisanCheckChange()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            huKeisan = !huKeisan;

            HuKeisanCheckBoxUpdate();
        }
    }
    void HuKeisanCheckBoxUpdate()
    {
        if (huKeisan)
        {
            huKeisanCheckBoxImage.sprite = DataManager.checkImage;
        }
        else
        {
            huKeisanCheckBoxImage.sprite = DataManager.noImage;
        }
    }

    public void Calculation()
    {
        //得点表示中は操作を受け入れない
        if (!scoreDisplay)
        {
            //牌が13個あったら
            if (totalMahjongTileList.Count == 13)
            {
                //上がり牌が選択されていたら
                if (selectEndMahjongTile != MahjongTile.NoSelect)
                {
                    //得点計算開始。
                    //Textリセット。
                    yakuText.text = "";
                    //最高翻数。
                    int highHansuu = 0;
                    //最高翻数時の役。
                    string highYaku = "";
                    //最高符。
                    int highHu = 0;
                    //最高符内訳。
                    string highHuStr = "";

                    //リスト
                    calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                    calculationMahjongTileList.Add(selectEndMahjongTile);
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
                        calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                        calculationMahjongTileList.Add(selectEndMahjongTile);
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
                            kootuCount = KootuCount(0) + kakuteiKootuCount;
                            //順子の個数。同時に使った牌の削除。
                            syuntuCount = SyuntuCount() + kakuteiSyuntuCount;
                            if (calculationMahjongTileList.Count == 0)
                            {
                                MatiCheck();

                                //翻数カウント
                                HansuuCount();

                                if (hansuu > 0)
                                {
                                    DoraCheck();
                                }

                                //符計算
                                HuKeisan();

                                //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                                //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                                if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                                    || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                                {
                                    //Debug用。待ちを表示。
                                    if (matiHyouzi)
                                    {
                                        yaku += "\n待ち一覧(Debug)";
                                        if (ryanmen)
                                        {
                                            yaku += "\n待ち：リャンメン";
                                        }
                                        if (pentyan)
                                        {
                                            yaku += "\n待ち：ペンチャン";
                                        }
                                        if (kantyan)
                                        {
                                            yaku += "\n待ち：カンチャン";
                                        }
                                        if (tanki)
                                        {
                                            yaku += "\n待ち：単騎";
                                        }
                                    }
                                    Debug.Log("最高翻数更新しました。" + "以前は最高翻数" + highHansuu + "、最高符" + highHu + "でしたが" +
                                        "今回は最高翻数" + hansuu + "、符" + hu);

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
                            calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                            calculationMahjongTileList.Add(selectEndMahjongTile);
                            CalculationListSort();
                            mentuMahjongTileList = new List<List<MahjongTile>>();

                            kootuMentu = new List<List<MahjongTile>>();
                            syuntuMentu = new List<List<MahjongTile>>();

                            //2個削除。頭の分。
                            calculationMahjongTileList.Remove(mahjongTile);
                            calculationMahjongTileList.Remove(mahjongTile);

                            //順子の個数。同時に使った牌の削除。
                            syuntuCount = SyuntuCount() + kakuteiSyuntuCount;
                            //刻子の個数。同時に使った牌の削除。
                            kootuCount = KootuCount(0) + kakuteiKootuCount;
                            if (calculationMahjongTileList.Count == 0)
                            {
                                MatiCheck();

                                //翻数カウント
                                HansuuCount();

                                if (hansuu > 0)
                                {
                                    DoraCheck();
                                }

                                //符計算
                                HuKeisan();

                                //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                                //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                                if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                                    || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                                {
                                    //Debug用。待ちを表示。
                                    if (matiHyouzi)
                                    {
                                        yaku += "\n待ち一覧(Debug)";
                                        if (ryanmen)
                                        {
                                            yaku += "\n待ち：リャンメン";
                                        }
                                        if (pentyan)
                                        {
                                            yaku += "\n待ち：ペンチャン";
                                        }
                                        if (kantyan)
                                        {
                                            yaku += "\n待ち：カンチャン";
                                        }
                                        if (tanki)
                                        {
                                            yaku += "\n待ち：単騎";
                                        }
                                    }
                                    Debug.Log("最高翻数更新しました。" + "以前は最高翻数" + highHansuu + "、最高符" + highHu + "でしたが" +
                                        "今回は最高翻数" + hansuu + "、符" + hu);

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
                            calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                            calculationMahjongTileList.Add(selectEndMahjongTile);
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
                            syuntuCount = SyuntuCount() + kakuteiSyuntuCount;
                            //刻子の個数。同時に使った牌の削除。
                            kootuCount += KootuCount(0) + kakuteiKootuCount;
                            if (calculationMahjongTileList.Count == 0)
                            {
                                MatiCheck();

                                //翻数カウント
                                HansuuCount();

                                if (hansuu > 0)
                                {
                                    DoraCheck();
                                }

                                //符計算
                                HuKeisan();

                                //翻数が最高翻数より多ければ。もしくは、他では役満がなく今役満だったら。もしくは、翻数が最高翻数と同じでも符が高かったら。
                                //もしくは、(符計算が有効、かつ、翻数の差*2*最高翻数字の符を今の符が上回ったら(1翻の差は2倍の符でうめれる))。
                                if (hansuu > highHansuu || (!yakuman && yakumanNow) || (hansuu >= highHansuu && hu > highHu)
                                    || (huKeisan && (highHansuu - hansuu) * 2 * highHu < hu))
                                {
                                    //Debug用。待ちを表示。
                                    if (matiHyouzi)
                                    {
                                        yaku += "\n待ち一覧(Debug)";
                                        if (ryanmen)
                                        {
                                            yaku += "\n待ち：リャンメン";
                                        }
                                        if (pentyan)
                                        {
                                            yaku += "\n待ち：ペンチャン";
                                        }
                                        if (kantyan)
                                        {
                                            yaku += "\n待ち：カンチャン";
                                        }
                                        if (tanki)
                                        {
                                            yaku += "\n待ち：単騎";
                                        }
                                    }
                                    Debug.Log("最高翻数更新しました。" + "以前は最高翻数" + highHansuu + "、最高符" + highHu + "でしたが" +
                                        "今回は最高翻数" + hansuu + "、符" + hu);

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


                    //翻数が1以上なら。
                    if (highHansuu > 0 || yakuman)
                    {
                        //得点表示。
                        //得点。符計算無し。
                        //役満
                        if (yakuman)
                        {
                            if (parent)
                            {
                                scoreText.text = "48000点";
                                scorePayText.text = "16000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "32000点";
                                scorePayText.text = "親：16000点\n----------------\n子：8000点";
                            }
                        }
                        //一翻
                        else if (highHansuu == 1)
                        {
                            if (huKeisan)
                            {
                                if (highHu == 30)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "1500点";
                                        scorePayText.text = "500点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "1000点";
                                        scorePayText.text = "親：500点\n----------------\n子：300点";
                                    }
                                }
                                else if (highHu == 40)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "2000点";
                                        scorePayText.text = "700点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "1300点";
                                        scorePayText.text = "親：700点\n----------------\n子：400点";
                                    }
                                }
                                else if (highHu == 50)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "2400点";
                                        scorePayText.text = "800点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "1600点";
                                        scorePayText.text = "親：800点\n----------------\n子：400点";
                                    }
                                }
                                else if (highHu == 60)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "2900点";
                                        scorePayText.text = "1000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2000点";
                                        scorePayText.text = "親：1000点\n----------------\n子：500点";
                                    }
                                }
                                else if (highHu == 70)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "3400点";
                                        scorePayText.text = "1200点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2300点";
                                        scorePayText.text = "親：1200点\n----------------\n子：600点";
                                    }
                                }
                                else if (highHu == 80)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "3900点";
                                        scorePayText.text = "1300点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2600点";
                                        scorePayText.text = "親：1300点\n----------------\n子：700点";
                                    }
                                }
                                else if (highHu == 90)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "4400点";
                                        scorePayText.text = "1500点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2900点";
                                        scorePayText.text = "親：1500点\n----------------\n子：800点";
                                    }
                                }
                                else if (highHu == 100)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "4800点";
                                        scorePayText.text = "1600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3200点";
                                        scorePayText.text = "親：1600点\n----------------\n子：800点";
                                    }
                                }
                                else if (highHu == 110)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "5300点";
                                        scorePayText.text = "1800点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3600点";
                                        scorePayText.text = "親：1800点\n----------------\n子：900点";
                                    }
                                }
                            }
                            else
                            {
                                if (parent)
                                {
                                    scoreText.text = "1500点";
                                    scorePayText.text = "500点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "1000点";
                                    scorePayText.text = "親：500点\n----------------\n子：300点";
                                }
                            }
                        }
                        //二翻
                        else if (highHansuu == 2)
                        {
                            if (huKeisan)
                            {
                                if (highHu == 20)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "(2000点)";
                                        scorePayText.text = "700点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "(1300点)";
                                        scorePayText.text = "親：700点\n----------------\n子：400点";
                                    }
                                }
                                else if (highHu == 30)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "2900点";
                                        scorePayText.text = "1000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2000点";
                                        scorePayText.text = "親：1000点\n----------------\n子：500点";
                                    }
                                }
                                else if (highHu == 40)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "3900点";
                                        scorePayText.text = "1300点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2600点";
                                        scorePayText.text = "親：1300点\n----------------\n子：700点";
                                    }
                                }
                                else if (highHu == 50)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "4800点";
                                        scorePayText.text = "1600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3200点";
                                        scorePayText.text = "親：1600点\n----------------\n子：800点";
                                    }
                                }
                                else if (highHu == 60)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "5800点";
                                        scorePayText.text = "2000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3900点";
                                        scorePayText.text = "親：2000点\n----------------\n子：1000点";
                                    }
                                }
                                else if (highHu == 70)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "6800点";
                                        scorePayText.text = "2300点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "4500点";
                                        scorePayText.text = "親：2300点\n----------------\n子：1200点";
                                    }
                                }
                                else if (highHu == 80)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "7700点";
                                        scorePayText.text = "2600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "5200点";
                                        scorePayText.text = "親：2600点\n----------------\n子：1300点";
                                    }
                                }
                                else if (highHu == 90)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "8700点";
                                        scorePayText.text = "2900点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "5800点";
                                        scorePayText.text = "親：2900点\n----------------\n子：1500点";
                                    }
                                }
                                else if (highHu == 100)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "9600点";
                                        scorePayText.text = "3200点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "6400点";
                                        scorePayText.text = "親：3200点\n----------------\n子：1600点";
                                    }
                                }
                                else if (highHu == 110)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "10600点";
                                        scorePayText.text = "3600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "7100点";
                                        scorePayText.text = "親：3600点\n----------------\n子：1800点";
                                    }
                                }
                            }
                            else
                            {
                                if (parent)
                                {
                                    scoreText.text = "3000点";
                                    scorePayText.text = "1000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "2000点";
                                    scorePayText.text = "親：1000点\n----------------\n子：500点";
                                }
                            }
                        }
                        //三翻
                        else if (highHansuu == 3)
                        {
                            if (huKeisan)
                            {
                                if (highHu == 20)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "(3900点)";
                                        scorePayText.text = "1300点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "(2600点)";
                                        scorePayText.text = "親：1300点\n----------------\n子：700点";
                                    }
                                }
                                else if (highHu == 30)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "5800点";
                                        scorePayText.text = "2000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3900点";
                                        scorePayText.text = "親：2000点\n----------------\n子：1000点";
                                    }
                                }
                                else if (highHu == 40)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "7700点";
                                        scorePayText.text = "2600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "5200点";
                                        scorePayText.text = "親：2600点\n----------------\n子：1300点";
                                    }
                                }
                                else if (highHu == 50)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "9600点";
                                        scorePayText.text = "3200点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "6400点";
                                        scorePayText.text = "親：3200点\n----------------\n子：1600点";
                                    }
                                }
                                else if (highHu == 60)
                                {
                                    if (!kiriageMangan)
                                    {
                                        if (parent)
                                        {
                                            scoreText.text = "11600点";
                                            scorePayText.text = "3900点\nオール";
                                        }
                                        else
                                        {
                                            scoreText.text = "7700点";
                                            scorePayText.text = "親：3900点\n----------------\n子：2000点";
                                        }
                                    }
                                    else
                                    {
                                        if (parent)
                                        {
                                            scoreText.text = "12000点";
                                            scorePayText.text = "4000点\nオール";
                                        }
                                        else
                                        {
                                            scoreText.text = "8000点";
                                            scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                        }
                                    }
                                }
                                else if (highHu == 70)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 80)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 90)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 100)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 110)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                            }
                            else
                            {
                                if (parent)
                                {
                                    scoreText.text = "5800点";
                                    scorePayText.text = "2000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "3900点";
                                    scorePayText.text = "親：2000点\n----------------\n子：1000点";
                                }
                            }
                        }
                        //満貫?
                        else if (highHansuu == 4)
                        {
                            if (huKeisan)
                            {
                                if (highHu == 20)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "(7700点)";
                                        scorePayText.text = "2600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "(5200点)";
                                        scorePayText.text = "親：2600点\n----------------\n子：1300点";
                                    }
                                }
                                else if (highHu == 30)
                                {
                                    if (!kiriageMangan)
                                    {
                                        if (parent)
                                        {
                                            scoreText.text = "11600点";
                                            scorePayText.text = "3900点\nオール";
                                        }
                                        else
                                        {
                                            scoreText.text = "7700点";
                                            scorePayText.text = "親：3900点\n----------------\n子：2000点";
                                        }
                                    }
                                    else
                                    {
                                        if (parent)
                                        {
                                            scoreText.text = "12000点";
                                            scorePayText.text = "4000点\nオール";
                                        }
                                        else
                                        {
                                            scoreText.text = "8000点";
                                            scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                        }
                                    }
                                }
                                else if (highHu == 40)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 50)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 60)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 70)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 80)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 90)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 100)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                                else if (highHu == 110)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                            }
                            else
                            {
                                if (parent)
                                {
                                    scoreText.text = "12000点";
                                    scorePayText.text = "4000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "8000点";
                                    scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                }
                            }
                        }
                        //満貫
                        else if (highHansuu == 5)
                        {
                            if (parent)
                            {
                                scoreText.text = "12000点";
                                scorePayText.text = "4000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "8000点";
                                scorePayText.text = "親：4000点\n----------------\n子：2000点";
                            }
                        }
                        //跳満
                        else if (highHansuu == 6 || highHansuu == 7)
                        {
                            if (parent)
                            {
                                scoreText.text = "18000点";
                                scorePayText.text = "6000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "12000点";
                                scorePayText.text = "親：6000点\n----------------\n子：3000点";
                            }
                        }
                        //倍満
                        else if (highHansuu == 8 || highHansuu == 9 || highHansuu == 10)
                        {
                            if (parent)
                            {
                                scoreText.text = "24000点";
                                scorePayText.text = "8000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "16000点";
                                scorePayText.text = "親：8000点\n----------------\n子：4000点";
                            }
                        }
                        //三倍満
                        else if (highHansuu == 11 || highHansuu == 12)
                        {
                            if (parent)
                            {
                                scoreText.text = "36000点";
                                scorePayText.text = "12000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "24000点";
                                scorePayText.text = "親：12000点\n----------------\n子：6000点";
                            }
                        }
                        //数え役満
                        else if (highHansuu >= 13)
                        {
                            if (parent)
                            {
                                scoreText.text = "48000点";
                                scorePayText.text = "16000点\nオール";
                            }
                            else
                            {
                                scoreText.text = "32000点";
                                scorePayText.text = "親：16000点\n----------------\n子：8000点";
                            }
                        }

                        yakuText.text = highYaku;
                        if (huKeisan)
                        {
                            yakuText.text += highHuStr;
                        }
                    }
                    else
                    {
                        bool tiitoitu = false;

                        //リスト
                        calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                        calculationMahjongTileList.Insert(0, selectEndMahjongTile);
                        //HashSetを作成。HashSetは重複しない。
                        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

                        //ポン、チー、カンがなければ
                        if (kakuteiKootuCount + kakuteiKannCount + kakuteiSyuntuCount == 0)
                        {
                            //全ての役を頭ととらえ考える。
                            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
                            {
                                if (calculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                                {
                                    //2個削除。
                                    calculationMahjongTileList.Remove(mahjongTile);
                                    calculationMahjongTileList.Remove(mahjongTile);
                                }
                            }

                            //全ての牌が2こずつあったら。
                            if (calculationMahjongTileList.Count == 0)
                            {
                                tiitoitu = true;
                            }
                        }

                        if (tiitoitu)
                        {
                            TiitoituHansuuCount();

                            if (hansuu > 0)
                            {
                                DoraCheck();
                            }

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

                            if (yakuman)
                            {
                                if (parent)
                                {
                                    scoreText.text = "48000点";
                                    scorePayText.text = "16000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "32000点";
                                    scorePayText.text = "親：16000点\n----------------\n子：8000点";
                                }
                            }
                            //二翻
                            else if (highHansuu == 2)
                            {
                                if (huKeisan)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "2400点";
                                        scorePayText.text = "(800点\nオール)";
                                    }
                                    else
                                    {
                                        scoreText.text = "1600点";
                                        scorePayText.text = "(親：800点)\n----------------\n(子：400点)";
                                    }
                                }
                                else
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "3000点";
                                        scorePayText.text = "1000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "2000点";
                                        scorePayText.text = "親：1000点\n----------------\n子：500点";
                                    }
                                }
                            }
                            //三翻
                            else if (highHansuu == 3)
                            {
                                if (huKeisan)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "4800点";
                                        scorePayText.text = "1600点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3200点";
                                        scorePayText.text = "親：1600点\n----------------\n子：800点";
                                    }
                                }
                                else
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "5800点";
                                        scorePayText.text = "2000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "3900点";
                                        scorePayText.text = "親：2000点\n----------------\n子：1000点";
                                    }
                                }
                            }
                            //満貫?
                            else if (highHansuu == 4)
                            {
                                if (huKeisan)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "9600点";
                                        scorePayText.text = "3200点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "6400点";
                                        scorePayText.text = "親：3200点\n----------------\n子：1600点";
                                    }
                                }
                                else
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "12000点";
                                        scorePayText.text = "4000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "8000点";
                                        scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                    }
                                }
                            }
                            //満貫
                            else if (highHansuu == 5)
                            {
                                if (parent)
                                {
                                    scoreText.text = "12000点";
                                    scorePayText.text = "4000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "8000点";
                                    scorePayText.text = "親：4000点\n----------------\n子：2000点";
                                }
                            }
                            //跳満
                            else if (highHansuu == 6 || highHansuu == 7)
                            {
                                if (parent)
                                {
                                    scoreText.text = "18000点";
                                    scorePayText.text = "6000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "12000点";
                                    scorePayText.text = "親：6000点\n----------------\n子：3000点";
                                }
                            }
                            //倍満
                            else if (highHansuu == 8 || highHansuu == 9 || highHansuu == 10)
                            {
                                if (parent)
                                {
                                    scoreText.text = "24000点";
                                    scorePayText.text = "8000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "16000点";
                                    scorePayText.text = "親：8000点\n----------------\n子：4000点";
                                }
                            }
                            //三倍満
                            else if (highHansuu == 11 || highHansuu == 12)
                            {
                                if (parent)
                                {
                                    scoreText.text = "36000点";
                                    scorePayText.text = "12000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "24000点";
                                    scorePayText.text = "親：12000点\n----------------\n子：6000点";
                                }
                            }
                            //数え役満
                            else if (highHansuu >= 13)
                            {
                                if (parent)
                                {
                                    scoreText.text = "48000点";
                                    scorePayText.text = "16000点\nオール";
                                }
                                else
                                {
                                    scoreText.text = "32000点";
                                    scorePayText.text = "親：16000点\n----------------\n子：8000点";
                                }
                            }

                            yakuText.text = highYaku;
                            if (huKeisan)
                            {
                                yakuText.text += huStr;
                            }
                        }
                        else
                        {
                            bool kokusi = false;

                            //リスト
                            calculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                            calculationMahjongTileList.Insert(0, selectEndMahjongTile);
                            //HashSetを作成。HashSetは重複しない。
                            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);

                            //ポン、チー、カンがなければ
                            if (kakuteiKootuCount + kakuteiKannCount + kakuteiSyuntuCount == 0)
                            {
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
                                    //頭が一九字牌いずれかなら。
                                    if ((int)selectEndMahjongTile == 0 || (int)selectEndMahjongTile == 8
                                        || (int)selectEndMahjongTile == 9 || (int)selectEndMahjongTile == 17
                                        || (int)selectEndMahjongTile == 18 || (int)selectEndMahjongTile == 26
                                        || (int)selectEndMahjongTile == 27 || (int)selectEndMahjongTile == 28
                                        || (int)selectEndMahjongTile == 29 || (int)selectEndMahjongTile == 30
                                        || (int)selectEndMahjongTile == 31 || (int)selectEndMahjongTile == 32
                                        || (int)selectEndMahjongTile == 33)
                                    {
                                        kokusi = true;
                                    }
                                }
                            }

                            if (kokusi)
                            {
                                KokusiHansuuCount();

                                if (hansuu > 0)
                                {
                                    DoraCheck();
                                }

                                //役満の設定。
                                yakuman = yakumanNow;
                                //最高翻数を更新。
                                highHansuu = hansuu;
                                //最高翻数時の役を更新。
                                highYaku = yaku;

                                if (yakuman)
                                {
                                    if (parent)
                                    {
                                        scoreText.text = "48000点";
                                        scorePayText.text = "16000点\nオール";
                                    }
                                    else
                                    {
                                        scoreText.text = "32000点";
                                        scorePayText.text = "親：16000点\n----------------\n子：8000点";
                                    }
                                }

                                yakuText.text = highYaku;
                            }
                            else
                            {
                                yakuText.text = "役無し";
                                scoreText.text = "0点";
                                scorePayText.text = "";
                            }
                        }
                    }
                }
                else
                {
                    yakuText.text = "上がり牌無し\n右クリックで上がり牌を選択して下さい。";
                    scoreText.text = "0点";
                    scorePayText.text = "";
                }
            }
            else
            {
                yakuText.text = "手牌不足\n左クリックで手牌を選択して下さい。";
                scoreText.text = "0点";
                scorePayText.text = "";
            }

            scoreDisplay = true;
            scoreObj.SetActive(true);
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

                Debug.Log("頭：" + head + "/" + mahjongTile + "：刻子");

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

                    Debug.Log("頭：" + head + "/" + mahjongTile + "：順子");

                    count++;
                }
            }
        }
        return count;
    }

    void HansuuCount()
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        foreach (List<MahjongTile> mentu in kakuteiMentuMahjongTileList)
        {
            mentuMahjongTileList.Add(mentu);
        }

        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();
        foreach (List<MahjongTile> mentu in mentuMahjongTileList)
        {
            if (mentuMahjongTileHashSet.Count(item => item.SequenceEqual(mentu)) == 0)
            {
                mentuMahjongTileHashSet.Add(mentu);
            }
        }

        List<MahjongTile> allMahjongTileList = new List<MahjongTile>(totalMahjongTileList);
        allMahjongTileList.Add(selectEndMahjongTile);

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

        Debug.Log("翻数計算");

        //１翻
        addHansuu = 1;
        //立直
        if (riiti && !naki && !doubleRiiti)
        {
            hansuu += addHansuu;
            yaku += "\n立直：１翻";
        }
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        if (riiti && ippatu && !naki && !rinsyankaihoo)
        {
            hansuu += addHansuu;
            yaku += "\n一発：１翻";
        }
        //面前自摸(メンゼンツモ)
        if (tumo && !naki)
        {
            hansuu += addHansuu;
            yaku += "\n面前自摸：１翻";
        }
        //平和(ピンフ)
        bool pinhu = false;
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        if (!naki && syuntuCount == 4)
        {
            //雀頭が役牌でない
            if ((int)head != 31 && (int)head != 32 && (int)head != 33 && head != baMahjongTile && head != ziMahjongTile)
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
            if (mentu.Count(item => item == baMahjongTile) == 3)
            {
                hansuu += addHansuu;
                yaku += "\n翻牌/役牌(" + DataManager.mahjongTileNames[(int)baMahjongTile] + ")：１翻";
            }
            //場風牌
            if (mentu.Count(item => item == ziMahjongTile) == 3)
            {
                hansuu += addHansuu;
                yaku += "\n翻牌/役牌(" + DataManager.mahjongTileNames[(int)ziMahjongTile] + ")：１翻";
            }
        }
        //一盃口(イーペーコー)
        if (!naki)
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
        if (rinsyankaihoo)
        {
            hansuu += addHansuu;
            yaku += "\n嶺上開花：１翻";
        }
        //槍槓(チャンカン)
        if (tyankan)
        {
            hansuu += addHansuu;
            yaku += "\n槍槓：１翻";
        }
        //海底(ハイテイ)/河底(ホーテイ)
        if (haitei)
        {
            if (tumo)
            {
                hansuu += addHansuu;
                yaku += "\n海底：１翻";
            }
            else
            {
                hansuu += addHansuu;
                yaku += "\n河底：１翻";
            }
        }
        //２翻
        addHansuu = 2;
        //ダブル立直
        if (doubleRiiti && !naki)
        {
            hansuu += addHansuu;
            yaku += "\nダブル立直：２翻";
        }
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
                                if (!naki)
                                {
                                    hansuu += addHansuu;
                                    yaku += "\n三色同順：２翻";
                                }
                                //食い下がり１翻
                                else
                                {
                                    hansuu += 1;
                                    yaku += "\n三色同順：１翻";
                                }

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
                        if (!naki)
                        {
                            hansuu += addHansuu;
                            yaku += "\n一気通貫：２翻";
                        }
                        //食い下がり１翻
                        else
                        {
                            hansuu += 1;
                            yaku += "\n一気通貫：１翻";
                        }
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
                        if (!naki)
                        {
                            hansuu += addHansuu;
                            yaku += "\n一気通貫：２翻";
                        }
                        //食い下がり１翻
                        else
                        {
                            hansuu += 1;
                            yaku += "\n一気通貫：１翻";
                        }
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
                        if (!naki)
                        {
                            hansuu += addHansuu;
                            yaku += "\n一気通貫：２翻";
                        }
                        //食い下がり１翻
                        else
                        {
                            hansuu += 1;
                            yaku += "\n一気通貫：１翻";
                        }
                    }
                }
            }
        }
        //対々和(トイトイホー)
        //4個の刻子があったら
        if (kootuCount + kakuteiKannCount >= 4)
        {
            hansuu += addHansuu;
            yaku += "\n対々和：２翻";
        }
        //三暗刻(サンアンコー)
        if (kootuCount + anKannCount >= 3)
        {
            List<List<MahjongTile>> menzenMentu = new List<List<MahjongTile>>();

            //面子を列挙。
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                //面前なら。
                if (!kakuteiMentuMahjongTileList.Contains(mentu))
                {
                    //全て同じ牌なら。
                    if (mentu.Count(item => item == mentu[0]) == 3)
                    {
                        //牌が4つ、もしくは、(牌が上がり牌ではない、もしくは、ツモなら)無かったら。
                        //ロンで作った刻子は暗槓ではないため。
                        if (allMahjongTileList.Count(item => item == mentu[0]) == 4 || (mentu[0] != selectEndMahjongTile || tumo))
                        {
                            menzenMentu.Add(mentu);
                        }
                    }
                }
            }

            if (menzenMentu.Count + anKannCount >= 3)
            {
                hansuu += addHansuu;
                yaku += "\n三暗刻：２翻";
            }
        }
        //三槓子(サンカンツ)
        if (kakuteiKannCount >= 3)
        {
            hansuu += addHansuu;
            yaku += "\n三槓子：２翻";
        }
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
                if (!naki)
                {
                    hansuu += addHansuu;
                    yaku += "\n全帯么：２翻";
                }
                //食い下がり１翻
                else
                {
                    hansuu += 1;
                    yaku += "\n全帯么：１翻";
                }
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
        if (kootuCount + kakuteiKannCount >= 2)
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
        if (!naki)
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
                if (!naki)
                {
                    hansuu += addHansuu;
                    yaku += "\n混一色：３翻";
                }
                //食い下がり２翻
                else
                {
                    hansuu += 2;
                    yaku += "\n混一色：２翻";
                }
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
                if (!naki)
                {
                    hansuu += addHansuu;
                    yaku += "\n純全帯么：３翻";
                }
                //食い下がり２翻
                else
                {
                    hansuu += 2;
                    yaku += "\n純全帯么：２翻";
                }
            }
        }
        //６翻
        addHansuu = 6;
        //清一色(チンイーソー、チンイツ)
        //一色。
        if (totalMahjongTileList.Count(item => (int)item <= 8) == 13
            ||
            totalMahjongTileList.Count(item => (int)item <= 17) - totalMahjongTileList.Count(item => (int)item <= 8) == 13
            ||
            totalMahjongTileList.Count(item => (int)item <= 26) - totalMahjongTileList.Count(item => (int)item <= 17) == 13)
        {
            if (!naki)
            {
                hansuu += addHansuu;
                yaku += "\n清一色：６翻";
            }
            //食い下がり５翻
            else
            {
                hansuu += 5;
                yaku += "\n清一色：５翻";
            }
        }
        //役満。yakumanNow=true
        //四暗刻(スーアンコー)
        if (kootuCount + anKannCount >= 4 && !naki)
        {
            List<List<MahjongTile>> menzenMentu = new List<List<MahjongTile>>();

            //面子を列挙。
            foreach (List<MahjongTile> mentu in mentuMahjongTileList)
            {
                //面前なら。
                if (!kakuteiMentuMahjongTileList.Contains(mentu))
                {
                    //全て同じ牌なら。
                    if (mentu.Count(item => item == mentu[0]) == 3)
                    {
                        //牌が上がり牌では無かったら。
                        //ロンで作った刻子は暗槓ではないため。
                        if (mentu[0] != selectEndMahjongTile || tumo)
                        {
                            menzenMentu.Add(mentu);
                        }
                    }
                }
            }

            if (menzenMentu.Count + anKannCount >= 4)
            {
                yakumanNow = true;
                yaku += "\n四暗刻：役満";
            }
        }
        //大三元(ダイサンゲン)
        if (kootuCount + kakuteiKannCount >= 3)
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
        if (kootuCount + kakuteiKannCount >= 3)
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
        if (totalMahjongTileList.Count(item => (int)item >= 27) == 13)
        {
            yakumanNow = true;
            yaku += "\n字一色：役満";
        }
        //九連宝燈(チューレンポートン)
        //一色。1、9が各3牌。2~8が各1牌以上。
        //鳴いていないか、ポン、チー、カン(暗槓も)をしていないか。
        if (!naki && selectMahjongTileList.Count == 13)
        {
            List<MahjongTile> chuuren = new List<MahjongTile>(selectMahjongTileList);
            chuuren.Add(selectEndMahjongTile);

            //萬子一色か。
            if (chuuren.Count(item => (int)item <= 8) == 14)
            {
                if (chuuren.Count(item => (int)item == 0) >= 3 && chuuren.Count(item => (int)item == 1) >= 1 && chuuren.Count(item => (int)item == 2) >= 1
                && chuuren.Count(item => (int)item == 3) >= 1 && chuuren.Count(item => (int)item == 4) >= 1 && chuuren.Count(item => (int)item == 5) >= 1
                && chuuren.Count(item => (int)item == 6) >= 1 && chuuren.Count(item => (int)item == 7) >= 1 && chuuren.Count(item => (int)item == 8) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
            //筒子一色か。
            else if (chuuren.Count(item => (int)item <= 17) - chuuren.Count(item => (int)item <= 8) == 14)
            {
                if (chuuren.Count(item => (int)item == 9) >= 3 && chuuren.Count(item => (int)item == 10) >= 1 && chuuren.Count(item => (int)item == 11) >= 1
                && chuuren.Count(item => (int)item == 12) >= 1 && chuuren.Count(item => (int)item == 14) >= 1 && chuuren.Count(item => (int)item == 16) >= 1
                && chuuren.Count(item => (int)item == 15) >= 1 && chuuren.Count(item => (int)item == 17) >= 1 && chuuren.Count(item => (int)item == 17) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
            //索子一色か。
            else if (chuuren.Count(item => (int)item <= 26) - chuuren.Count(item => (int)item <= 8) == 14)
            {
                if (chuuren.Count(item => (int)item == 18) >= 3 && chuuren.Count(item => (int)item == 19) >= 1 && chuuren.Count(item => (int)item == 20) >= 1
                && chuuren.Count(item => (int)item == 21) >= 1 && chuuren.Count(item => (int)item == 22) >= 1 && chuuren.Count(item => (int)item == 23) >= 1
                && chuuren.Count(item => (int)item == 24) >= 1 && chuuren.Count(item => (int)item == 25) >= 1 && chuuren.Count(item => (int)item == 26) >= 3)
                {
                    yakumanNow = true;
                    yaku += "\n九連宝燈：役満";
                }
            }
        }
        //緑一色(リューイーソー)
        //緑一色。日本プロ麻雀連盟公式ルール改訂により、2023年度以降發無しでも緑一色となる。
        if (totalMahjongTileList.Count(item => (int)item == 19) + totalMahjongTileList.Count(item => (int)item == 20)
        + totalMahjongTileList.Count(item => (int)item == 21) + totalMahjongTileList.Count(item => (int)item == 23)
        + totalMahjongTileList.Count(item => (int)item == 28) + totalMahjongTileList.Count(item => (int)item == 32) == 13)
        {
            yakumanNow = true;
            yaku += "\n緑一色：役満";
        }
        //清老頭(チンロートー)
        //一九のみ。
        if (totalMahjongTileList.Count(item => (int)item == 0) + totalMahjongTileList.Count(item => (int)item == 8)
        + totalMahjongTileList.Count(item => (int)item == 9) + totalMahjongTileList.Count(item => (int)item == 17)
        + totalMahjongTileList.Count(item => (int)item == 18) + totalMahjongTileList.Count(item => (int)item == 26) == 13)
        {
            yakumanNow = true;
            yaku += "\n清老頭：役満";
        }
        //四槓子(スーカンツ)
        if (kakuteiKannCount >= 4)
        {
            yakumanNow = true;
            yaku += "\n四槓子：役満";
        }
        //天和(テンホー)/地和(チーホー)
        if (tenhoo)
        {
            if (parent)
            {
                yakumanNow = true;
                yaku += "\n天和：役満";
            }
            else
            {
                yakumanNow = true;
                yaku += "\n地和：役満";
            }
        }
    }

    void TiitoituHansuuCount()
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        yakumanNow = false;

        Debug.Log("翻数計算");

        //リスト
        List<MahjongTile> tiitoituCalculationMahjongTileList = new List<MahjongTile>(totalMahjongTileList);
        tiitoituCalculationMahjongTileList.Add(selectEndMahjongTile);
        //HashSetを作成。HashSetは重複しない。
        HashSet<MahjongTile> tiitoituCalculationMahjongTileHashSet = new HashSet<MahjongTile>(tiitoituCalculationMahjongTileList);

        //１翻
        addHansuu = 1;
        //立直
        if (riiti && !naki)
        {
            hansuu += addHansuu;
            yaku += "\n立直：１翻";
        }
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        if (riiti && ippatu && !naki && !rinsyankaihoo)
        {
            hansuu += addHansuu;
            yaku += "\n一発：１翻";
        }
        //面前自摸(メンゼンツモ)
        if (tumo && !naki)
        {
            hansuu += addHansuu;
            yaku += "\n面前自摸：１翻";
        }
        //平和(ピンフ)
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        //断么九(タンヤオ)
        //一九時牌抜き。
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item == 0) == 0 && tiitoituCalculationMahjongTileList.Count(item => (int)item == 8) == 0
            && tiitoituCalculationMahjongTileList.Count(item => (int)item == 9) == 0 && tiitoituCalculationMahjongTileList.Count(item => (int)item == 17) == 0
            && tiitoituCalculationMahjongTileList.Count(item => (int)item == 18) == 0 && tiitoituCalculationMahjongTileList.Count(item => (int)item == 26) == 0
            && tiitoituCalculationMahjongTileList.Count(item => (int)item > 26) == 0)
        {
            hansuu += addHansuu;
            yaku += "\n断么九：１翻";
        }
        //翻牌(ファンパイ)・役牌(ヤクハイ)
        //三元牌/場風牌/自風牌。
        //一盃口(イーペーコー)
        //嶺上開花(リンシャンカイホー)
        //槍槓(チャンカン)
        if (tyankan)
        {
            hansuu += addHansuu;
            yaku += "\n槍槓：１翻";
        }
        //海底(ハイテイ)/河底(ホーテイ)
        if (haitei)
        {
            if (tumo)
            {
                hansuu += addHansuu;
                yaku += "\n海底：１翻";
            }
            else
            {
                hansuu += addHansuu;
                yaku += "\n河底：１翻";
            }
        }
        //２翻
        addHansuu = 2;
        //ダブル立直
        if (doubleRiiti && !naki)
        {
            hansuu += addHansuu;
            yaku += "\nダブル立直：２翻";
        }
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
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item == 0) + tiitoituCalculationMahjongTileList.Count(item => (int)item == 8)
            + tiitoituCalculationMahjongTileList.Count(item => (int)item == 9) + tiitoituCalculationMahjongTileList.Count(item => (int)item == 17)
            + tiitoituCalculationMahjongTileList.Count(item => (int)item == 18) + tiitoituCalculationMahjongTileList.Count(item => (int)item == 26)
            + tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27) == 14)
        {
            if (!naki)
            {
                hansuu += addHansuu;
                yaku += "\n全帯么：２翻";
            }
            //食い下がり１翻
            else
            {
                hansuu += 1;
                yaku += "\n全帯么：１翻";
            }
        }
        //混老頭(ホンロウトウ)
        //全て一九字牌。
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item == 0) + tiitoituCalculationMahjongTileList.Count(item => (int)item == 8)
            + tiitoituCalculationMahjongTileList.Count(item => (int)item == 9) + tiitoituCalculationMahjongTileList.Count(item => (int)item == 17)
            + tiitoituCalculationMahjongTileList.Count(item => (int)item == 18) + tiitoituCalculationMahjongTileList.Count(item => (int)item >= 26) == 14)
        {
            if (!naki)
            {
                hansuu += addHansuu;
                yaku += "\n混老頭：２翻";
            }
            //食い下がり１翻
            else
            {
                hansuu += 1;
                yaku += "\n混老頭：１翻";
            }
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
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item <= 8) + tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27) == 14
            ||
            tiitoituCalculationMahjongTileList.Count(item => (int)item <= 17) + tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27)
            - tiitoituCalculationMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            tiitoituCalculationMahjongTileList.Count(item => (int)item <= 26) + tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27)
            - tiitoituCalculationMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            //字牌があったら。
            //字牌がなかったら、清一色になるため。処理しない。
            if (tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27) >= 1)
            {
                if (!naki)
                {
                    hansuu += addHansuu;
                    yaku += "\n混一色：３翻";
                }
                //食い下がり２翻
                else
                {
                    hansuu += 2;
                    yaku += "\n混一色：２翻";
                }
            }
        }
        //純全帯么(ジュンチャンタ)
        //一九絡め。一九が六個しかないため不可。
        //６翻
        addHansuu = 6;
        //清一色(チンイーソー、チンイツ)
        //一色。
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            tiitoituCalculationMahjongTileList.Count(item => (int)item <= 17) - tiitoituCalculationMahjongTileList.Count(item => (int)item <= 8) == 14
            ||
            tiitoituCalculationMahjongTileList.Count(item => (int)item <= 26) - tiitoituCalculationMahjongTileList.Count(item => (int)item <= 17) == 14)
        {
            if (!naki)
            {
                hansuu += addHansuu;
                yaku += "\n清一色：６翻";
            }
            //食い下がり５翻
            else
            {
                hansuu += 5;
                yaku += "\n清一色：５翻";
            }
        }
        //役満。yakumanNow=true
        //四暗刻(スーアンコー)
        //大三元(ダイサンゲン)
        //国士無双
        //別に判定。
        //四喜和(スーシーホー)
        //字一色(ツーイーソー)
        //字牌。
        if (tiitoituCalculationMahjongTileList.Count(item => (int)item >= 27) == 14)
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
        if (tenhoo)
        {
            if (parent)
            {
                yakumanNow = true;
                yaku += "\n天和：役満";
            }
            else
            {
                yakumanNow = true;
                yaku += "\n地和：役満";
            }
        }
    }

    void KokusiHansuuCount()
    {
        //役は　麻雀の役一覧【簡単でわかりやすいイラスト形式】　と　日本プロ麻雀連盟の採用役一覧　を参考に。
        //翻数
        hansuu = 0;
        //何翻の役か。
        int addHansuu = 0;
        yaku = "役一覧";

        yakumanNow = false;

        Debug.Log("翻数計算");

        //リスト
        List<MahjongTile> kokusiCalculationMahjongTileList = new List<MahjongTile>(totalMahjongTileList);
        kokusiCalculationMahjongTileList.Add(selectEndMahjongTile);
        //HashSetを作成。HashSetは重複しない。
        HashSet<MahjongTile> kokusiCalculationMahjongTileHashSet = new HashSet<MahjongTile>(kokusiCalculationMahjongTileList);

        //１翻
        addHansuu = 1;
        //立直
        if (riiti && !naki)
        {
            hansuu += addHansuu;
            yaku += "\n立直：１翻";
        }
        //一発
        //嶺上開花と複合しない。
        //実は立直にチェックを入れないと、一発にチェックを入れれないようになっている。念のため。
        if (riiti && ippatu && !naki && !rinsyankaihoo)
        {
            hansuu += addHansuu;
            yaku += "\n一発：１翻";
        }
        //面前自摸(メンゼンツモ)
        if (tumo && !naki)
        {
            hansuu += addHansuu;
            yaku += "\n面前自摸：１翻";
        }
        //平和(ピンフ)
        //鳴いていない、順子が4つ、雀頭が役牌でない、リャンメン待ち。
        //断么九(タンヤオ)
        //一九時牌抜き。国士無双は一九字牌のみ。
        //翻牌(ファンパイ)・役牌(ヤクハイ)
        //三元牌/場風牌/自風牌。
        //一盃口(イーペーコー)
        //嶺上開花(リンシャンカイホー)
        //槍槓(チャンカン)
        if (tyankan)
        {
            hansuu += addHansuu;
            yaku += "\n槍槓：１翻";
        }
        //海底(ハイテイ)/河底(ホーテイ)
        if (haitei)
        {
            if (tumo)
            {
                hansuu += addHansuu;
                yaku += "\n海底：１翻";
            }
            else
            {
                hansuu += addHansuu;
                yaku += "\n河底：１翻";
            }
        }
        //２翻
        addHansuu = 2;
        //ダブル立直
        if (doubleRiiti && !naki)
        {
            hansuu += addHansuu;
            yaku += "\nダブル立直：２翻";
        }
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
        if (!naki)
        {
            hansuu += addHansuu;
            yaku += "\n全帯么：２翻";
        }
        //食い下がり１翻
        else
        {
            hansuu += 1;
            yaku += "\n全帯么：１翻";
        }
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
        if (tenhoo)
        {
            if (parent)
            {
                yakumanNow = true;
                yaku += "\n天和：役満";
            }
            else
            {
                yakumanNow = true;
                yaku += "\n地和：役満";
            }
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
        if (!naki && syuntuCount == 4 && (int)head != 31 && (int)head != 32 && (int)head != 33 && head != baMahjongTile && head != ziMahjongTile && ryanmen
            && tumo)
        {
            hu = 20;
            huStr += "\nピンフツモ：一律２０符";
        }
        //鳴いている、順子が4つ、雀頭が役牌でない、リャンメン待ち。ロン。
        else if (naki && syuntuCount == 4 && (int)head != 31 && (int)head != 32 && (int)head != 33 && head != baMahjongTile && head != ziMahjongTile && ryanmen
            && !tumo)
        {
            hu = 20;
            huStr += "\n喰いピンフ：３０符";
        }
        else
        {
            //副底(フーテイ)
            hu = 20;
            huStr += "\n副底：２０符";


            if (!naki && !tumo)
            {
                //メンゼンロン
                hu += 10;
                huStr += "\nメンゼンロン：１０符";
            }
            else if (tumo)
            {
                //ツモ
                hu += 2;
                huStr += "\nツモ：２符";
            }


            //順子は0符。
            //明刻(ミンコ)
            foreach (List<MahjongTile> mentu in kakuteiKootuMentu)
            {
                //2~8なら
                if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7) || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16) || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                {
                    hu += 2;
                    huStr += "\n２～８の明刻：２符";
                }
            }
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
            foreach (List<MahjongTile> mentu in kakuteiMinkanMentu)
            {
                //2~8なら
                if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7) || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16) || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                {
                    hu += 8;
                    huStr += "\n２～８の明槓：８符";
                }
            }
            //暗槓(アンカン)
            foreach (List<MahjongTile> mentu in kakuteiAnkanMentu)
            {
                //2~8なら
                if (((int)mentu[0] >= 1 && (int)mentu[0] <= 7) || ((int)mentu[0] >= 10 && (int)mentu[0] <= 16) || ((int)mentu[0] >= 19 && (int)mentu[0] <= 25))
                {
                    hu += 16;
                    huStr += "\n２～８の暗槓：１６符";
                }
            }

            //明刻(ミンコ)
            foreach (List<MahjongTile> mentu in kakuteiKootuMentu)
            {
                //一九字牌なら
                if ((int)mentu[0] == 0 || (int)mentu[0] == 8 || (int)mentu[0] == 9 || (int)mentu[0] == 17 || (int)mentu[0] == 18 || (int)mentu[0] == 26
                    || (int)mentu[0] >= 27)
                {
                    hu += 4;
                    huStr += "\n１・９・字牌の明刻：４符";
                }
            }
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
            foreach (List<MahjongTile> mentu in kakuteiMinkanMentu)
            {
                //一九字牌なら
                if ((int)mentu[0] == 0 || (int)mentu[0] == 8 || (int)mentu[0] == 9 || (int)mentu[0] == 17 || (int)mentu[0] == 18 || (int)mentu[0] == 26
                    || (int)mentu[0] >= 27)
                {
                    hu += 16;
                    huStr += "\n１・９・字牌の明槓：１６符";
                }
            }
            //暗槓(アンカン)
            foreach (List<MahjongTile> mentu in kakuteiAnkanMentu)
            {
                //一九字牌なら
                if ((int)mentu[0] == 0 || (int)mentu[0] == 8 || (int)mentu[0] == 9 || (int)mentu[0] == 17 || (int)mentu[0] == 18 || (int)mentu[0] == 26
                    || (int)mentu[0] >= 27)
                {
                    hu += 32;
                    huStr += "\n１・９・字牌の暗槓：３２符";
                }
            }


            if (!ryenfonpaiYonHu)
            {
                if (head == baMahjongTile || head == ziMahjongTile || (int)head == 31 || (int)head == 32 || (int)head == 33)
                {
                    hu += 2;
                    huStr += "\n雀頭が役牌：２符";
                }
            }
            else
            {
                if (head == baMahjongTile && head == ziMahjongTile)
                {
                    hu += 4;
                    huStr += "\n雀頭が連風牌：４符";
                }
                else if (head == baMahjongTile || head == ziMahjongTile || (int)head == 31 || (int)head == 32 || (int)head == 33)
                {
                    hu += 2;
                    huStr += "\n雀頭が役牌：２符";
                }
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

        //旧
        if (0 == 1)
        {
            //リャンメン。(旧：平和)
            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
            {
                //リスト
                List<MahjongTile> ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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

                int count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                if (ryanmenCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == 1
                        || (int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == -1)
                    {
                        //各2~8だったら。
                        if (((int)ryanmenCalculationMahjongTileList[0] >= 1 && (int)ryanmenCalculationMahjongTileList[0] <= 7)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 10 && (int)ryanmenCalculationMahjongTileList[0] <= 16)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 19 && (int)ryanmenCalculationMahjongTileList[0] <= 25))
                        {
                            //各2~8だったら。
                            if (((int)ryanmenCalculationMahjongTileList[1] >= 1 && (int)ryanmenCalculationMahjongTileList[1] <= 7)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 10 && (int)ryanmenCalculationMahjongTileList[1] <= 16)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 19 && (int)ryanmenCalculationMahjongTileList[1] <= 25))
                            {
                                //上がり牌がどちらかと隣接していたら。
                                if ((int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
                                {
                                    ryanmen = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //リスト
                ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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

                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                if (ryanmenCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == 1
                        || (int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == -1)
                    {
                        //各2~8だったら。
                        if (((int)ryanmenCalculationMahjongTileList[0] >= 1 && (int)ryanmenCalculationMahjongTileList[0] <= 7)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 10 && (int)ryanmenCalculationMahjongTileList[0] <= 16)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 19 && (int)ryanmenCalculationMahjongTileList[0] <= 25))
                        {
                            //各2~8だったら。
                            if (((int)ryanmenCalculationMahjongTileList[1] >= 1 && (int)ryanmenCalculationMahjongTileList[1] <= 7)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 10 && (int)ryanmenCalculationMahjongTileList[1] <= 16)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 19 && (int)ryanmenCalculationMahjongTileList[1] <= 25))
                            {
                                //上がり牌がどちらかと隣接していたら。
                                if ((int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
                                {
                                    ryanmen = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //リスト
                ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (1 > 0 && 1 == count)
                        {
                            break;
                        }
                    }
                }
                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (ryanmenCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);
                        ryanmenCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                if (ryanmenCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == 1
                        || (int)ryanmenCalculationMahjongTileList[0] - (int)ryanmenCalculationMahjongTileList[1] == -1)
                    {
                        //各2~8だったら。
                        if (((int)ryanmenCalculationMahjongTileList[0] >= 1 && (int)ryanmenCalculationMahjongTileList[0] <= 7)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 10 && (int)ryanmenCalculationMahjongTileList[0] <= 16)
                            || ((int)ryanmenCalculationMahjongTileList[0] >= 19 && (int)ryanmenCalculationMahjongTileList[0] <= 25))
                        {
                            //各2~8だったら。
                            if (((int)ryanmenCalculationMahjongTileList[1] >= 1 && (int)ryanmenCalculationMahjongTileList[1] <= 7)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 10 && (int)ryanmenCalculationMahjongTileList[1] <= 16)
                                || ((int)ryanmenCalculationMahjongTileList[1] >= 19 && (int)ryanmenCalculationMahjongTileList[1] <= 25))
                            {
                                //上がり牌がどちらかと隣接していたら。
                                if ((int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)selectEndMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
                                {
                                    ryanmen = true;
                                    break;
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
                List<MahjongTile> pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                pentyanCalculationMahjongTileList.Remove(mahjongTile);
                pentyanCalculationMahjongTileList.Remove(mahjongTile);

                int count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                if (pentyanCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == 1
                        || (int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == -1)
                    {
                        //どちらかがいずれかの1、9だったら。
                        if (((int)pentyanCalculationMahjongTileList[0] == 0 || (int)pentyanCalculationMahjongTileList[0] == 8
                            || (int)pentyanCalculationMahjongTileList[0] == 9 || (int)pentyanCalculationMahjongTileList[0] == 17
                            || (int)pentyanCalculationMahjongTileList[0] == 18 || (int)pentyanCalculationMahjongTileList[0] == 26)
                            ||
                            ((int)pentyanCalculationMahjongTileList[1] == 0 || (int)pentyanCalculationMahjongTileList[1] == 8
                            || (int)pentyanCalculationMahjongTileList[1] == 9 || (int)pentyanCalculationMahjongTileList[1] == 17
                            || (int)pentyanCalculationMahjongTileList[1] == 18 || (int)pentyanCalculationMahjongTileList[1] == 26))
                        {
                            pentyan = true;
                            break;
                        }
                    }
                }

                //リスト
                pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                pentyanCalculationMahjongTileList.Remove(mahjongTile);
                pentyanCalculationMahjongTileList.Remove(mahjongTile);

                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                if (pentyanCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == 1
                        || (int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == -1)
                    {
                        //どちらかがいずれかの1、9だったら。
                        if (((int)pentyanCalculationMahjongTileList[0] == 0 || (int)pentyanCalculationMahjongTileList[0] == 8
                            || (int)pentyanCalculationMahjongTileList[0] == 9 || (int)pentyanCalculationMahjongTileList[0] == 17
                            || (int)pentyanCalculationMahjongTileList[0] == 18 || (int)pentyanCalculationMahjongTileList[0] == 26)
                            ||
                            ((int)pentyanCalculationMahjongTileList[1] == 0 || (int)pentyanCalculationMahjongTileList[1] == 8
                            || (int)pentyanCalculationMahjongTileList[1] == 9 || (int)pentyanCalculationMahjongTileList[1] == 17
                            || (int)pentyanCalculationMahjongTileList[1] == 18 || (int)pentyanCalculationMahjongTileList[1] == 26))
                        {
                            pentyan = true;
                            break;
                        }
                    }
                }

                //リスト
                pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                pentyanCalculationMahjongTileList.Remove(mahjongTile);
                pentyanCalculationMahjongTileList.Remove(mahjongTile);

                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                if (pentyanCalculationMahjongTileList.Count == 2)
                {
                    //隣接していたら
                    if ((int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == 1
                        || (int)pentyanCalculationMahjongTileList[0] - (int)pentyanCalculationMahjongTileList[1] == -1)
                    {
                        //どちらかがいずれかの1、9だったら。
                        if (((int)pentyanCalculationMahjongTileList[0] == 0 || (int)pentyanCalculationMahjongTileList[0] == 8
                            || (int)pentyanCalculationMahjongTileList[0] == 9 || (int)pentyanCalculationMahjongTileList[0] == 17
                            || (int)pentyanCalculationMahjongTileList[0] == 18 || (int)pentyanCalculationMahjongTileList[0] == 26)
                            ||
                            ((int)pentyanCalculationMahjongTileList[1] == 0 || (int)pentyanCalculationMahjongTileList[1] == 8
                            || (int)pentyanCalculationMahjongTileList[1] == 9 || (int)pentyanCalculationMahjongTileList[1] == 17
                            || (int)pentyanCalculationMahjongTileList[1] == 18 || (int)pentyanCalculationMahjongTileList[1] == 26))
                        {
                            pentyan = true;
                            break;
                        }
                    }
                }
            }
            //カンチャン。
            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
            {
                //リスト
                List<MahjongTile> kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                kantyanCalculationMahjongTileList.Remove(mahjongTile);
                kantyanCalculationMahjongTileList.Remove(mahjongTile);

                int count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                if (kantyanCalculationMahjongTileList.Count == 2)
                {
                    //一個飛ばしだったら
                    if ((int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == 2
                        || (int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == -2)
                    {
                        //上がり牌が二つの牌の間だったら
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == -1))
                        {
                            kantyan = true;
                            break;
                        }
                    }
                }

                //リスト
                kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                kantyanCalculationMahjongTileList.Remove(mahjongTile);
                kantyanCalculationMahjongTileList.Remove(mahjongTile);

                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                if (kantyanCalculationMahjongTileList.Count == 2)
                {
                    //一個飛ばしだったら
                    if ((int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == 2
                        || (int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == -2)
                    {
                        //上がり牌が二つの牌の間だったら
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == -1))
                        {
                            kantyan = true;
                            break;
                        }
                    }
                }

                //リスト
                kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                kantyanCalculationMahjongTileList.Remove(mahjongTile);
                kantyanCalculationMahjongTileList.Remove(mahjongTile);

                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (1 > 0 && 1 == count)
                        {
                            break;
                        }
                    }
                }
                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                        }
                    }
                }
                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
                {
                    //3個以上あれば
                    if (kantyanCalculationMahjongTileList.Count(item => item == kootuMahjongTile) >= 3)
                    {
                        //3個削除。
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);
                        kantyanCalculationMahjongTileList.Remove(kootuMahjongTile);

                        count++;

                        if (0 > 0 && 0 == count)
                        {
                            break;
                        }
                    }
                }
                if (kantyanCalculationMahjongTileList.Count == 2)
                {
                    //一個飛ばしだったら
                    if ((int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == 2
                        || (int)kantyanCalculationMahjongTileList[0] - (int)kantyanCalculationMahjongTileList[1] == -2)
                    {
                        //上がり牌が二つの牌の間だったら
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)selectEndMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)selectEndMahjongTile == -1))
                        {
                            kantyan = true;
                            break;
                        }
                    }
                }
            }
            //タンキ
            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
            {
                //リスト
                List<MahjongTile> tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                if (mahjongTile != selectEndMahjongTile)
                {
                    continue;
                }

                if (tankiCalculationMahjongTileList.Count(item => item == mahjongTile) >= 2)
                {
                    //2個削除。頭の分。
                    tankiCalculationMahjongTileList.Remove(mahjongTile);
                    tankiCalculationMahjongTileList.Remove(mahjongTile);
                }
                //適当。
                else if (tankiCalculationMahjongTileList.Count(item => item == mahjongTile) == 1)
                {
                    tanki = true;
                }

                int count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                if (tankiCalculationMahjongTileList.Count == 1)
                {
                    //同じだったら
                    if (tankiCalculationMahjongTileList[0] == selectEndMahjongTile)
                    {
                        tanki = true;
                        break;
                    }
                }

                //リスト
                tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                tankiCalculationMahjongTileList.Remove(mahjongTile);
                tankiCalculationMahjongTileList.Remove(mahjongTile);

                //順子の削除。
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                if (tankiCalculationMahjongTileList.Count == 1)
                {
                    //同じだったら
                    if (tankiCalculationMahjongTileList[0] == selectEndMahjongTile)
                    {
                        tanki = true;
                        break;
                    }
                }

                //リスト
                tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

                //2個削除。頭の分。
                tankiCalculationMahjongTileList.Remove(mahjongTile);
                tankiCalculationMahjongTileList.Remove(mahjongTile);

                count = 0;
                //刻子の削除。
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile syuntuMahjongTile in calculationMahjongTileHashSet)
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
                foreach (MahjongTile kootuMahjongTile in calculationMahjongTileHashSet)
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
                if (tankiCalculationMahjongTileList.Count == 1)
                {
                    //同じだったら
                    if (tankiCalculationMahjongTileList[0] == selectEndMahjongTile)
                    {
                        tanki = true;
                        break;
                    }
                }
            }
        }
        //新
        else if (0 == 0)
        {
            //リャンメン。(旧：平和)
            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
            {
                //リスト
                List<MahjongTile> ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                ryanmenCalculationMahjongTileList.Insert(0, selectEndMahjongTile);
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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                ryanmenCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                List<MahjongTile> pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                pentyanCalculationMahjongTileList.Insert(0, selectEndMahjongTile);
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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                pentyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
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
                List<MahjongTile> kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                kantyanCalculationMahjongTileList.Insert(0, selectEndMahjongTile);
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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[0] - (int)selectEndMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[1] - (int)selectEndMahjongTile == -1))
                                {
                                    kantyan = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //リスト
                kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[0] - (int)selectEndMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[1] - (int)selectEndMahjongTile == -1))
                                {
                                    kantyan = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //リスト
                kantyanCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);

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
                        if (mentu.Count(item => item == selectEndMahjongTile) == 1)
                        {
                            mentu.Remove(selectEndMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[0] - (int)selectEndMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)selectEndMahjongTile == 1
                                    || (int)mentu[1] - (int)selectEndMahjongTile == -1))
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
                List<MahjongTile> tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                tankiCalculationMahjongTileList.Insert(0, selectEndMahjongTile);
                List<MahjongTile> tankiCalculationMahjongTileListCopy = new List<MahjongTile>(tankiCalculationMahjongTileList);

                //頭にならない。
                if (tankiCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
                {
                    continue;
                }


                if (mahjongTile != selectEndMahjongTile)
                {
                    continue;
                }

                if (tankiCalculationMahjongTileList.Count(item => item == selectEndMahjongTile) >= 2)
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
                tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                tankiCalculationMahjongTileList.Insert(0, selectEndMahjongTile);

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
                tankiCalculationMahjongTileList = new List<MahjongTile>(selectMahjongTileList);
                tankiCalculationMahjongTileList.Insert(0, selectEndMahjongTile);

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
    }

    void DoraCheck()
    {
        int doraCount = 0;
        List<MahjongTile> doraCheck = new List<MahjongTile>(totalMahjongTileList);
        doraCheck.Add(selectEndMahjongTile);
        //カンしている牌は便宜上、3牌で扱っているため、ここで1牌足しておく。
        foreach (List<MahjongTile> mentu in kakuteiMinkanMentu)
        {
            doraCheck.Add(mentu[0]);
        }
        foreach (List<MahjongTile> mentu in kakuteiAnkanMentu)
        {
            doraCheck.Add(mentu[0]);
        }

        foreach (MahjongTile dora in doraMahjongTileList)
        {
            doraCount += doraCheck.Count(item => item == dora);
        }

        if (doraCount >= 1)
        {
            hansuu += doraCount;
            yaku += "\nドラ：" + doraCount + "翻";
        }
    }

    public void ScoreHide()
    {
        scoreObj.SetActive(false);

        scoreDisplay = false;
    }

    void DebugMahjongTileGenerate()
    {
        Clear();

        List<MahjongTile> debugMahjongTileList = new List<MahjongTile>();
        for (int i = 0; i <= 33; i++)
        {
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
        }
        HashSet<MahjongTile> debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);


        //bool型の設定。
        bool nakiNasi = false;
        //今はすべて無効になっている。
        int boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //親
        if (boolRnd <= 25)
        {
            ParentCheckBoxUpdate();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //鳴き無し
        if (boolRnd <= 75)
        {
            nakiNasi = true;
        }
        //立直
        if (boolRnd <= 60)
        {
            RiitiCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //ツモ
        if (boolRnd <= 45)
        {
            TumoCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //鳴き
        //鳴きは判定無し。立直でない時の面子作成時にポン、チー、明槓をしたら鳴きにチェックが入る。
        //一発。立直があったら。0.35*0.2=0.07。
        if (boolRnd <= 20 && riiti)
        {
            IppatuCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //槍槓。ツモがなければ。(1-0.3)*0.01=0.007。
        if (boolRnd <= 1 && !tumo)
        {
            TyankanCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //嶺上開花。ツモがあれば。0.3*0.01=0.003。
        if (boolRnd <= 1 && tumo)
        {
            RinsyankaihooCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //海底/河底。
        if (boolRnd <= 1)
        {
            HaiteiCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //ダブル立直。立直がなければ。(1-0.35)*0.01=0.0065
        if (boolRnd <= 1)
        {
            DoubleRiitiCheckChange();
        }
        boolRnd = UnityEngine.Random.Range(1, 100 + 1);
        //天和/地和。立直がなければ。(1-0.35)*0.01=0.0065
        if (boolRnd <= 1 && !riiti && tumo)
        {
            TenhooCheckChange();
        }
        //立直かダブル立直があれば鳴きは無し。嶺上開花があればカンを必ずする。


        //場風牌、自風牌の作成。
        //新たに牌を設定。
        int bahuuhaiRnd = UnityEngine.Random.Range(1, 100 + 1);
        //東場
        if (bahuuhaiRnd <= 70)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_East;

            baMahjongTile = mahjongTile;
            baMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)baMahjongTile];
        }
        //南場
        else if (bahuuhaiRnd <= 97)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_South;

            baMahjongTile = mahjongTile;
            baMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)baMahjongTile];
        }
        //西場
        else if (bahuuhaiRnd <= 99)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_West;

            baMahjongTile = mahjongTile;
            baMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)baMahjongTile];
        }
        //北場
        else if (bahuuhaiRnd <= 100)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_North;

            baMahjongTile = mahjongTile;
            baMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)baMahjongTile];
        }
        //新たに牌を設定。
        int zihuuhaiRnd = UnityEngine.Random.Range(1, 100 + 1);
        //東場
        if (zihuuhaiRnd <= 25)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_East;

            ziMahjongTile = mahjongTile;
            ziMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)ziMahjongTile];
        }
        //南場
        else if (zihuuhaiRnd <= 50)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_South;

            ziMahjongTile = mahjongTile;
            ziMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)ziMahjongTile];
        }
        //西場
        else if (zihuuhaiRnd <= 75)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_West;

            ziMahjongTile = mahjongTile;
            ziMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)ziMahjongTile];
        }
        //北場
        else if (zihuuhaiRnd <= 100)
        {
            MahjongTile mahjongTile = MahjongTile.Kazehai_North;

            ziMahjongTile = mahjongTile;
            ziMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)ziMahjongTile];
        }
        ParentCheck();


        //ドラ作成。
        int doraCountRnd = UnityEngine.Random.Range(1, 100 + 1);
        int doraCount = 0;
        if (doraCountRnd <= 70)
        {
            doraCount = 1;
        }
        else if (doraCount <= 94)
        {
            doraCount = 2;
        }
        else if (doraCount <= 97)
        {
            doraCount = 3;
        }
        else if (doraCount <= 99)
        {
            doraCount = 4;
        }
        else if (doraCount <= 100)
        {
            doraCount = 5;
        }
        if (riiti || doubleRiiti)
        {
            doraCount *= 2;
        }
        for (int i = 1; i <= doraCount; i++)
        {
            debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);
            HashSet<MahjongTile> debugDoraMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

            //ないものを削除。
            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
            {
                if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) == 0)
                {
                    debugDoraMahjongTileHashSet.Remove(removeDebugMahjongTile);
                }
            }
            //HashSetでは要素を取得できないため、HashSetListを作成。
            List<MahjongTile> debugDoraMahjongTileHashSetList = new List<MahjongTile>(debugDoraMahjongTileHashSet);

            //ランダムに一つ牌を選ぶ。
            MahjongTile mahjongTile = debugDoraMahjongTileHashSetList[UnityEngine.Random.Range(0, debugDoraMahjongTileHashSet.Count)];

            DoraMahjongTileSelect(mahjongTile);

            debugMahjongTileList.Remove(mahjongTile);
        }


        //雀頭作成。
        //リセットされた直後のため2個以上ある前提。
        MahjongTile headMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), UnityEngine.Random.Range(0, 33 + 1));

        //雀頭だから2回同じことを繰り返す。
        for (int i = 1; i <= 2; i++)
        {
            int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
            if (tyankan)
            {
                agarihaiRnd = 1;
            }
            //上がり牌に設定。
            if (agarihaiRnd == 0 && selectEndMahjongTile == MahjongTile.NoSelect)
            {
                selectEndMahjongTile = headMahjongTile;
                selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];

                debugMahjongTileList.Remove(headMahjongTile);
            }
            else
            {
                //頭の分。
                GameObject generateMahjongTileImageObj =
                    Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                MahjongTileSelect(headMahjongTile);

                debugMahjongTileList.Remove(headMahjongTile);
            }
        }


        //立直かダブル立直があれば鳴きは無し。嶺上開花があればカンを必ずする。
        //ここで七対子か分岐する。雀頭作成まではOK。
        //面子作成。
        while (totalMahjongTileList.Count < 13)
        {
            debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);

            int rnd = 0;
            //鳴き無しか立直かダブル立直なら鳴きは無し。もしくは、最後の面子作成かつ上がり牌が設定されていなければ(鳴くと、上がり牌は設定できない)。
            if (nakiNasi || riiti || doubleRiiti || (totalMahjongTileList.Count == 11 && selectEndMahjongTile == MahjongTile.NoSelect))
            {
                rnd = UnityEngine.Random.Range(1, 100 + 1);
                if (rnd <= 35)
                {
                    //35%で刻子。
                    rnd = 1;
                }
                else
                {
                    //65%で順子。
                    rnd = 2;
                }
            }
            //カンした分ドラが増えるため、ドラの個数以上カンをしないように。立直の可能性はないため、doraCountをそのまま使用。
            //初めからドラ一つ確定なため、-1。
            else if (kakuteiKannCount < doraCount - 1)
            {
                rnd = UnityEngine.Random.Range(1, 100 + 1);
                if (rnd <= 15)
                {
                    //15%で刻子。
                    rnd = 1;
                }
                else if (rnd <= 40)
                {
                    //25%で順子。
                    rnd = 2;
                }
                else if (rnd <= 55)
                {
                    //15%でポン。
                    rnd = 3;
                }
                else if (rnd <= 85)
                {
                    //30%でチー。
                    rnd = 4;
                }
                else if (rnd <= 95)
                {
                    //10%で明槓。
                    rnd = 5;
                }
                else
                {
                    //5%で暗槓。
                    rnd = 6;
                }
            }
            else
            {
                rnd = UnityEngine.Random.Range(1, 4 + 1);
            }
            if (tyankan && selectEndMahjongTile == MahjongTile.NoSelect)
            {
                rnd = 2;
            }
            if (rinsyankaihoo && kakuteiKannCount == 0)
            {
                rnd = UnityEngine.Random.Range(5, 6 + 1);
            }
            //刻子
            if (rnd == 1)
            {
                HashSet<MahjongTile> debugKootuMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //2つ以下のものを削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) <= 2)
                    {
                        debugKootuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugKootuMahjongTileHashSetList = new List<MahjongTile>(debugKootuMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugKootuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugKootuMahjongTileHashSet.Count)];

                //刻子だから三回同じことを繰り返す。
                for (int i = 1; i <= 3; i++)
                {
                    int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && selectEndMahjongTile == MahjongTile.NoSelect)
                    {
                        selectEndMahjongTile = mahjongTile;
                        selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    else
                    {
                        GameObject generateMahjongTileImageObj =
                            Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                        selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                        MahjongTileSelect(mahjongTile);

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                }
            }
            //順子
            else if (rnd == 2)
            {
                HashSet<MahjongTile> debugSyuntuMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //1つ隣、2つ隣が0のもの、もしくは、8、9、字牌を削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    //1~7なら
                    if ((int)removeDebugMahjongTile <= 6 || ((int)removeDebugMahjongTile >= 9 && (int)removeDebugMahjongTile <= 15)
                    || ((int)removeDebugMahjongTile >= 18 && (int)removeDebugMahjongTile <= 24))
                    {
                        if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) == 0
                            || debugMahjongTileList.Count(item => (int)item == (int)removeDebugMahjongTile + 1) == 0
                            || debugMahjongTileList.Count(item => (int)item == (int)removeDebugMahjongTile + 2) == 0)
                        {
                            debugSyuntuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                        }
                    }
                    //そうでなければ
                    else
                    {
                        debugSyuntuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugSyuntuMahjongTileHashSetList = new List<MahjongTile>(debugSyuntuMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugSyuntuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugSyuntuMahjongTileHashSet.Count)];


                //順子1つ目。一番低い数字。
                int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                if (tyankan)
                {
                    agarihaiRnd = 0;
                }
                //上がり牌に設定。
                if (agarihaiRnd == 0 && selectEndMahjongTile == MahjongTile.NoSelect)
                {
                    selectEndMahjongTile = mahjongTile;
                    selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];

                    debugMahjongTileList.Remove(mahjongTile);
                }
                else
                {
                    GameObject generateMahjongTileImageObj =
                        Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                    selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                    MahjongTileSelect(mahjongTile);

                    debugMahjongTileList.Remove(mahjongTile);
                }
                //順子2つ目。間の数字。
                agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                //上がり牌に設定。
                if (agarihaiRnd == 0 && selectEndMahjongTile == MahjongTile.NoSelect)
                {
                    selectEndMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1);
                    selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];

                    debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                else
                {
                    GameObject generateMahjongTileImageObj =
                        Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                    selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                    MahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));

                    debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                }
                //順子3つ目。間の数字。
                agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                //上がり牌に設定。
                if (agarihaiRnd == 0 && selectEndMahjongTile == MahjongTile.NoSelect)
                {
                    selectEndMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2);
                    selectEndMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)selectEndMahjongTile];

                    debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
                else
                {
                    GameObject generateMahjongTileImageObj =
                        Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                    selectMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                    MahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));

                    debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                }
            }
            //ポン
            else if (rnd == 3)
            {
                HashSet<MahjongTile> debugPonMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //2つ以下のものを削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) <= 2)
                    {
                        debugPonMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugPonMahjongTileHashSetList = new List<MahjongTile>(debugPonMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugPonMahjongTileHashSetList[UnityEngine.Random.Range(0, debugPonMahjongTileHashSet.Count)];

                GameObject generatePonObj = Instantiate(ponObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                PonMahjongTileSelect(mahjongTile, generatePonObj);

                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
            }
            //チー
            else if (rnd == 4)
            {
                HashSet<MahjongTile> debugChiiMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //1つ隣、2つ隣が0のもの、もしくは、8、9、字牌を削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    //1~7なら
                    if ((int)removeDebugMahjongTile <= 6 || ((int)removeDebugMahjongTile >= 9 && (int)removeDebugMahjongTile <= 15)
                    || ((int)removeDebugMahjongTile >= 18 && (int)removeDebugMahjongTile <= 24))
                    {
                        if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) == 0
                            || debugMahjongTileList.Count(item => (int)item == (int)removeDebugMahjongTile + 1) == 0
                            || debugMahjongTileList.Count(item => (int)item == (int)removeDebugMahjongTile + 2) == 0)
                        {
                            debugChiiMahjongTileHashSet.Remove(removeDebugMahjongTile);
                        }
                    }
                    //そうでなければ
                    else
                    {
                        debugChiiMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugChiiMahjongTileHashSetList = new List<MahjongTile>(debugChiiMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugChiiMahjongTileHashSetList[UnityEngine.Random.Range(0, debugChiiMahjongTileHashSet.Count)];

                GameObject generateChiiObj = Instantiate(ponObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                ChiiMahjongTileSelect(mahjongTile, generateChiiObj);

                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
            }
            //明槓
            else if (rnd == 5)
            {
                HashSet<MahjongTile> debugMinKanMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //3つ以下のものを削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) <= 3)
                    {
                        debugMinKanMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugMinkanMahjongTileHashSetList = new List<MahjongTile>(debugMinKanMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugMinkanMahjongTileHashSetList[UnityEngine.Random.Range(0, debugMinKanMahjongTileHashSet.Count)];

                GameObject generateMinkannObj = Instantiate(minKannObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                KannMahjongTileSelect(mahjongTile, generateMinkannObj, false);

                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
            }
            //暗槓
            else if (rnd == 6)
            {
                HashSet<MahjongTile> debugAnKanMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileHashSet);

                //3つ以下のものを削除。
                foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                {
                    if (debugMahjongTileList.Count(item => item == removeDebugMahjongTile) <= 3)
                    {
                        debugAnKanMahjongTileHashSet.Remove(removeDebugMahjongTile);
                    }
                }
                //HashSetでは要素を取得できないため、HashSetListを作成。
                List<MahjongTile> debugAnkanMahjongTileHashSetList = new List<MahjongTile>(debugAnKanMahjongTileHashSet);

                //ランダムに一つ牌を選ぶ。
                MahjongTile mahjongTile = debugAnkanMahjongTileHashSetList[UnityEngine.Random.Range(0, debugAnKanMahjongTileHashSet.Count)];

                GameObject generateAnkannObj = Instantiate(anKannObj, new Vector3(0, 0, 0), Quaternion.identity, selectMahjongTilesParent);
                KannMahjongTileSelect(mahjongTile, generateAnkannObj, true);

                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
                debugMahjongTileList.Remove(mahjongTile);
            }
        }
    }
}