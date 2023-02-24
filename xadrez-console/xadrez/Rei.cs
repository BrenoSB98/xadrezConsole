using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao posicao = new Posicao(0, 0);

            //Acima
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
            var aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Nordeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Direita
            posicao.DefinirValores(posicao.Linha, posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Baixo
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudoeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Esquerda
            posicao.DefinirValores(posicao.Linha, posicao.Coluna - 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Noroeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            return mat;
        }

        public override string ToString() {
            return "R";
        }
    }
}
