﻿namespace GetJob.Models.MenuModel;

public class PrivateInput
{

    public string privateString = "";

    public string GetPrivateString()
    {
        return privateString;
    }

    public void InputPrivately()
    {
        string tempString = "";
        do
        {
            // reads key presses from input
            ConsoleKeyInfo key = Console.ReadKey(true);

            // check for backspace or enter key
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {

                // add character to temporary string
                tempString += key.KeyChar;

                //write a * for the character for secrecy
                Console.Write("*");
            }
            else
            {
                // backspace is pressed and the string has characters in it
                if (key.Key == ConsoleKey.Backspace && tempString.Length > 0)
                {
                    // reduce the temporary string by one character each time bs is pressed
                    tempString = tempString.Substring(0, (tempString.Length - 1));

                    // removes a '*' from the end of the output
                    Console.Write("\b \b");

                }
                else if (key.Key == ConsoleKey.Enter) { break; }
            }
        } while (true);

        privateString = tempString;
        tempString = null;
        Console.WriteLine();

        return;
    }
    public override string ToString()
    {
        string pass = "";
        foreach (var item in privateString)
        {
            pass += "*";
        }
        return pass;
    }
}
