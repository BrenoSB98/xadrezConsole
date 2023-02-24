using System;
using tabuleiro;

namespace xadrez {
    internal class PartidaDeXadrez {
        public Tabuleiro Tab { get; private set; }
        private int _turno;
        private Cor _jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branco;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementaQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
        }

        private void ColocarPecas() {
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 5).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('a', 7).ToPosicao());

            Tab.ColocarPeca(new Cavaleiro(Cor.Preta, Tab), new PosicaoXadrez('b', 7).ToPosicao());

            Tab.ColocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('c', 7).ToPosicao());

            Tab.ColocarPeca(new Rainha(Cor.Preta, Tab), new PosicaoXadrez('d', 7).ToPosicao());

            Tab.ColocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('e', 7).ToPosicao());

            Tab.ColocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('f', 7).ToPosicao());

            Tab.ColocarPeca(new Cavaleiro(Cor.Preta, Tab), new PosicaoXadrez('g', 7).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('h', 7).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('a', 1).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('h', 1).ToPosicao());
        }
    }
}
