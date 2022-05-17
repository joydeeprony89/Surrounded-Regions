using System;
using System.Collections.Generic;

namespace Surrounded_Regions
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }

    public void Solve(char[][] board)
    {
      if (board == null || board.Length == 1) return;
      // Queue has been used here to insert the positions where we found '0'.
      Queue<(int, int)> q = new Queue<(int, int)>();
      // check for the 0's in left and right side

      for (int i = 0; i < board.Length; ++i)
      {
        if (board[i][0] == '0') q.Enqueue((i, 0));
        int lastColumn = board[i].Length - 1;
        if (board[i][lastColumn] == '0') q.Enqueue((i, lastColumn));
      }

      // check for the 0's in top and bottom side
      int lastRow = board.Length - 1;
      for (int i = 0; i < board[0].Length; ++i)
      {
        if (board[0][i] == '0') q.Enqueue((0, i));
        if (board[lastRow][i] == '0') q.Enqueue((lastRow, i));
      }

      // Visited will have position of the 0's in the board
      var visited = new bool[board.Length][];
      for (int i = 0; i < board.Length; ++i)
      {
        visited[i] = new bool[board[i].Length];
      }

      var directions = new List<List<int>>();
      directions.Add(new List<int>() { 0, 1 }); // right 
      directions.Add(new List<int>() { 0, -1 }); // left
      directions.Add(new List<int>() { 1, 0 }); // down
      directions.Add(new List<int>() { -1, 0 }); // up

      while (q.Count > 0)
      {
        // row and column where 0 we have found.
        var (row, column) = q.Dequeue();
        // mark position as 0 value cell.
        visited[row][column] = true;
        // Now check for each 0 value cell any neighbour is also 0
        foreach (var direction in directions)
        {
          int newRow = row + direction[0];
          int newColumn = row + direction[1];
          if (IsValid(board, newRow, newColumn) && !visited[newRow][newColumn] && board[newRow][newColumn] == '0')
          {
            // Push the neighbour 0 value cell position
            q.Enqueue((newRow, newColumn));
          }
        }
      }

      for (int i = 0; i < board.Length; ++i)
      {
        for (int j = 0; j < board[i].Length; ++j)
        {
          // visited now has all the 0's cell position which are not surrounded by X in all four directions
          if (board[i][j] == '0' && !visited[i][j])
          {
            board[i][j] = 'X';
          }
        }
      }
    }

    private bool IsValid(char[][] board, int i, int j)
    {
      return i >= 0 && i < board.Length && j >= 0 && j < board[i].Length;
    }
  }
}
