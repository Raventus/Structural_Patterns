using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    /// <summary>
    ///  Компоновщик компонует объекты в древовидные структуры для представления иерархии часть - целое. ПОзволяет клиентам единообразно трактовать индивидуальные и составные объекты
    ///  Клиент должен работать с простыми и составными объектами единообразно, но добавление операций Add Remove в составной объект нарушит принцип постановки Лисков
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Composite root = new Composite("root");

            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));

            Composite comp = new Composite("Composite X");

            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            root.Add(comp);
            root.Add(new Leaf("Leaf C"));

            // Add and remove a leaf
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);

            // Recursively display tree
            root.Display(1);

            // Wait for user
            Console.Read();
        }
    }

    abstract class Component
    {
        protected string name;

        // Constructor
        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Display(int depth);
    }

    class Composite : Component
    {
        private List<Component> children = new List<Component>();

        // Constructor
        public Composite(string name) : base(name)
        {
        }

        public void Add(Component component)
        {
            children.Add(component);
        }

        public void Remove(Component component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }

    class Leaf : Component
    {
        // Constructor
        public Leaf(string name) : base(name)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }
    }
    

}
