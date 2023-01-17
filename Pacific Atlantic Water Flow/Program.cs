using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacific_Atlantic_Water_Flow
{
  class Program
  {
    static void Main(string[] args)
    {
      // https://leetcode.com/problems/pacific-atlantic-water-flow/
      // https://www.youtube.com/watch?v=9z2BunfoZ5Y
      var board = new char[4][]
      {
        new char[] { 'X','X','X','0' },
        new char[] { 'X','O','O','X' },
        new char[] { 'X','X','O','X' },
        new char[] { 'X','O','X','X' }
      };

      Solution1 s = new Solution1();
      s.Solve(board);
      foreach (var r in board)
        Console.WriteLine(string.Join(",", r));
    }
  }

  public class Solution
  {
    public IList<IList<int>> PacificAtlantic(int[][] heights)
    {

      int row = heights.Length;
      int column = heights[0].Length;

      var pacificSet = new HashSet<(int, int)>();
      var atlanticSet = new HashSet<(int, int)>();
      // Step 1 - All the first row and last row touched to the pacific and atlantic ocean so these cells are deffinetly reaching the oceans. We need to do DFS for each cells here.
      for (int c = 0; c < column; c++)
      {
        DFS(0, c, pacificSet, heights[0][c], heights);
        DFS(row - 1, c, atlanticSet, heights[row - 1][c], heights);
      }

      // Step 2 - All the first column and last columns cells are touched with pacific and atlantic oceans so these are also definetly reaching the oceans. We need to do DFS for each cells here.
      for (int r = 0; r < row; r++)
      {
        DFS(r, 0, pacificSet, heights[r][0], heights);
        DFS(r, column - 1, atlanticSet, heights[r][column - 1], heights);
      }

      // Once we are done with the Spe 1 and 2, we will get to know all the cells which will reach to pacific and atlantic in two different sets, finally will be taking the common elements from these sets to return our result.

      var res = new List<IList<int>>();

      for (int r = 0; r < row; r++)
      {
        for (int c = 0; c < column; c++)
        {
          var key = (r, c);
          if (pacificSet.Contains(key) && atlanticSet.Contains(key))
          {
            res.Add(new List<int> { r, c });
          }
        }
      }

      return res;
    }

    private void DFS(int r, int c, HashSet<(int, int)> visited, int prevHeight, int[][] heights)
    {
      // check the stop condition for DFS
      if (r < 0 || c < 0 || r >= heights.Length || c >= heights[0].Length || visited.Contains((r, c)) || prevHeight > heights[r][c]) return;

      visited.Add((r, c));

      // 4 direction DFS

      DFS(r + 1, c, visited, heights[r][c], heights);
      DFS(r - 1, c, visited, heights[r][c], heights);
      DFS(r, c + 1, visited, heights[r][c], heights);
      DFS(r, c - 1, visited, heights[r][c], heights);
    }
  }


  public class Solution1
  {
    public void Solve(char[][] board)
    {
      // step 1 - mark O which are on the border as T
      // step 2 - mark surrounded O as X
      // step 3 - revert T to O

      var row = board.Length;
      var column = board[0].Length;

      var borderRows = new int[] { 0, row - 1 };
      var borderColumns = new int[] { 0, column - 1 };
      // Step 1
      for (int i = 0; i < row; i++)
      {
        for (int j = 0; j < column; j++)
        {
          if (board[i][j] == 'O' && (borderRows.Contains(i) || borderColumns.Contains(j)))
          {
            Dfs(i, j);
          }
        }
      }

      // Step 2
      for (int i = 0; i < row; i++)
      {
        for (int j = 0; j < column; j++)
        {
          if (board[i][j] == 'O')
          {
            board[i][j] = 'X';
          }
        }
      }

      // Step 3
      for (int i = 0; i < row; i++)
      {
        for (int j = 0; j < column; j++)
        {
          if (board[i][j] == 'T')
          {
            board[i][j] = 'O';
          }
        }
      }

      void Dfs(int r, int c)
      {
        if (r < 0 || c < 0 || r >= row || c >= column || board[r][c] != 'O') return;
        board[r][c] = 'T';
        Dfs(r + 1, c);
        Dfs(r - 1, c);
        Dfs(r, c + 1);
        Dfs(r, c - 1);
      }
    }
  }
}
