using System;

namespace Surrounded_Regions
{
  class Program
  {
    static void Main(string[] args)
    {
      var board = new char[][] { new char[] { 'X', 'X', 'X', 'X' }, new char[] { 'X', '0', '0', 'X' }, 
        new char[] { 'X', 'X', '0', 'X' }, new char[] { 'X', '0', 'X', 'X' } };
      Program p = new Program();
      p.Solve(board);
      foreach (var b in board)
        Console.WriteLine(string.Join(",", b));
    }

    public void Solve(char[][] board)
    {
      int ROW = board.Length;
      int COLUMN = board[0].Length;
      // Step 1 - Find all the cells which are on the boarder of the board and mark them 0 => T
      // Do DFS for the border cells and also mark the connected cells to border cells 0 => T
      for (int i = 0; i < ROW; i++)
      {
        for (int j = 0; j < COLUMN; j++)
        {
          if (board[i][j] == '0' && (i == 0 || i == ROW - 1 || j == 0 || j == COLUMN - 1))
          {
            DFS(i, j, board);
          }
        }
      }
      // Step 2 - two for loops mark all the cells 0 => X
      for (int i = 0; i < ROW; i++)
      {
        for (int j = 0; j < COLUMN; j++)
        {
          if (board[i][j] == '0')
            board[i][j] = 'X';
        }
      }
      // Step 3 - Finally change the T => 0
      for (int i = 0; i < ROW; i++)
      {
        for (int j = 0; j < COLUMN; j++)
        {
          if (board[i][j] == 'T')
            board[i][j] = '0';
        }
      }
    }

    private void DFS(int r, int c, char[][] board)
    {
      if (IsInValid(board, r, c) || board[r][c] != '0') return;
      board[r][c] = 'T';
      DFS(r + 1, c, board);
      DFS(r - 1, c, board);
      DFS(r, c + 1, board);
      DFS(r, c - 1, board);
    }

    private bool IsInValid(char[][] board, int row, int col)
    {
      return row < 0 || row >= board.Length || col < 0 || col >= board[row].Length;
    }
  }
}
