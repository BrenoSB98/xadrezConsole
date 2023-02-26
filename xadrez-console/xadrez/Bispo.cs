using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    internal class Bispo : Peca {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "B";
        }
        private bool PodeMover(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new (0, 0);

            // NO
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }

            // NE
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            }

            // SE
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }

            // SO
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }
            return mat;
        }
    }
}
