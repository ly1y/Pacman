using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Emit;
using System.Threading;

namespace Pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadFile("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            int pacmanX = 1;
            int pacmanY = 1;

            int score = 0;

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            while(true)
            {
                Console.Clear();
                HandleInput(pressedKey,ref pacmanX,ref pacmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Magenta;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write("@");


                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(50, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(1000);

            }
            Console.ReadKey();
        }

        private static char[,] ReadFile(string path)
        {
            string[] file = File.ReadAllLines("map.txt"); // переменная которая хранит файл

            char[,] map = new char[GetMaxLengthOfLines(file), file.Length]; //определяем двухмерный массив

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x,y]);
                }
                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo consoleKeyInfo, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(consoleKeyInfo);
            int nextPacmanPosX = pacmanX + direction[0];
            int nextPacmanPosY = pacmanY + direction[1];
            char nextCell = map[nextPacmanPosX, nextPacmanPosY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPosX;
                pacmanY = nextPacmanPosY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPosX, nextPacmanPosY] = ' ';
                }
            }

            
        }

        private static int[] GetDirection (ConsoleKeyInfo consoleKeyInfo)
        {
            int[] direction = { 0, 0 };

            if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                direction[1] = -1;
            else if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if(consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            else if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                direction[0] = 1;

            return direction;
        }
        private static int GetMaxLengthOfLines(string[] lines)
        {
            int maxLength = lines.Length;

            foreach (string line in lines)
            {
                if(line.Length > maxLength)
                    maxLength = line.Length;
            }
            return maxLength;
        }
    }
}
