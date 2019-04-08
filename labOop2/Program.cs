using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace labOop2
{
    class Program
    {
        static void Report1(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.WriteLine($"Призы - @\nЛовушки  - %\nТочки остановки - #\nТелепорт-Т\nМед помощь-M\nБомбочка(полностью забирает ваши жизни)-*");
        }
        static void Timer()
        {
            for (int a = 180; a >= 0; a--)
            {
                Console.SetCursorPosition(1, 16);
                Console.Write("\rУ вас осталось {0:00}", a);

                System.Threading.Thread.Sleep(1000);
                if (a == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Время вышло, вы проиграли");

                }
            }

        }
        static bool CheckForSave()
        {
            string str = "";
            string readPath = @"C:\Users\katri\OneDrive\Рабочий стол\labOop2\info.txt";
            using (StreamReader sr = new StreamReader(readPath))
            {
                str = sr.ReadToEnd();
            }
            return str == "";
        }
        static void Main(string[] args)
        {

            int choice = 0;
            Console.Write("Введите свое имя: ");
            string name = Console.ReadLine();
            Console.WriteLine($"Привет {name}\nДобро пожаловать в игру Inertia.\nСоберите все призы и переходите на новый уровень\n");
            if (!CheckForSave())
            {
                Console.WriteLine("Загрузить последнюю сохраненную игру? (0/1)");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
            Console.ReadKey();
            int steps = 0;
            bool lives = true;
            Field field = new Field();
            if(choice == 1)
            {
                field = Field.Download();
            }
            else
            {
                field = new Field(15, 25);
                field.player = new Player(2, 2, 3);
                field.FillInGameField();
            }
            
            string connectionString = @"Data Source = DESKTOP-OQ106UV\SQLEXPRESS;Initial Catalog=Lab1;Integrated Security=True";
            while (lives)
            {
                Console.Clear();         
                field.Draw();
                Thread myThread = new Thread(new ThreadStart(field.MoveEnemys));
                //Thread myThread = new Thread(new ThreadStart(Timer));               
                int count = 0;
                ConsoleKeyInfo keyinfo;
                while (!field.StopGame())
                {
                    Console.ResetColor();                   
                    Console.SetCursorPosition(3, field.Height + 3);
                    Report1(field.Height + 7);
                    Console.WriteLine(field.Report());
                    if (!myThread.IsAlive)
                    {
                        myThread.Start();
                    }                  
                    keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.A)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(-1, 0);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.D)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(1, 0);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.W)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(0, -1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.S)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(0, 1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.Q)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(-1, -1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.E)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(1, -1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.Z)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(-1, 1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell( x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.X)
                    {
                        count++;
                        bool go = true;
                        while (go)
                        {
                            int x = field.player.X;
                            int y = field.player.Y;
                            go = field.MovePlayer(1, 1);
                            field.DrawCell(field.player.X, field.player.Y);
                            field.DrawCell(x, y);
                            System.Threading.Thread.Sleep(60);
                        }

                    }
                    else if (keyinfo.Key == ConsoleKey.O)
                    {
                        field.Save();

                    }
                    Console.SetCursorPosition(44, 10);
                    Console.Write($"Вы совершили шагов:{ count}");
                    if (field.FailGame())
                    {
                        lives = false;
                        break;
                    }

                }

                Console.ResetColor();
                myThread.Abort();
                steps += count;

            }
            if (lives == false)
            {
                field.FailReport(name, steps);
            }
            else
            {

                Console.Clear();
                Console.WriteLine("Ура, Вы победили!");
                Console.WriteLine("");
                Console.WriteLine("Статистика игр: ");
                string insert = String.Format("INSERT INTO Games (Name, Steps, State) VALUES ('{0}', {1}, '{2}')", name, steps, "Success");
                string select = "Select * from Games ORDER BY State DESC, Steps";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insert, connection);//создаем объект комманда
                    //выполняем
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand(select, connection);
                    SqlDataReader reader = command1.ExecuteReader();//читаем данные

                    while (reader.Read())
                    {
                        object login = reader["Name"];
                        object stp = reader["Steps"];
                        object state = reader["State"];
                        Console.WriteLine("Name: {0} \t Steps: {1} \t State: {2}", login, stp, state);
                    }
                    connection.Close();
                }


                Console.ReadLine();
            }

        }


    }


}
