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

        public void ColocarPeca(Peca peca, Posicao posicao) {
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}
