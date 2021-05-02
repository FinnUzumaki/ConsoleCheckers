using System;

/*
Tem um erro nesse trabalho, as variaveis não são resetadas ao usar voltar ao menu
então todas as mudanças continuam após voltar ao jogo
por exemplo, no jogo de damas as peças continuam nos mesmos lugares da ultima jogada
na sequencia de fibonacci os numeros continuam da ultima sequencia
*/

namespace Trabalho_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Damas d = new Damas();
            Forca f = new Forca();
            Fibonacci fcc = new Fibonacci();

            int selected = -1;
            string input;

            //loop do menu
            while (selected != 0)
            {
                //limpa a tela
                Console.Clear();

                Console.WriteLine("Digite o que gostaria de fazer.\nDentro dos modos digite sair para voltar ao menu. \n1 - Sequencia de fibonacci \n2 - Damas \n3 - Forca \n0 - Fechar");

                //pega a escolha do usuário
                do
                {
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out selected));
                
                //envia o usuário para o escolhido
                switch(selected)
                {
                    case 1: fcc.Start();
                        break;
                    case 2: d.Start();
                        break;
                    case 3: f.Start();
                        break;
                    case 0:
                    Console.WriteLine("Aperte enter para fechar.");
                    Console.ReadLine();
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
