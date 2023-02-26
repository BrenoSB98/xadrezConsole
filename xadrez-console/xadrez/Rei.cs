using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {

        private PartidaDeXadrez _partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            _partida = partida;
        }
        
        public override string ToString() {
            return "R";
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

            // #jogadaespecial roque
            if (QteMovimentos == 0 && !_partida.Xeque) {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(posicao.Linha, posicao.Coluna + 3);
                
                if (TesteTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao p2 = new Posicao(posicao.Linha, posicao.Coluna + 2);
                    
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null) {
                        mat[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(posicao.Linha, posicao.Coluna - 4);
                
                if (TesteTorreParaRoque(posT2)) {
                    Posicao p1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    Posicao p2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                    Posicao p3 = new Posicao(posicao.Linha, posicao.Coluna - 3);
                    
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null) {
                        mat[posicao.Linha, posicao.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }        
    }
}
