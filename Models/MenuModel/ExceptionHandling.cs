using GetJob.Models.DB;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GetJob.Models.MenuModel;

public static class ExceptionHandling
{
    public static bool ForName(ref string name)
    {
        try
        {
            //Console.ForegroundColor = ConsoleColor.White;
            string pattern = @"^[\p{L}'\-]+$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(name)) throw new Exception("Name must contain only letters and start with uppercase!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 12);
            Console.WriteLine(ex.Message);
            name = "";
            Thread.Sleep(2000);
            return false;
        }
        return true;
    }

    public static bool ForSurname(ref string surname)
    {
        try
        {
            //Console.ForegroundColor = ConsoleColor.White;
            string pattern = @"^[\p{L}'\-]+$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(surname)) throw new Exception("Surname must contain only letters and start with uppercase!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 13);
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            surname = "";
            return false;
        }
        return true;
    }
    public static bool ForMail(ref string email)
    {
        try
        {
            //Console.ForegroundColor = ConsoleColor.White;
            var mail = new MailAddress(email);
            bool isValidEmail = mail.Host.Contains(".");
            if (!isValidEmail)
                throw new Exception("Invalid mail");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 15);
            Console.Write(ex.Message);
            email = "";
            return false;
        }
        return true;
    }
    public static bool ForPassword(string password)
    {
        try
        {
            //en az 8 simvol, en az bir kicik herf, boyuk herf, reqem ve simvol olmalidir!
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(password))
                throw new Exception("Password must contain at least eight characters, including at least one number and includes both lower and uppercase letters and special characters!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 18);
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            return false;
        }
        return true;
    }
    public static bool ForUsername(ref string username, Database db)
    {
        try
        {
            //En az 7 sumvoldan ibaret olub, herfle baslamalidir, boyuk, kicik, reqemler ve _ ola biler
            string pattern = "^[A-Za-z][A-Za-z0-9_]{7,29}$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(username))
                throw new Exception("Username must be at least 7 character, should start with alphabet,and can contain uppercase, lowercase, numbers, underscore(_) ! ");
            foreach (var item in db.Employees)
            {
                if (item.Username == username) throw new Exception("This username already used!");
            }
            foreach (var item in db.Employers)
            {
                if (item.Username == username) throw new Exception("This username already used!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 17);
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            username = "";
            return false;
        }
       
        return true;
    }
    public static bool ForPhone(ref string phone)
    {
        try
        {
            //0501234567
            //+994501234567
            //0991234567
            //994991234567 formalarinda yazila biler
            string azPhonePattern = @"^(?:\+994|994|0)[1-9][0-9]{8}$";
            Regex rg = new Regex(azPhonePattern);
            if (!rg.IsMatch(phone))
                throw new Exception("Invalid phone number!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 16);
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            phone = "";
            return false;
        }
        return true;
    }
    public static bool ForCity(ref string city)
    {
        try
        {
            string pattern = @"^[a-zA-Z\u00C0-\u017F\s-]+$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(city))
                throw new Exception("Invalid City name!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            city = "";
            return false;
        }
        return true;
    }
    public static bool ForAge(ref string age)
    {
        //Console.ForegroundColor = ConsoleColor.White;
        try
        {
            int age_;
            string pattern = @"^(0?[1-9]|[1-9][0-9])$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(age)) throw new Exception("Invalid age format!");
            age_ = Int32.Parse(age);
            if (age_ < 18 || age_ > 130) throw new Exception("Age must be greater than 18 and smaller than 130!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 14);
            Console.Write(ex.Message);
            Thread.Sleep(2000);
            age = "";
            return false;
        }
        return true;
    }
}
