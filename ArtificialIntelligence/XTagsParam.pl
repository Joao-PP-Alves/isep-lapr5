%getUsersWithXTagsParam 

getUsersWithXTagsParam(Param,ListaTags,ListaCombTags,ListaUserTags,ListaUsers,Result):-
    length(ListaTags,Q),                                                                    % verifica o tamanho da lista de tags
    Param =< Q,                                                                             % verifica se o tamanho é > ou = que Param
    todas_combinacoes(Param,ListaTags,ListaCombTags),                                       % devolve uma lista com todas as combinações possiveis
    listarTodosUsers(ListaUserTags,ListaUsers),                                             % devolve a listagem dos users e suas tags
    x(ListaCombTags, ListaCombTags, ListaUserTags, ListaUsers, Result).                     % devolve users que tenham X tags em comum
    

todas_combinacoes(X,LTags,LcombXTags):-findall(L,xCombinacoesTags(X,LTags,L),LcombXTags).

xCombinacoesTags(0,_,[]):-!.
xCombinacoesTags(X,[Tag|L],[Tag|T]):-X1 is X-1,xCombinacoesTags(X1,L,T).
xCombinacoesTags(X,[_|L],T):-xCombinacoesTags(X,L,T).


listarTodosUsers(L,U):-findall(User,(no(_,User,_)),U),
    listarTodosUsers1(L,U).
listarTodosUsers1([],[]):-!.
listarTodosUsers1([LTags|L],[U|Us]):-findall(Tags,(no(_,U,Tags)),LTags),
    listarTodosUsers1(L,Us).


intersecaoListas([],_,[]). 
intersecaoListas([X|L1],L2,[X|Result]):-member(X,L2),!,intersecaoListas(L1,L2,Result).
intersecaoListas([_|L1],L2,Result):-intersecaoListas(L1,L2,Result).


x([], _,_,_,[]):-!.
x(_, _,_,[],[]):-!.
x(Combs, [],[UTs|LUTs], [U|Us], Rs):-!,
    x(Combs, Combs, LUTs, Us, Rs).
x(Combs, _, [], [U|Us], Rs):-!,
    x(Combs, Combs, Us, Us, Rs).
x(Combs, [C|Cs], [[UTs|_]|LUTs], [U|Us], [U|Rs]):-
    intersecaoListas(C,UTs,C),!, 
    x(Combs, Combs, LUTs, Us, Rs).
x(Combs, [C|Cs], LUTs, Users, Rs):-
    x(Combs, Cs, LUTs, Users, Rs).