using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Rastreador
{
    private Dictionary<string,string> _etiquetas = new Dictionary<string,string>();

    public void IniciarGerenciamento()
    {
        Console.WriteLine("----Gerenciamento de Etiquetas Iniciado----");

        int quantidadeDeEntradas = ObterQuantidade();

        Console.WriteLine($"\n inserção de {quantidadeDeEntradas} pacotes");

        for (int i = 0; i < quantidadeDeEntradas; i++)
        {
            Console.WriteLine($"\n Pacote {i + 1} de {quantidadeDeEntradas}");
            InserirPacote();
        }

        ExibirPacotesAtuais();

        Console.WriteLine("\n Realizar busca: ");
        RealizarBusca();
        
    }

    private int ObterQuantidade()
    {
        int quantidade;
        while(true)
        {
            Console.Write("Quantas informações (pares Rastreio/Código de Barras) você deseja inserir?: ");
            if (int.TryParse(Console.ReadLine(),out quantidade)&& quantidade>0)
            {
                return quantidade;
            }
            Console.WriteLine("Entrada inválida. Por favor, insira um número inteiro positivo");
        }
    }

    private void InserirPacote()
    {
        string codigoRastreio = ObterEntrada("Digite o Código de Rastreio (Chave): ");
        string codigoBarras = ObterEntrada("Digite o Código de Barras (Valor): ");

        if (_etiquetas.ContainsKey(codigoRastreio))
        {
            Console.WriteLine("O código de rastreio {codigoRastreio} já está cadastrada com o código de barras {_etiquetas[codigoRastreio]} ");
            Console.WriteLine("A inserção foi ignorada para evitar repetição.");
        }
        else if (_etiquetas.ContainsValue(codigoBarras))
        {
            var rastreioExistente = _etiquetas.First(kv => kv.Value == codigoBarras).Key;
            Console.WriteLine($"\n O Código de Barras '{codigoBarras}' já está cadastrado com o Código de Rastreio '{rastreioExistente}'.");
            Console.WriteLine("A inserção foi ignorada para evitar repetição.");
        }
        else
        {
            _etiquetas.Add(codigoRastreio, codigoBarras);
            Console.WriteLine($"\n Pacote cadastrado com sucesso! Rastreio: {codigoRastreio}, Barras: {codigoBarras}");
        }
    }

    private void RealizarBusca()
    {
        while(true)
        {
            string termoDeBusca = ObterEntrada("\n Digite o código de rastreio ou o código de barras para buscar(ou 'sair' para encerrar)");
            if (termoDeBusca.ToUpper() == "SAIR")
            {
                Console.WriteLine("\nBusca encerrada");
                break;
            }
            if (_etiquetas.ContainsKey(termoDeBusca))
            {
                string codigoBarras = _etiquetas[termoDeBusca];
                Console.WriteLine($"\n **Resultado da Busca por Rastreio:**");
                Console.WriteLine($"   Código de Rastreio: **{termoDeBusca}**");
                Console.WriteLine($"   Código de Barras: **{codigoBarras}**");
            }
            else if (_etiquetas.ContainsValue(termoDeBusca))
            {
                // Encontra a primeira chave (Rastreio) que corresponde ao valor (Código de Barras)
                var resultado = _etiquetas.First(kv => kv.Value == termoDeBusca);
                Console.WriteLine($"\n **Resultado da Busca por Código de Barras:**");
                Console.WriteLine($"   Código de Barras: **{termoDeBusca}**");
                Console.WriteLine($"   Código de Rastreio: **{resultado.Key}**");
            }
            else
            {
                Console.WriteLine($"\n **Nenhum resultado encontrado** para o termo '{termoDeBusca}'. Por favor, verifique se o código foi digitado corretamente.");
            }
        }
    }

    private string ObterEntrada(string prompt)
    {
        string entrada;
        do
        {
            Console.Write(prompt);
            entrada = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("A entrada não pode ser vazia.");
            }
        }while (string.IsNullOrWhiteSpace(entrada));
        return entrada;
    }

    private void ExibirPacotesAtuais()
    {
        Console.WriteLine("\n--- Pacotes Atualmente Cadastrados ---");
        if (_etiquetas.Count == 0)
        {
            Console.WriteLine("Nenhum pacote cadastrado");
            return;
        }

        Console.WriteLine($"Total de Pacotes: {_etiquetas.Count}");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("| Rastreio (Chave) | Código de Barras (Valor) |");
        Console.WriteLine("------------------------------------------");
    }

}

public class Program
{
    public static void Main(string[] args)
    {
        var gerenciador = new Rastreador();
        gerenciador.IniciarGerenciamento();
        
        // Mantém a janela do console aberta até que uma tecla seja pressionada
        Console.WriteLine("\nPressione qualquer tecla para fechar...");
        Console.ReadKey();
    }
}
