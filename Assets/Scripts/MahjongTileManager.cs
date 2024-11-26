using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MahjongTileManager : MonoBehaviour
{
    private Image image;

    public MahjongTile mahjongTile;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = DataManager.mahjongTileImages[(int)mahjongTile];
    }
}
