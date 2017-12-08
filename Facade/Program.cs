using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    /// <summary>
    /// Предоставляет унифицированный интерфейс вместо набора интерфейсов некоторой подсистемы.
    /// Фасад определяет интерфейс более высокого уровня, который упрощяет использование подсистемы.
    /// Фасад - высокоуровневый класс над подсистемами
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Facade facade = new Facade();

            facade.Operation1();
            facade.Operation2();

            // Wait for user
            Console.Read();
        }
    }

    public class Facade
    {
        Library.SubsystemA a = new Library.SubsystemA();
        Library.SubsystemB b = new Library.SubsystemB();
        Library.SubsystemC c = new Library.SubsystemC();

        public  void Operation1()
        {
            Console.WriteLine("Operation 1\n" +
            a.A1() +
            a.A2() +
            b.B1());
        }
        public  void Operation2()
        {
            Console.WriteLine("Operation 2\n" +
            b.B1() +
            c.C1());
        }
    }
}


namespace Library
{
    /// <summary>
    /// Класс подсистемы
    /// </summary>
    /// <remarks>
    /// <li>
    /// <lu>реализует функциональность подсистемы;</lu>
    /// <lu>выполняет работу, порученную объектом <see cref="Facade"/>;</lu>
    /// <lu>ничего не "знает" о существовании фасада, то есть не хранит ссылок на него;</lu>
    /// </li>
    /// </remarks>
    internal class SubsystemA
    {
        internal string A1()
        {
            return "Subsystem A, Method A1\n";
        }
        internal string A2()
        {
            return "Subsystem A, Method A2\n";
        }
    }
    internal class SubsystemB
    {
        internal string B1()
        {
            return "Subsystem B, Method B1\n";
        }
    }
    internal class SubsystemC
    {
        internal string C1()
        {
            return "Subsystem C, Method C1\n";
        }
    }
}