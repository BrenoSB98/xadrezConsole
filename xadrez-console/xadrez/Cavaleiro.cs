using tabuleiro;

namespace xadrez {
    internal class Cavaleiro : Peca {
        public Cavaleiro(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "C";
        }
    }
}
