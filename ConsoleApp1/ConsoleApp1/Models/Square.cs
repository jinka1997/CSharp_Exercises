using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;


namespace ConsoleApp1.Models
{
    /// <summary>
    /// 10. 長方形クラス(Squareクラス)を作って、図形の面積の数値を返すメソッドを持たせる  
    /// -> コンストラクタで底辺と高さを受け取り、publicなプロパティに値をセットしてください。
    /// </summary>
    public class Square : IShape
    {
        public double Bottom { set; get; }
        public double Height { set; get; }

        public Square(double bottom, double height)
        {
            this.Bottom = bottom;
            this.Height = height;
        }
        public double CalcArea()
        {
            return this.Bottom * this.Height;
        }
    }
}
