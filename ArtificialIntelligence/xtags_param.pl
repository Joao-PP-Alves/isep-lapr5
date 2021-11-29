:- module(xtags_param, [retornarUsersComXTagsEmComum/6]).

retornarUsersComXTagsEmComum(Param,ListaTags,ListaCombTags,ListaUserTags,ListaUsers,Result):-
    length(ListaTags,Q),                                                                    
    Param =< Q,                                                                             
    todasCombinacoes(Param,ListaTags,ListaCombTags),                                       
    listarTodosUsers(ListaUserTags,ListaUsers),                                             
    retornarUsers(ListaCombTags, ListaCombTags, ListaUserTags, ListaUsers, Result).                     
    

todasCombinacoes(X,LTags,LcombXTags):-
    findall(L,xCombinacoesTags(X,LTags,L),LcombXTags).

xCombinacoesTags(0,_,[]):-!.
xCombinacoesTags(X,[Tag|L],[Tag|T]):-
    X1 is X-1,
    xCombinacoesTags(X1,L,T).
xCombinacoesTags(X,[_|L],T):-
    xCombinacoesTags(X,L,T).


listarTodosUsers(L,U):-findall(User,(no(_,User,_)),U),
    listarTodosUsers1(L,U).
listarTodosUsers1([],[]):-!.
listarTodosUsers1([LTags|L],[U|Us]):-
    findall(Tags,(no(_,U,Tags)),LTags),
    listarTodosUsers1(L,Us).


intersecaoListas([],_,[]). 
intersecaoListas([X|L1],L2,[X|Result]):-
    member(X,L2),!,intersecaoListas(L1,L2,Result).
intersecaoListas([_|L1],L2,Result):-
    intersecaoListas(L1,L2,Result).


retornarUsers([], _,_,_,[]):-!.
retornarUsers(_, _,_,[],[]):-!.
retornarUsers(Combs, [],[UsrTags|LUsrTags], [Usr|Usrs], Result):-!,
    retornarUsers(Combs, Combs, LUsrTags, Usrs, Result).
retornarUsers(Combs, _, [], [Usr|Usrs], Result):-!,
    retornarUsers(Combs, Combs, Usrs, Usrs, Result).
retornarUsers(Combs, [Cmb|Cmbs], [[UsrTags|_]|LUsrTags], [Usr|Usrs], [Usr|Result]):-
    intersecaoListas(Cmb,UsrTags,Cmb),!, 
    retornarUsers(Combs, Combs, LUsrTags, Usrs, Result).
retornarUsers(Combs, [Cmb|Cmbs], LUsrTags, Users, Result):-
    retornarUsers(Combs, Cmbs, LUsrTags, Users, Result).