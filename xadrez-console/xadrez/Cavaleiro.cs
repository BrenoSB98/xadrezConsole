using System;
using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    internal class Cavaleiro : Peca {
        public Cavaleiro(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "C";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new(0, 0);

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            return mat;
        }


    }
}
