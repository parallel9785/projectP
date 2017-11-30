using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pokeSelectCursor : MonoBehaviour
{
    public enum Direction : int { UP, DOWN, LEFT, RIGHT }
    Direction direction;

    public bool canDo;

    public int selectNumber;//選んでいる番号
    public int selectNumberMax;//全体のポケ数

    public RectTransform myPosition;
    public RectTransform contentPosition;

    public GameObject content;

    public static Select SelectSC;

    void Start()
    {
        selectNumber = 1;

        //メインスクリプトを取得
        //SelectSC = GameObject.Find("GamaManager").gameObject.GetComponent<Select>();

        //myPosition = GetComponent<RectTransform>();
        //contentPosition = content.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        if (canDo)
        {
            //上入力
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = Direction.UP;
                CursorMove(direction);
            }
            //下入力
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = Direction.DOWN;
                CursorMove(direction);
            }
            //左入力
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = Direction.LEFT;
                CursorMove(direction);
            }
            //右入力
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = Direction.RIGHT;
                CursorMove(direction);
            }
            //キャンセル入力
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (Select.finishNumber > 0)
                {
                    Select.DeletePoke();
                }
                else
                {
                    //カーソルを初期位置に戻す
                    myPosition.localPosition = new Vector3(-226, 49, 0);
                    selectNumber = 1;
                    //表を最上段に戻す
                    contentPosition.localPosition = new Vector3(0, 150, 0);

                    //ルール選択へ戻る
                    Select.DecessionScene();
                }
            }
            //決定入力
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //確認
                Select.ConfirmationPoke(selectNumber);
            }
        }
    }

    public void CursorMove(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                //カーソルが上段にある場合
                if (myPosition.localPosition.y >= 49)//数値に謎アリ
                {
                    //一覧の最上段の場合
                    if (selectNumber > 3)
                    {
                        //一覧を上にスクロール
                        contentPosition.localPosition += new Vector3(0, -50, 0);

                        selectNumber += -3;
                    }
                }
                else//通常時
                {
                    //カーソルを上に1段移動
                    myPosition.localPosition += new Vector3(0, 50, 0);

                    selectNumber += -3;
                }
                break;

            case Direction.DOWN:
                //カーソルが下段にある場合
                if (myPosition.localPosition.y <= -101)//数値に謎アリ
                {
                    //一覧の最下段の場合
                    if (selectNumber < selectNumberMax - 2)
                    {
                        //一覧を下にスクロール
                        contentPosition.localPosition += new Vector3(0, 50, 0);

                        selectNumber += 3;
                    }
                }
                else//通常時
                {
                    //一覧の最後では無い場合
                    if (selectNumber != selectNumberMax)
                    {
                        //カーソルを下に1段移動
                        myPosition.localPosition += new Vector3(0, -50, 0);

                        selectNumber += 3;
                    }
                }
                break;

            case Direction.LEFT:
                //カーソルが左端にある場合
                if (myPosition.localPosition.x <= -226)//数値に謎アリ
                {
                    //カーソルが上段にある場合
                    if (myPosition.localPosition.y >= 49)//数値に謎アリ
                    {
                        //一覧の最上段では無い場合
                        if (selectNumber > 3)
                        {
                            //一覧を上にスクロール
                            contentPosition.localPosition += new Vector3(0, -50, 0);

                            //カーソルを右端に移動
                            myPosition.localPosition += new Vector3(300, 0, 0);

                            selectNumber--;
                        }
                    }
                    else
                    {
                        //カーソルを上に一段,右端に移動
                        myPosition.localPosition += new Vector3(300, 50, 0);

                        selectNumber--;
                    }
                }
                else//通常時
                {
                    //カーソルを左に1個移動
                    myPosition.localPosition += new Vector3(-150, 0, 0);

                    selectNumber--;
                }
                break;

            case Direction.RIGHT:
                //カーソルが右端にある場合
                if (myPosition.localPosition.x >= 74)//数値に謎アリ
                {
                    //カーソルが下段にある場合
                    if (myPosition.localPosition.y <= -101)//数値に謎アリ
                    {
                        //一覧の最下段では無い場合
                        if (selectNumber != selectNumberMax)
                        {
                            //一覧を下にスクロール
                            contentPosition.localPosition += new Vector3(0, 50, 0);

                            //カーソルを左端に移動
                            myPosition.localPosition += new Vector3(-300, 0, 0);

                            selectNumber++;
                        }
                    }
                    else
                    {
                        //カーソルを下に一段,左端に移動
                        myPosition.localPosition += new Vector3(-300, -50, 0);

                        selectNumber++;
                    }
                }
                else//通常時
                {
                    //一覧の最後では無い場合
                    if (selectNumber != selectNumberMax)
                    {
                        //カーソルを右に1個移動
                        myPosition.localPosition += new Vector3(150, 0, 0);

                        selectNumber++;
                    }
                }
                break;
        }
    }
}
