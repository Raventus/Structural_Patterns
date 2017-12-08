using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    /// <summary>
    /// является суррогатом другого объекта и контролирует доступ к нему
    /// 1. Удалённый заместитель - отвечает за кодирование запроса и его аргументов для работы с компонентом в другом адрессном пространстве
    /// 2. Виртуальный заместитель - кеширует дополнительную информацию о реальном компоненте, чтобы отложить его создание
    /// 3. Защищающий заместитель - проверяет, имеет ли вызывающий объект необходимые длявыполнения запроса права
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IMath p = new MathProxy();

            Console.WriteLine("4 + 2 = " + p.Add(4, 2));
            Console.WriteLine("4 - 2 = " + p.Sub(4, 2));
            Console.WriteLine("4 * 2 = " + p.Mul(4, 2));
            Console.WriteLine("4 / 2 = " + p.Div(4, 2));
            Console.Read();
        }
    }

    public interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
        double Mul(double x, double y);
        double Div(double x, double y);
    }

    class Math : IMath
    {
        public Math()
        {
            Console.WriteLine("Create object Math. Wait...");
            Thread.Sleep(1000);
        }

        public double Add(double x, double y) { return x + y; }
        public double Sub(double x, double y) { return x - y; }
        public double Mul(double x, double y) { return x * y; }
        public double Div(double x, double y) { return x / y; }
    }

    class MathProxy : IMath
    {
        Math math;

        public MathProxy()
        {
            math = null;
        }

        /// <summary>
        /// Быстрая операция - не требует реального субъекта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }

        /// <summary>
        /// Медленная операция - требует создания реального субъекта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Mul(double x, double y)
        {
            if (math == null)
                math = new Math();
            return math.Mul(x, y);
        }

        public double Div(double x, double y)
        {
            if (math == null)
                math = new Math();
            return math.Div(x, y);
        }
    }
}
