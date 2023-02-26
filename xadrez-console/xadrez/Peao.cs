using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    internal class Peao : Peca {

        private PartidaDeXadrez _partidaDeXadrez;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partidaDeXadrez) : base(tab, cor) {
            _partidaDeXadrez = partidaDeXadrez;
        }

        public override string ToString() {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos) {
            Peca peca = Tab.Peca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao pos) {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branco) {
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(posicao) && Livre(posicao) && QteMovimentos == 0) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (Posicao.Linha == 3) {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(posicao) && Livre(posicao) && QteMovimentos == 0) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao)) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Linha == 4) {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }
            return mat;
        }

    }
}
