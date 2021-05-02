using System;

namespace Trabalho_1
{
    class Damas
    {
        bool playing, exit, cancel, canEat;
        int row, col, rowN, colN, Apieces, Vpieces, winner, turn;
        int[,] newBoard = new int[10, 10]
        {     {9, 8, 8 , 8, 8 , 8, 8 , 8, 8 ,9 },
              {7, 9,-1 , 9,-1 , 9,-1 , 9,-1 ,7 },
              {7,-1, 9 ,-1, 9 ,-1, 9 ,-1, 9 ,7 },
              {7, 9,-1 , 9,-1 , 9,-1 , 9,-1 ,7 },
              {7, 0, 9 , 0, 9 , 0, 9 , 0, 9 ,7 },
              {7, 9, 0 , 9, 0 , 9, 0 , 9, 0 ,7 },
              {7, 1, 9 , 1, 9 , 1, 9 , 1, 9 ,7 },
              {7, 9, 1 , 9, 1 , 9, 1 , 9, 1 ,7 },
              {7, 1, 9 , 1, 9 , 1, 9 , 1, 9 ,7 },
              {9, 8, 8 , 8, 8 , 8, 8 , 8, 8 ,9 }
        }, board = new int[10,10], savedBoard = new int[10, 10];

        public void Start()
        {
            playing = true;
            exit = false;
            cancel = false;
            canEat = false;
            turn = 1;
            winner = 0;
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = newBoard[i, j];
                }
            }
            Game();
        }

        void Game()
        {
            Console.Clear();
            Console.WriteLine("Selecione a peça pela posição no tabuleiro.\nUse primeiro o número à esquerda e depois o de cima. Ex: 4,3 \nPode-se usar espaço, virgula ou ponto para separar.");

            //looping de jogo
            do
            {
            Redo:

                Apieces = 0;
                Vpieces = 0;

                //salva o campo para caso o player queria cancelar a jogada
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (cancel)
                            board[i, j] = savedBoard[i, j];
                        else
                        {
                            savedBoard[i, j] = board[i, j];
                            if (board[i, j] == 1)
                                Apieces++;
                            else if (board[i, j] == -1)
                                Vpieces++;
                        }

                    }
                }

                //se a jogada foi cancelada ele limpa a tela e imprime de novo
                if (cancel)
                {
                    Console.Clear();
                    cancel = false;
                }
                
                //texto para o player entender melhor
                Console.WriteLine($"Player 1 = {Apieces} \nPlayer 2 = {Vpieces}");
                DrawBoard();
                if (turn > 0)
                {
                    Console.WriteLine($"Player 1 selecione a peça que quer mover.");
                }else
                {
                    Console.WriteLine($"Player 2 selecione a peça que quer mover.");
                }

                //pega a posição da peça escolhida
                do
                {
                    GetPos(out row, out col);
                    if (exit)
                    {
                        goto Exit;
                    }
                } while (board[row, col] != turn);

                //para o player não poder cancelar a ação antes de selecionar alguma peça
                cancel = false;

                //looping para caso haja mais de uma peça a ser comida
                do
                {
                    //contador de peças comidas
                    int counter = 0;

                    //transforma a peça em uma peça selecionada
                    board[row, col] = turn * 2;

                    //verifica os movimentos possíveis
                    CheckMovements(row, col, out canEat);

                    //mostra os movimentos possíveis
                    Console.Clear();
                    DrawBoard();
                    Console.WriteLine("Para onde quer mover?\nDigite x para cancelar.");

                    //pega a posição que o player escolher para mover
                    do
                    {
                        GetPos(out rowN, out colN);
                        //tem a possibilidade de cancelar a seleção, mesmo depois de tentar comer as peças
                        if (cancel)
                        {
                            goto Redo;
                        }
                        if (exit)
                        {
                            goto Exit;
                        }
                    } while (board[rowN, colN] != 4);

                    //verifica os movimentos possíveis de onde o player escolheu se mover, caso não haja nenhum a jogada é encerrada
                    CheckMovements(rowN, colN, out canEat);

                    //tira os movimentos possíveis da tela
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            if (board[i, j] == 4)
                                board[i, j] = 0;
                            //conta o numero de peças comidas
                            else if (board[i, j] == -turn * 3)
                                counter++;
                        }
                    }

                    //tira a peça selecionada da posição anterior
                    board[row, col] = 0;

                    //caso o looping se repita essa é a nova posição da peça selecionada
                    row = rowN;
                    col = colN;

                    //caso não haja nenhuma peça comida, não será possível comer mesmo que haja a possibilidade
                    if (counter == 0)
                        canEat = false;
                } while (canEat);

                //deseleciona a peça
                board[row, col] = turn;

                //remove as peças comidas
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == -turn * 3)
                        {
                            board[i, j] = 0;
                            if (turn > 0)
                                Vpieces--;
                            else
                                Apieces--;
                        }
                    }
                }

                //verifica o numero de peças de cada player, quem estiver com 0 perde
                if (Apieces == 0)
                {
                    playing = false;
                    winner = 2;
                }
                else if (Vpieces == 0)
                {
                    playing = false;
                    winner = 1;
                }

                //independentemente se o jogo continuar ou não a tela deve ser limpa
                Console.Clear();

                //caso o jogo continue a tela será renderizada no começo do looping portanto apenas o turno é trocado
                if (playing)
                    turn *= -1;
                //caso o jogo termine a tela é renderizada uma ultima vez
                else
                    DrawBoard();

            } while (playing);

            //anuncia o ganhador
            Console.WriteLine($"O Player {winner} ganhou");

        //caso a opção de sair seja usada o usuário pula para essa parte
        Exit:
            Console.WriteLine("Aperte enter para voltar para o menu");
            Console.ReadLine();
        }
        void GetPos(out int row, out int col)
        {
            string inputRaw;
            string[] input = new string[2] { "a", "b" };
            int rowRaw = 0, colRaw = 0;
            do
            {
                do
                {
                    //pega o input do usuário
                    inputRaw = Console.ReadLine();
                    //caso seja um dos comandos a ação correspondente será tomada
                    if(inputRaw.ToLower() == "sair")
                    {
                        exit = true;
                        break;
                    }else if (inputRaw.ToLower() == "x")
                    {
                        cancel = true;
                        break;
                    }else if (inputRaw.Contains(",") || inputRaw.Contains(".") || inputRaw.Contains(" "))

                        input = inputRaw.Split(',', ' ', '.');

                    //até serem colocadas as opções validas o looping continuará
                } while (!int.TryParse(input[0], out rowRaw) || !int.TryParse(input[1], out colRaw));
                if (cancel || exit)
                    break;
            } while (rowRaw < 1 || rowRaw > 8 || colRaw < 1 || colRaw > 8);

            //são retornados os valores finais
            row = rowRaw;
            col = colRaw;
        }
        void DrawBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //cada opção tem sua própria renderização
                    switch (board[i, j])
                    {
                        case 0:
                            Console.Write("- ");
                            break;
                        case 1:
                            Console.Write("A ");
                            break;
                        case -1:
                            Console.Write("V ");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("A ");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case -2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("V ");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("A ");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case -3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("V ");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 4:
                            Console.Write("O ");
                            break;
                        case 9:
                            Console.Write("  ");
                            break;
                        case 7:
                            Console.Write($"{i}  ");
                            break;
                        case 8:
                            Console.Write($" {j}");
                            break;
                    }
                }
            }
            Console.WriteLine();
        }
        void CheckMovements(int row, int col, out bool canEat)
        {
            canEat = false;

            //verifica a diagonal à esquerda e em cima
            //caso tenha uma peça no local
            if (board[row - 1, col - 1] == -turn)
            {
                //caso o local verificado esteja disponivel, a opção de pode comer é ativada e o local é marcado como possivel
                if (board[row - 2, col - 2] == 0)
                {
                    canEat = true;
                    board[row - 2, col - 2] = 4;
                }
                //caso o local anterior tenha uma peça selecionada marca a que está entre eles como comida
                else if (board[row - 2, col - 2] == turn * 2)
                {
                    board[row - 1, col - 1] *= 3;
                }
            }
            //caso seja um local vazio
            else if (board[row - 1, col - 1] == 0 && board[row, col] == turn * 2)
            {
                //apenas se for uma peça do player 1 pode ir para essa posição
                if (turn > 0)
                    board[row - 1, col - 1] = 4;
            }

            //verifica a diagonal à direita e em cima
            //caso tenha uma peça no local
            if (board[row - 1, col + 1] == -turn)
            {
                //caso o local verificado esteja disponivel, a opção de pode comer é ativada e o local é marcado como possivel
                if (board[row - 2, col + 2] == 0)
                {
                    canEat = true;
                    board[row - 2, col + 2] = 4;
                }
                //caso o local anterior tenha uma peça selecionada marca a que está entre eles como comida
                else if (board[row - 2, col + 2] == turn * 2)
                {
                    board[row - 1, col + 1] *= 3;
                }
            }
            //caso seja um local vazio
            else if (board[row - 1, col + 1] == 0 && board[row, col] == turn * 2)
            {
                //apenas se for uma peça do player 1 pode ir para essa posição
                if (turn > 0)
                    board[row - 1, col + 1] = 4;
            }

            //verifica a diagonal à direita e em baixo
            //caso tenha uma peça no local
            if (board[row + 1, col + 1] == -turn)
            {
                //caso o local verificado esteja disponivel, a opção de pode comer é ativada e o local é marcado como possivel
                if (board[row + 2, col + 2] == 0)
                {
                    canEat = true;
                    board[row + 2, col + 2] = 4;
                }
                //caso o local anterior tenha uma peça selecionada marca a que está entre eles como comida
                else if (board[row + 2, col + 2] == turn * 2)
                {
                    board[row + 1, col + 1] *= 3;
                }
            }
            //caso seja um local vazio
            else if (board[row + 1, col + 1] == 0 && board[row, col] == turn * 2)
            {
                //apenas se for uma peça do player 2 pode ir para essa posição
                if (turn < 0)
                    board[row + 1, col + 1] = 4;
            }

            //verifica a diagonal à esquerda e em baixo
            //caso tenha uma peça no local
            if (board[row + 1, col - 1] == -turn)
            {
                //caso o local verificado esteja disponivel, a opção de pode comer é ativada e o local é marcado como possivel
                if (board[row + 2, col - 2] == 0)
                {
                    canEat = true;
                    board[row + 2, col - 2] = 4;
                }
                //caso o local anterior tenha uma peça selecionada marca a que está entre eles como comida
                else if (board[row + 2, col - 2] == turn * 2)
                {
                    board[row + 1, col - 1] *= 3;
                }
            }
            //caso seja um local vazio
            else if (board[row + 1, col - 1] == 0 && board[row, col] == turn * 2)
            {
                //apenas se for uma peça do player 2 pode ir para essa posição
                if (turn < 0)
                    board[row + 1, col - 1] = 4;
            }
        }
    }
}
