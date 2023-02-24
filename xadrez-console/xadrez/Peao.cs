using tabuleiro;

namespace xadrez {
    internal class Peao : Peca {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "P";
        }
    }
}
