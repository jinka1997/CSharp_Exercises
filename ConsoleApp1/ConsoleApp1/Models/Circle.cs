using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;


namespace ConsoleApp1.Models
{
    /// <summary>
    /// 13. 円クラス(Circleクラス)を作って、図形の面積の数値を返すメソッドを持たせる  
    ///   -> コンストラクタで半径を受け取り、publicなプロパティに値をセットしてください。  
    /// </summary>
    public class Circle : IShape
    {
        public double Radius { set; get; }
        public Circle(double radius)
        {
            this.Radius = radius;
        }
        public double CalcArea()
        {
            return this.Radius * this.Radius * Math.PI;
        }
    }
}
