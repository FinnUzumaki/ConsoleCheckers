using System;
using System.Linq;

namespace Trabalho_1
{
    class Forca
    {
        Random rnd = new Random();
        string input, word;
        char letterRaw;
        char[] wordChar = new char[10], letters = new char[15];
        int selected, pos, attempts = 0, missing = 0;
        string[] easyWords = new string[10] { "mexer", "algoz", "termo", "senso", "nobre", "plena", "afeto", "sutil", "audaz", "inato" };
        string[] mediumWords = new string[10] { "empatia", "embuste", "prolixo", "cinico", "idoneo", "ambito", "sublime", "nescio", "indole", "sucinto" };
        string[] hardWords = new string[10] { "encharcado", "prescindir", "corroborar", "detrimento", "maturidade", "habilidade", "mobilidade", "melindroso", "ascendente", "incoerente" };

        public void Start()
        {
            Game();
        }

        void Game()
        {
            Console.Clear();


            Console.WriteLine("Escolha a dificuldade: \n1 - Fácil \n2 - Médio \n3 - Difícil");

            //pega a dificuldade que o player escolher
            do
            {
                do
                {
                    input = Console.ReadLine();
                    if (input.ToLower() == "sair")
                        goto Exit;
                } while (!int.TryParse(input, out selected));
            } while (selected < 1 || selected > 3);

            //pega randomicamente uma palavra da lista
            pos = (int)rnd.Next(10);

            //dependendo da dificuldade ele escolhe uma lista diferente
            switch (selected)
            {
                case 1:
                    word = easyWords[pos];
                    break;
                case 2:
                    word = mediumWords[pos];
                    break;
                case 3:
                    word = hardWords[pos];
                    break;
            }

            //transforma a string em uma array de chars
            wordChar = word.ToCharArray();

            //looping de jogo
            for (int i = 0; i < letters.Length; i++)
            {

                Console.Clear();

                Console.WriteLine($"Digite uma letra por vez e tente adivinhar a palavra. \nVocê pode errar {(letters.Length - wordChar.Length) - attempts} vezes.");

                //contador de quantas letras faltam para acertar a palavra
                missing = 0;

                //imprime a palavra
                for (int j = 0; j < wordChar.Length; j++)
                {
                    //verifica se o player colocou alguma letra certa
                    if (letters.Contains(wordChar[j]))
                    {
                        //se sim imprime apenas aquela letra
                        Console.Write(wordChar[j]);
                    }
                    else
                    {
                        //se não imprime "_" no lugar e aumenta o numero de letras faltando
                        Console.Write('_');
                        missing++;
                    }
                }

                Console.WriteLine($"\nFaltam {missing}");

                //imprime as letras tentadas até o momento
                Console.Write("Letras tentadas:");
                for (int j = 0; j < letters.Length; j++)
                    Console.Write(letters[j]);
                Console.WriteLine();

                //se não falta nenhuma letra o jogo é ganho
                if (missing == 0)
                {
                    Console.WriteLine("Você ganhou.");
                    break;
                }
                //caso o numero de tentativas seja esgotado o jogo é perdido e a palavra é revelada
                else if ((letters.Length - wordChar.Length) - attempts == 0)
                {
                    Console.WriteLine($"Você perdeu. \nA palavra era {word}");
                    break;
                }

                //pega a letra que o usuário escolhe, caso não seja válida o looping repete
                do
                {
                    do
                    {
                        input = Console.ReadLine().ToLower();
                        //caso a opção de sair seja usada o jogador é enviado para o final
                        if (input.ToLower() == "sair")
                            goto Exit;
                    } while (!char.TryParse(input, out letterRaw));
                } while (letters.Contains(letterRaw));

                //, se for uma letra válida é colocada no proximo espaço da array de letras tentadas
                letters[i] = letterRaw;

                //caso não seja uma letra da palavra as tentativas aumentam
                if (!wordChar.Contains(letters[i]))
                {
                    attempts++;
                }

            }
        Exit:
            Console.WriteLine("Aperte enter para voltar ao menu");
            Console.ReadLine();
        }
    }
}
