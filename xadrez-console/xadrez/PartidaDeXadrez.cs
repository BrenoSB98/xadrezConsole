using System.Collections.Generic;
using tabuleiro;


namespace xadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;
        internal Peca vulneravelEnPassant;

        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            _pecas= new HashSet<Peca>();
            _capturadas= new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementaQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
            if (pecaCapturada != null) {
                _capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em XEQUE!");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else {
                Xeque = false;
            }
            if(TesteXequemate(Adversaria(JogadorAtual))) {
                Terminada = true;
            }
            else {
                Turno++;
                MudaJogador();
            }            
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca peca = Tab.RetirarPeca(destino);
            peca.DecrementarQteMovimentos();
            if (pecaCapturada != null) {
                Tab.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(peca, origem);
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao) {

            if (Tab.Peca(posicao) == null) {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if (JogadorAtual != Tab.Peca(posicao).Cor) {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!Tab.Peca(posicao).ExisteMovimentosPossiveis()) {
                throw new TabuleiroException("Não é possivel mexer essa peça!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!Tab.Peca(origem).MovimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        private void MudaJogador() {

            if (JogadorAtual == Cor.Branco) {
                JogadorAtual = Cor.Preta;
            }
            else {
                JogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in _capturadas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasNaPartida(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in _pecas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor) {
            if (cor == Cor.Branco) {
                return Cor.Preta;
            }
            return Cor.Branco;
        }

        private Peca Rei(Cor cor) {
            foreach (var x in PecasNaPartida(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("O Rei " + cor + "não está no Tabuleiro");
            }
            foreach (var x in PecasNaPartida(Adversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate (Cor cor) {
            if (!EstaEmXeque(cor)) {
                return false;
            }
            foreach (var x in PecasNaPartida(cor)) {
                bool[,] mar = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++) {
                    for (int j = 0; j < Tab.Colunas; j++) {
                        if (mar[i, j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }     
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca) {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas() {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Branco));            
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));
           
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preta));

        }
    }
}
