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
        public Peca VulneravelEnPassant;

        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            VulneravelEnPassant = null;
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
            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemT = new (origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new (origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementaQteMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemT = new (origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new (origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementaQteMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (peca is Peao) {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null) {
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branco) {
                        posicaoPeao = new (destino.Linha + 1, destino.Coluna);
                    }
                    else {
                        posicaoPeao = new (destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posicaoPeao);
                    _capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em XEQUE!");
            }

            Peca peca = Tab.Peca(destino);

            // #jogadaespecial promocao
            if (peca is Peao) {
                if ((peca.Cor == Cor.Branco && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7)) {
                    peca = Tab.RetirarPeca(destino);
                    _pecas.Remove(peca);
                    Peca rainha = new Rainha(Tab, peca.Cor);
                    Tab.ColocarPeca(rainha, destino);
                    _pecas.Add(rainha);
                }
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

            // #jogadaespecial en passant
            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)) {
                VulneravelEnPassant = peca;
            }
            else {
                VulneravelEnPassant = null;
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

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (peca is Peao) {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant) {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branco) {
                        posicaoPeao = new Posicao(3, destino.Coluna);
                    }
                    else {
                        posicaoPeao = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posicaoPeao);
                }
            }
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
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavaleiro(Tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rainha(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavaleiro(Tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branco, this));
                
            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavaleiro(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavaleiro(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));

        }
    }
}
