%%sugere amigos com base nas tags em comum e nas conexões em comum, tendo em conta n níveis

%%DEPOIS MUDAR ISTO PARA FAZER SÓ A PARTIR DO USER E NÃO COM TAGSLIST E CONNECTIONS
sugestConnections(User,TagsList, Connections, Level, Sugestions):-
        tag(TagList, TagResult),                                                        %%vai verificar a semântica das TagsList
        append([TagList], TagResult, ListTags),                                         %%adiciona os sinónimos das tags encontrados a uma lista para poder pesquisar todas as tags relacionadas
        getUsers(User, Level, UsersList),                                               %%retorna uma lista com os users que se encontram até n níveis
        findAllSugestionsByTag(ListTags, UsersList, SugestionsTemp),                    %%vai até n níveis pesquisar users pela tag
        findAllConnectionsByConnections(Connections, SugestionsTemp, SugestionsTemp2).  %%vai até n níveis pesquisar users pelas conexões em comum


%%retorna uma lista com os users que se encontram até n níveis

%%MAIS OU MENOS ACABADO; VER COMO RETORNAR A LISTA COMPLETA COM TODOS OS USERS
getUsers(User,0,L):-!,
	no(_,User,_),
	L is 1.

getUsers(User, Level, Result):-
    Level1 is Level-1,  
    %no(Orig,User,_),                   
	directConnections(Orig,L),  
    append([Orig],L,LX),            
	moreFriends(LX, Result, Level1).

directConnections(Origem,L):-
findall(X,(ligacao(Origem,X,_,_);ligacao(X,Origem,_,_)),L).

moreFriends(L2,X,0):-reverse(L2,X),!.

moreFriends(L,Tamanho,N):-
	friendsoffriends(L,L2),
	N1 is N-1,
	moreFriends(L2,Tamanho,N1),!.


friendsoffriends([],[]):-!.
friendsoffriends([H|T],LR):-
	directConnections(H,L),
	friendsoffriends(T,L2),union(L,L2,LR).
  


%%pesquisa de users para sugerir com base nas tags 

filterSugestionsByTag(L,U,R):- filterSugestionsByTag(L,U,[],R).

filterSugestionsByTag(L,[],R1,R1).
filterSugestionsByTag(L,[U|T],R,R1):-
    checkTag(L,U), (filterSugestionsByTag(L, T, [U|R],R1),!; filterSugestionsByTag(L, T, R, R1)).

%%este método retorna true ou false, dependendo se o user possui a tag ou não
checkTag(TagsList, User):-
    no(User,_,ListTags),
    (hasTag(TagsList, ListTags)).

hasTag(TagsList, []):-false.
hasTag(TagsList, [H|T]):-member(H,TagsList),!;hasTag(TagsList,T).



%%pesquisa de users para sugerir com base nas conexões em comum
filterByConnections(User,U,R):-directConnections(User,L), filterByConnections(L,U,[],R).

filterByConnections(L,[],R1,R1).
filterByConnections(L,[U|T],R,R1):-
    checkConnections(L,U), (filterByConnections(L,T,[U|R],R1),!;filterByConnections(L,T,R,R1)).

%%este método retorna true ou false, dependendo se o user possui uma connection em comum ou não
checkConnection(ConnectionsList, User):-
    directConnections(User,L),
    (hasConnection(ConnectionsList,L)).

hasConnection(ConnectionsList, []):-false.
hasConnection(ConnectionsList,[H|T]):-member(H,ConnectionsList),!;hasConnection(ConnectionsList,T).



%%junta 2 listas e remove elementos repetidos
joinLists([],L,L).
joinLists([X|L],L1,LU):-member(X,L1),!,joinLists(L,L1,LU).
joinLists([X|L],L1,[X|LU]):-joinLists(L,L1,LU).