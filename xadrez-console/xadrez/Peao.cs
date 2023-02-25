using tabuleiro;

namespace xadrez {
    internal class Peao : Peca {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "P";
        }
    }
}
