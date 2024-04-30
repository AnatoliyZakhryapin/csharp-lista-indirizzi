using System.IO;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace csharp_lista_indirizzi
{
    internal class Program
    {
        public class ValueEqualNull: Exception
        {
        }

        public class StringLengthExceededException : Exception
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

                        string name = "Nome non trovato", surname = "Cognome non trovato", street = "", city = "", province = "", zipCode = "";

                        i++;
                        if (i <= 1)
                            continue;

                        try
                        {
                            var lineData = line.Split(',');
                
                            name = GetName(lineData);
                            surname = GetSurname(lineData);
                            street = GetStreet(lineData);
                            city = GetCity(lineData);
                            province = GetProvince(lineData);
                            zipCode = GetZipCode(lineData);    
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Errore nella riga del file: " + line);
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                        }
                        finally
                        {
                            User user = new User(new Person(name, surname), new Address(street, city, province, zipCode));
                            users.Add(user);
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

        public static string GetZipCode(string[] array)
        {
            string zipCode;
            try
            {
                int zipCodeIndex = FindIndexOfZipCode(array);
                if (zipCodeIndex == -1)
                    throw new ValueEqualNull();
                return zipCode = array[zipCodeIndex].ToString().PadLeft(array[zipCodeIndex].Trim().Length, '0'); ;
            }
            catch (ValueEqualNull)
            {
                Console.WriteLine("Zip non trovato assegnamo 0");
                return zipCode = "00000";
            }
        }

        public static string GetProvince(string[] array)
        {
            string province;
            try
            {
                int provinceIndex = FindIndexOfProvince(array);
                if (provinceIndex == -1)
                    throw new ValueEqualNull();
                return province = array[provinceIndex];
            }
            catch (ValueEqualNull)
            {
                Console.WriteLine("Province non trovata assegnamo AA");
                return province = "AA";
            }
        }
        public static int FindIndexOfZipCode(string[] array)
        {
            int i = 0;
            foreach (string item in array)
            {
                if (int.TryParse(item, out int result))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static int FindIndexOfProvince(string[] array)
        {
            int i = 0;
            foreach (string item in array)
            {
                if (item.Trim().Length == 2)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static string GetCity(string[] array)
        {
            string city;

            try
            {
                int streetIndex = FindIndexOfStreet(array);
                int provinceIndex = FindIndexOfProvince(array);
                int zipCodeIndex = FindIndexOfZipCode(array);

                switch (true)
                {
                    case true when (streetIndex != -1 && provinceIndex != -1) && (streetIndex + 2 == provinceIndex):
                        city = array[streetIndex + 1];
                        break;
                    case true when (streetIndex != -1 && zipCodeIndex != -1) && (streetIndex + 2 == zipCodeIndex):
                        city = array[streetIndex + 1];
                        break;
                    case true when streetIndex == -1 && provinceIndex != -1:
                        city = array[provinceIndex - 1];
                        break;
                    case true when streetIndex == -1 && provinceIndex == -1 && zipCodeIndex != -1:
                        city = array[zipCodeIndex - 1];
                        break;
                    default: throw new Exception("City not found");
                }
                return city;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                return city = "City non trovato";
            }
        }

        public static string GetStreet(string[] array)
        {
            string street;
            try
            {
                int streetIndex = FindIndexOfStreet(array);
                if (streetIndex == -1)
                    throw new ValueEqualNull();
                return street = array[streetIndex];
            }
            catch (ValueEqualNull)
            {
                Console.WriteLine("Indirizzo non trovato");
                return street = "Indirizzo non indicato";
            }
        }
        public static int FindIndexOfStreet(string[] array)
        {
            int i = 0;
            foreach (string item in array)
            {
                if (IsStreet(item))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        
        public static bool IsStreet(string input)
        {
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            bool HasNumbers = false;
            bool HasWords = false;

            foreach (string word in words)
            {
                if (word.Any(char.IsDigit))
                {
                    HasNumbers = true;
                }
                else if (word.Any(char.IsLetter))
                {
                    HasWords = true;
                }
            }

            return HasNumbers && HasWords;
        }
        public static string GetSurname(string[] array)
        {
            string surname = "Cognome non trovato"; ;

            try
            {   int arrayLength = array.Length;
                int streetIndex = FindIndexOfStreet(array);
                int provinceIndex = FindIndexOfProvince(array);
                int zipCodeIndex = FindIndexOfZipCode(array);

                int count = 0;

                if (zipCodeIndex != -1)
                    count++;

                if (provinceIndex != -1)
                    count++;

                if (GetCity(array) != "City not found")
                    count++;

                if (streetIndex != -1)
                    count++;

                int potentialSurnameCount = arrayLength - count;

                if (potentialSurnameCount >= 2)
                {
                    for (int i = potentialSurnameCount; i >= 0; i--)
                    {
                        if (!string.IsNullOrWhiteSpace(array[i - 1]))
                        {
                            surname = array[i - 1];
                            break;
                        }
                    }
                }
                else
                {
                    throw new ValueEqualNull();
                }


            }
            catch (ValueEqualNull)
            {
                Console.WriteLine("Cognome non trovato");
                return surname;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
            return surname;
        }
        public static string GetName(string[] array)
        {
            string name = "Nome non trovato";

            try
            {
                string surname = GetSurname(array);

                if (surname != "Cognome non trovato")
                {
                    int surnameIndex = Array.IndexOf(array, surname);

                    string[] nameArray = new string[surnameIndex];
                    Array.Copy(array, nameArray, surnameIndex);

                    name = string.Join(" ", nameArray);

                    if(name.Trim() == "")
                        throw new ValueEqualNull();
                } 
                else if (array[0].Trim() != "")
                {
                    return name = array[0];
                }
            }
            catch (ValueEqualNull)
            {
                Console.WriteLine("Nome non trovato");
                return name = "Nome non trovato";
            }
            catch (Exception e)
            {
                Console.WriteLine("Si è verificato un errore durante la ricerca del nome:");
                Console.WriteLine(e.Message);
            }

            return name;
        }   
    }
}
