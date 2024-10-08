using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class Generator
{
    public int Width = 10;
    public int Height = 10;

    public Maze GenerateMaze(int width, int height)
    {
        Width = width;
        Height = height;

        MazeCell[,] cells = new MazeCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++) 
            { 
                cells[x, y] = new MazeCell { X = x, Y = y };
            }
        }

        RecursiveBacktracker(cells);

        Maze maze = new Maze();
        maze.cells = cells;

        return maze;
    }

    private void RecursiveBacktracker(MazeCell[,] cells)
    {
        MazeCell currentCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        currentCell.Visited = true;

        Stack<MazeCell> stack = new Stack<MazeCell>();

        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = currentCell.X;
            int y = currentCell.Y;

            if (x > 0 && !cells[x - 1, y].Visited)
                unvisitedNeighbours.Add(cells[x - 1, y]);

            if (y > 0 && !cells[x, y - 1].Visited)
                unvisitedNeighbours.Add(cells[x, y - 1]);

            if (x < Width - 1 && !cells[x + 1, y].Visited)
                unvisitedNeighbours.Add(cells[x + 1, y]);

            if (y < Height - 1 && !cells[x, y + 1].Visited)
                unvisitedNeighbours.Add(cells[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell nextCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(currentCell, nextCell);

                nextCell.Visited = true;
                stack.Push(nextCell);

                currentCell = nextCell;
            }
            else
                currentCell = stack.Pop();
        }
        while (stack.Count > 0);
    }

    private void AldousBroderAlgorithm()
    {

    }

    private void WilsonsAlgorithm()
    {

    }

    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.South = false;
                b.North = false;
            }
            else
            {
                b.South = false;
                a.North = false;
            }
        }
        else
        {
            if (a.X > b.X)
            {
                a.West = false;
                b.East = false;
            }
            else
            {
                b.West = false;
                a.East = false;
            }
        }
    }
}