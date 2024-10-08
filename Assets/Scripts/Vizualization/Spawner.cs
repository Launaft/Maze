using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Camera cam;
    public GameObject mazeHandler;

    public Cell CellPrefab;
    public Vector2 CellSize = new Vector2(1, 1);

    public int Width = 10;
    public int Height = 10;

    public void GenerateMaze()
    {
        Debug.Log("Проверка");
        foreach(Transform child in mazeHandler.transform)
            GameObject.Destroy(child.gameObject);

        Generator generator = new Generator();
        Maze maze = generator.GenerateMaze(Width, Height);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.cells.GetLength(1); z++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, 0, z * CellSize.y), Quaternion.identity);

                if (maze.cells[x, z].West == false)
                    Destroy(c.West);
                if (maze.cells[x, z].East == false)
                    Destroy(c.East);
                if (maze.cells[x, z].North == false)
                    Destroy(c.North);
                if (maze.cells[x, z].South == false)
                    Destroy(c.South);

                c.transform.parent = mazeHandler.transform;
            }
        }
        cam.transform.position = new Vector3((Width * CellSize.x) / 2, Mathf.Max(Width, Height) * 8, (Height * CellSize.y) / 2);
    }
}
