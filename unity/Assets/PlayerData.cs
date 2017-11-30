using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public static int[,] status = new int[6, 30];

    void Start()
    {
        for (int i = 1; i < 7; i++)
        {
            status[i - 1, 1] = i * 3;
        }

        for(int i = 0; i < 6; i++)
        {
            for(int j = 3; j < 30; j++)
            {
                status[i, j] = Data.getPokeStatus(status[i, 1], j);//種類 & 入れる場所
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
