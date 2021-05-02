using System;

namespace Trabalho_1
{
    class Fibonacci
    {
        int f = 1, f1 = 0, f2 = 0, limit, option;
        string input;
        public void Start()
        {
            f = 1;
            f1 = 0;
            f2 = 0;
            Sequence();
        }

        void Sequence()
        {
            Redo:
            Console.Clear();

            Console.WriteLine("Digite até qual posição você gostaria que a sequencia fosse.");

            //pega o numero que o usuário escolher
            do
            {
                input = Console.ReadLine();
                if(input.ToLower() == "sair")
                    goto Exit;
            } while (!int.TryParse(input, out limit));

            Console.WriteLine("\nGostaria da sequência representada numericamente ou graficamente?\n1 - Numericamente \n2 - Graficamente");

            //pega o numero que o usuário escolher
            do
            {
                input = Console.ReadLine();
                if (input.ToLower() == "sair")
                    goto Exit;
            } while (!int.TryParse(input, out option) || option < 1 || option > 2);

            if (option == 2 && limit > 15)
            {
                Console.WriteLine("Isso sobrecarregaria o computador. \nEscolha o modo numerico ou um numero menor.\nEnter para prosseguir");
                Console.ReadLine();
                goto Redo;
            }

            //calcula e imprime a sequência
            for (int i = 0; i < limit; i++)
            {
                if (option == 1)
                    Console.Write($"{f} ");
                else if (option == 2)
                {
                    for (int j = 0; j < f; j++)
                        Console.Write("* ");
                    Console.WriteLine();
                }
                f2 = f1;
                f1 = f;
                f = f1 + f2;
            }

            Exit:

            Console.WriteLine("\nAperte enter para voltar ao menu.");
            Console.ReadLine();
        }
    }
}
