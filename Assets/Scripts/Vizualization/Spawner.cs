using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Camera cam;
    public GameObject mazeHandler;

    public Cell CellPrefab;
    public Vector2 CellSize = new Vector2(1, 1);

    public GameObject PlayerPrefab;

    public int Width;
    public int Height;

    private int _generationAlgorithm;

    public void GenerateMaze()
    {
        foreach(Transform child in mazeHandler.transform)
            GameObject.Destroy(child.gameObject);

        Generator generator = new Generator();
        Maze maze = generator.GenerateMaze(Width, Height, _generationAlgorithm);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            Destroy(player);

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

                if (maze.cells[x, z].WestE == true)
                {
                    Destroy(c.West);
                    c.WestEntrance.SetActive(true);
                }
                if (maze.cells[x, z].EastE == true)
                {
                    Destroy(c.East);
                    c.EastEntrance.SetActive(true);
                }
                if (maze.cells[x, z].NorthE == true)
                {
                    Destroy(c.North);
                    c.NorthEntrance.SetActive(true);
                }
                if (maze.cells[x, z].SouthE == true)
                {
                    Destroy(c.South);
                    c.SouthEntrance.SetActive(true);
                }

                c.transform.parent = mazeHandler.transform;
                c.distance.text = maze.cells[x, z].Distance.ToString();

                if (maze.cells[x, z].Distance == 0)
                    Instantiate(PlayerPrefab, new Vector3(x * CellSize.x, 1, z * CellSize.y), Quaternion.identity);
            }
        }

        CameraSwitch.OnMazeGenerated?.Invoke();
        cam.transform.position = new Vector3((Width * CellSize.x) / 2, Mathf.Max(Width, Height) * 3, (Height * CellSize.y) / 2);
    }

    public void ChangeHeight(float value) => Height = (int)value;
    public void ChangeWidht(float value) => Width = (int)value;
    public void ChangeGenerationAlgorithm (int value) => _generationAlgorithm = value;
}
