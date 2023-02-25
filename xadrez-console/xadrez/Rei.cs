using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {

        //private PartidaDeXadrez _partida;
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TesteTorreParaRoque(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            bool aux1 = peca != null && peca is Torre;
            bool aux2 = peca.Cor == Cor && peca.QteMovimentos == 0;
            return aux1 && aux2;
        }
        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao posicao = new Posicao(0, 0);

            //Acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            var aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Baixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            aux = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            if (aux) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
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
