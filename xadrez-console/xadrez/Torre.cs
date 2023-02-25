using tabuleiro;

namespace xadrez {
    internal class Torre : Peca {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "T";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao posicao = new Posicao(0, 0);

            //Acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {

                mat[posicao.Linha, posicao.Coluna] = true;                
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.Linha = posicao.Linha - 1;

            }

            //Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);          
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {

                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1;

            }       

            //Baixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {

                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.Linha = posicao.Linha + 1;

            }

            //Esquerda         
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao)) {

                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor) {
                    break;
                }
                posicao.Coluna = posicao.Coluna - 1;

            }
            return mat;
        }
    }
}
