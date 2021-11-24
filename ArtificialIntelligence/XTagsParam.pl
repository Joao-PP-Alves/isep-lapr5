%getUsersWithXTagsParam 

getUsersWithXTagsParam(Param,ListaTags,ListaCombTags,ListaUserTags,ListaUsers,Result):-
    length(ListaTags,Q),
    Param =< Q,
    todas_combinacoes(Param,ListaTags,ListaCombTags),
    listarTodosUsers(ListaUserTags,ListaUsers),
  %  writeln(ListaUserTags),
    x(ListaCombTags, ListaCombTags, ListaUserTags, ListaUsers, Result).
    

x([], _,_,_,[]):-!.
x(_, _,_,[],[]):-!.
x(Combs, [],[UTs|LUTs], [U|Us], Rs):-!,
    x(Combs, Combs, LUTs, Us, Rs).
x(Combs, _, [], [U|Us], Rs):-!,
    x(Combs, Combs, Us, Us, Rs).
x(Combs, [C|Cs], [[UTs|_]|LUTs], [U|Us], [U|Rs]):-
    %write(C),write(' -> '), writeln(UTs),nl,nl,
    intersecaoListas(C,UTs,C),!, 
    x(Combs, Combs, LUTs, Us, Rs).
x(Combs, [C|Cs], LUTs, Users, Rs):-
    x(Combs, Cs, LUTs, Users, Rs).
    


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

listarTodosUsers(L,U):-findall(User,(no(_,User,_)),U),
    listarTodosUsers1(L,U).
listarTodosUsers1([],[]):-!.
listarTodosUsers1([LTags|L],[U|Us]):-findall(Tags,(no(_,U,Tags)),LTags),
    listarTodosUsers1(L,Us).



intersecaoListas([],_,[]). 
intersecaoListas([X|L1],L2,[X|Result]):-member(X,L2),!,intersecaoListas(L1,L2,Result).
intersecaoListas([_|L1],L2,Result):-intersecaoListas(L1,L2,Result).

%se false, apresentar mensagem de erro sugestiva
  
%se true, devolver lista de users por ordem de match