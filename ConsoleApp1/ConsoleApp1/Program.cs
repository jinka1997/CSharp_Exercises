using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp1.Models;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly List<string> lines = new List<string>();
        private static readonly List<ParsedArgs> parsedArgs = new List<ParsedArgs>();
        private static string fileName = "";

        public static void Main(string[] args)
        {
            fileName = args[0];
            Prepare();

            var triList = new List<Triangle>();
            triList.AddRange(parsedArgs.Select(p => new Triangle(double.Parse(p.TriangleBottom), double.Parse(p.TriangleHeight))));

            var squList = new List<Square>();
            squList.AddRange(parsedArgs.Select(p => new Square(double.Parse(p.SquareBottom), double.Parse(p.SquareHeight))));

            var cirList = new List<Circle>();
            cirList.AddRange(parsedArgs.Select(p => new Circle(double.Parse(p.CircleRadius))));

            //2. 100組の底辺と高さの組み合わせの数値列を読み込ませて、面積を計算して出力する
            Problem02(triList);

            //3. 2のデータで、面積が1000以上の三角形が存在するかを"Linqを用いずに"判断する  
            // (for文を使わず、foreachを用いるようにしてください)
            Problem03(triList);

            //4. 2のデータで、面積が1000以上の三角形が存在するかを"Linqを用いて"判断する
            Problem04(triList);

            //5. 2のデータで、面積が1000を初めて超えるたときの三角形の面積を"Linqを用いて"出力
            Problem05(triList);

            //6. 2のデータで、面積が1000以上の三角形の列番号を"Linqを用いて"出力  
            Problem06(triList);

            //7. 2のデータで、面積が1000以上の三角形の数を"Linqを用いて"出力
            Problem07(triList);

            //8. 2のデータで、三角形の面積の平均値を"Linqを用いて"出力
            Problem08(triList);

            //9. 2のデータで、三角形の面積を降順に並べる
            Problem09(triList);

            //11. 100組の底辺と高さの組み合わせの数値列を読み込ませて、面積を計算して出力する
            Problem11(squList);

            //12. 11のデータで、面積が1000以上の正方形が存在するかを"Linqを用いて"判断する
            Problem12(squList);

            //14. 100組の半径の数値列を読み込ませて、面積を計算して出力する
            Problem14(cirList);

            //15. 11のデータで、面積が1000以上の円が存在するかを"Linqを用いて"判断する
            Problem15(cirList);

            //17.三角形クラス、長方形クラス、円クラスの面積の合計をまとめて計算してくれるメソッドを作成する
            var shapes = new List<IShape>();
            shapes.AddRange(triList);
            shapes.AddRange(squList);
            shapes.AddRange(cirList);
            Problem17(shapes);
        }

        #region 練習問題
        private static void Problem02(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem02";
            StartProblem(title);

            var cnt = 0;
            foreach (var t in triList)
            {
                cnt++;
                Console.WriteLine($"cnt={cnt}, bottom={t.Bottom}, height={t.Height}, area={t.CalcArea()}");
            }
            EndProblem(title);
        }

        private static void Problem03(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem03";
            StartProblem(title);

            var exist = false;
            foreach (var t in triList)
            {
                if (t.CalcArea() >= 1000d)
                {
                    exist = true;
                    break;
                }
            }
            Console.WriteLine($"面積が1000以上の三角形は存在{(exist ? "する" : "しない")}");

            EndProblem(title);
        }

        private static void Problem04(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem04";
            StartProblem(title);

            var exist = triList.Any(t => t.CalcArea() >= 1000d);
            Console.WriteLine($"面積が1000以上の三角形は存在{(exist ? "する" : "しない")}");

            EndProblem(title);
        }

        private static void Problem05(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem05";
            StartProblem(title);

            var firstTriange = triList.Select(t => t.CalcArea()).FirstOrDefault(a => a > 1000d);
            Console.WriteLine($"面積が1000以上の三角形の最初の面積は{firstTriange}");

            EndProblem(title);
        }

        private static void Problem06(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem06";
            StartProblem(title);

            var query = triList.Select((t, i) => new { Index = i, Area = t.CalcArea() }).Where(a => a.Area >= 1000d).Select(a => a.Index);
            Console.WriteLine($"面積が1000以上の三角形の列番号は、{string.Join(",", query)}");

            EndProblem(title);
        }

        private static void Problem07(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem07";
            StartProblem(title);

            var cnt = triList.Count(t => t.CalcArea() >= 1000d);
            Console.WriteLine($"面積が1000以上の三角形の数は、{cnt}");

            EndProblem(title);
        }

        private static void Problem08(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem08";
            StartProblem(title);

            var ave = triList.Average(t => t.CalcArea());
            Console.WriteLine($"三角形の面積の平均値は、{ave}");

            EndProblem(title);
        }

        private static void Problem09(IReadOnlyList<Triangle> triList)
        {
            var title = "Problem09";
            StartProblem(title);

            var query = triList.Select(t => t.CalcArea()).OrderByDescending(t => t);
            query.ToList().ForEach(a => Console.WriteLine(a.ToString()));

            EndProblem(title);
        }

        private static void Problem11(IReadOnlyList<Square> squList)
        {
            var title = "Problem11";
            StartProblem(title);

            var cnt = 0;
            foreach (var s in squList)
            {
                cnt++;
                Console.WriteLine($"cnt={cnt}, bottom={s.Bottom}, height={s.Height}, area={s.CalcArea()}");
            }

            EndProblem(title);
        }

        private static void Problem12(IReadOnlyList<Square> squList)
        {
            var title = "Problem12";
            StartProblem(title);

            var exist = squList.Any(t => t.Bottom == t.Height && t.CalcArea() >= 1000d);
            Console.WriteLine($"面積が1000以上の正方形は存在{(exist ? "する" : "しない")}");

            EndProblem(title);
        }
        private static void Problem14(IReadOnlyList<Circle> cirList)
        {
            var title = "Problem14";
            StartProblem(title);

            var cnt = 0;
            foreach (var c in cirList)
            {
                cnt++;
                Console.WriteLine($"cnt={cnt}, radius={c.Radius}, area={c.CalcArea()}");
            }

            EndProblem(title);
        }
        private static void Problem15(IReadOnlyList<Circle> cirList)
        {
            var title = "Problem15";
            StartProblem(title);

            var exist = cirList.Any(t => t.CalcArea() >= 1000d);
            Console.WriteLine($"面積が1000以上の円は存在{(exist ? "する" : "しない")}");

            EndProblem(title);
        }
        private static void Problem17(IReadOnlyList<IShape> shaList)
        {
            var title = "Problem17";
            StartProblem(title);

            var areaSum = shaList.Sum(s => s.CalcArea());
            Console.WriteLine($"三角形クラス、長方形クラス、円クラスの面積の合計は、{areaSum}");

            EndProblem(title);
        }


        #endregion

        #region 練習問題前処理
        private static void Prepare()
        {
            // Csvファイルの読み込み
            using (var sr = new StreamReader(fileName, Encoding.UTF8, false))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            // Csvのファイル一行ずつの情報を格納
            // Csvのヘッダーを除くデータ部分は100行あるので、parsedArgsには100個のParsedArgsが入る
            foreach (var arg in lines.Skip(1))
            {
                parsedArgs.Add(new ParsedArgs(arg));
            }
        }

        #endregion

        #region 各練習問題メソッドの前処理・後処理
        private static void StartProblem(string title)
        {
            Console.WriteLine("");
            Console.WriteLine($"★ ----- {title} start {DateTime.Now.ToString("HH:mm:ss.fff")}----- ★");
        }
        private static void EndProblem(string title)
        {
            Console.WriteLine($"★ ----- {title} end   {DateTime.Now.ToString("HH:mm:ss.fff")}----- ★");
            Console.WriteLine("");
        }
        #endregion
    }

    #region CsvLayout
    /// <summary>
    /// 一行の情報をカンマで分解して格納
    /// </summary>
    public class ParsedArgs
    {
        public ParsedArgs(string args)
        {
            var fragment = args.Split(',');
            this.TriangleBottom = fragment[0];
            this.TriangleHeight = fragment[1];
            this.SquareBottom = fragment[2];
            this.SquareHeight = fragment[3];
            this.CircleRadius = fragment[4];
        }

        /// <summary>
        /// 三角形の底辺
        /// </summary>
        public string TriangleBottom { get; set; }

        /// <summary>
        /// 三角形の高さ
        /// </summary>
        public string TriangleHeight { get; set; }

        /// <summary>
        /// 正方形の底辺
        /// </summary>
        public string SquareBottom { get; set; }

        /// <summary>
        /// 正方形の高さ
        /// </summary>
        public string SquareHeight { get; set; }

        /// <summary>
        /// 円の半径
        /// </summary>
        public string CircleRadius { get; set; }
    }
    #endregion
}
