using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // dimensions of the grid layout will always be same row-wise and column-wise
    [SerializeField] private int dimensions = 32;

    // prefab of the mining tile
    [SerializeField] private GameObject MiningTile;

    public List<TileBehaviour> tileList;

    // Lazy singleton for now
    /// <summary>
    /// **NOTE** Please use professor Falsitta's Singleton template as that seems more better
    /// </summary>
    private static GridManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTileGrid();
        _instance = this;
    }

    public static GridManager GetInstance()
    {
        return _instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generate Tile Grid as per the given dimensions
    /// </summary>
    private void GenerateTileGrid()
    {
        int tileCounter = 0;
        for (int i = 0; i < dimensions; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                TileBehaviour tile = Instantiate(MiningTile, transform).GetComponent<TileBehaviour>();
                tile.coordinates = new Vector2Int(i, j);
                tile.tileNumber = tileCounter++;
                tileList.Add(tile);
            }
        }
    }
}
