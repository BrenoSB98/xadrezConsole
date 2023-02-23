using tabuleiro;

namespace xadrez_console.xadrez {
    internal class Peao : Peca {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "P";
        }
    }
}
