using System;
using System.IO;
using System.Xml;

internal class Program
{
    struct Dados
    {
        public Dados(int id, string nome, int telefone, string e_mail, string aniversario)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            E_mail = e_mail;
            Aniversario = aniversario;

        }

        public int Id;
        public string Nome;
        public int Telefone;

        public string E_mail;
        public string Aniversario;

    }
    private static void Main(string[] args)
    {
        Menu();
    }
    static void Menu()
    {
        string localID = "1";
        string path = @"C:\Agenda_telefonica\.gitignore\Lista.Database";
        string lastIdPath = @"C:\Agenda_telefonica\.gitignore\id.management";
        int lastId = LerUltimoId(lastIdPath);
        string linhaAtual = "";
        bool repetido = true;

        Console.ForegroundColor = ConsoleColor.Green;

        string[] tela = {
                "========================================",
                "|       AGENDA TELEFÔNICA DIGITAL       |",
                "========================================",
                "|  1 - Cadastrar novos contatos        |",
                "|  2 - Editar a lista de contatos      |",
                "|  3 - Visualizar a lista de contatos  |",
                "|  4 - Organizar contatos A-Z          |",
                "|  5 - Pesquisar contato               |",
                "|  7 - Sair                            |",
                "========================================",
                "   Escolha uma opção para continuar     "
            };

        foreach (var linha in tela)
        {
            Console.WriteLine(linha);
        }

        Console.ResetColor();
        Console.Write("\nDigite sua escolha: ");
        short opcao = short.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1: Cadastro(path, lastIdPath, ref lastId, linhaAtual, repetido); break;
            case 2: Editar(path, localID); break;
            case 3: MostrarLista(path); break;
            case 5: Pesquisar(path); break;
            case 7: Console.WriteLine("Saindo do programa... Até mais!"); return;
            default: Console.WriteLine("Opção inválida!"); Thread.Sleep(3000); Menu(); break;
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
    static void Cadastro(string path, string lastIdPath, ref int lastId, string linhaAtual, bool repetido)
    {
        string Converter = "o";
        repetido = true;
        Console.Clear();
        Console.WriteLine("== CADASTRO DE NOVO CONTATO ==");

        Dados Novo_contato = new Dados();
        Console.Write("Digite o nome do contato: ");
        Novo_contato.Nome = Console.ReadLine();
        Console.Write("Digite o número de telefone: ");
        while (repetido == true)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (((linhaAtual = sr.ReadLine()) != null) && repetido == true)
                {
                    while (1 > 0)
                    {
                        try
                        {
                            Novo_contato.Telefone = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("O telefone não pode conter letras nem caracteres especiais, tente novamente");
                        }
                    }
                    Converter = Convert.ToString(Novo_contato.Telefone);
                    if (linhaAtual.Contains(Converter, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Este numero ja esta sendo utilizado... tente novamente");

                    }
                    else
                    {
                        repetido = false;
                    }

                }
            }
        }

        Console.Write("Digite o e-mail: ");
        Novo_contato.E_mail = Console.ReadLine();
        Console.Write("Digite a data de aniversário: ");
        Novo_contato.Aniversario = Console.ReadLine();

        Novo_contato.Id = ++lastId;



        using (TextWriter tsw = new StreamWriter(path, true))
        {
            tsw.WriteLine($"{Novo_contato.Id}|{Novo_contato.Nome}|{Converter}|{Novo_contato.E_mail}|{Novo_contato.Aniversario}");
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
        Thread.Sleep(10000);
    }
    static void Pesquisar(string path)
    {
        int linha;
        Console.Clear();
        Console.WriteLine("Digite o nome do contato, telefone ou id");
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
    static void Editar(string path, string localID)
    {
        Console.Clear();
        string[] subtela = {
                "========================================",
                "|            MENU DE EDIÇÃO            |",
                "========================================",
                "|  1 - Excluir umm contato             |",
                "|  2 - Editar um contato               |",
                "|  3 - Sair do menu de edição          |",
                "========================================",
                "   Escolha uma opção para continuar     "
            };
        foreach (var linha in subtela)
        {
            Console.WriteLine(linha);
        }
        short opcao = short.Parse(Console.ReadLine());
        switch (opcao)
        {
            case 1: Excluircontato(out localID, path); break;
            case 2: Editar(path, localID); break;
            case 3: Menu(); break;
            case 4: Console.WriteLine(); break;
            default: Console.WriteLine("Opção invalida tente novamente"); Editar(path, localID); break;

        }
        static void LocalizarID(string path, string localID)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string linhaAtual;
                int numeroLinha = 0;
                bool encontrado = false;


                while ((linhaAtual = sr.ReadLine()) != null)
                {
                    numeroLinha++;


                    if (!string.IsNullOrEmpty(linhaAtual) && linhaAtual[0] == localID[0])
                    {
                        Console.WriteLine("esse é o id que deseja excluir?");
                        Console.WriteLine($"Linha {linhaAtual}");
                        encontrado = true;


                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine($"Nenhuma linha encontrada começando com o ID {localID}.");
                    Thread.Sleep(2000);
                    Editar(path, localID);
                }
            }

        }
        static void Excluircontato(out string localID, string path)
        {

            string newText = " ";

            Console.WriteLine("Digite o ID do contato que deseja excluir");
            localID = (Console.ReadLine());
            LocalizarID(path, localID);
            Console.WriteLine(@"Caso deseje Excluir esse contato digite /confirmar/ ");
            string confirmar = Console.ReadLine().ToLower();

            if (confirmar == "confirmar")
            {
                Mudar_linha(newText, path, localID);
            }
            Editar(path, localID);
        }
        static void EditarInfo(out string localID, string path)
        {
            Console.WriteLine("Digite o ID do contato que deseja editar");
            localID = (Console.ReadLine());
            LocalizarID(path, localID);
            Console.WriteLine(@"Caso deseje editar este contato digite /confirmar/ ");
            string confirmar = Console.ReadLine().ToLower();
            newText = "o";
            if (confirmar == "confirmar")
            {
                Mudar_linha(newText, path, localID);
            }
            Editar(path, localID);

        }
    }
    static void Mudar_linha(string newText, string path, string localID)
    {
        int line_edit = Int32.Parse(localID);
        string[] arrLine = File.ReadAllLines(path);
        arrLine[line_edit - 1] = newText;
        File.WriteAllLines(path, arrLine);
        Console.WriteLine("Contato Excluido com sucesso");
        Thread.Sleep(3000);
    }
}