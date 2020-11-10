using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp10
{

    class Car
    {
        public Car(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(int dir) // 1 - fwd  2 - right 
        {
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 0 + Y;
            Console.Write("  -******-\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 1 + Y;
            Console.Write("||*      *||\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 2 + Y;
            Console.Write(" |*      *|\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 3 + Y;
            Console.Write(" |*      *|\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 4 + Y;
            Console.Write(" |*      *|\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 5 + Y;
            Console.Write("||*      *||\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 6 + Y;
            Console.Write(" |*      *|\n");
            Console.CursorLeft = 0 + X;
            Console.CursorTop = 7 + Y;
            Console.Write("  --------");
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Car car = new Car(15, 15);
            car.Draw(1);
            Thread myThread = new Thread(new ParameterizedThreadStart(MoveIt));
            myThread.Start(car);

        }
        public static void MoveIt(object obj)
        {
            Car car = obj as Car;

            for (int i = 0; i < 10; i++)
            {
                //car.X += 1;
                car.Y -= 1;
                Console.Clear();
                car.Draw(1);
                
                System.Threading.Thread.Sleep(100);
            }
        }
        public static void Hello(object a)
        {

        }

    }

    class GasStation
    {

    }

}
