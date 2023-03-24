using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class StageManager
{
    static bool[,] tileMatrix;
    static int height, weight;
    static int mineCount;

    static void Init()
    {
        height = 10;
        weight = 10;
        tileMatrix = new bool[height, weight];
        for (int x = 0; x < weight; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileMatrix[x, y] = false;
            }
        }
    }
    static void RandomizePattern()  // �Ķ���ͷ� Ŭ���� Ÿ���� ��ǥ �޾ƿ��� �ֺ� 3x3 Ÿ�� �����ϰ� ���� ���� ��� �߰� �ʿ�
    {
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, height - 1);
            int y = Random.Range(0, weight - 1);
            if (tileMatrix[x, y])
            {
                i--;
                continue;
            }
            tileMatrix[x, y] = true;
        }
    }
}
