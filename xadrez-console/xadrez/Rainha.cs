using tabuleiro;

namespace xadrez {
    internal class Rainha : Peca {
        public Rainha(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "D";
        }
    }
}
