using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Select : MonoBehaviour
{
    public enum selectScene : int { rule, poke, details }
    static selectScene nowScene = selectScene.rule;

    //ルールの種類
    public enum ruleName : int { nin97, nin98, nin99, castom1, noLimit, castum2, Ultra, Fancy, Yellow, }
    static ruleName r = ruleName.noLimit;
    
    //ルール
    static GameObject RuleView;
    public static GameObject RuleCursor;

    //選択
    static GameObject PokeView;
    static GameObject PokeContent;
    public static GameObject PokeCursor;

    //確認
    static GameObject PokeConfirmation;
    static Text ConfirmationName;
    static Text[] BaseStatusText = new Text[5];
    static Text[] typeText = new Text[2];
    public static GameObject ConfirmationCursor;

    //決定表
    static GameObject PokeSelectedView;
    static GameObject[] node = new GameObject[6];
    static Text[] selectedName = new Text[6];

    //詳細設定
    static GameObject PokeDetail;
    public static DetailCursor detailCursor;
    static Text NameText;
    public static Text LevelText;
    public static Text[] wazaText = new Text[4];
    public static Text[] StatusText = new Text[5];
    public static Text[] IVText = new Text[5];
    public static Text[] EVText = new Text[5];
    public static Text[] ELText = new Text[5];

    //わざの詳細
    static GameObject WazaSelectView;
    static GameObject[] WazaNode;
    static GameObject WazaDetailView;
    static Text[] WazaDetailText = new Text[5];

    //ポケ数値
    public static int finishNumber;                        //選択完了数
    public static int[] selectedPoke = new int[6];         //本体
    public static int[] LowestLevel = new int[6];          //最低レベル
    public static int[] Level = new int[6];                //レベル
    public static int[,] PokeType = new int[6, 2];         //タイプ
    public static int[,] waza = new int[6, 4];             //技
    public static int[] MinimumWaza = new int[6];          //取得技最少数
    public static int[,] status = new int[6, 5];           //実数値
    public static int[,] BaseStats = new int[6, 5];        //種族値
    public static int[,] IndividualValue = new int[6, 5];  //個体値
    public static int[,] EffortValue = new int[6, 5];      //努力値
    public static int[,] EffortLevel = new int[6, 5];      //努力レベル

    void Start()
    {
        //初期値設定
        finishNumber = 0;

        //ルールに関するオブジェクトを取得
        RuleView = GameObject.Find("RuleSelectView");
        RuleCursor = RuleView.transform.Find("Cursor").gameObject;

        //選択に関するオブジェクトを取得
        PokeView = GameObject.Find("PokeSelectView");
        PokeContent = PokeView.transform.Find("Content").gameObject;
        PokeCursor = PokeView.transform.Find("Cursor").gameObject;

        //選択確認に関するオブジェクトを取得
        PokeConfirmation = PokeView.transform.Find("ConfirmationView").gameObject;
        ConfirmationName = PokeConfirmation.transform.Find("NameText").gameObject.GetComponent<Text>();
        for (int i = 0; i < 5; i++)
        {
            BaseStatusText[i] = PokeConfirmation.transform.Find("Status (" + i + ")").gameObject.GetComponent<Text>();
        }
        for(int i = 0; i < 2; i++)
        {
            typeText[i]= PokeConfirmation.transform.Find("Type (" + i + ")").gameObject.GetComponent<Text>();
        }
        ConfirmationCursor = PokeConfirmation.transform.Find("Cursor").gameObject;


        //選択完了に関するオブジェクトを取得
        PokeSelectedView = GameObject.Find("PokeSelectedView");
        for(int i = 0; i < 6; i++)
        {
            GameObject content = PokeSelectedView.transform.Find("Content").gameObject;
            node[i] = content.transform.Find("Node (selected) (" + i + ")").gameObject;
            selectedName[i] = node[i].transform.Find("Text").gameObject.GetComponent<Text>();
            node[i].SetActive(false);
        }

        //詳細設定に関するオブジェクトを取得
        PokeDetail = GameObject.Find("MoreDetailView").gameObject;
        WazaSelectView = PokeDetail.transform.Find("WazaSelectView").gameObject;
        WazaDetailView = PokeDetail.transform.Find("WazaDetailView").gameObject;
        detailCursor = PokeDetail.transform.Find("Cursor").gameObject.GetComponent<DetailCursor>();
        detailCursor.CanDo = false;
        NameText = PokeDetail.transform.Find("NameText").gameObject.GetComponent<Text>();
        LevelText = PokeDetail.transform.Find("Level").gameObject.transform.Find("LevelText").gameObject.GetComponent<Text>();
        for (int i = 0; i < 4; i++)
        {
            wazaText[i] = PokeDetail.transform.Find("Waza (" + i + ")").gameObject.GetComponent<Text>();
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject obj1 = PokeDetail.transform.Find("Status (" + i + ")").gameObject;
            switch (i)
            {
                case 0:
                    StatusText[i] = obj1.transform.Find("HPText").gameObject.GetComponent<Text>();
                    WazaDetailText[i] = WazaDetailView.transform.Find("TypeText").gameObject.GetComponent<Text>();
                    break;
                case 1:
                    StatusText[i] = obj1.transform.Find("AttackText").gameObject.GetComponent<Text>();
                    WazaDetailText[i] = WazaDetailView.transform.Find("PowerText").gameObject.GetComponent<Text>();
                    break;
                case 2:
                    StatusText[i] = obj1.transform.Find("DefenseText").gameObject.GetComponent<Text>();
                    WazaDetailText[i] = WazaDetailView.transform.Find("AccuracyText").gameObject.GetComponent<Text>();
                    break;
                case 3:
                    StatusText[i] = obj1.transform.Find("SpeedText").gameObject.GetComponent<Text>();
                    WazaDetailText[i] = WazaDetailView.transform.Find("PPText").gameObject.GetComponent<Text>();
                    break;
                case 4:
                    StatusText[i] = obj1.transform.Find("SpecialText").gameObject.GetComponent<Text>();
                    WazaDetailText[i] = WazaDetailView.transform.Find("EffectText").gameObject.GetComponent<Text>();
                    break;
            }
            IVText[i] = PokeDetail.transform.Find("IV (" + i + ")").gameObject.GetComponent<Text>();
            EVText[i] = PokeDetail.transform.Find("EV (" + i + ")").gameObject.GetComponent<Text>();
            ELText[i] = PokeDetail.transform.Find("EL (" + i + ")").gameObject.GetComponent<Text>();
        }

        //ルール一覧以外を非表示に
        PokeView.SetActive(false);
        PokeConfirmation.SetActive(false);
        PokeSelectedView.SetActive(false);
        WazaSelectView.SetActive(false);
        WazaDetailView.SetActive(false);
        PokeDetail.SetActive(false);
    }

    //ルール決定
    public static void DecisionRule(int i)
    {
        //ルール確定
        var v = (ruleName)i;
        r = v;

        //シーンの切り替え
        AdvanceScene();
    }

    //確認
    public static void ConfirmationPoke(int i)
    {
        //確認画面を表示
        PokeConfirmation.SetActive(true);

        //選択画面のカーソルを一時的に非表示
        PokeCursor.SetActive(false);

        //種族値を検索し表示する
        int j = 0;
        switch (r)
        {
            case ruleName.nin97:
                j = Data.nintendo97[(i - 1)];
                break;

            case ruleName.nin98:
                j = Data.nintendo98[(i - 1)];
                break;

            case ruleName.nin99:
                j = Data.nintendo99[(i - 1)];
                break;

            case ruleName.Fancy:
                j = Data.Fancy[(i - 1)];
                break;

            case ruleName.Yellow:
                j = Data.Yellow[(i - 1)];
                break;

            case ruleName.castom1:
                break;

            case ruleName.castum2:
                break;
                
            default:
                j = i;
                break;
        }
        for (int k = 0; k < 5; k++)
        {
            //種族値書き込み
            string s = Data.getPokeStatus(j, k).ToString();
            switch (k)
            {
                case 0:
                    BaseStatusText[k].text = "HP:" + s;
                    break;
                case 1:
                    BaseStatusText[k].text = "こうげき:" + s;
                    break;
                case 2:
                    BaseStatusText[k].text = "ぼうぎょ:" + s;
                    break;
                case 3:
                    BaseStatusText[k].text = "すばやさ:" + s;
                    break;
                case 4:
                    BaseStatusText[k].text = "とくしゅ:" + s;
                    break;
            }
        }

        //タイプを検索し確認画面に表記
        for (int k = 0; k < 2; k++)
        {
            int a = Data.getPokeStatus(j, (k + 5));
            string s = Data.getTypeName(a);
            typeText[k].text = "タイプ" + (k + 1) + ":" + s;
        }

        //名前検索し確認画面に表記
        ConfirmationName.text = Data.getPokemonName(j);

        //決定カーソルに番号を渡す
        PokeConfirmationCursor.selectNumber = i;
    }

    //確認キャンセル
    public static void Cancel()
    {
        //確認画面を非表示
        PokeConfirmation.SetActive(false);

        //選択画面のカーソルを表示
        PokeCursor.SetActive(true);
    }

    //決定
    public static void DecisionPoke(int i)
    {
        //枠を1つ表示
        node[finishNumber].gameObject.SetActive(true);

        //番号検索し保存する
        switch (r)
        {
            case ruleName.nin97:
                selectedPoke[finishNumber] = Data.nintendo97[(i - 1)];
                break;

            case ruleName.nin98:
                selectedPoke[finishNumber] = Data.nintendo98[(i - 1)];
                break;

            case ruleName.nin99:
                selectedPoke[finishNumber] = Data.nintendo99[(i - 1)];
                break;

            case ruleName.Fancy:
                selectedPoke[finishNumber] = Data.Fancy[(i - 1)];
                break;

            case ruleName.Yellow:
                selectedPoke[finishNumber] = Data.Yellow[(i - 1)];
                break;

            case ruleName.castom1:
                break;

            case ruleName.castum2:
                break;

            default:
                selectedPoke[finishNumber] = i;
                break;
        }

        //名前検索し決定欄に入力
        selectedName[finishNumber].text = Data.getPokemonName(selectedPoke[finishNumber]);

        //決定数を1上げる
        finishNumber++;

        //確認画面を非表示
        PokeConfirmation.SetActive(false);

        //選択画面のカーソルを表示
        PokeCursor.SetActive(true);

        //全員決定
        if (finishNumber >= 6)
        {
            //シーン進行
            AdvanceScene();
        }
    }

    //選択済み削除
    public static void DeletePoke()
    {
        finishNumber--;

        //データ削除
        selectedPoke[finishNumber] = 0;
        selectedName[finishNumber].text = "";

        //表から削除
        node[finishNumber].gameObject.SetActive(false);
    }

    //実数値更新
    public static void StatusTextUpdate(int i)//(体数目)
    {
        //ステータスを計算する
        StatusText[0].text = Data.HPCalculation(selectedPoke[i], IndividualValue[i, 0], EffortValue[i, 0], Level[i]).ToString();//(種族,個体,努力値,レベル)
        for(int j = 1; j < 5; j++)
        {
            StatusText[j].text = Data.StatusCalculation(selectedPoke[i], IndividualValue[i, j], EffortValue[i, j], Level[i], j).ToString();
        }
    }

    //わざリスト作成
    public static void WazaListCreate(int Times)
    {
        //書き換えを可能に
        WazaSelectView.SetActive(true);

        int[] NumberOfWaza = new int[1];
        NumberOfWaza[0] = 0;
        int beforeKind = 0;

        /*進化前のわざを追加*/
        if (Level[Times] >= Data.getAdvanceLevel(selectedPoke[Times]) && Data.getAdvanceLevel(selectedPoke[Times]) != 0)
        {
            //例外処理
            if (selectedPoke[Times] == 135 || selectedPoke[Times] == 136)
            {
                beforeKind = 133;
            }
            //通常処理
            else
            {
                beforeKind = selectedPoke[Times] - 1;
            }

            /*2進化前のわざを追加*/
            if (Data.getAdvanceLevel(beforeKind) != 0 && Level[Times] >= Data.getAdvanceLevel(beforeKind))
            {
                beforeKind += -1;

                int x = 0;
                while (x < Data.WazaLength(beforeKind))//(2進化前検索回数 < 2進化前のわざ数)
                {
                    //そのレベルで取得できるか
                    if (Level[Times] >= Data.WazaLevel(beforeKind, x))
                    {
                        int i = Data.getWaza(beforeKind, x);//(種類, 回数)

                        //被り判定
                        switch (x)
                        {
                            //初回のみ被り判定免除
                            case 0:
                                NumberOfWaza[0] = i;
                                break;

                            default:
                                int y = 0;
                                while (y < NumberOfWaza.Length)
                                {
                                    if (i == NumberOfWaza[y])
                                    {
                                        //被ったら検索終了
                                        break;
                                    }
                                    else
                                    {
                                        //被らなかったら次
                                        y++;
                                    }
                                    //被りはなかった
                                    if (y == NumberOfWaza.Length)
                                    {
                                        //NumberOfWazaの要素数を増やす
                                        int[] copy = new int[NumberOfWaza.Length + 1];
                                        NumberOfWaza.CopyTo(copy, 0);
                                        NumberOfWaza = new int[copy.Length];
                                        copy.CopyTo(NumberOfWaza, 0);

                                        //増やした箇所に代入
                                        NumberOfWaza[(NumberOfWaza.Length - 1)] = i;
                                    }
                                }
                                break;
                        }

                    }
                    else
                    {
                        //覚えられないので検索終了
                        break;
                    }
                    x++;
                }
                beforeKind += 1;
            }

            /*1進化前のわざを追加*/
            if (Data.getAdvanceLevel(selectedPoke[Times]) != 0 && Level[Times] >= Data.getAdvanceLevel(selectedPoke[Times]))
            {
                int x = 0;
                while (x < Data.WazaLength(beforeKind))//(進化前検索回数 < 進化前のわざ数)
                {
                    //そのレベルで取得できるか
                    if (Level[Times] >= Data.WazaLevel(beforeKind, x))
                    {
                        int i = Data.getWaza(beforeKind, x);//(種類, 回数)

                        if (NumberOfWaza[0] == 0)
                        {
                            //初登録
                            NumberOfWaza[0] = i;
                        }
                        else
                        {
                            //被り判定
                            int y = 0;
                            while (y < NumberOfWaza.Length)
                            {
                                if (i == NumberOfWaza[y])
                                {
                                    //被ったら検索終了
                                    break;
                                }
                                else
                                {
                                    //被らなかったら次
                                    y++;
                                }
                                //被りはなかった
                                if (y == NumberOfWaza.Length)
                                {
                                    //NumberOfWazaの要素数を増やす
                                    int[] copy = new int[NumberOfWaza.Length + 1];
                                    NumberOfWaza.CopyTo(copy, 0);
                                    NumberOfWaza = new int[copy.Length];
                                    copy.CopyTo(NumberOfWaza, 0);

                                    //増やした箇所に代入
                                    NumberOfWaza[(NumberOfWaza.Length - 1)] = i;
                                }
                            }
                        }
                    }
                    else
                    {
                        //覚えられないので検索終了
                        break;
                    }
                    x++;
                }
            }
        }

        /*現在取得可能なわざを追加*/
        int j = 0;
        while (j < Data.WazaLength(selectedPoke[Times]))//(進化前検索回数 < 進化前のわざ数)
        {
            //そのレベルで取得できるか
            if (Level[Times] >= Data.WazaLevel(selectedPoke[Times], j))
            {
                int i = Data.getWaza(selectedPoke[Times], j);//(種類, 回数)

                if (NumberOfWaza[0] == 0)
                {
                    //初登録
                    NumberOfWaza[0] = i;
                }
                else
                {
                    //被り判定
                    int x = 0;
                    while (x < NumberOfWaza.Length)
                    {
                        if (i == NumberOfWaza[x])
                        {
                            //被ったら検索終了
                            break;
                        }
                        else
                        {
                            //被らなかったら次
                            x++;
                        }
                        //被りはなかった
                        if (x == NumberOfWaza.Length)
                        {
                            //NumberOfWazaの要素数を増やす
                            int[] copy = new int[NumberOfWaza.Length + 1];
                            NumberOfWaza.CopyTo(copy, 0);
                            NumberOfWaza = new int[copy.Length];
                            copy.CopyTo(NumberOfWaza, 0);

                            //増やした箇所に代入
                            NumberOfWaza[(NumberOfWaza.Length - 1)] = i;
                        }
                    }
                }
            }
            else
            {
                //覚えられないので検索終了
                break;
            }
            j++;
        }

        /*わざ最少数*/
        if (NumberOfWaza.Length >= 4)
        {
            MinimumWaza[Times] = 4;
        }
        else
        {
            MinimumWaza[Times] = NumberOfWaza.Length;
        }

        /*わざマシンを追加*/
        int beforeLength = NumberOfWaza.Length;//通常技数を一時記憶

        //NumberOfWazaの要素数をわざマシンの数だけ増やす
        int[] o = new int[NumberOfWaza.Length + Data.getWazaMachineLength(selectedPoke[Times])];
        NumberOfWaza.CopyTo(o, 0);
        NumberOfWaza = new int[o.Length];
        o.CopyTo(NumberOfWaza, 0);

        for (int i = 0; i < Data.getWazaMachineLength(selectedPoke[Times]); i++)
        {
            //一覧に加える
            NumberOfWaza[beforeLength + i] = Data.getWazaMachine(selectedPoke[Times], i);
        }
        


        //確認用
        for (int i = 0; i < NumberOfWaza.Length; i++)
        {
            Debug.Log(i + " " + Data.getWazaName(NumberOfWaza[i]));
        }

        /*表作成*/
        WazaSelectView.SetActive(true);//書き込むため一時表示

        //最少取得数が4未満
        if (MinimumWaza[Times] < 4)
        {
            //レベルで覚えるわざを詰める
            for (int i = 0; i < MinimumWaza[Times]; i++)
            {
                waza[Times, i] = NumberOfWaza[i];
                wazaText[i].text = Data.getWazaName(NumberOfWaza[i]);
                
                //最少数分確定させる
                switch (i)
                {
                    case 0:
                        detailCursor.wazaRock1 = true;
                        break;
                    case 1:
                        detailCursor.wazaRock2 = true;
                        break;
                    case 2:
                        detailCursor.wazaRock3 = true;
                        break;
                }
            }

            //わざマシンを踏まえれば4以上
            if (NumberOfWaza.Length >= 4)
            {
                int i = 0;
                ScrollController SC = WazaSelectView.transform.Find("Content").GetComponent<ScrollController>();
                
                while (i < NumberOfWaza.Length)
                {
                    //
                    SC.WazaListCreate(NumberOfWaza[i], i);//(種類, 回数)
                    i++;
                }

                int a = 0;
                while (a < (4 - MinimumWaza[Times]))
                {
                    //NumberOfWazaの要素数を増やす
                    int[] copy = new int[NumberOfWaza.Length + 1];
                    NumberOfWaza.CopyTo(copy, 0);
                    NumberOfWaza = new int[copy.Length];
                    copy.CopyTo(NumberOfWaza, 0);

                    //増やした箇所に代入
                    NumberOfWaza[(NumberOfWaza.Length - 1)] = 0;

                    //わざリストに「ー」を追加
                    SC.WazaListCreate(NumberOfWaza[i], i);//(種類, 回数)

                    i++;
                    a++;
                }
            }
            //取得技が4未満
            else
            {
                //わざ構成を固定
                for (int i = MinimumWaza[Times]; i < 4; i++)
                {
                    switch (i)
                    {
                        case 1:
                            detailCursor.wazaRock2 = true;
                            break;
                        case 2:
                            detailCursor.wazaRock3 = true;
                            break;
                        case 3:
                            detailCursor.wazaRock4 = true;
                            break;
                    }
                }


            }
        }
        //わざは4種なければならない
        else
        {
            //初期化
            detailCursor.wazaRock1 = false;
            detailCursor.wazaRock2 = false;
            detailCursor.wazaRock3 = false;
            detailCursor.wazaRock4 = false;

            ScrollController SC = WazaSelectView.transform.Find("Content").GetComponent<ScrollController>();
            for (int i = 0; i < NumberOfWaza.Length; i++)
            {
                SC.WazaListCreate(NumberOfWaza[i], i);//(種類, 回数)
            }
        }

        //書き込みが終わったのでわざリストを非表示に
        WazaSelectView.SetActive(false);

        ////わざの最小数を検索
        //MinimumWaza[Times] = Data.getWazaLength(selectedPoke[Times], Level[Times], Times);
        //Debug.Log(MinimumWaza[Times]);//コンソール確認

        ////わざリストを作成
        //if (MinimumWaza[Times] < 4)
        //{
        //    for (int i = 0; i < MinimumWaza[Times]; i++)
        //    {
        //        waza[Times, i] = Data.getWaza(selectedPoke[Times], i);
        //        wazaText[i].text = Data.getWazaName(waza[Times, i]);
        //    }
        //}
    }

    //シーン進行
    public static void AdvanceScene()
    {
        switch (nowScene)
        {
            case selectScene.rule:
                //ルール選択画面を非表示に
                RuleView.SetActive(false);
                //ポケ選択画面を表示する
                PokeView.SetActive(true);
                PokeSelectedView.SetActive(true);

                //選択画面作成
                pokeSelectCursor CursorSc = PokeCursor.GetComponent<pokeSelectCursor>();    //スクリプト取得
                ScrollController contentSc = PokeContent.GetComponent<ScrollController>();  //スクリプト取得
                CursorSc.selectNumberMax = Data.getLength(r);//ポケ数設定
                for (int j = 0; j < CursorSc.selectNumberMax; j++)
                {
                    contentSc.PokeListCreate(j, r);//(試行回数,ルール)
                }
                CursorSc.canDo = true;//カーソル移動可能

                //現在シーンをpokeに変更
                nowScene = selectScene.poke;
                break;

            case selectScene.poke:
                //ポケ選択画面を非表示する
                PokeCursor.GetComponent<pokeSelectCursor>().canDo = false;//カーソル移動不可
                PokeView.SetActive(false);

                //詳細設定画面を表示する
                PokeDetail.SetActive(true);
                detailCursor.CanDo = true;  //カーソル移動可

                //数値取得
                for (int i = 0; i < 6; i++)
                {
                    //レベル
                    Level[i] = Data.getLowestLevel(selectedPoke[i]);
                    LowestLevel[i] = Level[i];

                    for (int j = 0; j < 2; j++)
                    {
                        //タイプ
                        PokeType[i, j] = Data.getType(i, j);
                    }
                    for (int j = 4; j >= 0; j--)//とくしゅ→HP
                    {
                        //種族値
                        BaseStats[i, j] = Data.getPokeStatus(selectedPoke[i], j);

                        //初期値(MAX値)
                        if (j == 0)
                        {
                            IndividualValue[i, j] = Data.HPIV(IndividualValue[i, 1], IndividualValue[i, 2], IndividualValue[i, 3], IndividualValue[i, 4]);
                        }
                        else
                        {
                            IndividualValue[i, j] = 15;
                        }
                        EffortValue[i, j] = 63002;
                        EffortLevel[i, j] = Data.EffortCalculation(EffortValue[i, j]);
                    }
                }
                
                for (int i = 0; i < 5; i++)
                {
                    //詳細設定画面に1体目の個体値,努力値,努力レベルを反映
                    IVText[i].text = IndividualValue[0, i].ToString();
                    EVText[i].text = EffortValue[0, i].ToString();
                    ELText[i].text = EffortLevel[0, i].ToString();

                    //実数値を書き出す
                    if (i == 0)//HP
                    {
                        StatusText[i].text = Data.HPCalculation(selectedPoke[0], IndividualValue[0, i], EffortValue[0, i], Level[0]).ToString();
                    }
                    else//それ以外
                    {
                        StatusText[i].text = Data.StatusCalculation(selectedPoke[0], IndividualValue[0, i], EffortValue[0, i], Level[0], i).ToString();
                    }
                }

                //とりあえず「ー」をわざリストへ
                for (int i = 0; i < 4; i++)
                {
                    wazaText[i].text = "ー";
                }

                //名前・レベルテキスト更新
                NameText.text = Data.getPokemonName(selectedPoke[0]);
                LevelText.text = Level[0].ToString();

                //詳細カーソルのMode,SelectStatusをLEVELへ変更
                detailCursor.nowMode = DetailCursor.Mode.LEVEL;
                detailCursor.nowSelected = DetailCursor.SelectStatus.LEVEL;

                //詳細カーソルを初期状態へ
                detailCursor.GetComponent<DetailCursor>().StartPosition();
                detailCursor.Times = 0;

                //わざリストを非表示に
                WazaSelectView.SetActive(false);

                //現在シーンをdetailsに変更
                nowScene = selectScene.details;
                break;

            case selectScene.details:
                break;
        }
    }

    //シーン後退
    public static void DecessionScene()
    {
        switch (nowScene)
        {
            case selectScene.rule:
                break;

            case selectScene.poke:
                //選択画面解体
                pokeSelectCursor CursorSc = PokeCursor.GetComponent<pokeSelectCursor>();//スクリプト取得
                for (int i = CursorSc.selectNumberMax; 0 < i; i--)
                {
                    //欄削除
                    GameObject g = PokeContent.transform.Find("Node (" + i + ")").gameObject;
                    Destroy(g);
                }
                CursorSc.canDo = false;//カーソル移動不可

                //ポケ選択画面を非表示に
                PokeView.SetActive(false);
                PokeSelectedView.SetActive(false);

                //ルール選択画面を表示する
                RuleView.SetActive(true);

                nowScene = selectScene.rule;
                break;
        }
    }
}
