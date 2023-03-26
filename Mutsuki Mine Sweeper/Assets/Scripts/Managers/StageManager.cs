using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEngine;

public class StageManager
{
    static bool[,] tileMatrix;
    static int height, weight;
    static int _mineCount;
    int[, ] _tileNums;

    GameObject _normalTile, _mineTile, _tileParent;
    GameObject[, ] _tileObjs;

    Sprite[] _numSprites;

    public void Init()
    {
        _normalTile = (GameObject)Resources.Load("Prefabs/NormalTile");
        _mineTile = (GameObject)Resources.Load("Prefabs/MineTile");
        _tileParent = GameObject.Find("Tiles");
        weight = 9;
        height = 9;
        _mineCount = 10;
        tileMatrix = new bool[height, weight];
        _tileObjs = new GameObject[height, weight];
        _numSprites = new Sprite[8];
        for (int x = 0; x < weight; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileMatrix[x, y] = false;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            _numSprites[i] = Resources.Load($"Texture/Numbers/{i + 1}", typeof(Sprite)) as Sprite;
        }

        RandomizePattern();
    }
    void RandomizePattern()  // 파라미터로 클릭한 타일의 좌표 받아오고 주변 3x3 타일 제외하고 지뢰 생성 기능 추가 필요
    {
        for (int i = 0; i < _mineCount; i++)    // 지뢰 여부 랜덤 bool값
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

        for (int x = 0; x < weight; x++)    // 랜덤 bool값에 따른 타일 생성
        {
            for (int y = 0; y < height; y++)
            {
                if (tileMatrix[x, y])
                {
                    _tileObjs[x, y] = Object.Instantiate(_mineTile, new Vector2(x, y), Quaternion.identity);
                    Tile temp = _tileObjs[x, y].GetComponent<Tile>();
                    temp.transform.parent = _tileParent.transform;
                }
                else
                {
                    _tileObjs[x, y] = Object.Instantiate(_normalTile, new Vector2(x, y), Quaternion.identity);
                    Tile temp = _tileObjs[x, y].GetComponent<Tile>();
                    temp.transform.parent = _tileParent.transform;
                    CheckMineMount(x, y, temp);
                    switch (temp.mineTileCount)
                    {
                        case 1:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[0];
                            break;
                        case 2:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[1];
                            break;
                        case 3:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[2];
                            break;
                        case 4:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[3];
                            break;
                        case 5:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[4];
                            break;
                        case 6:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[5];
                            break;
                        case 7:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[6];
                            break;
                        case 8:
                            _tileObjs[x, y].transform.Find("NumberTile").GetComponent<SpriteRenderer>().sprite = _numSprites[7];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        Vector2 _cameraCenter = new Vector2(-weight / 2, -height / 2);
        if (weight % 2 == 0)
            _cameraCenter.x += 0.5f;
        if (height % 2 == 0)
            _cameraCenter.y += 0.5f;
        _tileParent.transform.Translate(_cameraCenter);
    }

    void CheckMineMount(int _targetX, int _targetY, Tile _target)
    {
        int minX = _targetX - 1;
        int maxX = _targetX + 1;
        int minY = _targetY - 1;
        int maxY = _targetY + 1;

        if (_targetX == 0)
            minX += 1;
        if (_targetX == weight - 1)
            maxX -= 1;
        if (_targetY == 0)
            minY += 1;
        if (_targetY == height - 1)
            maxY -= 1;

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (tileMatrix[x, y])
                    _target.mineTileCount++;
            }
        }
    }
}
