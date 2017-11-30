using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailCursor : MonoBehaviour
{
    public enum Mode : int { LEVEL, SELECT, INPUT }
    public Mode nowMode = Mode.LEVEL;

    public enum Direction : int { UP, DOWN, LEFT, RIGHT, DECISION }
    Direction direction;

    public RectTransform myPosition;
    public enum SelectStatus : int
    {
        //HPIVは選択不可
        LEVEL, WAZA1, WAZA2, WAZA3, WAZA4,
        HPIV, AttackIV, DefenseIV, SpeedIV, SpecialIV,
        HPEV, AttackEV, DefenseEV, SpeedEV, SpecialEV,
        HPEL, AttackEL, DefenseEL, SpeedEL, SpecialEL
    }
    public SelectStatus nowSelected = SelectStatus.LEVEL;
    Vector3[] positionPoint =
    {
        new Vector3( 150, 75, 0 ),  //レベル
        new Vector3( -285, -5, 0 ), //わざ
        new Vector3( -285, -30, 0 ),
        new Vector3( -285, -55, 0 ),
        new Vector3( -285, -80, 0) ,
        new Vector3( -115, 40, 0 ), //個体値
        new Vector3( -115, 10, 0 ),
        new Vector3( -115, -20, 0 ),
        new Vector3( -115, -50, 0 ),
        new Vector3( -115, -80, 0 ),
        new Vector3( -50, 40, 0 ),   //努力値
        new Vector3( -50, 10, 0 ),
        new Vector3( -50, -20, 0 ),
        new Vector3( -50, -50, 0 ),
        new Vector3( -50, -80, 0 ),
        new Vector3( 75, 40, 0 ),   //努力レベル
        new Vector3( 75, 10, 0 ),
        new Vector3( 75, -20, 0 ),
        new Vector3( 75, -50, 0 ),
        new Vector3( 75, -80, 0 )
    };

    public bool CanDo = false;
    public int Times;
    
    public bool wazaRock1 = false;
    public bool wazaRock2 = false;
    public bool wazaRock3 = false;
    public bool wazaRock4 = false;

    public static int[] WazaList;
    
    void Update()
    {
        if (CanDo)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = Direction.UP;
                CursorMove();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = Direction.DOWN;
                CursorMove();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = Direction.RIGHT;
                CursorMove();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = Direction.LEFT;
                CursorMove();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = Direction.DECISION;
                CursorMove();
            }
        }
    }

    void CursorMove()
    {
        switch (nowMode)
        {
            case Mode.LEVEL:
                switch (direction)
                {
                    case Direction.UP:
                        if (Select.Level[Times] < 100)
                        {
                            Select.Level[Times]++;
                            LevelAction();
                        }
                        break;

                    case Direction.DOWN:
                        if (Select.Level[Times] > Select.LowestLevel[Times])
                        {
                            Select.Level[Times]--;
                            LevelAction();
                        }
                        break;

                    case Direction.RIGHT:
                        if ((Select.Level[Times] + 10) < 100)
                        {
                            Select.Level[Times] += 10;
                            LevelAction();
                        }
                        else if (Select.Level[Times] < 100)
                        {
                            Select.Level[Times] = 100;
                            LevelAction();
                        }
                        break;

                    case Direction.LEFT:
                        if ((Select.Level[Times] - 10) >= Select.LowestLevel[Times])
                        {
                            Select.Level[Times] -= 10;
                            LevelAction();
                        }
                        else if(Select.Level[Times] < Select.LowestLevel[Times])
                        {
                            Select.Level[Times] = Select.LowestLevel[Times];
                            LevelAction();
                        }
                        break;

                    case Direction.DECISION:
                        //レベル確定
                        nowMode = Mode.SELECT;

                        Select.WazaListCreate(Times);

                        //カーソルを移動
                        if (Select.MinimumWaza[Times] < 4)
                        {
                            myPosition.localPosition = positionPoint[5];
                        }
                        else
                        {
                            myPosition.localPosition = positionPoint[1];
                        }
                        break;
                }
                break;

            case Mode.SELECT:
                switch (direction)
                {
                    //決定入力
                    case Direction.DECISION:
                        switch (nowSelected)
                        {
                            case SelectStatus.WAZA1:
                                break;
                            case SelectStatus.WAZA2:
                                break;
                            case SelectStatus.WAZA3:
                                break;
                            case SelectStatus.WAZA4:
                                break;
                            case SelectStatus.HPIV:
                                break;
                            case SelectStatus.AttackIV:
                                break;
                            case SelectStatus.DefenseIV:
                                break;
                        }
                        nowMode = Mode.INPUT;
                        break;
                    //選択入力
                    default:
                        nowSelected = ModeSelect();
                        myPosition.localPosition = positionPoint[(int)nowSelected];
                        break;
                }
                break;

            case Mode.INPUT:
                break;
        }
    }

    public void StartPosition()
    {
        myPosition.localPosition = positionPoint[0];
    }

    //CursorMove内メソッド
    SelectStatus ModeSelect()
    {
        switch (direction)
        {
            case Direction.UP:

                if ((int)nowSelected % 5 != 0)
                {
                    //固定されているわざがある
                    if (wazaRock1)
                    {
                        if (wazaRock2)
                        {
                            if (wazaRock3)
                            {
                                if (nowSelected != SelectStatus.WAZA4)
                                {
                                    nowSelected--;
                                }
                            }
                            else if (nowSelected != SelectStatus.WAZA3)
                            {
                                nowSelected--;
                            }
                        }
                        else if (nowSelected != SelectStatus.WAZA2)
                        {
                            nowSelected--;
                        }
                    }
                    //通常処理
                    else if (nowSelected != SelectStatus.WAZA1)
                    {
                        nowSelected--;
                    }
                }
                break;

            case Direction.DOWN:
                if ((int)nowSelected % 5 != 4)
                {
                    nowSelected++;
                }
                break;

            case Direction.RIGHT:
                if (nowSelected < SelectStatus.HPEL)
                {
                    nowSelected += 5;
                }
                break;

            case Direction.LEFT:
                if (wazaRock1)
                {
                    if (wazaRock2)
                    {
                        if (wazaRock3)
                        {
                            if (wazaRock4)
                            {
                                if (nowSelected >= SelectStatus.HPEV)
                                {
                                    nowSelected += -5;
                                }
                            }
                        }
                        else if (nowSelected >= SelectStatus.HPIV && nowSelected <= SelectStatus.DefenseIV)
                        {
                            nowSelected = SelectStatus.WAZA3;
                        }
                        else
                        {
                            nowSelected += -5;
                        }
                    }
                    else if (nowSelected == SelectStatus.HPIV || nowSelected == SelectStatus.AttackIV)
                    {
                        nowSelected = SelectStatus.WAZA2;
                    }
                    else
                    {
                        nowSelected += -5;
                    }
                }
                else if (nowSelected == SelectStatus.HPIV)
                {
                    nowSelected = SelectStatus.WAZA1;
                }
                else
                {
                    nowSelected += -5;
                }
                break;
        }
        return nowSelected;
    }

    void LevelAction()
    {
        //レベルを基に実数値を計算
        Select.StatusTextUpdate(Times);

        //数値をテキストに更新して画面に表示
        Select.LevelText.text = Select.Level[Times].ToString();
    }
}
