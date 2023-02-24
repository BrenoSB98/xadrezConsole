namespace tabuleiro {
    internal class Tabuleiro {

        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linhas, int colunas) {
            return _pecas[linhas, colunas];
        }

        public Peca Peca(Posicao posicao) {
            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public bool ExistePecaNaPosicao(Posicao posicao) { 
            ValidarPosicao(posicao);
            return Peca(posicao) != null;
        }
        public void ColocarPeca(Peca peca, Posicao posicao) {
            if (ExistePecaNaPosicao(posicao)) {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao) {
            if (Peca(posicao) == null) {
                return null;
            }
            Peca aux = Peca(posicao);
            aux.Posicao = null;
            _pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao posicao) {
            var PosLinha = posicao.Linha < 0 || posicao.Linha >= Linhas;
            var PosColuna = posicao.Coluna < 0 || posicao.Coluna >= Colunas;
            var PosFinal = PosLinha || PosColuna;
            if (PosFinal) {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao posicao) {
            if (!PosicaoValida(posicao)) {
                throw new TabuleiroException("Posição Inválida");
            }
        }
    }
}
