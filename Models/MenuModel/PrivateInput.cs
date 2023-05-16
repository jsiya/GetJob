namespace GetJob.Models.MenuModel;

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
            ConsoleKeyInfo key = Console.ReadKey(true);   
            //Eger silmirse ve tesdiqlemirse gelen key tempstringe elave olunsun ve ekrana bir * cixsin
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                tempString += key.KeyChar;   
                Console.Write("*");
            }
            else
            {
                //eger backspace edibse ve daxil edilen sifre 0dan uzundusa tempstringi substring edirik axirinci elementden bir evvelkiye kimi
                if (key.Key == ConsoleKey.Backspace && tempString.Length > 0)
                { 
                    tempString = tempString.Substring(0, (tempString.Length - 1));       
                    //iki backspace cunki silende de bir element elave olur console iki defe geri qayitsin deye
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
