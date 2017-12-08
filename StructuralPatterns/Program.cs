using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    /// <summary>
    ///  Преобразует интерфейс одного класса в интерфейс другого, который ожидают клиенты. Адаптер делает возможным совместную работу классов
    ///  с несовместимыми интерфейсами
    ///  Адаптер это клей связывающий между собой два мира воедино путём подгонки текущих классов к требуемому интерфейсу
    ///  Позволяет использовать существующие типы в новом контексте
    ///  Примеры: TextReader, TextWritter, BinaryReader, BinaryWritter, ReadonlyCollection<T>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Target target = new Adapter();
            target.Request();
            Console.Read();
        }
    }

    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }

    class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();

        public override void Request()
        {
            // Possibly do some other work
            // and then call SpecificRequest
            adaptee.SpecificRequest();
        }
    }

    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}
