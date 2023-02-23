using tabuleiro;

namespace tabuleiro {
    internal class Peca {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovientos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab) {
            Posicao = posicao;
            Cor = cor;
            QteMovientos = 0;
            Tab = tab;
        }
    }
}
