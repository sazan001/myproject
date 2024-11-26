using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class AgariQuestion : MonoBehaviour
{
    [Tooltip("手牌\n13牌")]
    private List<MahjongTile> tehaiMahjongTileList = new List<MahjongTile>();

    [SerializeField, Tooltip("手牌\n13牌")]
    private List<Image> tehaiMahjongTileImageList = new List<Image>();

    [Tooltip("選択された牌\n上がり牌になりうるもの")]
    private List<MahjongTile> matiMahjongTileList = new List<MahjongTile>();

    [Tooltip("上がり牌になりうるもの")]
    private List<MahjongTile> matiAnswerMahjongTileList = new List<MahjongTile>();

    [Tooltip("選択された牌\n点数を計算する牌\n上がり牌")]
    private List<Image> matiMahjongTileImageList = new List<Image>();

    [Tooltip("上がり牌\n計算時に使用")]
    private MahjongTile agariMahjongTile;


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
    [Tooltip("親")]
    //不使用。
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
    [Tooltip("鳴いたか")]
    //不使用。
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

    [SerializeField, Tooltip("MatiMahjongTiles\nBackGround")]
    private Transform matiMahjongTilesParent;
    [SerializeField, Tooltip("MahjongTile_Image\nPrefabs")]
    private GameObject mahjongTileImageObj;

    [Tooltip("確定している面子")]
    private List<List<MahjongTile>> kakuteiMentuMahjongTileList = new List<List<MahjongTile>>();

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


    [SerializeField, Tooltip("待ちがあっているかを表示するテキスト")]
    private Text matiMahjongTileAnswerText;

    [SerializeField, Tooltip("One\n初めに表示するオブジェクト")]
    private GameObject one;
    [SerializeField, Tooltip("Two\n初めに表示するオブジェクト")]
    private GameObject two;

    [SerializeField, Tooltip("翻数")]
    private InputField hansuuInputField;
    [SerializeField, Tooltip("符の親オブジェクト")]
    private GameObject huObj;
    [SerializeField, Tooltip("符")]
    private InputField huInputField;
    [SerializeField, Tooltip("親の時の点数")]
    private InputField parentScoreInputField;
    [SerializeField, Tooltip("子の支払い")]
    private InputField parentAllPayInputField;
    [SerializeField, Tooltip("子の時の点数")]
    private InputField childScoreInputField;
    [SerializeField, Tooltip("子の時の親の支払い")]
    private InputField childParentPayInputField;
    [SerializeField, Tooltip("子の時の子の支払い")]
    private InputField childChildPayInputField;
    [SerializeField, Tooltip("点数があっているかを表示するテキスト")]
    private Text scoreMahjongTileAnswerText;

    [SerializeField, Tooltip("間違えたときのヒント")]
    private Text scoreHintText;

    [SerializeField, Tooltip("何の牌で上がったと仮定しているか")]
    private Image agariMahjongTileImage;
    private int matiNum = 0;

    private MahjongTile matiMahjongTileCheckHansuuKeisanKatei;


    [Tooltip("通常\n何もなくただ出題")]
    private bool tuuzyouZyuusi = true;
    [SerializeField, Tooltip("通常\n何もなくただ出題")]
    private Image tuuzyouZyuusiCheckBoxImage;
    [Tooltip("待ち\n待ちが複数ある問題を出題")]
    private bool matiZyuusi = false;
    [SerializeField, Tooltip("待ち\n待ちが複数ある問題を出題")]
    private Image matiZyuusiCheckBoxImage;
    [Tooltip("符\n翻があまりつかない問題を出題\nドラは1つで固定\n立直も付けない")]
    private bool huZyuusi = false;
    [SerializeField, Tooltip("符\n翻があまりつかない問題を出題\nドラは1つで固定\n立直、ツモ以外判定無し")]
    private Image huZyuusiCheckBoxImage;

    void Start()
    {
        foreach (Image doraMahjongTileImage in doraMahjongTileImageList)
        {
            doraMahjongTileImage.sprite = DataManager.noImage;
        }
        baMahjongTileImage.sprite = DataManager.noImage;
        ziMahjongTileImage.sprite = DataManager.noImage;

        //ParentCheckBoxUpdate();
        RiitiCheckBoxUpdate();
        TumoCheckBoxUpdate();
        //NakiCheckBoxUpdate();
        IppatuCheckBoxUpdate();
        TyankanCheckBoxUpdate();
        RinsyankaihooCheckBoxUpdate();
        HaiteiCheckBoxUpdate();
        DoubleRiitiCheckBoxUpdate();
        TenhooCheckBoxUpdate();
        HuKeisanCheckBoxUpdate();

        Reset();
        TwoHide();

        TuuzyouZyuusi();
    }

    void Update()
    {
        //左クリック。手持ちの牌。
        if (Input.GetMouseButtonDown(0))
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
                    //6牌以上は入れない。7面待ち以上は通常はないため。
                    if (matiMahjongTileList.Count < 6)
                    {
                        MahjongTileManager mahjongTileManager = result.gameObject.GetComponent<MahjongTileManager>();
                        //同じ牌が何個あるか計算。
                        int count = matiMahjongTileList.Count(item => (int)item == (int)mahjongTileManager.mahjongTile);
                        //同じ牌は4つまで。
                        if (count == 0)
                        {
                            GameObject generateMahjongTileImageObj =
                                Instantiate(mahjongTileImageObj, new Vector3(0, 0, 0), Quaternion.identity, matiMahjongTilesParent);
                            matiMahjongTileImageList.Add(generateMahjongTileImageObj.GetComponent<Image>());
                            MatiMahjongTileSelect(mahjongTileManager.mahjongTile);
                        }
                    }
                }
                //牌を消去。
                else if (result.gameObject.tag == "SelectMahjongTile")
                {
                    MatiMahjongTileRemove(matiMahjongTileList[matiMahjongTileImageList.IndexOf(result.gameObject.GetComponent<Image>())], result.gameObject);
                }
            }
        }
    }

    void TehaiMahjongTileSelect(MahjongTile mahjongTile)
    {
        //MahjongTileを追加。
        tehaiMahjongTileList.Add(mahjongTile);

        //enum型の番号を追加。
        List<int> selectMahjongTileNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in tehaiMahjongTileList)
        {
            selectMahjongTileNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newSelectMahjongTileList = new List<MahjongTile>();
        int count = tehaiMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = selectMahjongTileNumList.Min();
            int minNumNum = selectMahjongTileNumList.IndexOf(minNum);

            newSelectMahjongTileList.Add(tehaiMahjongTileList[minNumNum]);
            tehaiMahjongTileList.Remove(tehaiMahjongTileList[minNumNum]);
            selectMahjongTileNumList.Remove(minNum);
        }

        tehaiMahjongTileList = new List<MahjongTile>(newSelectMahjongTileList);
        TehaiMahjongTileImageListUpdate();
    }

    void TehaiMahjongTileRemove(MahjongTile mahjongTile)
    {
        //MahjongTileを削除。
        tehaiMahjongTileList.Remove(mahjongTile);

        TehaiMahjongTileImageListUpdate();
    }

    void TehaiMahjongTileImageListUpdate()
    {
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

    void MatiMahjongTileSelect(MahjongTile mahjongTile)
    {
        //MahjongTileを追加。
        matiMahjongTileList.Add(mahjongTile);

        //enum型の番号を追加。
        List<int> selectMahjongTileNumList = new List<int>();
        foreach (MahjongTile selectMahjongTile in matiMahjongTileList)
        {
            selectMahjongTileNumList.Add((int)selectMahjongTile);
        }

        //新しいMahjongTileのリストを作成。これはenum型の番号順になる。
        List<MahjongTile> newSelectMahjongTileList = new List<MahjongTile>();
        int count = matiMahjongTileList.Count;
        for (int i = 0; i < count; i++)
        {
            int minNum = selectMahjongTileNumList.Min();
            int minNumNum = selectMahjongTileNumList.IndexOf(minNum);

            newSelectMahjongTileList.Add(matiMahjongTileList[minNumNum]);
            matiMahjongTileList.Remove(matiMahjongTileList[minNumNum]);
            selectMahjongTileNumList.Remove(minNum);
        }

        matiMahjongTileList = new List<MahjongTile>(newSelectMahjongTileList);
        MatiMahjongTileImageListUpdate();
    }

    void MatiMahjongTileRemove(MahjongTile mahjongTile, GameObject image)
    {
        //MahjongTileを削除。
        matiMahjongTileList.Remove(mahjongTile);

        matiMahjongTileImageList.Remove(image.GetComponent<Image>());
        Destroy(image);

        MatiMahjongTileImageListUpdate();
    }

    void MatiMahjongTileImageListUpdate()
    {
        foreach (Image selectMahjongTileImage in matiMahjongTileImageList)
        {
            selectMahjongTileImage.sprite = DataManager.noImage;
        }
        for (int i = 0; i < matiMahjongTileList.Count; i++)
        {
            matiMahjongTileImageList[i].sprite = DataManager.mahjongTileImages[(int)matiMahjongTileList[i]];
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
        //もし今が点数入力状態じゃなかったら。
        if (!two.activeSelf)
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

    void MatiMahjongTileCheck()
    {
        //得点計算開始。

        matiAnswerMahjongTileList = new List<MahjongTile>();

        //全ての牌で、上がれるかどうかを検討。
        for (int i = 0; i < 34; i++)
        {
            //手牌にが4個以上あったら
            if (tehaiMahjongTileList.Count(item => item == (MahjongTile)Enum.ToObject(typeof(MahjongTile), i)) == 4)
            {
                continue;
            }

            //最高翻数。
            highHansuu = 0;
            //最高翻数時の役。
            highYaku = "";
            //最高符。
            highHu = 0;
            //最高符内訳。
            highHuStr = "";

            //リスト
            calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            calculationMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            CalculationListSort();
            mentuMahjongTileList = new List<List<MahjongTile>>();
            //HashSetを作成。HashSetは重複しない。
            calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
            //HashSetを作成。HashSetは重複しない。
            mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();

            matiMahjongTileCheckHansuuKeisanKatei = (MahjongTile)Enum.ToObject(typeof(MahjongTile), i);

            yakuman = false;

            //全ての役を頭ととらえ考える。
            foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
            {
                //リスト
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                calculationMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
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
                        HansuuCount();

                        //翻数が1以上なら。
                        if (hansuu > 0 || yakuman)
                        {
                            matiAnswerMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
                            break;
                        }
                    }

                    //リスト
                    calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                    calculationMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
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
                        HansuuCount();

                        //翻数が1以上なら。
                        if (hansuu > 0 || yakuman)
                        {
                            matiAnswerMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
                            break;
                        }
                    }

                    //リスト
                    calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                    calculationMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
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
                        HansuuCount();

                        //翻数が1以上なら。
                        if (hansuu > 0 || yakuman)
                        {
                            matiAnswerMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
                            break;
                        }
                    }
                }
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

        List<MahjongTile> allMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        if (matiMahjongTileCheckHansuuKeisanKatei == MahjongTile.NoSelect)
        {
            allMahjongTileList.Add(matiAnswerMahjongTileList[matiNum]);
        }
        else
        {
            allMahjongTileList.Add(matiMahjongTileCheckHansuuKeisanKatei);
        }

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
        if (kootuCount >= 4)
        {
            hansuu += addHansuu;
            yaku += "\n対々和：２翻";
        }
        //三暗刻(サンアンコー)
        if (kootuCount >= 3)
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
                        if (allMahjongTileList.Count(item => item == mentu[0]) == 4 || (mentu[0] != agariMahjongTile || tumo))
                        {
                            menzenMentu.Add(mentu);
                        }
                    }
                }
            }

            if (menzenMentu.Count >= 3)
            {
                hansuu += addHansuu;
                yaku += "\n三暗刻：２翻";
            }
        }
        //三槓子(サンカンツ)
        //判定無し。
        if (0 == 1)
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
        if (tehaiMahjongTileList.Count(item => (int)item <= 8) == 13
            ||
            tehaiMahjongTileList.Count(item => (int)item <= 17) - tehaiMahjongTileList.Count(item => (int)item <= 8) == 13
            ||
            tehaiMahjongTileList.Count(item => (int)item <= 26) - tehaiMahjongTileList.Count(item => (int)item <= 17) == 13)
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
        if (kootuCount >= 4 && !naki)
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
                        if (mentu[0] != agariMahjongTile || tumo)
                        {
                            menzenMentu.Add(mentu);
                        }
                    }
                }
            }

            if (menzenMentu.Count >= 4)
            {
                yakumanNow = true;
                yaku += "\n四暗刻：役満";
            }
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
        if (tehaiMahjongTileList.Count(item => (int)item >= 27) == 13)
        {
            yakumanNow = true;
            yaku += "\n字一色：役満";
        }
        //九連宝燈(チューレンポートン)
        //一色。1、9が各3牌。2~8が各1牌以上。
        //鳴いていないか、ポン、チー、カン(暗槓も)をしていないか。
        if (!naki && tehaiMahjongTileList.Count == 13)
        {
            List<MahjongTile> chuuren = new List<MahjongTile>(tehaiMahjongTileList);
            chuuren.Add(agariMahjongTile);

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
        if (tehaiMahjongTileList.Count(item => (int)item == 19) + tehaiMahjongTileList.Count(item => (int)item == 20)
        + tehaiMahjongTileList.Count(item => (int)item == 21) + tehaiMahjongTileList.Count(item => (int)item == 23)
        + tehaiMahjongTileList.Count(item => (int)item == 28) + tehaiMahjongTileList.Count(item => (int)item == 32) == 13)
        {
            yakumanNow = true;
            yaku += "\n緑一色：役満";
        }
        //清老頭(チンロートー)
        //一九のみ。
        if (tehaiMahjongTileList.Count(item => (int)item == 0) + tehaiMahjongTileList.Count(item => (int)item == 8)
        + tehaiMahjongTileList.Count(item => (int)item == 9) + tehaiMahjongTileList.Count(item => (int)item == 17)
        + tehaiMahjongTileList.Count(item => (int)item == 18) + tehaiMahjongTileList.Count(item => (int)item == 26) == 13)
        {
            yakumanNow = true;
            yaku += "\n清老頭：役満";
        }
        //四槓子(スーカンツ)
        //判定無し。
        if (0 == 1)
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
                List<MahjongTile> ryanmenCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                                if ((int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
                                {
                                    ryanmen = true;
                                    break;
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
                                if ((int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
                                {
                                    ryanmen = true;
                                    break;
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
                                if ((int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[0] == -1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == 1
                                    || (int)agariMahjongTile - (int)ryanmenCalculationMahjongTileList[1] == -1)
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
                List<MahjongTile> pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                pentyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                List<MahjongTile> kantyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == -1))
                        {
                            kantyan = true;
                            break;
                        }
                    }
                }

                //リスト
                kantyanCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == -1))
                        {
                            kantyan = true;
                            break;
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
                        if (((int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[0] - (int)agariMahjongTile == -1)
                            &&
                            ((int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == 1
                            || (int)kantyanCalculationMahjongTileList[1] - (int)agariMahjongTile == -1))
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
                List<MahjongTile> tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

                if (mahjongTile != agariMahjongTile)
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
                    if (tankiCalculationMahjongTileList[0] == agariMahjongTile)
                    {
                        tanki = true;
                        break;
                    }
                }

                //リスト
                tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                    if (tankiCalculationMahjongTileList[0] == agariMahjongTile)
                    {
                        tanki = true;
                        break;
                    }
                }

                //リスト
                tankiCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);

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
                    if (tankiCalculationMahjongTileList[0] == agariMahjongTile)
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
                List<MahjongTile> ryanmenCalculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                ryanmenCalculationMahjongTileList.Insert(0, agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                pentyanCalculationMahjongTileList.Insert(0, agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
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
                kantyanCalculationMahjongTileList.Insert(0, agariMahjongTile);
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)agariMahjongTile == 1
                                    || (int)mentu[0] - (int)agariMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)agariMahjongTile == 1
                                    || (int)mentu[1] - (int)agariMahjongTile == -1))
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)agariMahjongTile == 1
                                    || (int)mentu[0] - (int)agariMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)agariMahjongTile == 1
                                    || (int)mentu[1] - (int)agariMahjongTile == -1))
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
                        if (mentu.Count(item => item == agariMahjongTile) == 1)
                        {
                            mentu.Remove(agariMahjongTile);
                            //一個飛ばしだったら
                            if ((int)mentu[0] - (int)mentu[1] == 2
                                || (int)mentu[0] - (int)mentu[1] == -2)
                            {
                                //上がり牌が二つの牌の間だったら
                                if (((int)mentu[0] - (int)agariMahjongTile == 1
                                    || (int)mentu[0] - (int)agariMahjongTile == -1)
                                    &&
                                    ((int)mentu[1] - (int)agariMahjongTile == 1
                                    || (int)mentu[1] - (int)agariMahjongTile == -1))
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
                tankiCalculationMahjongTileList.Insert(0, agariMahjongTile);
                List<MahjongTile> tankiCalculationMahjongTileListCopy = new List<MahjongTile>(tankiCalculationMahjongTileList);

                //頭にならない。
                if (tankiCalculationMahjongTileList.Count(item => item == mahjongTile) <= 1)
                {
                    continue;
                }


                if (mahjongTile != agariMahjongTile)
                {
                    continue;
                }

                if (tankiCalculationMahjongTileList.Count(item => item == agariMahjongTile) >= 2)
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
                tankiCalculationMahjongTileList.Insert(0, agariMahjongTile);

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
                tankiCalculationMahjongTileList.Insert(0, agariMahjongTile);

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
        List<MahjongTile> doraCheck = new List<MahjongTile>(tehaiMahjongTileList);
        doraCheck.Add(matiAnswerMahjongTileList[matiNum]);

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

    void AgariQuestionGenerate()
    {
        doraMahjongTileList = new List<MahjongTile>();
        doraHyouziMahjongTileList = new List<MahjongTile>();
        DoraMahjongTileImageListUpdate();

        tehaiMahjongTileList = new List<MahjongTile>();
        TehaiMahjongTileImageListUpdate();
        agariMahjongTile = MahjongTile.NoSelect;

        parent = false;
        //ParentCheckBoxUpdate();
        riiti = false;
        RiitiCheckBoxUpdate();
        tumo = false;
        TumoCheckBoxUpdate();
        naki = false;
        //NakiCheckBoxUpdate();
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

        List<MahjongTile> debugMahjongTileList = new List<MahjongTile>();
        for (int i = 0; i <= 33; i++)
        {
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
            debugMahjongTileList.Add((MahjongTile)Enum.ToObject(typeof(MahjongTile), i));
        }
        HashSet<MahjongTile> debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);


        //通常。今までのやり方。ただ出題。
        if (tuuzyouZyuusi)
        {
            //bool型の設定。
            //今はすべて無効になっている。
            int boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //親
            //親は判定無し。自風牌が東だったら鳴きにチェックが入る。
            //立直
            if (boolRnd <= 60)
            {
                riiti = true;
                RiitiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //ツモ
            if (boolRnd <= 45)
            {
                tumo = true;
                TumoCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //鳴き
            //鳴きは判定無し。立直でない時の面子作成時にポン、チー、明槓をしたら鳴きにチェックが入る。
            //一発。立直があったら。0.35*0.2=0.07。
            if (boolRnd <= 20 && riiti)
            {
                ippatu = true;
                IppatuCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //槍槓。ツモがなければ。(1-0.3)*0.01=0.007。
            if (boolRnd <= 1 && !tumo)
            {
                tyankan = true;
                TyankanCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //嶺上開花。
            //カンがないため判定無し。
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //海底/河底。
            if (boolRnd <= 1)
            {
                haitei = true;
                HaiteiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //ダブル立直。立直がなければ。(1-0.35)*0.01=0.0065
            if (boolRnd <= 1)
            {
                doubleRiiti = true;
                DoubleRiitiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //天和/地和。立直がなければ。(1-0.35)*0.01=0.0065
            if (boolRnd <= 1 && !riiti && tumo)
            {
                tenhoo = true;
                TenhooCheckBoxUpdate();
            }


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
            //ParentCheck();


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
                if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect)
                {
                    agariMahjongTile = headMahjongTile;

                    debugMahjongTileList.Remove(headMahjongTile);
                }
                else
                {
                    //頭の分。
                    TehaiMahjongTileSelect(headMahjongTile);

                    debugMahjongTileList.Remove(headMahjongTile);
                }
            }


            //立直かダブル立直があれば鳴きは無し。嶺上開花があればカンを必ずする。
            //ここで七対子か分岐する。雀頭作成まではOK。
            //面子作成。
            while (tehaiMahjongTileList.Count < 13)
            {
                debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);

                int rnd = 0;
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
                if (tyankan && agariMahjongTile == MahjongTile.NoSelect)
                {
                    rnd = 2;
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
                        if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                            || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                        {
                            agariMahjongTile = mahjongTile;

                            debugMahjongTileList.Remove(mahjongTile);
                        }
                        else
                        {
                            TehaiMahjongTileSelect(mahjongTile);

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
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = mahjongTile;

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    else
                    {
                        TehaiMahjongTileSelect(mahjongTile);

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    //順子2つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //順子3つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
            }
        }
        else if (matiZyuusi)
        {
            //bool型の設定。
            //今はすべて無効になっている。
            int boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //親
            //親は判定無し。自風牌が東だったら鳴きにチェックが入る。
            //立直
            if (boolRnd <= 60)
            {
                riiti = true;
                RiitiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //ツモ
            //役無しにならないように、立直でなかったら絶対にツモにする。
            if (boolRnd <= 45 || !riiti)
            {
                tumo = true;
                TumoCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //鳴き
            //鳴きは判定無し。立直でない時の面子作成時にポン、チー、明槓をしたら鳴きにチェックが入る。
            //一発。立直があったら。0.35*0.2=0.07。
            if (boolRnd <= 20 && riiti)
            {
                ippatu = true;
                IppatuCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //槍槓。ツモがなければ。(1-0.3)*0.01=0.007。
            if (boolRnd <= 1 && !tumo)
            {
                tyankan = true;
                TyankanCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //嶺上開花。
            //カンがないため判定無し。
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //海底/河底。
            if (boolRnd <= 1)
            {
                haitei = true;
                HaiteiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //ダブル立直。立直がなければ。(1-0.35)*0.01=0.0065
            if (boolRnd <= 1)
            {
                doubleRiiti = true;
                DoubleRiitiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //天和/地和。立直がなければ。(1-0.35)*0.01=0.0065
            if (boolRnd <= 1 && !riiti && tumo)
            {
                tenhoo = true;
                TenhooCheckBoxUpdate();
            }


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
            //ParentCheck();


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
            //待ち重視のため、雀頭から字牌を除外。
            MahjongTile headMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), UnityEngine.Random.Range(0, 26 + 1));

            //多面待ちにしたいため雀頭がどこに属するかを把握したい。
            bool headManzu = false;
            bool headPinzu = false;
            bool headSouzu = false;
            if ((int)headMahjongTile <= 8)
            {
                headManzu = true;
            }
            else if ((int)headMahjongTile <= 17)
            {
                headPinzu = true;
            }
            else if ((int)headMahjongTile <= 26)
            {
                headSouzu = true;
            }

            //雀頭だから2回同じことを繰り返す。
            for (int i = 1; i <= 2; i++)
            {
                int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                if (tyankan)
                {
                    agarihaiRnd = 1;
                }
                //上がり牌に設定。
                if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect)
                {
                    agariMahjongTile = headMahjongTile;

                    debugMahjongTileList.Remove(headMahjongTile);
                }
                else
                {
                    //頭の分。
                    TehaiMahjongTileSelect(headMahjongTile);

                    debugMahjongTileList.Remove(headMahjongTile);
                }
            }


            //立直かダブル立直があれば鳴きは無し。嶺上開花があればカンを必ずする。
            //ここで七対子か分岐する。雀頭作成まではOK。
            //面子作成。
            while (tehaiMahjongTileList.Count < 13)
            {
                debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);

                int onaziRnd = UnityEngine.Random.Range(1, 100 + 1);
                bool onazi = false;
                if (onaziRnd <= 98)
                {
                    onazi = true;
                }

                int rnd = 0;
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
                if (tyankan && agariMahjongTile == MahjongTile.NoSelect)
                {
                    rnd = 2;
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
                    //雀頭と柄をそろえたい。
                    if (onazi)
                    {
                        //萬子が一つでもあれば
                        if (headManzu && debugKootuMahjongTileHashSetList.Count(item => (int)item <= 8) > 0)
                        {
                            //萬子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile >= 9)
                                {
                                    debugKootuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(萬子)の中からランダムに一つ。
                            mahjongTile = debugKootuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugKootuMahjongTileHashSet.Count)];
                        }
                        //筒子が一つでもあれば
                        else if (headPinzu &&
                            debugKootuMahjongTileHashSetList.Count(item => (int)item <= 17) - debugKootuMahjongTileHashSetList.Count(item => (int)item <= 8) > 0)
                        {
                            //筒子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile <= 8 || (int)removeDebugMahjongTile >= 18)
                                {
                                    debugKootuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(筒子)の中からランダムに一つ。
                            mahjongTile = debugKootuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugKootuMahjongTileHashSet.Count)];
                        }
                        //索子が一つでもあれば
                        else if (headSouzu &&
                            debugKootuMahjongTileHashSetList.Count(item => (int)item <= 26) - debugKootuMahjongTileHashSetList.Count(item => (int)item <= 17) > 0)
                        {
                            //索子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile <= 17 || (int)removeDebugMahjongTile >= 27)
                                {
                                    debugKootuMahjongTileHashSet.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(索子)の中からランダムに一つ。
                            mahjongTile = debugKootuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugKootuMahjongTileHashSet.Count)];
                        }
                    }

                    //刻子だから三回同じことを繰り返す。
                    for (int i = 1; i <= 3; i++)
                    {
                        int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                        //上がり牌に設定。
                        if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                            || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                        {
                            agariMahjongTile = mahjongTile;

                            debugMahjongTileList.Remove(mahjongTile);
                        }
                        else
                        {
                            TehaiMahjongTileSelect(mahjongTile);

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
                    //雀頭と柄をそろえたい。
                    if (onazi)
                    {
                        //萬子が一つでもあれば
                        if (headManzu && debugSyuntuMahjongTileHashSetList.Count(item => (int)item <= 8) > 0)
                        {
                            //萬子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile >= 9)
                                {
                                    debugSyuntuMahjongTileHashSetList.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(萬子)の中からランダムに一つ。
                            mahjongTile = debugSyuntuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugSyuntuMahjongTileHashSetList.Count)];
                        }
                        //筒子が一つでもあれば
                        else if (headPinzu &&
                            debugSyuntuMahjongTileHashSetList.Count(item => (int)item <= 17) - debugSyuntuMahjongTileHashSetList.Count(item => (int)item <= 8) > 0)
                        {
                            //筒子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile <= 8 || (int)removeDebugMahjongTile >= 18)
                                {
                                    debugSyuntuMahjongTileHashSetList.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(筒子)の中からランダムに一つ。
                            mahjongTile = debugSyuntuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugSyuntuMahjongTileHashSetList.Count)];
                        }
                        //索子が一つでもあれば
                        else if (headSouzu &&
                            debugSyuntuMahjongTileHashSetList.Count(item => (int)item <= 26) - debugSyuntuMahjongTileHashSetList.Count(item => (int)item <= 17) > 0)
                        {
                            //索子以外を削除。
                            foreach (MahjongTile removeDebugMahjongTile in debugMahjongTileHashSet)
                            {
                                if ((int)removeDebugMahjongTile <= 17 || (int)removeDebugMahjongTile >= 27)
                                {
                                    debugSyuntuMahjongTileHashSetList.Remove(removeDebugMahjongTile);
                                }
                            }
                            //残った牌(索子)の中からランダムに一つ。
                            mahjongTile = debugSyuntuMahjongTileHashSetList[UnityEngine.Random.Range(0, debugSyuntuMahjongTileHashSetList.Count)];
                        }
                    }


                    //順子1つ目。一番低い数字。
                    int agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    if (tyankan)
                    {
                        agarihaiRnd = 0;
                    }
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = mahjongTile;

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    else
                    {
                        TehaiMahjongTileSelect(mahjongTile);

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    //順子2つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //順子3つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
            }
        }
        //通常。今までのやり方。ただ出題。
        else if (huZyuusi)
        {
            //bool型の設定。
            //今はすべて無効になっている。
            int boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //親
            //親は判定無し。自風牌が東だったら鳴きにチェックが入る。
            //立直
            if (boolRnd <= 20)
            {
                riiti = true;
                RiitiCheckBoxUpdate();
            }
            boolRnd = UnityEngine.Random.Range(1, 100 + 1);
            //ツモ
            //役無しにならないように、立直でなかったら絶対にツモにする。
            if (boolRnd <= 35 || !riiti)
            {
                tumo = true;
                TumoCheckBoxUpdate();
            }
            //鳴き
            //鳴きは判定無し。立直でない時の面子作成時にポン、チー、明槓をしたら鳴きにチェックが入る。
            //一発。立直があったら。0.35*0.2=0.07。
            //槍槓。ツモがなければ。(1-0.3)*0.01=0.007。
            //嶺上開花。
            //カンがないため判定無し。
            //海底/河底。
            //ダブル立直。立直がなければ。(1-0.35)*0.01=0.0065
            //天和/地和。立直がなければ。(1-0.35)*0.01=0.0065


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
            //ParentCheck();


            //ドラ作成。
            int doraCount = 1;
            //立直の判定はないが一応残しておく。
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
                if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect)
                {
                    agariMahjongTile = headMahjongTile;

                    debugMahjongTileList.Remove(headMahjongTile);
                }
                else
                {
                    //頭の分。
                    TehaiMahjongTileSelect(headMahjongTile);

                    debugMahjongTileList.Remove(headMahjongTile);
                }
            }


            //立直かダブル立直があれば鳴きは無し。嶺上開花があればカンを必ずする。
            //ここで七対子か分岐する。雀頭作成まではOK。
            //面子作成。
            while (tehaiMahjongTileList.Count < 13)
            {
                debugMahjongTileHashSet = new HashSet<MahjongTile>(debugMahjongTileList);

                int rnd = 0;
                rnd = UnityEngine.Random.Range(1, 100 + 1);
                if (rnd <= 40)
                {
                    //40%で刻子。
                    rnd = 1;
                }
                else
                {
                    //60%で順子。
                    rnd = 2;
                }
                if (tyankan && agariMahjongTile == MahjongTile.NoSelect)
                {
                    rnd = 2;
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
                        if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                            || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                        {
                            agariMahjongTile = mahjongTile;

                            debugMahjongTileList.Remove(mahjongTile);
                        }
                        else
                        {
                            TehaiMahjongTileSelect(mahjongTile);

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
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = mahjongTile;

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    else
                    {
                        TehaiMahjongTileSelect(mahjongTile);

                        debugMahjongTileList.Remove(mahjongTile);
                    }
                    //順子2つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 1));
                    }
                    //順子3つ目。間の数字。
                    agarihaiRnd = UnityEngine.Random.Range(0, 1 + 1);
                    //上がり牌に設定。
                    if (agarihaiRnd == 0 && agariMahjongTile == MahjongTile.NoSelect
                        || tehaiMahjongTileList.Count == 12 && agariMahjongTile == MahjongTile.NoSelect)
                    {
                        agariMahjongTile = (MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2);

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                    else
                    {
                        TehaiMahjongTileSelect((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));

                        debugMahjongTileList.Remove((MahjongTile)Enum.ToObject(typeof(MahjongTile), (int)mahjongTile + 2));
                    }
                }
            }
        }

        MatiMahjongTileCheck();
    }

    public void MatiMahjongTileAnswer()
    {
        //回答と一致していたら
        if (matiMahjongTileList.SequenceEqual(matiAnswerMahjongTileList))
        {
            matiMahjongTileAnswerText.text = "正解";
            scoreHintText.text = "";
            if (matiAnswerMahjongTileList.Count == 0)
            {
                Reset();
            }
            else
            {
                OneHide();
            }
        }
        else
        {
            matiMahjongTileAnswerText.text = "不正解";
        }
    }
    public void ScoreCalculation()
    {
        //得点計算開始。
        agariMahjongTileImage.sprite = DataManager.mahjongTileImages[(int)matiAnswerMahjongTileList[matiNum]];

        //最高翻数。
        highHansuu = 0;
        //最高翻数時の役。
        highYaku = "";
        //最高符。
        highHu = 0;
        //最高符内訳。
        highHuStr = "";

        //リスト
        calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
        calculationMahjongTileList.Add(matiAnswerMahjongTileList[matiNum]);
        CalculationListSort();
        mentuMahjongTileList = new List<List<MahjongTile>>();
        //HashSetを作成。HashSetは重複しない。
        calculationMahjongTileHashSet = new HashSet<MahjongTile>(calculationMahjongTileList);
        //HashSetを作成。HashSetは重複しない。
        mentuMahjongTileHashSet = new HashSet<List<MahjongTile>>();

        matiMahjongTileCheckHansuuKeisanKatei = MahjongTile.NoSelect;

        yakuman = false;

        //全ての役を頭ととらえ考える。
        foreach (MahjongTile mahjongTile in calculationMahjongTileHashSet)
        {
            //リスト
            calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
            calculationMahjongTileList.Add(matiAnswerMahjongTileList[matiNum]);
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
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                calculationMahjongTileList.Add(matiAnswerMahjongTileList[matiNum]);
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
                calculationMahjongTileList = new List<MahjongTile>(tehaiMahjongTileList);
                calculationMahjongTileList.Add(matiAnswerMahjongTileList[matiNum]);
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
    }

    public void ScoreAnswer()
    {
        bool seikai = false;
        if (huKeisan)
        {
            //回答と一致していたら
            if (yakuman || (hansuuInputField.text != "" && int.Parse(hansuuInputField.text) == highHansuu &&
                (highHansuu >= 5 || (huInputField.text != "" && int.Parse(huInputField.text) == highHu))))
            {
                //役満
                if (yakuman)
                {
                    if (parentScoreInputField.text == "48000" && parentAllPayInputField.text == "16000"
                        && childScoreInputField.text == "32000"
                        && childParentPayInputField.text == "16000" && childChildPayInputField.text == "8000")
                    {
                        seikai = true;
                    }
                }
                //一翻
                else if (highHansuu == 1)
                {
                    if (highHu == 30)
                    {
                        if (parentScoreInputField.text == "1500" && parentAllPayInputField.text == "500"
                            && childScoreInputField.text == "1000"
                            && childParentPayInputField.text == "500" && childChildPayInputField.text == "300")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 40)
                    {
                        if (parentScoreInputField.text == "2000" && parentAllPayInputField.text == "700"
                            && childScoreInputField.text == "1300"
                            && childParentPayInputField.text == "700" && childChildPayInputField.text == "400")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 50)
                    {
                        if (parentScoreInputField.text == "2400" && parentAllPayInputField.text == "800"
                            && childScoreInputField.text == "1600"
                            && childParentPayInputField.text == "800" && childChildPayInputField.text == "400")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 60)
                    {
                        if (parentScoreInputField.text == "2900" && parentAllPayInputField.text == "1000"
                            && childScoreInputField.text == "2000"
                            && childParentPayInputField.text == "1000" && childChildPayInputField.text == "500")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 70)
                    {
                        if (parentScoreInputField.text == "3400" && parentAllPayInputField.text == "1200"
                            && childScoreInputField.text == "2300"
                            && childParentPayInputField.text == "1200" && childChildPayInputField.text == "600")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 80)
                    {
                        if (parentScoreInputField.text == "3900" && parentAllPayInputField.text == "1300"
                            && childScoreInputField.text == "2600"
                            && childParentPayInputField.text == "1300" && childChildPayInputField.text == "700")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 90)
                    {
                        if (parentScoreInputField.text == "4400" && parentAllPayInputField.text == "1500"
                            && childScoreInputField.text == "2900"
                            && childParentPayInputField.text == "1500" && childChildPayInputField.text == "800")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 100)
                    {
                        if (parentScoreInputField.text == "4800" && parentAllPayInputField.text == "1600"
                            && childScoreInputField.text == "3200"
                            && childParentPayInputField.text == "1600" && childChildPayInputField.text == "800")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 110)
                    {
                        if (parentScoreInputField.text == "5300" && parentAllPayInputField.text == "1800"
                            && childScoreInputField.text == "3600"
                            && childParentPayInputField.text == "1800" && childChildPayInputField.text == "900")
                        {
                            seikai = true;
                        }
                    }
                }
                //二翻
                else if (highHansuu == 2)
                {
                    if (highHu == 20)
                    {
                        if ((parentScoreInputField.text == "" || parentScoreInputField.text == "2000") && parentAllPayInputField.text == "700"
                            && (parentScoreInputField.text == "" || childScoreInputField.text == "1300")
                            && childParentPayInputField.text == "700" && childChildPayInputField.text == "400")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 30)
                    {
                        if (parentScoreInputField.text == "2900" && parentAllPayInputField.text == "1000"
                            && childScoreInputField.text == "2000"
                            && childParentPayInputField.text == "1000" && childChildPayInputField.text == "500")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 40)
                    {
                        if (parentScoreInputField.text == "3900" && parentAllPayInputField.text == "1300"
                            && childScoreInputField.text == "2600"
                            && childParentPayInputField.text == "1300" && childChildPayInputField.text == "700")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 50)
                    {
                        if (parentScoreInputField.text == "4800" && parentAllPayInputField.text == "1600"
                            && childScoreInputField.text == "3200"
                            && childParentPayInputField.text == "1600" && childChildPayInputField.text == "800")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 60)
                    {
                        if (parentScoreInputField.text == "5800" && parentAllPayInputField.text == "2000"
                            && childScoreInputField.text == "3900"
                            && childParentPayInputField.text == "2000" && childChildPayInputField.text == "1000")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 70)
                    {
                        if (parentScoreInputField.text == "6800" && parentAllPayInputField.text == "2300"
                            && childScoreInputField.text == "4500"
                            && childParentPayInputField.text == "2300" && childChildPayInputField.text == "1200")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 80)
                    {
                        if (parentScoreInputField.text == "7700" && parentAllPayInputField.text == "2600"
                            && childScoreInputField.text == "5200"
                            && childParentPayInputField.text == "2600" && childChildPayInputField.text == "1300")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 90)
                    {
                        if (parentScoreInputField.text == "8700" && parentAllPayInputField.text == "2900"
                            && childScoreInputField.text == "5800"
                            && childParentPayInputField.text == "2900" && childChildPayInputField.text == "1500")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 100)
                    {
                        if (parentScoreInputField.text == "9600" && parentAllPayInputField.text == "3200"
                            && childScoreInputField.text == "6400"
                            && childParentPayInputField.text == "3200" && childChildPayInputField.text == "1600")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 110)
                    {
                        if (parentScoreInputField.text == "10600" && parentAllPayInputField.text == "3600"
                            && childScoreInputField.text == "7100"
                            && childParentPayInputField.text == "3600" && childChildPayInputField.text == "1800")
                        {
                            seikai = true;
                        }
                    }
                }
                //三翻
                else if (highHansuu == 3)
                {
                    if (highHu == 20)
                    {
                        if ((parentScoreInputField.text == "" || parentScoreInputField.text == "3900") && parentAllPayInputField.text == "1300"
                            && (parentScoreInputField.text == "" || childScoreInputField.text == "2600")
                            && childParentPayInputField.text == "1300" && childChildPayInputField.text == "700")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 30)
                    {
                        if (parentScoreInputField.text == "5800" && parentAllPayInputField.text == "2000"
                            && childScoreInputField.text == "3900"
                            && childParentPayInputField.text == "2000" && childChildPayInputField.text == "1000")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 40)
                    {
                        if (parentScoreInputField.text == "7700" && parentAllPayInputField.text == "2600"
                            && childScoreInputField.text == "5200"
                            && childParentPayInputField.text == "2600" && childChildPayInputField.text == "1300")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 50)
                    {
                        if (parentScoreInputField.text == "9600" && parentAllPayInputField.text == "3200"
                            && childScoreInputField.text == "6400"
                            && childParentPayInputField.text == "3200" && childChildPayInputField.text == "1600")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 60)
                    {
                        if (!kiriageMangan)
                        {
                            if (parentScoreInputField.text == "11600" && parentAllPayInputField.text == "3900"
                                && childScoreInputField.text == "7700"
                                && childParentPayInputField.text == "3900" && childChildPayInputField.text == "2000")
                            {
                                seikai = true;
                            }
                        }
                        else
                        {
                            if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                                && childScoreInputField.text == "8000"
                                && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                            {
                                seikai = true;
                            }
                        }
                    }
                    else if (highHu >= 70)
                    {
                        if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                            && childScoreInputField.text == "8000"
                            && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                        {
                            seikai = true;
                        }
                    }
                }
                //満貫?
                else if (highHansuu == 4)
                {
                    if (highHu == 20)
                    {
                        if ((parentScoreInputField.text == "" || parentScoreInputField.text == "7700") && parentAllPayInputField.text == "2600"
                            && (parentScoreInputField.text == "" || childScoreInputField.text == "5200")
                            && childParentPayInputField.text == "2600" && childChildPayInputField.text == "1300")
                        {
                            seikai = true;
                        }
                    }
                    else if (highHu == 30)
                    {
                        if (!kiriageMangan)
                        {
                            if (parentScoreInputField.text == "11600" && parentAllPayInputField.text == "3900"
                                && childScoreInputField.text == "7700"
                                && childParentPayInputField.text == "3900" && childChildPayInputField.text == "2000")
                            {
                                seikai = true;
                            }
                        }
                        else
                        {
                            if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                                && childScoreInputField.text == "8000"
                                && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                            {
                                seikai = true;
                            }
                        }
                    }
                    else if (highHu >= 40)
                    {
                        if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                            && childScoreInputField.text == "8000"
                            && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                        {
                            seikai = true;
                        }
                    }
                }
                //満貫
                else if (highHansuu == 5)
                {
                    if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                        && childScoreInputField.text == "8000"
                        && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                    {
                        seikai = true;
                    }
                }
                //跳満
                else if (highHansuu == 6 || highHansuu == 7)
                {
                    if (parentScoreInputField.text == "18000" && parentAllPayInputField.text == "6000"
                        && childScoreInputField.text == "12000"
                        && childParentPayInputField.text == "6000" && childChildPayInputField.text == "3000")
                    {
                        seikai = true;
                    }
                }
                //倍満
                else if (highHansuu == 8 || highHansuu == 9 || highHansuu == 10)
                {
                    if (parentScoreInputField.text == "24000" && parentAllPayInputField.text == "8000"
                        && childScoreInputField.text == "16000"
                        && childParentPayInputField.text == "8000" && childChildPayInputField.text == "4000")
                    {
                        seikai = true;
                    }
                }
                //三倍満
                else if (highHansuu == 11 || highHansuu == 12)
                {
                    if (parentScoreInputField.text == "36000" && parentAllPayInputField.text == "12000"
                        && childScoreInputField.text == "24000"
                        && childParentPayInputField.text == "12000" && childChildPayInputField.text == "6000")
                    {
                        seikai = true;
                    }
                }
                //数え役満
                else if (highHansuu >= 13)
                {
                    if (parentScoreInputField.text == "48000" && parentAllPayInputField.text == "16000"
                        && childScoreInputField.text == "32000"
                        && childParentPayInputField.text == "16000" && childChildPayInputField.text == "8000")
                    {
                        seikai = true;
                    }
                }
            }
        }
        else
        {
            //回答と一致していたら
            if (yakuman || (hansuuInputField.text != "" && int.Parse(hansuuInputField.text) == highHansuu))
            {
                //役満
                if (yakuman)
                {
                    if (parentScoreInputField.text == "48000" && parentAllPayInputField.text == "16000"
                        && childScoreInputField.text == "32000"
                        && childParentPayInputField.text == "16000" && childChildPayInputField.text == "8000")
                    {
                        seikai = true;
                    }
                }
                //一翻
                else if (highHansuu == 1)
                {
                    if (parentScoreInputField.text == "1500" && parentAllPayInputField.text == "500"
                        && childScoreInputField.text == "1000"
                        && childParentPayInputField.text == "500" && childChildPayInputField.text == "300")
                    {
                        seikai = true;
                    }
                }
                //二翻
                else if (highHansuu == 2)
                {
                    if (parentScoreInputField.text == "3000" && parentAllPayInputField.text == "1000"
                        && childScoreInputField.text == "2000"
                        && childParentPayInputField.text == "1000" && childChildPayInputField.text == "500")
                    {
                        seikai = true;
                    }
                }
                //三翻
                else if (highHansuu == 3)
                {
                    if (parentScoreInputField.text == "5800" && parentAllPayInputField.text == "2000"
                        && childScoreInputField.text == "3900"
                        && childParentPayInputField.text == "2000" && childChildPayInputField.text == "1000")
                    {
                        seikai = true;
                    }
                }
                //満貫
                else if (highHansuu == 4)
                {
                    if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                        && childScoreInputField.text == "8000"
                        && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                    {
                        seikai = true;
                    }
                }
                //満貫
                else if (highHansuu == 5)
                {
                    if (parentScoreInputField.text == "12000" && parentAllPayInputField.text == "4000"
                        && childScoreInputField.text == "8000"
                        && childParentPayInputField.text == "4000" && childChildPayInputField.text == "2000")
                    {
                        seikai = true;
                    }
                }
                //跳満
                else if (highHansuu == 6 || highHansuu == 7)
                {
                    if (parentScoreInputField.text == "18000" && parentAllPayInputField.text == "6000"
                        && childScoreInputField.text == "12000"
                        && childParentPayInputField.text == "6000" && childChildPayInputField.text == "3000")
                    {
                        seikai = true;
                    }
                }
                //倍満
                else if (highHansuu == 8 || highHansuu == 9 || highHansuu == 10)
                {
                    if (parentScoreInputField.text == "24000" && parentAllPayInputField.text == "8000"
                        && childScoreInputField.text == "16000"
                        && childParentPayInputField.text == "8000" && childChildPayInputField.text == "4000")
                    {
                        seikai = true;
                    }
                }
                //三倍満
                else if (highHansuu == 11 || highHansuu == 12)
                {
                    if (parentScoreInputField.text == "36000" && parentAllPayInputField.text == "12000"
                        && childScoreInputField.text == "24000"
                        && childParentPayInputField.text == "12000" && childChildPayInputField.text == "6000")
                    {
                        seikai = true;
                    }
                }
                //数え役満
                else if (highHansuu >= 13)
                {
                    if (parentScoreInputField.text == "48000" && parentAllPayInputField.text == "16000"
                        && childScoreInputField.text == "32000"
                        && childParentPayInputField.text == "16000" && childChildPayInputField.text == "8000")
                    {
                        seikai = true;
                    }
                }
            }
        }

        if (seikai)
        {
            scoreMahjongTileAnswerText.text = "正解";
            if (matiNum == matiAnswerMahjongTileList.Count - 1)
            {
                TwoHide();
                Reset();
            }
            else
            {
                matiNum++;
                ScoreCalculation();
            }
        }
        else
        {
            if ((hansuuInputField.text == "" || int.Parse(hansuuInputField.text) != highHansuu ) && !yakuman)
            {
                scoreHintText.text = "翻数が違います";
            }
            else if ((huInputField.text == "" || int.Parse(huInputField.text) != highHu) && huKeisan && highHansuu <= 4)
            {
                scoreHintText.text = "符が違います";
            }
            else
            {
                scoreHintText.text = "点数/支払いが違います";
            }
            scoreMahjongTileAnswerText.text = "不正解";
        }
    }

    public void Reset()
    {
        AgariQuestionGenerate();

        matiMahjongTileList = new List<MahjongTile>();
        matiMahjongTileImageList = new List<Image>();

        foreach (Transform trn in matiMahjongTilesParent)
        {
            Destroy(trn.gameObject);
        }

        MatiMahjongTileImageListUpdate();


        hansuuInputField.text = "";
        huInputField.text = "";
        parentScoreInputField.text = "";
        parentAllPayInputField.text = "";
        childScoreInputField.text = "";
        childParentPayInputField.text = "";
        childChildPayInputField.text = "";

        matiMahjongTileAnswerText.text = "待機";
        scoreMahjongTileAnswerText.text = "待機";

        scoreHintText.text = "";

        matiNum = 0;

        TwoHide();
    }

    void OneHide()
    {
        one.SetActive(false);
        two.SetActive(true);
        if (huKeisan)
        {
            huObj.SetActive(true);
        }
        else
        {
            huObj.SetActive(false);
        }
        ScoreCalculation();
    }
    void TwoHide()
    {
        one.SetActive(true);
        two.SetActive(false);
    }

    public void TuuzyouZyuusi()
    {
        tuuzyouZyuusi = true;
        tuuzyouZyuusiCheckBoxImage.sprite = DataManager.checkImage;
        matiZyuusi = false;
        matiZyuusiCheckBoxImage.sprite = DataManager.noImage;
        huZyuusi = false;
        huZyuusiCheckBoxImage.sprite = DataManager.noImage;
    }
    public void MatiZyuusi()
    {
        tuuzyouZyuusi = false;
        tuuzyouZyuusiCheckBoxImage.sprite = DataManager.noImage;
        matiZyuusi = true;
        matiZyuusiCheckBoxImage.sprite = DataManager.checkImage;
        huZyuusi = false;
        huZyuusiCheckBoxImage.sprite = DataManager.noImage;
    }
    public void HuZyuusi()
    {
        tuuzyouZyuusi = false;
        tuuzyouZyuusiCheckBoxImage.sprite = DataManager.noImage;
        matiZyuusi = false;
        matiZyuusiCheckBoxImage.sprite = DataManager.noImage;
        huZyuusi = true;
        huZyuusiCheckBoxImage.sprite = DataManager.checkImage;
    }
}