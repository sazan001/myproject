using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> mahjongTileImagesInput = new List<Sprite>();
    public static List<Sprite> mahjongTileImages = new List<Sprite>();

    [SerializeField]
    private List<string> mahjongTileNamesInput = new List<string>();
    public static List<string> mahjongTileNames = new List<string>();

    [SerializeField]
    private Sprite noImageInput;
    public static Sprite noImage;

    [SerializeField]
    private Sprite checkImageInput;
    public static Sprite checkImage;

    [SerializeField]
    private Sprite uraImageInput;
    public static Sprite uraImage;

    void Awake()
    {
        mahjongTileImages = new List<Sprite>(mahjongTileImagesInput);
        mahjongTileNames = new List<string>(mahjongTileNamesInput);
        noImage = noImageInput;
        checkImage = checkImageInput;
        uraImage = uraImageInput;
    }
}
public enum MahjongTile
{
    Manzu_One,//0
    Manzu_Two,//1
    Manzu_Three,//2
    Manzu_Four,//3
    Manzu_Five,//4
    Manzu_Six,//5
    Manzu_Seven,//6
    Manzu_Eight,//7
    Manzu_Nine,//8
    Pinzu_One,//9
    Pinzu_Two,//10
    Pinzu_Three,//11
    Pinzu_Four,//12
    Pinzu_Five,//13
    Pinzu_Six,//14
    Pinzu_Seven,//15
    Pinzu_Eight,//16
    Pinzu_Nine,//17
    Souzu_One,//18
    Souzu_Two,//19
    Souzu_Three,//20
    Souzu_Four,//21
    Souzu_Five,//22
    Souzu_Six,//23
    Souzu_Seven,//24
    Souzu_Eight,//25
    Souzu_Nine,//26
    Kazehai_East,//27
    Kazehai_South,//28
    Kazehai_West,//29
    Kazehai_North,//30
    Sangenpai_Haku,//31
    Sangenpai_Hatu,//32
    Sangenpai_Tyun,//33
    NoSelect//34
}