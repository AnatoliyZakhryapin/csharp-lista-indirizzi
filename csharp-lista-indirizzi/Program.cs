using System.IO;
using System.Xml.Linq;

namespace csharp_lista_indirizzi
{
    internal class Program
    {
        public class ArrayLongher6element : Exception
        {
        }
        public class ZipDontFund: Exception
        {
        }

        static void Main(string[] args)
        {

            //User newUser = new User(new Person("Anatoliy", "Zakhryapin"), new Address("Via Torino 29", "Milano", "Mi", 20123));

            //user.StampaInfo();

            string path = "D:\\Studi\\CSharp\\CorsoCSharp\\csharp-lista-indirizzi\\csharp-lista-indirizzi\\addresses.csv";

            List<User> users = ReadFromFileAndCreateListOfUsers(path);

            int i = 1;
            foreach (User user in  users)
            {
                Console.WriteLine();
                Console.WriteLine($"STAMPA UTENE : {i}");
                user.StampaInfo();
                i++;
            }
        }

        public static List<User> ReadFromFileAndCreateListOfUsers(string path)
        {
            List<User> users = new List<User>();

            try
            {
                using (StreamReader fileStream = File.OpenText(path))
                {
                    int i = 0;
                    while (!fileStream.EndOfStream)
                    {
                        string line = fileStream.ReadLine();


                        i++;
                        if (i <= 1)
                            continue;

                        try
                        {
                            var lineData = line.Split(',');

                            if (lineData.Length != 6)
                                throw new ArrayLongher6element();
                
                            string name = (lineData[0] == "") ? "Indefinite" : lineData[0];
                            string surname = (lineData[1] == "") ? "Indefinite" : lineData[1];
                            string street = (lineData[2] == "") ? "Indefinite" : lineData[2];
                            string city = (lineData[3] == "") ? "Indefinite" : lineData[3];
                            string province = (lineData[4] == "") ? "Indefinite" : lineData[4];

                            string zipCode = GetZipCode(lineData, lineData[5]);
                
                            User user = new User(new Person(name, surname), new Address(street, city, province, zipCode));
                            users.Add(user);
                        }
                        catch (ArrayLongher6element e)
                        {
                            Console.WriteLine("Lunghezza array diversa");
                            Console.WriteLine("Errore nella riga del file: " + line);
                            Console.WriteLine(e.Message);
                            Console.WriteLine();

                            var lineData = line.Split(',');

                            string name = (lineData[0] == "") ? "Indefinite" : lineData[0];
                            string surname = (lineData[1] == "") ? "Indefinite" : lineData[1];
                            string street = (lineData[2] == "") ? "Indefinite" : lineData[2];
                            string city = (lineData[3] == "") ? "Indefinite" : lineData[3];
                            string province = (lineData[4] == "") ? "Indefinite" : lineData[4];

                            string zipCode = GetZipCode(lineData, lineData[5]);

                            User user = new User(new Person(name, surname), new Address(street, city, province, zipCode));
                            users.Add(user);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Errore nella riga del file: " + line);
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante la lettura del file: " + ex.Message);
            }

            return users;
        }

        public static string GetZipCode(string[] array, string code)
        {
            string zipCode;
            try
            {
                int parsedZipCode = int.Parse(code);
                return zipCode = parsedZipCode.ToString().PadLeft(code.Trim().Length, '0');
            }
            catch (FormatException)
            {
                Console.WriteLine("Tipo di dato non coretto");
                try
                {
                    zipCode = FindFirstIntegerInStringArray(array);
                    if (zipCode == "null")
                        throw new ZipDontFund();
                    return zipCode;
                }
                catch (ZipDontFund)
                {
                    Console.WriteLine("Zip non trovato assegnamo 0");
                    return zipCode = "00000";
                }
            }
        }

        public static string FindFirstIntegerInStringArray(string[] array)
        {
            foreach (string item in array)
            {
                if (int.TryParse(item, out int result))
                {
                    return result.ToString().PadLeft(item.Trim().Length, '0');
                }
            }
            return "null";
        }
    }
}
