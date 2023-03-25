using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    static bool[,] tileMatrix;
    static int height, weight;
    static int _mineCount;

    GameObject _normalTile, _mineTile;

    public void Init()
    {
        _normalTile = (GameObject)Resources.Load("Prefabs/NormalTile");
        _mineTile = (GameObject)Resources.Load("Prefabs/MineTile");
        weight = 10;
        height = 10;
        _mineCount = 10;
        tileMatrix = new bool[height, weight];
        for (int x = 0; x < weight; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileMatrix[x, y] = false;
            }
        }

        RandomizePattern();
    }
    void RandomizePattern()  // 파라미터로 클릭한 타일의 좌표 받아오고 주변 3x3 타일 제외하고 지뢰 생성 기능 추가 필요
    {
        for (int i = 0; i < _mineCount; i++)
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

        for (int x = 0; x < weight; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tileMatrix[x, y])
                    Managers.Instantiate(_mineTile, new Vector2(x, y), Quaternion.identity);
                else
                    Managers.Instantiate(_normalTile, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
