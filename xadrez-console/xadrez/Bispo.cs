using tabuleiro;

namespace xadrez_console.xadrez {
    internal class Bispo : Peca {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "B";
        }
    }
}
