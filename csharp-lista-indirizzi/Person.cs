using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lista_indirizzi
{
    internal class Person
    {
        private string nome;
        private string cognome;

        // -----------
        // Costruttore
        // -----------

        public Person(string nome, string cognome)
        {
            this.nome = nome;
            this.cognome = cognome;
        }

        // ---------
        // Get e Set
        // ----------

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }

        public string Cognome
        {
            get { return this.cognome; }
            set { this.cognome = value; }
        }

        // ---------
        // Metodi
        // ----------

        public void StampaInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Stampa informazione della persona: ");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Cognome: {cognome}");
        }
    }
}

