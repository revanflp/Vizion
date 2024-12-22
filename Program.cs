/* 
Made by R E V A N on 04/07/2024 
If you bought this you got scammed :)
Have Fun
*/

using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.Drawing;

namespace Autotclick
{
    internal class Program
    {

        const string Vizion = @" 
     __      ___     _                _____ _ _      _             
     \ \    / (_)   (_)              / ____| (_)    | |            
      \ \  / / _ _____  ___  _ __   | |    | |_  ___| | _____ _ __ 
       \ \/ / | |_  / |/ _ \| '_ \  | |    | | |/ __| |/ / _ \ '__|
        \  /  | |/ /| | (_) | | | | | |____| | | (__|   <  __/ |   
         \/   |_/___|_|\___/|_| |_|  \_____|_|_|\___|_|\_\___|_|  
                                                                   ";


        /* ======== DLL IMPORT ======== */

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vkey);

        /* ======== DEFINE USEFUL VARIABLE ======== */

        const int LEFTUP = 0x0201;     
        const int LEFTDOWN = 0x0202;
        const int LeftClick = 0x01;
        const int StartKeybind = 0x04;
        const int intervals = 55;
        static bool status = false;

        /* ======== CLICKER FUNCTION ======== */

        static void menu()
        {
            Process[] process = Process.GetProcessesByName("javaw");

            if (process.Length == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(Vizion);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Launch minecraft before continue".PadLeft(55));
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Vizion);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Status : ".PadLeft(45));

            if (status)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write(status.ToString());
        }

        static void Clicker()
        {
            while (true)
            {
                Process[] process = Process.GetProcessesByName("javaw");

                if(process.Length == 0)
                {
                    Console.WriteLine("Launch minecraft before continue");
                    Console.ReadLine();
                }

                Process javaw = process[0];


                IntPtr javawMain = javaw.MainWindowHandle;

                while ((GetAsyncKeyState(LeftClick) & 0x8000) != 0 & status != false)
                {
                    SendMessage(javawMain, LEFTUP, IntPtr.Zero, IntPtr.Zero);
                    Thread.Sleep(5);
                    SendMessage(javawMain, LEFTDOWN, IntPtr.Zero, IntPtr.Zero);
                    Thread.Sleep(intervals);
                }

            }
        }

        /* ======== MAIN FUNCTION ======== */

        static void Main(string[] args)
        {

            Task backgroundTask = Task.Run( () =>
            {
                while (true)
                {
                    if ((GetAsyncKeyState(StartKeybind) & 0x8000) != 0)
                    {
                        status = !status;
                        //Console.WriteLine(status);
                        Thread.Sleep(150);
                        menu();
                    }
 
                }
            });

            menu();
            Clicker();

        }

    }
}
