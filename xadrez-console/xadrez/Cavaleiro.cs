using tabuleiro;

namespace xadrez {
    internal class Cavaleiro : Peca {
        public Cavaleiro(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "C";
        }
    }
}
