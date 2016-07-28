using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeIq
{
    class AStar
    {
        static void Main(string[] args)
        {
            int m, n;
            string line;
            string[] values;
            for (; (line = Console.ReadLine()) != null; )
            {
                List<string> map = new List<string>();
                AStar astar = new AStar();
                values = line.Split(' ');   //M, N値取得
                if (int.TryParse(values[0], out m) && int.TryParse(values[1], out n))
                {
                    for (int i = 0; i < m; i++) map.Add(Console.ReadLine());    //マップ取得
                    Console.WriteLine(astar.Astar(m, n, map));
                }
            }
        }

        //４方向探索用
        private readonly Dictionary<char, int> dx = new Dictionary<char,int>();
        private readonly Dictionary<char, int> dy = new Dictionary<char,int>();
        private readonly char[] dir = { 'u', 'r', 'd', 'l' };

        //コンストラクタ
        public AStar()
        {
            dx.Add(dir[0], 0);
            dx.Add(dir[1], 1);
            dx.Add(dir[2], 0);
            dx.Add(dir[3], -1);
            dy.Add(dir[0], -1);
            dy.Add(dir[1], 0);
            dy.Add(dir[2], 1);
            dy.Add(dir[3], 0);
        }

        //マンハッタン距離取得
        private int GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        //A*(A-star)探索アルゴリズム
        //方向転換した回数を返す
        int Astar(int m, int n, List<string> map)
        {
            int[,] grid = new int[m, n];    //移動コスト(距離)の記録
            int sx, sy, gx, gy;             //スタートとゴール位置
            string path = "";               //ゴールまでの方向転換履歴
            sx = sy = gx = gy = 0;

            //迷路データのパース
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (map[i][j] == '#')
                    {
                        grid[i, j] = int.MinValue;       //壁
                    }
                    else
                    {
                        grid[i, j] = int.MaxValue;
                    }
                }
            }
            //スタートとゴールの位置設定
            grid[0, 0] = 0;
            grid[m - 1, n - 1] = int.MaxValue;
            gx = n - 1;
            gy = m - 1;

            //A*(A-star) 探索
            List<Position> q = new List<Position>();

            //スタート位置設定
            Position p = new Position(sx, sy);
            p.estimate = this.GetDistance(sx, sy, gx, gy);
            q.Add(p);

            //マップ探索開始
            while (0 < q.Count())
            {
                p = q[0];
                q.RemoveAt(0);
                if (p.cost > grid[p.y, p.x])
                {
                    continue;
                }
                if (p.y == gy && p.x == gx)
                {    //ゴールに到達
                    path = p.path;
                    break;
                }

                //直前の移動方向以外に移動した場合で探索する
                for (int i = 0; i < dir.Count(); i++)
                {
                    int nx = p.x + dx[dir[i]];
                    int ny = p.y + dy[dir[i]];
                    if (nx < 0 || n <= nx || ny < 0 || m <= ny)
                    {    //範囲外
                        continue;
                    }
                    Position p2 = new Position(nx, ny);
                    p2.cost = this.GetDistance(nx, ny, sx, sy);        //移動コスト(スタートからの移動量)
                    p2.estimate = this.GetDistance(nx, ny, gx, gy) + p2.cost;    //推定値
                    p2.path = p.path + dir[i];     //移動経路(移動方向の記録)
                    if (grid[ny, nx] >= p2.cost + p2.GetTurnCount(p2.path))
                    {
                        q.Add(p2);

                        grid[ny, nx] = p2.cost + p2.GetTurnCount(p2.path);
                    }
                }
                q = q.OrderBy(o => o.GetTurnCount(o.path))
                     .ThenBy(o => o.estimate)
                     .ToList();
                //Console.WriteLine("*****************************************************");
                //foreach (Position outP in q)
                //{
                //    Console.WriteLine("[{0}][{1}] = {2}", outP.x, outP.y, outP.path);
                //}
            }
            //Console.WriteLine(p.path);
            return p.GetTurnCount(p.path);
        }

        //位置情報
        public class Position
        {
            public int x;               //X座標
            public int y;               //Y座標
            public int cost;            //移動コスト(スタートからの移動量)
            public int estimate;        //推定値(ゴールまでのマンハッタン距離＋移動コスト)
            public string path = "";    //移動経路(移動方向の記録)

            //コンストラクタ
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            //方向転換した回数を返す
            public int GetTurnCount(string path)
            {
                if (path.Length < 2) return 0;

                int count = 0;
                for (int i = 1; i < path.Length; i++)
                {
                    if (path[i - 1] != path[i]) count++;
                }
                return count;
            }
        }
    }
}
