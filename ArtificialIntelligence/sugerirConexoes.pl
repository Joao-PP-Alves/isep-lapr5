%%sugere amigos com base nas tags em comum e nas conexões em comum, tendo em conta n níveis
sugestConnections(User,TagsList, Connections, Level, Sugestions):-
        tag(TagList, TagResult),                                                    %%vai verificar a semântica das TagsList
        append([TagList], TagResult, ListTags),                                     %%adiciona os sinónimos das tags encontrados a uma lista para poder pesquisar todas as tags relacionadas
        getUsers(User, Level, UsersList),                                           %%retorna uma lista com os users que se encontram até n níveis
        findAllSugestionsByTag(ListTags, UsersList, SugestionsTemp),                %%vai até n níveis pesquisar users pela tag
        findAllConnectionsByConnections(Connections, UsersList, SugestionsTemp2),   %%vai até n níveis pesquisar users pelas conexões em comum
        joinLists(SugestionsTemp, SugestionsTemp2, SugestionsTemp3).                %%junta as listas com as sugestões e remove elementos repetidos



%%retorna uma lista com os users que se encontram até n níveis
getUsers(User,0,L):-!,
	no(_,User,_),
	L is 1.

getUsers(User, Level, Result):-
    Level1 is Level-1,  
    no(Orig,User,_),                   
	directConnections(Orig,L),  
    append([Orig],L,LX),            
	moreConnections(L,Level1, Result).


    %%retorna as ligações diretas do User
    directConnections(User,L):-
        findall(X,(ligacao(User,X,_,_);ligacao(X,User,_,_)),L).

    
    moreConnections([], []):-!.
    
    %moreConnections(L, Result):- 
    
    moreConnections([H|L], Result):- 
        directConnections(H, L1), 
        union(L1, L2, Result),
        joinLists(L, Result, L3),
        apaga(H, L3, L4),
        moreConnections(L4, Result).    %%ver como passar só a head do result


    apaga(_,[],[]).
    apaga(X,[X|L],L1):-!,apaga(X,L,L1).
    apaga(X,[Y|L],[Y|L1]):- apaga(X,L,L1).


    removeFirst([],[]):-!.
    removeFirst([H|T],L):-joinLists(T,L,L).

    
    %%retorna os users até n níveis
    %%moreConnections(L2,X,0):-length(L2,X),!.

    %%moreConnections(L,Tamanho,N):-
	%%    friendsOfFriends(L,L2),
	%%    N1 is N-1,
	%%    moreConnections(L2,Tamanho,N1),!.

    friendsOfFriends([],[]):-!.

    friendsOfFriends([H|T],LR):-
	    directConnections(H,L),
	    friendsOfFriends(T,L2),joinLists(L,L2,LR).



%%pesquisa de users para sugerir com base nas tags - FALTA POR OS NIVEIS
findAllSugestionsByTag([],[],[]).                                               %%caso base

findAllSugestionsByTag([Head|Tail], [DefList|List]):-
        findall((UserA,UserB),(connects(UserA,UserB,_,ListTags),
        hasTag(ListTags, Head)), DefList),
	    findAllSugestionsByTag(Tail, List).

hasTag([Tag|_], Tag).

hasTag([_|List], Tag):-hasTag(List, Tag).



%%pesquisa de users para sugerir com base nas conexões em comum



%%junta 2 listas e remove elementos repetidos
joinLists([],L,L).
joinLists([X|L],L1,LU):-member(X,L1),!,joinLists(L,L1,LU).
joinLists([X|L],L1,[X|LU]):-joinLists(L,L1,LU).