using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using Newtonsoft.Json;

namespace labOop2
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Field
    {
        [JsonProperty]
        public int Height;
        [JsonProperty]
        public int Width;
        public Element[,] gameField;
        [JsonProperty]
        public string[,] field =  { { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" },
                                    { "#", " ", "#", " ", " ", " ", " ", " ", " ", "%", " ", " ", " ", " ", " ", "@", "#", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", " ", "I", "T", " ", " ", "M", " ", " ", " ", " ", " ", "%", " ", " ", " ", " ", " ", " ", "*", " ", " ", " ", " ", "#" },
                                    { "#", "O", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "%", " ", " ", " ", " ", " ", " ", " ", " ", "#", "#" },
                                    { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T", " ", "#" },
                                    { "#", " ", " ", " ", " ", " ", " ", " ", " ", "@", "#", " ", "%", " ", " ", " ", "#", " ", " ", " ", " ", "M", " ", "@", "#" },
                                    { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", " ", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", " ", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "@", " ", " ", " ", " ", " ", "%", " ", " ", " ", "#" },
                                    { "#", " ", " ", " ", "#", " ", " ", " ", "%", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", " ", " ", "@", "#", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", "%", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", "#", " ", "%", " ", "#", " ", " ", " ", " ", " ", "#", " ", "#" },
                                    { "#", " ", "#", " ", " ", " ", " ", "@", " ", " ", "%", " ", "*", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", " ", " ", " ", " ", " ", "#", "#", "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" },
                                    { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", " ", "@", " ", " ", " ", " ", " ", "#" },
                                    { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" },
                                    };
        public Element this[int i, int j]{
            get
            {
                return gameField[i, j];
            }
        }
        public void FillInGameField()
        {
            for (int n = 0; n < Height; n++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (field[n, j] == "T")
                        gameField[n, j] = new Teleport(n, j);
                    else if (field[n, j] == "#")
                        gameField[n, j] = new BreakPoint(n, j);
                    else if (field[n, j] == "@")
                    {
                        gameField[n, j] = new Prize(n, j);
                        Prizes++;
                    }                       
                    else if (field[n, j] == " ")
                        gameField[n, j] = new EmptyCell(n, j);
                    else if (field[n, j] == "%")
                        gameField[n, j] = new Trap(n, j);
                    else if (field[n, j] == "M")
                        gameField[n, j] = new MedHelp(n, j);
                    else if (field[n, j] == "*")
                        gameField[n, j] = new Death(n, j);
                    else if (field[n, j] == "O")
                        gameField[n, j] = new Enemy(n, j);
                    else if (field[n, j] == "I")
                    {
                        gameField[n, j] = player; 
                    }
                        
                }
            }
        }

        public Field() { }

        public Field(int h, int w)
        {
            Width = w;
            Height = h;
            gameField = new Element[h, w];
        }

        public void Draw()
        {
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    Element v = gameField[i, j];
                    Console.ForegroundColor = v.Color;
                    Console.SetCursorPosition(v.Y, v.X);
                    Console.WriteLine(v.Icon);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void DrawCell(int x, int y)
        {
            Element v = gameField[y, x] as Element;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = v.Color;
            Console.WriteLine(v.Icon);
            
        }

        [JsonProperty]
        public Player player { get; set; }
        public int Prizes { get; set; } = 0;

        public void MinusPrizes()
        {
            Prizes--;
        }

        public bool StopGame()
        {
            if (Prizes == 0)
            {
                return true;
            }
            return false;
        }

        public bool FailGame()
        {
            if (player.Lives == 0)
            {
                return true;
            }
            return false;
        }
        //сохраняем длину/ширину, игрока, призы
        public void Save()
        {
            FieldToArray();
            JsonSerializer serializer = new JsonSerializer();
            string jsonStr = JsonConvert.SerializeObject(this);
            string writePath = @"C:\Users\katri\OneDrive\Рабочий стол\labOop2\info.txt";            
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(jsonStr);
            }           
        }
        public void FieldToArray()
        {
            for (int n = 0; n < Height; n++)
            {
                for (int j = 0; j < Width; j++)
                {
                    field[n, j] = gameField[n, j].Icon;
                }
            }
        }
        public static Field Download()
        {
            JsonSerializer serializer = new JsonSerializer();
            string readPath = @"C:\Users\katri\OneDrive\Рабочий стол\labOop2\info.txt";
            string str = "";
            using (StreamReader sr = new StreamReader(readPath))
            {
               str = sr.ReadToEnd();
            }
            Field f = JsonConvert.DeserializeObject<Field>(str);
            f.gameField = new Element[f.Height, f.Width];
            f.FillInGameField();
            return f;
        }

        //public void GenerateTeleport()
        //{
        //    int x = 2, y = 2;
        //    int x1 = Width - 2, y1 = Height - 2;
        //    gameField[x, y] = new Teleport(x, y);
        //    gameField[x1, y1] = new Teleport(x1, y1);
        //}
        public void GeneratePlayer(int x, int y, int l)
        {
            player = new Player(x, y, l);
            gameField[player.Y, player.X] = player;
        }

        public void MoveEnemys()
        {
            int index = 1;
            int count = 0;
            while (true)
            {
                Console.CursorVisible = false;
                foreach (var c in gameField)
                {
                    if(c is Enemy)
                    {
                        int X = c.Y;
                        int Y = c.X;
                        c.X += index;
                        if(gameField[c.X, c.Y] is Player)
                        {
                            player.NullLives();                           
                            break;
                        }
                        gameField[c.X, c.Y] = c;
                        gameField[Y, X] = new EmptyCell(X, Y);
                        DrawCell(c.Y, c.X);
                        DrawCell(X, Y);
                        break;
                    }
                }
                count += index; 
                if(count == 4 || count == 0)
                {
                    index *= -1;
                }
                Thread.Sleep(300);
            }
        }

        public string Report()
        {
           return ($" У вас осталось {player.Lives} жизней. Осталось собрать  {Prizes} призов");
        }
        public void FailReport(string name, int steps)
        {
            string connectionString = @"Data Source = DESKTOP-OQ106UV\SQLEXPRESS;Initial Catalog=Lab1;Integrated Security=True";
            Console.Clear();
            Console.WriteLine("Вы проиграли - закончились жизни!");
            Console.WriteLine("");
            Console.WriteLine("Статистика игр: ");
            string insert = String.Format("INSERT INTO Games (Name, Steps, State) VALUES ('{0}', {1}, '{2}')", name, steps, "Fail");
            string select = "Select * from Games";
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
        public bool MovePlayer(int x, int y)
        {
            int X = player.X;
            int Y = player.Y;
            if (gameField[player.Y + y, player.X + x] is EmptyCell)
            {
                player.Move(x, y);
                gameField[player.Y, player.X] = player;
                gameField[Y, X] = new EmptyCell(X,Y);
                return true;
            }
            else if (gameField[player.Y + y, player.X + x].GetType() == typeof(Trap))
            {

                player.Move(x, y);
                player.MinusLives();
                gameField[player.Y, player.X] = player;
                gameField[Y, X] = new EmptyCell(X, Y);
            }
            else if (gameField[player.Y + y, player.X + x].GetType() == typeof(Enemy))
            {

                player.Move(x, y);
                player.NullLives();
                gameField[player.Y, player.X] = player;
                gameField[Y, X] = new EmptyCell(X, Y);
            }
            else if (gameField[Y + y, X + x].GetType() == typeof(Prize))
            {

                player.Move(x, y);
                gameField[player.Y, player.X] = player;
                gameField[Y, X] = new EmptyCell(X, Y);
                MinusPrizes();
                return false;
            }
            else if (gameField[Y + y, X + x].GetType() == typeof(MedHelp))
            {

                player.Move(x, y);
                gameField[player.Y, player.X] = player;
                player.PlusLives();
                gameField[Y, X] = new EmptyCell(X, Y);
                return false;
            }
            else if (gameField[Y+y, X + x].GetType() == typeof(Death))
            {

                player.Move(x, y);
                gameField[player.Y, player.X] = player;
                player.NullLives();
                gameField[Y, X] = new EmptyCell(X, Y);
                return false;
            }
            else if (gameField[Y + y, X + x].GetType() == typeof(Teleport))
            {

                if (player.X + x == 3 && player.Y + y == 2)
                {
                    player.Teleport(Width - 3, 4);
                    player.Move(x, y);
                    gameField[player.Y, player.X] = player;

                }
                else if (player.Y + y == 4 && player.X + x == Width - 3)
                {
                    player.Teleport(3, 2);
                    player.Move(x, y);
                    gameField[player.Y, player.X] = player;

                }
                gameField[Y, X] = new EmptyCell(X, Y);
                return true;
            }
            else if (gameField[Y + y, X + x].GetType() == typeof(BreakPoint))
            {
                return false;
            }
            return false;
        }
    }
}
    