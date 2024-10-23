using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
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

        CreateLoops(cells);
        Dijkstra(cells);
        
        Maze maze = new Maze();
        maze.cells = cells;

        return maze;
    }

    private void Dijkstra(MazeCell[,] cells)
    {
        for (int x = 0; x < cells.GetLength(0); x++)
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y].Visited = false;
                cells[x, y].Distance = 99;
            }

        MazeCell currentCell = CreateEntrance(cells);
        currentCell.Distance = 0;

        for (int i = 0;  i < cells.Length; i++)
        {
            currentCell = MinDistance(cells);
            currentCell.Visited = true;

            List<MazeCell> neighbours = new List<MazeCell>();

            int x = currentCell.X;
            int y = currentCell.Y;

            if (x > 0 && !currentCell.Visited && !currentCell.West)
                neighbours.Add(cells[x - 1, y]);

            if (y > 0 && !currentCell.Visited && !currentCell.South)
                neighbours.Add(cells[x, y - 1]);

            if (x < Width - 1 && !currentCell.Visited && !currentCell.East)
                neighbours.Add(cells[x + 1, y]);

            if (y < Height - 1 && !currentCell.Visited && !currentCell.North)
                neighbours.Add(cells[x, y + 1]);

            for (int n = 0; n < neighbours.Count; n++)
            {
                if (neighbours[n].Distance > currentCell.Distance + 1)
                    neighbours[n].Distance = currentCell.Distance + 1;
            }
        }
    }

    private MazeCell MinDistance(MazeCell[,] cells)
    {
        int min = 99;
        
        MazeCell mazeCell = new MazeCell();

        for (int x = 0; x < cells.GetLength(0); x++)
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (!cells[x, y].Visited && cells[x, y].Distance < min)
                {
                    min = cells[x, y].Distance;
                    mazeCell = cells[x, y];
                }
            }

        return mazeCell;
    }

    private MazeCell CreateEntrance(MazeCell[,] cells)
    {
        MazeCell entrance = cells[0, 0];

        switch (Random.Range(1, 4))
        {
            case 0:
                entrance = cells[0, Random.Range(0, Height - 1)];
                entrance.WestE = true;
                break;
            case 1:
                entrance = cells[Random.Range(0, Width - 1), 0];
                entrance.SouthE = true;
                break;
            case 2:
                entrance = cells[Width - 1, Random.Range(0, Height - 1)];
                entrance.EastE = true;
                break;
            case 3:
                entrance = cells[Random.Range(0, Width - 1), Height - 1];
                entrance.NorthE = true;
                break;
        }

        return entrance;
    }

    private void CreateLoops(MazeCell[,] cells)
    {
        int sectorWidth = Width / 2;
        int sectorHeight = Height / 2;
        int offsetX = 0;
        int offsetY = 0;

        MazeCell[,] sector = new MazeCell[sectorWidth, sectorHeight];

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < sectorWidth - 1; x++)
                for (int y = 0; y < sectorHeight - 1; y++)
                {
                    sector[x, y] = cells[x + offsetX, y + offsetY];
                }

            MazeCell cell1 = sector[Random.Range(0, sectorWidth - 1), Random.Range(0, sectorHeight - 1)];

            List<MazeCell> neighbours = new List<MazeCell>();

            int cx = cell1.X;
            int cy = cell1.Y;

            if (cx > 0)
                neighbours.Add(cells[cx - 1, cy]);

            if (cy > 0)
                neighbours.Add(cells[cx, cy - 1]);

            if (cx < Width - 1)
                neighbours.Add(cells[cx + 1, cy]);

            if (cy < Height - 1)
                neighbours.Add(cells[cx, cy + 1]);

            MazeCell cell2 = neighbours[Random.Range(0, neighbours.Count - 1)];

            RemoveWall(cell1, cell2);

            switch (i)
            {
                case 0:
                    offsetY += sectorHeight;
                    break;
                case 1:
                    offsetX += sectorWidth;
                    break;
                case 2: offsetY -= sectorHeight;
                    break;
            }
            
        }
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
        MazeCell currentCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        currentCell.Visited = true;
        int summVisited = 1;
        while (summVisited < cells.Length)
        {
            List<MazeCell> PossibleDirections = new List<MazeCell>();

            int x = currentCell.X;
            int y = currentCell.Y;

            if (x > 0)
                PossibleDirections.Add(cells[x - 1, y]);

            if (y > 0)
                PossibleDirections.Add(cells[x, y - 1]);

            if (x < Width - 1)
                PossibleDirections.Add(cells[x + 1, y]);

            if (y < Height - 1)
                PossibleDirections.Add(cells[x, y + 1]);

            MazeCell nextCell = PossibleDirections[Random.Range(0, PossibleDirections.Count)];

            if (!nextCell.Visited)
            {
                RemoveWall(currentCell, nextCell);
                nextCell.Visited = true;
                summVisited++;
            }

            currentCell = nextCell;
        }

        /*
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
                int i = Random.Range(0, 3);
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
                int i = Random.Range(0, 2);
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
                int i = Random.Range(0, 2);
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
                int i = Random.Range(0, 2);
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
                int i = Random.Range(0, 2);
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
                int i = Random.Range(0, 1);
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
                int i = Random.Range(0, 1);
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
                int i = Random.Range(0, 1);
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
                int i = Random.Range(0, 1);
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
        }*/
    }

    private void WilsonsAlgorithm(MazeCell[,] cells)
    {
        MazeCell firstCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        firstCell.Visited = true;
        
        MazeCell currentCell = SetStartCell(cells);

        //int unvisitedCells = cells.Length - 1;
        int sumVisited = 1;

        Stack<MazeCell> stack = new Stack<MazeCell>();

        do
        {
            int x = currentCell.X;
            int y = currentCell.Y;
            //float r = RandomNumberGenerator.GetInt32(1, 5);

            List<MazeCell> possibleDirections = new List<MazeCell>();

            if (x > 0)
                possibleDirections.Add(cells[x - 1, y]);

            if (y > 0)
                possibleDirections.Add(cells[x, y - 1]);

            if (x < Width - 1)
                possibleDirections.Add(cells[x + 1, y]);

            if (y < Height - 1)
                possibleDirections.Add(cells[x, y + 1]);


            if (possibleDirections.Count > 0)
            {
                MazeCell nextCell = possibleDirections[Random.Range(0, possibleDirections.Count)];

                if (!nextCell.Visited && !stack.Contains(nextCell))
                {
                    currentCell = nextCell;
                    stack.Push(currentCell);
                }
                else if (stack.Contains(nextCell))
                {
                    currentCell = nextCell;
                    RemoveCycle(stack, currentCell);
                }
                else if (nextCell.Visited)
                {
                    sumVisited += stack.Count;
                    Debug.Log(sumVisited);
                    stack.Push(nextCell);
                    GoBackOnStack(stack);
                    currentCell = SetStartCell(cells);
                }
            }
            /*switch (r)
            {
                case 1: // Left
                    if (x > 0)
                    {
                        if (!cells[x - 1, y].Visited && !stack.Contains(cells[x - 1, y]))
                        {
                            currentCell = cells[x - 1, y];
                        }
                        else if (stack.Contains(cells[x - 1, y]))
                        {
                            currentCell = cells[x - 1, y];
                            RemoveCycle(stack, currentCell);
                        }
                        else if (cells[x - 1, y].Visited)
                        {
                            unvisitedCells -= stack.Count;
                            stack.Push(cells[x - 1, y]);
                            GoBackOnStack(stack);
                        }
                    }
                break;

                case 2: // Down
                    if (y > 0)
                    {
                        if (!cells[x, y - 1].Visited && !stack.Contains(cells[x, y - 1]))
                        {
                            currentCell = cells[x, y - 1];
                        }
                        else if (stack.Contains(cells[x, y - 1]))
                        {
                            currentCell = cells[x, y - 1];
                            RemoveCycle(stack, currentCell);
                        }
                        else if (cells[x, y - 1].Visited)
                        {
                            unvisitedCells -= stack.Count;
                            stack.Push(cells[x, y - 1]);
                            GoBackOnStack(stack);
                        }
                    }
                break;

                case 3: // Right
                    if (x < Width - 1)
                    {
                        if (!cells[x + 1, y].Visited && !stack.Contains(cells[x + 1, y]))
                        {
                            currentCell = cells[x + 1, y];
                        }
                        else if (stack.Contains(cells[x + 1, y]))
                        {
                            currentCell = cells[x + 1, y];
                            RemoveCycle(stack, currentCell);
                        }
                        else if (cells[x + 1, y].Visited)
                        {
                            unvisitedCells -= stack.Count;
                            stack.Push(cells[x + 1, y]);
                            GoBackOnStack(stack);
                        }
                    }
                break;

                case 4: // Up
                    if (y < Height - 1)
                    {
                        if (!cells[x, y + 1].Visited && !stack.Contains(cells[x, y + 1]))
                        {
                            currentCell = cells[x, y + 1];
                        }
                        else if (stack.Contains(cells[x, y + 1]))
                        {
                            currentCell = cells[x, y + 1];
                            RemoveCycle(stack, currentCell);
                        }
                        else if (cells[x, y + 1].Visited)
                        {
                            unvisitedCells -= stack.Count;
                            stack.Push(cells[x, y + 1]);
                            GoBackOnStack(stack);
                        }
                    }                    
                break;
            }*/
        }
        while (sumVisited < cells.Length - 1);
    }

    private MazeCell SetStartCell(MazeCell[,] cells)
    {
        MazeCell startCell;

        do startCell = cells[Random.Range(0, Width - 1), Random.Range(0, Height - 1)];
        while (startCell.Visited);

        return startCell;
    }

    private void RemoveCycle(Stack<MazeCell> stack, MazeCell cell)
    {
        do stack.Pop();
        while (stack.Peek() != cell);
    }

    private void GoBackOnStack(Stack<MazeCell> stack)
    {
        while (stack.Count > 1)
        {
            MazeCell firstCell = stack.Pop();
            MazeCell nextCell = stack.Peek();

            firstCell.Visited = true;

            RemoveWall(firstCell, nextCell);
        }

        MazeCell lastCell = stack.Pop();
        lastCell.Visited = true;
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