% 
% Loads all files necessary for Logic module
%

% loads tag Semantics definitions
:- compile('tag_semantic.pl').

% loads knowledge base
:- compile('rede.pl').

%:-compile('rede_com_1_int.pl');

%:-compile('rede_com_2_int.pl');

%:-compile('rede_com_3_int.pl');

%:-compile('rede_com_4_int.pl');

:- compile('caminho_mais_curto.pl').

:- compile('caminho_mais_forte.pl').

:- compile('caminho_mais_seguro_1ligacao.pl').

:- compile('sugerir_conexoes.pl').

:- compile('xtags_param.pl').

:-compile("tamanho_da_rede.pl").