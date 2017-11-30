using UnityEngine;
using System.Collections;

public class RuleCursor : MonoBehaviour
{
    public enum selectRule : int { nin97, nin98, nin99, castom1, noLimit, castum2, Ultra, Fancy, Yellow, }
    selectRule RuleNumber;

    public enum Direction : int { UP, DOWN, LEFT, RIGHT }
    Direction direction = Direction.UP;

    private RectTransform myPosition;

    void Start()
    {
        RuleNumber = 0;
        myPosition = GetComponent<RectTransform>();
    }
    
    void Update()
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
        //決定
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            int i = (int)RuleNumber;
            Select.DecisionRule(i);
        }
    }

    public void CursorMove(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                //カーソルが上段に無い時
                if (myPosition.localPosition.y < 39)//数値に謎アリ
                {
                    //カーソルを1段上に移動
                    myPosition.localPosition += new Vector3(0, 70, 0);

                    RuleNumber += -3;
                }
                break;

            case Direction.DOWN:
                //カーソルが下段に無い時
                if (myPosition.localPosition.y > -101)//数値に謎アリ
                {
                    //カーソルを1段下に移動
                    myPosition.localPosition += new Vector3(0, -70, 0);

                    RuleNumber += 3;
                }
                break;

            case Direction.LEFT:
                //カーソルが左端に無い時
                if (myPosition.localPosition.x > -241)//数値に謎アリ
                {
                    //カーソルを左に1つ移動
                    myPosition.localPosition += new Vector3(-165, 0, 0);

                    RuleNumber--;
                }
                break;

            case Direction.RIGHT:
                //カーソルが右端に無い時
                if (myPosition.localPosition.x < -11)//数値に謎アリ
                {
                    //カーソルを右に1つ移動
                    myPosition.localPosition += new Vector3(165, 0, 0);

                    RuleNumber++;
                }
                break;
        }
    }
}
