using tabuleiro;

namespace xadrez {
    internal class Rainha : Peca {
        public Rainha(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "D";
        }
    }
}
