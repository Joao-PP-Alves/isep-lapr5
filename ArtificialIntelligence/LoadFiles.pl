% 
% Loads all files necessary for Logic module
%

% loads tag Semantics definitions
:- compile('tag_semantic.pl').

% loads knowledge base
:- compile('rede.pl').

:- compile('caminho_mais_curto.pl').

:- compile('caminho_mais_forte.pl').

:- compile('sugerir_conexoes.pl').

:- compile('xtags_param.pl').

:-compile("tamanho_da_rede.pl").