using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollController : MonoBehaviour
{
    public RectTransform prefab = null;

    public void PokeListCreate(int i, Select.ruleName r)//(試行回数,ルール)
    {
        //番号を取得
        int pokeNumber = Data.getJoinPoke(i, r);//(試行回数,ルール)

        var item = GameObject.Instantiate(prefab) as RectTransform;
        item.SetParent(transform, false);

        Text text = item.GetComponentInChildren<Text>();
        text.text = Data.getPokemonName(pokeNumber);//テキスト内容を変更

        item.name = "Node (" + (i + 1) + ")";
    }

    public void WazaListCreate(int i,int j)//(種類, 回数)
    {
        var item = GameObject.Instantiate(prefab) as RectTransform;
        item.SetParent(transform, false);

        Text text = item.GetComponentInChildren<Text>();
        text.text = Data.getWazaName(i);//テキスト内容を変更

        item.name = "Node (waza) (" + (j + 1) + ")";
    }
}
