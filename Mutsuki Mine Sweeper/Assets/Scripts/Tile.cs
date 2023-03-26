using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isMineTile;
    public int mineTileCount;

    void Start()
    {

    }

    public Tile()
    {
        mineTileCount = 0;
    }
}