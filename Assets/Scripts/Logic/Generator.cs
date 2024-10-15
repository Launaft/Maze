using System.Collections.Generic;
using UnityEngine;
public class Generator
{
    public int Width = 10;
    public int Height = 10;

    public Maze GenerateMaze(int width, int height, int generationAlgorthm)
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

        switch (generationAlgorthm)
        {
            case 0:
                RecursiveBacktracker(cells);
                break;
            case 1:
                AldousBroderAlgorithm(cells);
                break;
            case 2:
                WilsonsAlgorithm(cells);
                break;
        }
        
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

    private void AldousBroderAlgorithm(MazeCell[,] cells)
    {
        MazeCell currentCell = cells[0, 0];
        currentCell.Visited = true;

        //int[,] gridMaze = new int[Width, Height];
        //int[,] gridVizited = new int[Width, Height];
        int w = 0;
        int h = 0;
        //gridVizited[0, 0] = 1;
        int summVizited = 1;
        while (summVizited < cells.Length)
        {
            if (w > 0 && h > 0 && w < Width-1 && h < Height-1)
            {
                int i = Random.Range(0, 3);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w++; break;
                    case 1:
                        h++; break;
                    case 2:
                        w--; break;
                    case 3:
                        h--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == 0 && h > 0 && h < Height-1)
            {
                int i = Random.Range(0, 2);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w++; break;
                    case 1:
                        h++; break;
                    case 2:
                        h--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (h == 0 && w > 0 && w < Width-1)
            {
                int i = Random.Range(0, 2);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w++; break;
                    case 1:
                        h++; break;
                    case 2:
                        w--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == Width-1 && h > 0 && h < Height-1)
            {
                int i = Random.Range(0, 2);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        h--; break;
                    case 1:
                        h++; break;
                    case 2:
                        w--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (h == Height-1 && w > 0 && w < Width-1)
            {
                int i = Random.Range(0, 2);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        h--; break;
                    case 1:
                        w++; break;
                    case 2:
                        w--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == 0 && h == 0)
            {
                int i = Random.Range(0, 1);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w++; break;
                    case 1:
                        h++; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == 0 && h == Height-1)
            {
                int i = Random.Range(0, 1);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w++; break;
                    case 1:
                        h--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == Width-1 && h == 0)
            {
                int i = Random.Range(0, 1);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w--; break;
                    case 1:
                        h++; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
            if (w == Width-1 && h == Height-1)
            {
                int i = Random.Range(0, 1);//Случайное число для направления
                switch (i)
                {
                    case 0:
                        w--; break;
                    case 1:
                        h--; break;
                }
                if (cells[w, h].Visited == false)
                {
                    cells[w, h].Visited = true;
                    summVizited++;
                    RemoveWall(currentCell, cells[w, h]);
                }
                currentCell = cells[w, h];
            }
        }
    }

    private void WilsonsAlgorithm(MazeCell[,] cells)
    {
        MazeCell firstCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        firstCell.Visited = true;
        
        MazeCell currentCell = SetGraphStart(ref cells);

        int unvisitedCells = cells.Length - 1;

        do
        {
            Stack<MazeCell> stack = new Stack<MazeCell>();

            stack.Push(currentCell);

            int x = currentCell.X;
            int y = currentCell.Y;

            switch (Random.Range(1, 4))
            {
                case 1:
                    if (x > 0 && !cells[x - 1, y].Visited && !stack.Contains(cells[x - 1, y]))
                    {
                        currentCell = cells[x - 1, y];
                        stack.Push(cells[x - 1, y]);
                    }
                    else if (x > 0 && stack.Contains(cells[x - 1, y]))
                    {
                        currentCell = cells[x - 1, y];
                        stack = RemoveCycle(stack, currentCell);
                    }
                    else if (x > 0 && cells[x - 1, y].Visited)
                    {
                        unvisitedCells -= stack.Count;
                        GoBackOnStack(stack);
                        currentCell = SetGraphStart(ref cells);
                    }
                break;

                case 2:
                    if (y > 0 && !cells[x, y - 1].Visited && !stack.Contains(cells[x, y - 1]))
                    {
                        currentCell = cells[x, y - 1];
                        stack.Push(cells[x, y - 1]);
                    }
                    else if (y > 0 && stack.Contains(cells[x, y - 1]))
                    {
                        currentCell = cells[x, y - 1];
                        stack = RemoveCycle(stack, currentCell);
                    }
                    else if (y > 0 && cells[x, y - 1].Visited)
                    {
                        unvisitedCells -= stack.Count;
                        GoBackOnStack(stack);
                        currentCell = SetGraphStart(ref cells);
                    }
                break;

                case 3:
                    if (x < Width - 1 && !cells[x + 1, y].Visited && !stack.Contains(cells[x + 1, y]))
                    {
                        currentCell = cells[x + 1, y];
                        stack.Push(cells[x + 1, y]);
                    }
                    else if (x < Width - 1 && stack.Contains(cells[x + 1, y]))
                    {
                        currentCell = cells[x + 1, y];
                        stack = RemoveCycle(stack, currentCell);
                    }
                    else if (x < Width - 1 && cells[x + 1, y].Visited)
                    {
                        unvisitedCells -= stack.Count;
                        GoBackOnStack(stack);
                        currentCell = SetGraphStart(ref cells);
                    }
                break;

                case 4:
                    if (y < Height - 1 && !cells[x, y + 1].Visited && !stack.Contains(cells[x, y + 1]))
                    {
                        currentCell = cells[x, y + 1];
                        stack.Push(cells[x, y + 1]);
                    }
                    else if (y < Height - 1 && stack.Contains(cells[x, y + 1]))
                    {
                        currentCell = cells[x, y + 1];
                        stack = RemoveCycle(stack, currentCell);
                    }
                    else if (y < Height - 1 && cells[x, y + 1].Visited)
                    {
                        unvisitedCells -= stack.Count;
                        GoBackOnStack(stack);
                        currentCell = SetGraphStart(ref cells);
                    }
                break;
            }
            Debug.Log(unvisitedCells);
        }
        while (unvisitedCells > 0);
    }

    private MazeCell SetGraphStart(ref MazeCell[,] cells)
    {
        MazeCell startCell;

        do startCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        while (startCell.Visited);

        return startCell;
    }

    private Stack<MazeCell> RemoveCycle(Stack<MazeCell> stack, MazeCell cell)
    {
        do
            stack.Pop();
        while (stack.Peek() != cell);

        return stack;
    }

    private void GoBackOnStack(Stack<MazeCell> stack)
    {
        do
            RemoveWall(stack.Pop(), stack.Peek());
        while (stack.Count > 1);
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