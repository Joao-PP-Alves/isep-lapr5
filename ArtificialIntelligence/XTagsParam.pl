%receber X

%verificar se o nr de tags do user é > ou = que X

qntdTags([],0).
qntdTags([_|T],N):-qntdTags(T,N1),(N is N1 + 1).

maiorOuIgual(0,0).
maiorOuIgual(S,X):-S>=X.

%se false, permitir alteracao do X com mensagem sugestiva

%se true, combinacoes de X elementos de tags

todas_combinacoes(X,LTags,LcombXTags):-findall(L,xCombinacoesTags(X,LTags,L),LcombXTags).

xCombinacoesTags(0,_,[]):-!.
xCombinacoesTags(X,[Tag|L],[Tag|T]):-X1 is X-1,xCombinacoesTags(X1,L,T).
xCombinacoesTags(X,[_|L],T):-xCombinacoesTags(X,L,T).

%isolar users e suas tags

%traçar as combinacoes com as listas de tags dos users

listarTodosUsers([],[]).
listarTodosUsers(U,LT):-findall(no(U,_,LT)).

intersecaoListas([],_,[]). 
intersecaoListas([X|L1],L2,[X|Result]):-member(X,L2),!,intersecaoListas(L1,L2,Result).
intersecaoListas([_|L1],L2,Result):-intersecaoListas(L1,L2,Result).

%se false, apresentar mensagem de erro sugestiva

%se true, devolver lista de users por ordem de match