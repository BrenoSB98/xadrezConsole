using tabuleiro;

namespace xadrez {
    internal class Cavaleiro : Peca {
        public Cavaleiro(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "C";
        }
    }
}
