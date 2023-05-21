using System.Linq;
using System.Xml;
using static System.Console;

namespace MenuModel;

public class Menu
{
    //private string _prompt = "";
    private readonly string[] _options;
    public  List<string> _menuList;
    private int _currentSelection;
    private int _drawMenuColumnPos;
    private readonly int _drawMenuRowPos;
    private int _menuMaximumWidth;

    public Menu(string[] options, int row, int col)
    {
        //_prompt = prompt;
        _menuList = options.ToList();
        _options = options;
        _currentSelection = 0;
        _drawMenuRowPos = row;
        _drawMenuColumnPos = col;

    }

    public int GetMaximumWidth()
    {
        return _menuMaximumWidth;
    }

    public void CenterMenuToConsole()
    {
        _drawMenuColumnPos = GetConsoleWindowWidth() / 2 - (_menuMaximumWidth / 2);
    }

    // Menunu sol terefe
    public void ModifyMenuLeftJustified()
    {
        int maximumWidth = 0;
        string space = "";

        foreach (var t in _menuList)
        {
            if (t.Length > maximumWidth)
            {
                maximumWidth = t.Length;
            }
        }

        maximumWidth += 6;

        for (int i = 0; i < _menuList.Count; i++)
        {
            int spacesToAdd = maximumWidth - _menuList[i].Length;
            for (int j = 0; j < spacesToAdd; j++)
            {
                space += " ";
            }
            _menuList[i] = _menuList[i] + space;
            space = "";
        }

        _menuMaximumWidth = maximumWidth;
    }

    // Menyunu ortalamaq
    public void ModifyMenuCentered()
    {
        int maximumWidth = 0;
        string space = "";

        foreach (var t in _menuList)
        {
            if (t.Length > maximumWidth)
            {
                maximumWidth = t.Length;
            }
        }

        maximumWidth += 6;  // menyunu dar ve enli elemek
        for (int i = 0; i < _menuList.Count; i++)
        {
            if (_menuList[i].Length % 2 != 0)
            {
                _menuList[i] += " ";  
            }

            var minimumWidth = maximumWidth - _menuList[i].Length;
            minimumWidth /= 2;
            for (int j = 0; j < minimumWidth; j++)
            {
                space += " ";
            }

            _menuList[i] = space + _menuList[i] + space;      // her iki terefe bosluq 
            space = ""; 
        }

        for (int i = 0; i < _menuList.Count; i++)
        {
            if (_menuList[i].Length < maximumWidth) 
            {
                _menuList[i] += " ";
            }
        }
        _menuMaximumWidth = maximumWidth;  
    }
    public void SetConsoleWindowSize(int width, int height)
    {
        WindowWidth = width;
        WindowHeight = height;
    }

    public static int GetConsoleWindowWidth()
    {
        return WindowWidth;
    }

    public void SetConsoleTextColor(ConsoleColor foreground, ConsoleColor background)
    {
        ForegroundColor = foreground;
        BackgroundColor = background;
    }

    public void ResetCursorVisible()
    {
        CursorVisible = CursorVisible != true;
    }

    public void SetCursorPosition(int row, int column)
    {
        if (row > 0 && row < WindowHeight)
        {
            CursorTop = row;
        }

        if (column > 0 && column < WindowWidth)
        {
            CursorLeft = column;
        }
    }


    // Menunu run eden
    public int RunMenu()
    {
        bool run = true;
        DrawMenu();
        while (run)
        {
            var keyPressedCode = CheckKeyPress();
            if (keyPressedCode == 10)   // yuxari
            {
                _currentSelection--;
                if (_currentSelection < 0)
                {
                    _currentSelection = _menuList.Count - 1;
                }
            }
            else if (keyPressedCode == 11)  // asagi
            {
                _currentSelection++;
                if (_currentSelection > _menuList.Count - 1)
                {
                    _currentSelection = 0;
                }
            }
            else if (keyPressedCode == 12)  // enter
            {
                run = false;
            }
            
            DrawMenu();
        }
        return _currentSelection;
    }

    private void DrawMenu()
    {
        //string leftPointer = "    ";
        //string rightPointer = "    ";
        int count = 0;
        for (int i = 0; i < _menuList.Count; i++)
        {
            SetCursorPosition(_drawMenuRowPos + i + count, _drawMenuColumnPos);
            SetConsoleTextColor(ConsoleColor.White, ConsoleColor.Black);
            if (i == _currentSelection)
            {
                SetConsoleTextColor(ConsoleColor.Black, ConsoleColor.White);
                //leftPointer = "  ► ";
                //rightPointer = " ◄  ";

            }
            Console.WriteLine(_menuList[i]);
            //Console.WriteLine(leftPointer + _options[i] + rightPointer);
            //leftPointer = "    ";
            //rightPointer = "    ";
            ResetColor();
            count = _menuList[i].Count(x => x == '\n');
        }
    }

    private int CheckKeyPress()
    {
        ConsoleKeyInfo keyInfo = ReadKey(true);
        do
        {
            ConsoleKey keyPressed = keyInfo.Key;
            if (keyPressed == ConsoleKey.UpArrow)
            {
                return 10;
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                return 11;
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                return 12;
            }
            else if (keyPressed == ConsoleKey.Q)
            {
                return 13;
            }
            else
            {
                return 0;
            }

        } while (true);
    }
}