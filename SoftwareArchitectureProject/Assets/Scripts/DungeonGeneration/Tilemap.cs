using System;

[Serializable]
public class TileMap
{
    public int[,] Map {get;}

    public TileMap()
    {
        Map  = new int[0, 0];
    }
    public TileMap(int h, int w)
    {
        Map = new int[h, w];
    }

    public void SetTile(int row, int col,  int val)
    {
        Map[row, col] = val;
    }

    public int GetTile(int row, int col)
    {
        return Map[row, col];
    }
    
    public int GetValue(int row, int col)
    {
        return Map[row, col] | Map[row, col + 1] << 1 | Map[row + 1, col + 1] << 2 | Map[row + 1, col] << 3;
    }

}