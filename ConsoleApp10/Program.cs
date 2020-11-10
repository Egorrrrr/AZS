using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    /// <summary>
    /// рисует заправку
    /// </summary>
    class Station
    {
        public void DrawStation()
        {
            Console.CursorTop = 7;
            for (int i = 0; i < 120; i += 30)
            {
                Console.CursorLeft = i;
                Console.Write(" |                     [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                     [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                  (--[] \n");
                Console.CursorLeft = i;
                Console.Write(" |              <><>(  [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                  (  [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                  (--[] \n");
                Console.CursorLeft = i;
                Console.Write(" |                     [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                     [] \n");
                Console.CursorLeft = i;
                Console.Write(" |                     [] \n");
                Console.CursorLeft = i;
                Console.CursorTop = 7;

            }


        }
    }
    /// <summary>
    ///Класс машина. Он ее рисует и описывает, как ее рисовать и куда она едет
    /// </summary>
    class Car
    {
        public Thread cart { get; set; }
        public int StationNum { get; set; }
        public int SpawnPos { get; set; }
        public int Assignedstation { get; set; }
        public int Direction { get; set; }
        public Car(int x, int y)
        {
            X = x;
            Y = y;
            Direction = 1;
            
        }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw() // 1 - fwd  2 - right 
        {
            if (Direction == 1)
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
            else if(Direction == 2)
            {
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 0 + Y;
                Console.Write("  __     __\n");
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 1 + Y;
                Console.Write(" ------------- ");
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 2 + Y;
                Console.Write("| * * * * * * |");
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 3 + Y;
                Console.WriteLine("|             *");
                
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 4 + Y;
                Console.Write("| * * * * * * |");
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 5 + Y;
                Console.Write(" ------------- ");
                Console.CursorLeft = 0 + X;
                Console.CursorTop = 6 + Y;
                Console.Write("  --     --\n");





            }
        }
    }
    class GasStation
    {
        bool[] freest = new bool[4];
        List<Car> cars = new List<Car>();
        Station st = new Station();
        int freestations = 4;
        static Semaphore sem = new Semaphore(4,4);
        static Semaphore mover = new Semaphore(1, 1);
        public GasStation()
        {
            SpawnCars();
            for (int i = 0; i < freest.Length; i++)
            {
                freest[i] = true;
            }
        }
        /// <summary>
        /// начальное создание машин
        /// </summary>
        void SpawnCars()
        {
            int j = 0;
            for (int i = 20; i < 80; i += 15)
            {
                j++;
                Car cr = new Car(i, 30);
                cr.cart = new Thread(new ParameterizedThreadStart(Fill));
                //cr.Assignedstation = j;
                cr.SpawnPos = j;
                cr.cart.Start(cr);
                cars.Add(cr);
                
                
            }
            Update();
        }
        List<Car> helper = new List<Car>();
        /// <summary>
        /// обновляет графику
        /// </summary>
        void Update()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            st.DrawStation();
            foreach (var item in cars)
            {
                item.Draw();
                if (item.cart == null)
                {
                    item.cart = new Thread(new ParameterizedThreadStart(Fill));
                    item.cart.Start(item);
                }
            }
            foreach (var item in helper)
            {
                item.Draw();
            }
        }
        /// <summary>
        /// Общий ресурс с семиформаии
        /// </summary>
        /// <param name="car"></param>
        void Fill(object car)
        {
            int delay = 3;
            Car temporal = car as Car;
            //Console.ReadLine();
            mover.WaitOne();
            sem.WaitOne();
            ///маршурт от точки появления
            for (int i = 0; i < 7; i++)
            {
                temporal.Y -= 1;
                Update();
                System.Threading.Thread.Sleep(delay);
            }
            temporal.Direction = 2;
            int jj = 15 * temporal.SpawnPos;
            for (int i = 0; i < jj; i++)
            {
                temporal.X -= 1;
                System.Threading.Thread.Sleep(delay);
                Update();
            }
            for (int i = 0; i < freest.Length; i++)
            {
                if (freest[i] == true)
                {
                    temporal.Assignedstation = i;
                    freest[i] = false;
                    break;
                }
            }
            temporal.Direction = 1;

            System.Threading.Thread.Sleep(delay);
            Update();
            for (int i = 0; i < 6; i++)
            {
                temporal.Y -= 1;
                Update();
                System.Threading.Thread.Sleep(delay);
            }
            

            ///вариации маршрута взависимость это направльния
            switch (temporal.Assignedstation)
            {
                case 0:
                    {
                        Update();
                        for (int i = 0; i < 11; i++)
                        {
                            temporal.Y -= 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        break;

                    }
                case 1:
                    {
                        Update();
                        temporal.Direction = 2;
                        for (int i = 0; i < 30; i++)
                        {
                            temporal.X += 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        temporal.Direction = 1;
                        for (int i = 0; i < 11; i++)
                        {
                            temporal.Y -= 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        break;
                    }
                case 2:
                    {
                        Update();
                        temporal.Direction = 2;
                        for (int i = 0; i < 60; i++)
                        {
                            temporal.X += 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        temporal.Direction = 1;
                        for (int i = 0; i < 11; i++)
                        {
                            temporal.Y -= 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        break;
                    }
                case 3:
                    {
                        Update();
                        temporal.Direction = 2;
                        for (int i = 0; i < 90; i++)
                        {
                            temporal.X += 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        temporal.Direction = 1;
                        for (int i = 0; i < 11; i++)
                        {
                            temporal.Y -= 1;
                            Update();
                            Thread.Sleep(delay);

                        }
                        break;
                    }
            }
            int savepos = temporal.SpawnPos;
            Car help;
            switch (savepos)
            {
                case 1:
                    {
                        help = new Car(20, 30);
                        break;
                    }
                case 2:
                    {
                        help = new Car(35, 30);
                        break;
                    }
                case 3:
                    {
                        help = new Car(50, 30);
                        break;

                    }
                case 4:
                    {
                        help = new Car(65, 30);
                        break;
                    }
                default:
                    help = null;
                    break;
            }
            help.SpawnPos = savepos;
            helper.Add(help);
            mover.Release();
            Random r = new Random();
            Thread.Sleep(r.Next(40000, 45000));
            freest[temporal.Assignedstation] = true;
            cars[savepos - 1] = new Car(help.X, help.Y);
            cars[savepos - 1].SpawnPos = savepos;
            for (int i = 0; i < helper.Count; i++)
            {
                if(helper[i].SpawnPos == savepos)
                {
                    helper.RemoveAt(i);
                }
            }
            Update();
            sem.Release();




        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            //List<int> ds = new List<int>() { 2, 3, 4 };
            //ds.rep
            //foreach (var item in ds)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.ReadLine();
            GasStation mnn = new GasStation();
            Console.WriteLine();
            Console.CursorVisible = false;
            




        }
        //public static void MoveIt(object obj)
        //{
        //    Car car = obj as Car;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        //car.X += 1;
        //        car.Y -= 1;
        //        Console.Clear();
        //        car.Draw(1);
                
        //        System.Threading.Thread.Sleep(100);
        //    }
        //}
        public static void Hello(object a)
        {

        }

    }

    

}
