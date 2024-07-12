//-----------------------------------------------------------------------------
// Developed by Piotrek4, Piotrek4Software is an unregistered trademark
// used for software distribution. All software created by me is free to use,
// modify, sell, and redistribute, provided this disclaimer is included.
//
// No rights reserved.
//
// This code is licensed under the DONT ASK license.
// For more information, please visit: https://piotrekunitydeveloper.github.io/dontask/
//-----------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
//using System.Windows.Forms;

namespace copytoclipboard
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_MINIMIZE = 2;
        const int SW_RESTORE = 9;

        [STAThread]
        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), SW_MINIMIZE);

            if (args.Length == 0)
            {
                ShowWindow(GetConsoleWindow(), SW_RESTORE);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong usage, no arguments provided. Please refer to documentation for help.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You can see below for an example of usage.");
                Console.WriteLine("");
                Console.WriteLine("copytoclipboard.exe [filepath] [action]");
                Console.WriteLine("");
                Console.WriteLine("[filepath] - the path of the target file/folder");
                Console.WriteLine("[action] - avaiable: '-fp' , '-fn' , '-fnext' ");
                Console.WriteLine("'-fp'      - copies the file's full path");
                Console.WriteLine("'-fn'      - copies the file's name (excluding the extension)");
                Console.WriteLine("'-fnext'   - copies the file's name (including the extension)");
                Console.ReadKey();
                return;
            }

            if (args.Length < 2)
            {
                ShowWindow(GetConsoleWindow(), SW_RESTORE);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong usage, too few arguments provided. Please refer to documentation for help.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You can see below for an example of usage.");
                Console.WriteLine("");
                Console.WriteLine("copytoclipboard.exe [filepath] [action]");
                Console.WriteLine("");
                Console.WriteLine("[filepath] - the path of the target file/folder");
                Console.WriteLine("[action] - avaiable: '-fp' , '-fn' , '-fnext' ");
                Console.WriteLine("'-fp'      - copies the file's full path");
                Console.WriteLine("'-fn'      - copies the file's name (excluding the extension)");
                Console.WriteLine("'-fnext'   - copies the file's name (including the extension)");
                Console.ReadKey();
                return;
            }

            if (args.Length > 2)
            {
                ShowWindow(GetConsoleWindow(), SW_RESTORE);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong usage, too many arguments provided. Please refer to documentation for help.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You can see below for an example of usage.");
                Console.WriteLine("");
                Console.WriteLine("copytoclipboard.exe [filepath] [action]");
                Console.WriteLine("");
                Console.WriteLine("[filepath] - the path of the target file/folder");
                Console.WriteLine("[action] - avaiable: '-fp' , '-fn' , '-fnext' ");
                Console.WriteLine("'-fp'      - copies the file's full path");
                Console.WriteLine("'-fn'      - copies the file's name (excluding the extension)");
                Console.WriteLine("'-fnext'   - copies the file's name (including the extension)");
                Console.ReadKey();
                return;
            }

            string filePath = args[0];
            string option = args[1];

            if (!File.Exists(filePath))
            {
                ShowWindow(GetConsoleWindow(), SW_RESTORE);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! - The specified file does not exist.");
                Console.ReadKey();
                return;
            }

            string textToCopy = string.Empty;

            switch (option)
            {
                case "-fp":
                    textToCopy = Path.GetFullPath(filePath);
                    break;

                case "-fn":
                    textToCopy = Path.GetFileNameWithoutExtension(filePath);
                    break;

                case "-fnext":
                    textToCopy = Path.GetFileName(filePath);
                    break;

                default:
                    ShowWindow(GetConsoleWindow(), SW_RESTORE);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong usage, invalid arguments provided. Please refer to documentation for help.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You can see below for an example of usage.");
                    Console.WriteLine("");
                    Console.WriteLine("copytoclipboard.exe [filepath] [action]");
                    Console.WriteLine("");
                    Console.WriteLine("[filepath] - the path of the target file/folder");
                    Console.WriteLine("[action] - avaiable: '-fp' , '-fn' , '-fnext' ");
                    Console.WriteLine("'-fp'      - copies the file's full path");
                    Console.WriteLine("'-fn'      - copies the file's name (excluding the extension)");
                    Console.WriteLine("'-fnext'   - copies the file's name (including the extension)");
                    Console.ReadKey();
                    return;
            }

            CopyToClipboard(textToCopy);
            Console.WriteLine($"Copied to clipboard: {textToCopy}");

            Application.Exit();

            //TODO move all the disclaimers and the console writeline shit to a method
        }

        static void CopyToClipboard(string text)
        {
            if (text == null)
            {
                ShowWindow(GetConsoleWindow(), SW_RESTORE);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! - The copied value is null." +
                    "");
                Console.ReadKey();
                return;
            }

            Clipboard.SetText(text);
        }
    }
}