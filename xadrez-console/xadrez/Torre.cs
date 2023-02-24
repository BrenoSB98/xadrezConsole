using tabuleiro;

namespace xadrez {
    internal class Torre : Peca {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab) {
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
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
            var teste = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            while (teste) {

                mat[posicao.Linha, posicao.Coluna] = true;
                var aux = Tab.Peca(posicao) != null 
                    && Tab.Peca(posicao).Cor != Cor;
                if (aux) {
                    break;
                }
                posicao.Linha = posicao.Linha - 1;

            }

            //Direita
            posicao.DefinirValores(posicao.Linha, posicao.Coluna + 1);
            teste = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            while (teste) {

                mat[posicao.Linha, posicao.Coluna] = true;
                var aux = Tab.Peca(posicao) != null
                    && Tab.Peca(posicao).Cor != Cor;
                if (aux) {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1;

            }       

            //Baixo
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
            teste = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            while (teste) {

                mat[posicao.Linha, posicao.Coluna] = true;
                var aux = Tab.Peca(posicao) != null
                    && Tab.Peca(posicao).Cor != Cor;
                if (aux) {
                    break;
                }
                posicao.Linha = posicao.Linha + 1;

            }

            //Esquerda         
            posicao.DefinirValores(posicao.Linha, posicao.Coluna - 1);
            teste = Tab.PosicaoValida(posicao) && PodeMover(posicao);
            while (teste) {

                mat[posicao.Linha, posicao.Coluna] = true;
                var aux = Tab.Peca(posicao) != null
                    && Tab.Peca(posicao).Cor != Cor;
                if (aux) {
                    break;
                }
                posicao.Coluna = posicao.Coluna - 1;

            }
            return mat;
        }
    }
}
