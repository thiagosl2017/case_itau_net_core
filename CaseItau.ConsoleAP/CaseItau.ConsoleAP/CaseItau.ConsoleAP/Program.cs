using System;
using System.Threading.Tasks;

namespace CaseItau.ConsoleAP
{
    class Program
    {

        public static FundosRepository _fundos;
        public static FundosEntitie _fundosEntitie;

        static void Main(string[] args)
        {
            MontarCabeçalho();
            Resultado().Wait();
        }
        private static void MontarCabeçalho()
        {  
            Console.WriteLine("========================================================");
            Console.WriteLine("Case de Engenharia Itaú");
            Console.WriteLine("Esse console tem por objetivo consumir os endpoints:");
            Console.WriteLine("Obter Todos os Fundos - Digite a opção 1");
            Console.WriteLine("Obter Fundos por Código - Digite a opção 2");
            Console.WriteLine("Criar Fundo - Digite a opção 3");
            Console.WriteLine("Movimentar Patrimônio - Digite a opção 4");
            Console.WriteLine("Alterar Fundo - Digite a opção 5");
            Console.WriteLine("Excluir Fundo - Digite a opção 6");
            Console.WriteLine("Nome: Salatiel Luz Marinho");
            Console.WriteLine("========================================================");            
        }

        private static async Task Resultado()
        {
            _fundos = new FundosRepository();
            _fundosEntitie = new FundosEntitie();

            Console.Write("Qual endpoint deseja realizar o consumo: ");
            var opcao = Console.ReadLine();
            
            switch (opcao)
            {
                case "1":
                    var todosFundos = await _fundos.ObterTodosFundos();
                    foreach (var item in todosFundos)
                    {
                        Console.WriteLine("Código: " + item.Codigo);
                        Console.WriteLine("Nome: " + item.Nome);
                        Console.WriteLine("CNPJ: " + item.Cnpj);
                        Console.WriteLine("Código Tipo: " + item.CodigoTipo);
                        Console.WriteLine("Nome Tipo: " + item.NomeTipo);
                        Console.WriteLine("Patrimônio: " + item.Patrimonio);
                    }
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Informe por gentileza o código do fundo: ");
                    var codigo = Console.ReadLine();
                    var fundosCodigo = await _fundos.ObterFundoPorCodigo(codigo);

                    foreach (var item in fundosCodigo)
                    {
                        Console.WriteLine("Código: " + item.Codigo);
                        Console.WriteLine("Nome: " + item.Nome);
                        Console.WriteLine("CNPJ: " + item.Cnpj);
                        Console.WriteLine("Código Tipo: " + item.CodigoTipo);
                        Console.WriteLine("Nome Tipo: " + item.NomeTipo);
                        Console.WriteLine("Patrimônio: " + item.Patrimonio);
                    }
                    Console.ReadKey();
                    break;                    

                case "3":
                    Console.Write("Informe o codigo: ");
                    _fundosEntitie.Codigo = Console.ReadLine();
                    Console.Write("Informe o nome: ");
                    _fundosEntitie.Nome = Console.ReadLine();
                    Console.Write("Informe o cnpj: ");
                    _fundosEntitie.Cnpj = Console.ReadLine();                    
                    Console.Write("Informe o codigo tipo (1 - RENDA FIXA, 2 - ACOES, 3 - MULTI MERCADO): ");
                    _fundosEntitie.CodigoTipo = Convert.ToInt32(Console.ReadLine());
                    if(_fundosEntitie.CodigoTipo == 1)
                    {
                        _fundosEntitie.NomeTipo = "RENDA FIXA";
                    }
                    else if (_fundosEntitie.CodigoTipo == 2)
                    {
                        _fundosEntitie.NomeTipo = "ACOES";
                    }
                    else
                    {
                        _fundosEntitie.NomeTipo = "MULTI MERCADO";
                    }
                    Console.Write("Informe o valor patrimônio:");
                    _fundosEntitie.Patrimonio = Convert.ToDecimal(Console.ReadLine());

                    var fundoCriado = await _fundos.CriarFundo(_fundosEntitie);
                    Console.WriteLine(fundoCriado);
                    Console.ReadKey();
                    break;

                case "4":                    
                    Console.Write("Informe o codigo: ");
                    _fundosEntitie.Codigo = Console.ReadLine();
                    Console.Write("Informe o valor patrimônio:");
                    _fundosEntitie.Patrimonio = Convert.ToDecimal(Console.ReadLine());
                    var patrimonioMovimentado = await _fundos.MovimentarFundo(_fundosEntitie.Codigo, _fundosEntitie.Patrimonio);
                    Console.WriteLine(patrimonioMovimentado);
                    Console.ReadKey();
                    break;

                case "5":
                    Console.Write("Informe o nome: ");
                    _fundosEntitie.Nome = Console.ReadLine();
                    Console.Write("Informe o cnpj: ");
                    _fundosEntitie.Cnpj = Console.ReadLine();
                    Console.Write("Informe o codigo tipo (1 ou 2): ");
                    _fundosEntitie.Cnpj = Console.ReadLine();
                    Console.Write("Informe o valor patrimônio:");
                    _fundosEntitie.Patrimonio = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Informe o codigo tipo (1 - RENDA FIXA, 2 - ACOES, 3 - MULTI MERCADO): ");
                    _fundosEntitie.CodigoTipo = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Informe o codigo: ");
                    _fundosEntitie.Codigo = Console.ReadLine();
                    var fundoAlterado = await _fundos.AlterarFundo(_fundosEntitie.Codigo, _fundosEntitie);
                    Console.WriteLine(fundoAlterado);
                    Console.ReadKey();
                    break;

                case "6":
                    Console.Write("Informe o codigo: ");
                    _fundosEntitie.Codigo = Console.ReadLine();
                    var fundoExcluido = await _fundos.ExcluirFundo(_fundosEntitie.Codigo);
                    Console.WriteLine(fundoExcluido);
                    Console.ReadKey();
                    break;                

                default:
                    Console.WriteLine("Favor Digitar uma opção válida");
                    break;
            }
        }
    }
}
