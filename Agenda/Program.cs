using System;
using System.IO;
using System.Linq;

internal class Program
{
    struct Dados
    {
        public Dados(int id, string nome, string telefone, string telefone_fixo, string e_mail, string aniversario, string endereco)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Telefone_fixo = telefone_fixo;
            E_mail = e_mail;
            Aniversario = aniversario;
            Endereco = endereco;
        }

        public int Id;
        public string Nome;
        public string Telefone;
        public string Telefone_fixo;
        public string E_mail;
        public string Aniversario;
        public string Endereco;
    }

    private static void Main(string[] args)
    {
        string path = @"C:\Agenda_telefonica\Lista.Database";
        string lastIdPath = @"C:\Agenda_telefonica\id.management";


        Dados Novo_contato = new Dados();
        int lastId = LerUltimoId(lastIdPath);
        Menu(path, lastIdPath, lastId, Novo_contato);
    }
    static void Menu(string path, string lastIdPath, int lastId, Dados Novo_contato = default)
    {

        Console.WriteLine("Digite o numero de acordo com opção que deseja escolher");
        Console.WriteLine("1-Cadastrar novos contatos");
        Console.WriteLine("2-Editar a lista de contatos");
        Console.WriteLine("3-Visualizar a lista de contatos");
        Console.WriteLine("4-Organizar a ordem dos contatos por ordem Crescente de A-Z");
        Console.WriteLine("5-Pesquisar por nome");
        Console.WriteLine("6-Pesquisar por indicador");
        Console.WriteLine("7-Sair do programa");
        short opcao = short.Parse(Console.ReadLine());
        switch (opcao)
        {
            case 1: Cadastro(path, lastIdPath, lastId, Novo_contato); break;
        }

    }
    static int LerUltimoId(string lastIdPath)
    {
        if (File.Exists(lastIdPath))
        {
            string content = File.ReadAllText(lastIdPath).Trim();
            if (int.TryParse(content, out int lastId))
            {
                return lastId;
            }
        }


        return 0;
    }
    static void AtualizarUltimoId(string lastIdPath, int lastId)
    {
        //
        File.WriteAllText(lastIdPath, lastId.ToString());
    }
    static void Cadastro(string path, string lastIdPath, int lastId, Dados Novo_contato = default)
    {
        Console.WriteLine("Digite o nome do contato que deseja cadastrar:");
        Novo_contato.Nome = Console.ReadLine();

        Console.WriteLine("Digite o número de telefone do contato que deseja cadastrar:");
        Novo_contato.Telefone = Console.ReadLine();

        Console.WriteLine("Digite o telefone fixo do contato que deseja cadastrar:");
        Novo_contato.Telefone_fixo = Console.ReadLine();

        Console.WriteLine("Digite o e-mail do contato que deseja cadastrar:");
        Novo_contato.E_mail = Console.ReadLine();

        Console.WriteLine("Digite a data de aniversário do contato que deseja cadastrar:");
        Novo_contato.Aniversario = Console.ReadLine();

        Console.WriteLine("Digite o endereço do contato que deseja cadastrar:");
        Novo_contato.Endereco = Console.ReadLine();


        Novo_contato.Id = lastId + 1;


        using (TextWriter tsw = new StreamWriter(path, true))
        {
            tsw.WriteLine("Nome do contato -> " + Novo_contato.Nome);
            tsw.WriteLine("Telefone do contato -> " + Novo_contato.Telefone);
            tsw.WriteLine("Telefone fixo do contato -> " + Novo_contato.Telefone_fixo);
            tsw.WriteLine("Email do contato -> " + Novo_contato.E_mail);
            tsw.WriteLine("Data de aniversário -> " + Novo_contato.Aniversario);
            tsw.WriteLine("Endereço do contato -> " + Novo_contato.Endereco);
            tsw.WriteLine("ID -> " + Novo_contato.Id);
            tsw.WriteLine("__________________");
        }
        AtualizarUltimoId(lastIdPath, Novo_contato.Id);
    }
}