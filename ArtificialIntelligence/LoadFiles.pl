% 
% Loads all files necessary for Logic module
%

% loads tag Semantics definitions
:- compile('TagSemantic.pl').

% loads knowledge base
:- compile('Rede.pl').

:- compile('Caminho_Mais_Curto.pl').

:- compile('Caminho_Mais_Forte.pl').

:- compile('SugerirConexoes.pl').

:- compile('XTagsParam.pl').

:-compile("Tamanho_da_rede.pl").