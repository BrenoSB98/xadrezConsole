using tabuleiro;

namespace xadrez_console.xadrez {
    internal class Torre : Peca {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "T";
        }
    }
}
