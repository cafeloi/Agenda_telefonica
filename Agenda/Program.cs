using System;
using System.IO;

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
        Menu();
    }

    static void Menu()
    {
        string path = @"C:\Agenda_telefonica\.gitignore\Lista.Database";
        string lastIdPath = @"C:\Agenda_telefonica\.gitignore\id.management";
        int lastId = LerUltimoId(lastIdPath);

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string[] tela = {
                "========================================",
                "|       AGENDA TELEFÔNICA DIGITAL       |",
                "========================================",
                "|  1 - Cadastrar novos contatos        |",
                "|  2 - Editar a lista de contatos      |",
                "|  3 - Visualizar a lista de contatos  |",
                "|  4 - Organizar contatos A-Z          |",
                "|  5 - Pesquisar por nome              |",
                "|  6 - Pesquisar por ID                |",
                "|  7 - Sair                            |",
                "========================================",
                "       Escolha uma opção para continuar"
            };

            foreach (var linha in tela)
            {
                Console.WriteLine(linha);
            }

            Console.ResetColor();
            Console.Write("\nDigite sua escolha: ");
            if (!short.TryParse(Console.ReadLine(), out short opcao))
            {
                Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                continue;
            }

            switch (opcao)
            {
                case 1: Cadastro(path, lastIdPath, ref lastId); break;
                case 3: MostrarLista(path); break;
                case 5: PesquisarNome(path); break;
                case 7: Console.WriteLine("Saindo do programa... Até mais!"); return;
                default: Console.WriteLine("Opção inválida!"); break;
            }
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
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }

    }

    static void AtualizarUltimoId(string lastIdPath, int lastId)
    {


        File.WriteAllText(lastIdPath, lastId.ToString());


    }

    static void Cadastro(string path, string lastIdPath, ref int lastId)
    {
        Console.Clear();
        Console.WriteLine("== CADASTRO DE NOVO CONTATO ==");

        Dados Novo_contato = new Dados();
        Console.Write("Digite o nome do contato: ");
        Novo_contato.Nome = Console.ReadLine();
        Console.Write("Digite o número de telefone: ");
        Novo_contato.Telefone = Console.ReadLine();
        Console.Write("Digite o e-mail: ");
        Novo_contato.E_mail = Console.ReadLine();
        Console.Write("Digite a data de aniversário: ");
        Novo_contato.Aniversario = Console.ReadLine();

        Novo_contato.Id = ++lastId;



        using (TextWriter tsw = new StreamWriter(path, true))
        {
            tsw.WriteLine($"{Novo_contato.Id}|{Novo_contato.Nome}|{Novo_contato.Telefone}|{Novo_contato.E_mail}|{Novo_contato.Aniversario}");
        }
        AtualizarUltimoId(lastIdPath, lastId);
        Console.WriteLine("\nContato cadastrado com sucesso!");

    }

    static void MostrarLista(string path)
    {


        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Console.WriteLine(text);
        }
        else
        {
            Console.WriteLine("Nenhum contato encontrado!");
        }
    }
    static void PesquisarNome(string path)
    {
        int linha;
        Console.Clear();
        Console.WriteLine("Digite o nome do contato");
        string Localizar = Console.ReadLine();
        using (StreamReader sr = new StreamReader(path))
        {
            string linhaAtual;
            int numeroLinha = 0;
            bool encontrado = false;

            while ((linhaAtual = sr.ReadLine()) != null)
            {
                numeroLinha++;

                // Verifica se o termo está na linha atual
                if (linhaAtual.Contains(Localizar, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Contato encontrado aqui estão suas informarçoes");
                    Console.WriteLine(linhaAtual);
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Contato não encontrado.");
            }
            Menu();
        }


        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();



    }

    static void PesquisarID()
    {

    }
}
