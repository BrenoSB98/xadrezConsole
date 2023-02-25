using tabuleiro;

namespace xadrez {
    internal class Bispo : Peca {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "B";
        }
    }
}
