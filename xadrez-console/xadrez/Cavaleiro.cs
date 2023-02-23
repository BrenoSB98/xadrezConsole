using tabuleiro;

namespace xadrez_console.xadrez {
    internal class Cavaleiro : Peca {
        public Cavaleiro(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "C";
        }
    }
}
