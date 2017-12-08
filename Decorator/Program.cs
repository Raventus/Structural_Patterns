using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    /// <summary>
    /// Динамически добавляет объекту новый обязанности. Является гибкой альтернативой порождения подклассов с целью расширения функциональности
    /// НАследование не решает проблему, так как декоратор будет жестко зависеть от наследуемого типа и не сможет работать с другими, реализующими интерфейс (реализованный наследуемым типом)
    /// Недостатки: 1. Чувствительность к порядку (код инициализации очень важен(правильно определить вложенность)).
    /// 2. Сложность отладки , 3. Увеличение сложности
    /// Пременяются для добавления всем методом интерфейса некоторого поведения, которое не является частью основной функциональности: кэширование, замеры времени, логирование аргументов,
    /// управление доступом пользователей, шифрования, распоковки, упаковки.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA dA = new ConcreteDecoratorA();
            ConcreteDecoratorB dB = new ConcreteDecoratorB();

            // Link decorators
            dA.SetComponent(c);
            dB.SetComponent(dA);

            dA.Operation();

            Console.WriteLine();

            dB.Operation();

            // Wait for user
            Console.Read();
        }
    }

    abstract class Component
    {
        public abstract void Operation();
    }

    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.Write("Привет");
        }
    }

    abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
        }
    }

    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();

            Console.Write(" Мир!");
        }
    }
}
