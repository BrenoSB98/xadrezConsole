using System;
using tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console{
    
    class Program { 
        static void Main(string[] args) {

            try {
                Tabuleiro Tab = new(8, 8);

                Tab.ColocarPeca(new Torre(Cor.Amarela, Tab), new Posicao(0, 0));
                Tab.ColocarPeca(new Cavaleiro(Cor.Amarela, Tab), new Posicao(0, 1));
                Tab.ColocarPeca(new Bispo(Cor.Amarela, Tab), new Posicao(0, 2));
                Tab.ColocarPeca(new Rainha(Cor.Amarela, Tab), new Posicao(0, 3));
                Tab.ColocarPeca(new Rei(Cor.Amarela, Tab), new Posicao(0, 4));
                Tab.ColocarPeca(new Bispo(Cor.Amarela, Tab), new Posicao(0, 5));
                Tab.ColocarPeca(new Cavaleiro(Cor.Amarela, Tab), new Posicao(0, 6));
                Tab.ColocarPeca(new Torre(Cor.Amarela, Tab), new Posicao(0, 7));

                Tab.ColocarPeca(new Torre(Cor.Amarela, Tab), new Posicao(7, 0));
                Tab.ColocarPeca(new Torre(Cor.Amarela, Tab), new Posicao(7, 7));

                Tela.ImprimirTabuleiro(Tab);
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }
        
    }
}