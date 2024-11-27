using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArabaYarisi
{
    class Program
    {
        static int oyuncuKonumu = 5; 
        static int yolGenisligi = 10;
        static bool oyunDevam = true;
        static Random rastgele = new Random();
        static int[,] yol = new int[20, 12]; 

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread oyunThread = new Thread(OyunBaslat);
            oyunThread.Start();

            while (oyunDevam)
            {
                var tus = Console.ReadKey(true).Key;
                if (tus == ConsoleKey.LeftArrow && oyuncuKonumu > 1)
                {
                    oyuncuKonumu--;
                }
                else if (tus == ConsoleKey.RightArrow && oyuncuKonumu < yolGenisligi)
                {
                    oyuncuKonumu++;
                }
            }

            oyunThread.Join();
            Console.Clear();
            Console.WriteLine("Oyun Bitti! Çarptınız!");
        }

        static void OyunBaslat()
        {
            int skor = 0;

            while (oyunDevam)
            {
                Console.Clear();

                for (int i = 18; i >= 0; i--)
                {
                    for (int j = 1; j <= yolGenisligi; j++)
                    {
                        yol[i + 1, j] = yol[i, j];
                    }
                }

                for (int j = 1; j <= yolGenisligi; j++)
                {
                    yol[0, j] = rastgele.Next(0, 10) == 0 ? 1 : 0;
                }

                for (int i = 0; i < 20; i++)
                {
                    Console.Write("|");
                    for (int j = 1; j <= yolGenisligi; j++)
                    {
                        if (i == 19 && j == oyuncuKonumu)
                        {
                            Console.Write("A"); 
                            if (yol[i, j] == 1)
                            {
                                oyunDevam = false; 
                            }
                        }
                        else if (yol[i, j] == 1)
                        {
                            Console.Write("X"); 
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine("|");
                }

                skor++;
                Console.WriteLine($"Skor: {skor}");
                Thread.Sleep(100); 
            }
        }
    }
}
