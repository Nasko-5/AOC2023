namespace AOCDayTemplate;

public class Grid<T>
{
    private T[][] Values = new T[1][];
    public readonly int gridWidth;
    public readonly int gridHeight;

    public Grid(IEnumerable<IEnumerable<T>> initialValues)
    {
        Values = initialValues.Select(row => row.ToArray()).ToArray();

        gridWidth = Values[0].Length;
        gridHeight = Values.Length;
    }


    // ===--- -                                                       - ---=== //
    // #+       More general methods for getting neighbors of a point       +# //
    // ===--- -                                                       - ---=== //

    public (int x, int y)[] GetPointDirectNeighbors1D(int x, int y)
    {
        return new (int x, int y)[9]
        {
            (x-1, y-1), (x, y-1), (x+1, y-1),
            (x-1, y),   (x, y),   (x+1, y),
            (x-1, y+1), (x, y+1), (x+1, y+1)
        };
    }

    public (int x, int y)[,] GetPointDirectNeighbors2D(int x, int y)
    {
        return new (int x, int y)[3, 3]
        {
            { (x-1, y-1), (x, y-1), (x+1, y-1)},
            { (x-1, y),   (x, y),   (x+1, y)},
            { (x-1, y+1), (x, y+1), (x+1, y+1)}
        };
    }


    // ===--- -                                                                   - ---=== //
    // #+       Methods for getting the neighbors of a point a Grid object's grid       +# //
    // ===--- -                                                                   - ---=== //

    // =~- - Get direct neighbors as points - -~=

    // =~- - As a 2D array - -~= //

    public (int x, int y)[,] GetBoundedPointDirectNeighbors2D(int x, int y)
    {
        return new (int x, int y)[3, 3]
        {
            { IndexIsInBounds(x-1, y-1)?(x-1, y-1):(int.MinValue, int.MinValue), IndexIsInBounds(x, y-1)?(x, y):(int.MinValue, int.MinValue),IndexIsInBounds(x+1, y-1)?(x+1, y-1):(int.MinValue, int.MinValue)},
            { IndexIsInBounds(x-1, y)?(x=1, y):(int.MinValue, int.MinValue),   IndexIsInBounds(x, y)?(x, y):(int.MinValue, int.MinValue),   IndexIsInBounds(x+1, y)?(x+1, y):(int.MinValue, int.MinValue)},
            { IndexIsInBounds(x-1, y+1)?(x-1, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(x, y+1)?(x, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(x + 1, y + 1) ?(x + 1, y + 1) :(int.MinValue, int.MinValue)}
        };
    }

    // =~- - As a 1D array - -~= //

    public (int x, int y)[] GetBoundedPointDirectNeighbors1D(int x, int y)
    {
        return new (int x, int y)[9]
        {
            IndexIsInBounds(x-1, y-1)?(x-1, y-1):(int.MinValue, int.MinValue), IndexIsInBounds(x, y-1)?(x, y):(int.MinValue, int.MinValue),IndexIsInBounds(x+1, y-1)?(x+1, y-1):(int.MinValue, int.MinValue),
            IndexIsInBounds( x-1, y)?(x=1, y):(int.MinValue, int.MinValue),   IndexIsInBounds(x, y)?(x, y):(int.MinValue, int.MinValue),   IndexIsInBounds(x+1, y)?(x+1, y):(int.MinValue, int.MinValue),
            IndexIsInBounds(x-1, y+1)?(x-1, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(x, y+1)?(x, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(x + 1, y + 1) ?(x + 1, y + 1) :(int.MinValue, int.MinValue)
        };
    }

    // =~- - Get direct neighbors - -~=

    // =~- - As a 1D array - -~=

    public T[] GetDirectNeighbors1D(int x, int y)
    {
        return new T[9]
        {
            FancyIndex(x-1, y-1), FancyIndex(x, y-1), FancyIndex(x+1, y-1),
            FancyIndex(x-1, y),      FancyIndex(x, y),     FancyIndex(x+1, y),
            FancyIndex(x-1, y + 1), FancyIndex(x, y + 1), FancyIndex(x+1, y+1)
        };
    }

    // =~- - As a 2D array - -~=

    public T[,] GetDirectNeighbors2D(int x, int y)
    {
        return new T[3,3]
        {
            { FancyIndex(x-1, y-1), FancyIndex(x, y-1), FancyIndex(x+1, y-1) },
            { FancyIndex(x-1, y),      FancyIndex(x, y),     FancyIndex(x+1, y) },
            { FancyIndex(x-1, y + 1), FancyIndex(x, y + 1), FancyIndex(x+1, y+1) }
        };
    }

    // =~- - Get any N neighbors - -~=

    // =~- - As a 2D array - -~= //

    public T[,] GetNeighbors2D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[,] neighbors = new T[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = FancyIndex(neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }

    public T[,] GetWrapNeighbors2D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[,] neighbors = new T[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = FancyWrapIndex(neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }

    // =~- - As a 1D array - -~= //

    public T[] GetNeighbors1D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[] neighbors = new T[neighborSquareSideLength *  neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i * neighborSquareSideLength + j] = FancyIndex(neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }



    public T[] GetWrapNeighbors1D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[] neighbors = new T[neighborSquareSideLength * neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i * neighborSquareSideLength + j] = FancyWrapIndex(neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }

    // =~- - As a 2D array - -~= //

    public (int x, int y)[,] GetPointNeighbors2D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        (int x, int y)[,] neighbors = new (int x, int y)[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = (neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }

    public (int x, int y)[,] GetBoundedPointNeighbors2D(int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        (int x, int y)[,] neighbors = new (int x, int y)[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = IndexIsInBounds(neighborSquareStart.x + j, neighborSquareStart.y + i) ? 
                        (neighborSquareStart.x + j, neighborSquareStart.y + i) 
                        : (int.MinValue, int.MinValue);
            }
        }

        return neighbors;
    }

    // ===--- -                                                                  - ---=== //
    // #+       Methods for getting the neighbors of a point in a specified grid       +# //
    // ===--- -                                                                  - ---=== //

    // =~- - Get direct neighbors as points for specified grid - -~=

    // =~- - As a 2D array - -~= //
    
    public (int x, int y)[,] GetBoundedPointDirectNeighbors2D(T[,] grid, int x, int y)
    {
        return new (int x, int y)[3, 3]
        {
            { IndexIsInBounds(grid, x-1, y-1)?(x-1, y-1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x, y-1)?(x, y):(int.MinValue, int.MinValue),IndexIsInBounds(grid, x+1, y-1)?(x+1, y-1):(int.MinValue, int.MinValue)},
            { IndexIsInBounds(grid, x-1, y)?(x=1, y):(int.MinValue, int.MinValue),   IndexIsInBounds(grid, x, y)?(x, y):(int.MinValue, int.MinValue),   IndexIsInBounds(grid, x+1, y)?(x+1, y):(int.MinValue, int.MinValue)},
            { IndexIsInBounds(grid, x-1, y+1)?(x-1, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x, y+1)?(x, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x + 1, y + 1) ?(x + 1, y + 1) :(int.MinValue, int.MinValue)}
        };
    }


    // =~- - As a 1D array - -~= //

    public (int x, int y)[] GetBoundedPointDirectNeighbors1D(T[,] grid, int x, int y)
    {
        return new (int x, int y)[9]
        {
            IndexIsInBounds(grid, x-1, y-1)?(x-1, y-1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x, y-1)?(x, y):(int.MinValue, int.MinValue),IndexIsInBounds(grid, x+1, y-1)?(x+1, y-1):(int.MinValue, int.MinValue),
            IndexIsInBounds(grid, x-1, y)?(x=1, y):(int.MinValue, int.MinValue),   IndexIsInBounds(grid, x, y)?(x, y):(int.MinValue, int.MinValue),   IndexIsInBounds(grid, x+1, y)?(x+1, y):(int.MinValue, int.MinValue),
            IndexIsInBounds(grid, x-1, y+1)?(x-1, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x, y+1)?(x, y+1):(int.MinValue, int.MinValue), IndexIsInBounds(grid, x + 1, y + 1) ?(x + 1, y + 1) :(int.MinValue, int.MinValue)
        };
    }

    // =~- - Get direct neighbors for specified grid - -~=

    // =~- - As a 2D array - -~= //

    public T[,] GetDirectNeighbors2D(T[,] grid, int x, int y)
    {
        return new T[3, 3]
        {
            { FancyIndex(grid, x-1, y-1), FancyIndex(grid, x, y-1), FancyIndex(grid, x+1, y-1)},
            { FancyIndex(grid, x-1, y),      FancyIndex(grid, x, y),     FancyIndex(grid, x+1, y)},
            { FancyIndex(grid, x-1, y + 1), FancyIndex(grid, x, y + 1), FancyIndex(grid, x+1, y+1)}
        };
    }

    public T[,] GetWrapDirectNeighbors2D(T[,] grid, int x, int y)
    {
        return new T[3, 3]
        {
            { FancyWrapIndex(grid, x-1, y-1), FancyWrapIndex(grid, x, y-1), FancyWrapIndex(grid, x+1, y-1)},
            { FancyWrapIndex(grid, x-1, y),      FancyWrapIndex(grid, x, y),     FancyWrapIndex(grid, x+1, y)},
            { FancyWrapIndex(grid, x-1, y + 1), FancyWrapIndex(grid, x, y + 1), FancyWrapIndex(grid, x+1, y+1)}
        };
    }

    // =~- - As a 1D array - -~= //

    public T[] GetDirectNeighbors1D(T[,] grid, int x, int y)
    {
        return new T[9]
        {
            FancyIndex(grid, x-1, y-1), FancyIndex(grid, x, y-1), FancyIndex(grid, x+1, y-1),
            FancyIndex(grid, x-1, y),      FancyIndex(grid, x, y),     FancyIndex(grid, x+1, y),
            FancyIndex(grid, x-1, y + 1), FancyIndex(grid, x, y + 1), FancyIndex(grid, x+1, y+1)
        };
    }

    public T[] GetWrapDirectNeighbors1D(T[,] grid, int x, int y)
    {
        return new T[9]
        {
            FancyWrapIndex(grid, x-1, y-1), FancyWrapIndex(grid, x, y-1), FancyWrapIndex(grid, x+1, y-1),
            FancyWrapIndex(grid, x-1, y),      FancyWrapIndex(grid, x, y),     FancyWrapIndex(grid, x+1, y),
            FancyWrapIndex(grid, x-1, y + 1), FancyWrapIndex(grid, x, y + 1), FancyWrapIndex(grid, x+1, y+1)
        };
    }

    // =~- - Get any N neighbors for specified gird - -~= //

    // =~- - As a 2D array - -~= //

    public T[,] GetNeighbors2D(T[,] grid, int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int xPos, int yPos) neighborSquareStart = (x - n, y - n);

        T[,] neighbors = new T[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = FancyIndex(grid, neighborSquareStart.xPos + j, neighborSquareStart.yPos + i);
            }
        }

        return neighbors;
    }

    public T[,] GetWrapNeighbors2D(T[,] grid, int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int xPos, int yPos) neighborSquareStart = (x - n, y - n);

        T[,] neighbors = new T[neighborSquareSideLength, neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i, j] = FancyWrapIndex(grid, neighborSquareStart.xPos + j, neighborSquareStart.yPos + i);
            }
        }

        return neighbors;
    }

    // =~- - As a 1D array - -~= //

    public T[] GetNeighbors1D(T[,] grid, int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[] neighbors = new T[neighborSquareSideLength * neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[i * (neighborSquareSideLength + j)] = FancyIndex(grid, neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }

    public T[] GetWrapNeighbors1D(T[,] grid, int x, int y, int n)
    {
        int neighborSquareSideLength = (2 * n) + 1;
        (int x, int y) neighborSquareStart = (x - n, y - n);

        T[] neighbors = new T[neighborSquareSideLength * neighborSquareSideLength];

        for (int i = 0; i < neighborSquareSideLength; i++)
        {
            for (int j = 0; j < neighborSquareSideLength; j++)
            {
                neighbors[j * (neighborSquareSideLength + i)] = FancyWrapIndex(grid, neighborSquareStart.x + j, neighborSquareStart.y + i);
            }
        }

        return neighbors;
    }


    // ===--- -                                               - ---=== //
    // #+        Bound checking / Проверки за валиден индекс        +# //
    // ===--- -                                               - ---=== //

    public bool IndexIsInBounds(int x, int y)
    {
        return (x >= 0 && y >= 0) && (x < gridWidth && y < gridHeight);
    }

    public bool IndexIsInBounds(T[,] grid, int x, int y)
    {
        return (x >= 0 && y >= 0) && (x < grid.GetLength(1) && y < grid.GetLength(0));
    }


    // ===--- -                                           - ---=== //
    // #+        Indexing Methods / Методи за индексиране       +# //
    // ===--- -                                           - ---=== //

    public T FancyWrapIndex(int x, int y)
    {
        return Values[((y % gridHeight) + gridHeight) % gridHeight][((x % gridWidth) + gridWidth) % gridWidth];
    }

    public T FancyWrapIndex(T[,] grid, int x, int y)
    {
        return Values[((y % grid.GetLength(0)) + grid.GetLength(0)) % grid.GetLength(0)][((x % grid.GetLength(1)) + grid.GetLength(1)) % grid.GetLength(1)];
    }

    public T FancyIndex(int x, int y)
    {
        if (IndexIsInBounds(x, y))
        {
            return Values[y][x];
        }

        if (typeof(T) == typeof(int))
        {
            return (T)(object)int.MinValue;
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)float.MinValue;
        }
        else if (typeof(T) == typeof(double))
        {
            return (T)(object)float.MinValue;
        }
        else if (typeof(T) == typeof(char))
        {
            return (T)(object)'\0';
        }

        return default(T);
    }

    public T FancyIndex(T[,] grid, int x, int y)
    {
        if (IndexIsInBounds(grid, x, y))
        {
            return grid[y, x];
        }

        if (typeof(T) == typeof(int))
        {
            return (T)(object)int.MinValue;
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)float.MinValue;
        }
        else if (typeof(T) == typeof(double))
        {
            return (T)(object)float.MinValue;
        }
        else if (typeof(T) == typeof(char))
        {
            return (T)(object)'\0';
        }

        return default(T);
    }
}