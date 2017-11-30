using UnityEngine;
using System.Collections;

public class PokeConfirmationCursor : MonoBehaviour
{
    public RectTransform myPosition;

    enum Direction : int { UP, DOWN }
    Direction direction = Direction.UP;

    public static bool canDo = true;

    bool Decision = true;

    public static int selectNumber = 0;

    void Start()
    {

    }

    void Update()
    {
        if (canDo)
        {
            //上入力
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //カーソルが下にある時
                if (Decision == false)
                {
                    direction = Direction.UP;
                    CursorMove(direction);
                }
                
            }
            //下入力
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //カーソルが上にある時
                if (Decision)
                {
                    direction = Direction.DOWN;
                    CursorMove(direction);
                }
            }
            //決定入力
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Decision)
                {
                    //ポケ決定
                    Select.DecisionPoke(selectNumber);//(ポケ番号)
                }
                else
                {
                    //カーソルを上に戻す
                    myPosition.localPosition += new Vector3(0, 30, 0);
                    Decision = true;

                    //キャンセル
                    Select.Cancel();
                }
            }
        }
    }

    private void CursorMove(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                //カーソルを上に移動
                myPosition.localPosition += new Vector3(0, 30, 0);

                Decision = true;
                break;

            case Direction.DOWN:
                //カーソルを下に移動
                myPosition.localPosition += new Vector3(0, -30, 0);

                Decision = false;
                break;
        }
    }
}
