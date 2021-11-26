%%sugere amigos com base nas tags em comum e nas conexões em comum, tendo em conta n níveis

%%DEPOIS MUDAR ISTO PARA FAZER SÓ A PARTIR DO USER E NÃO COM TAGSLIST E CONNECTIONS
suggestConnections(User, Level, Sugestions):-
        no(User,_,ListTags),
        verifySemantic(ListTags,TagResult),                                       
        getUsers(User, Level, UsersList), 
            %%aqui ver como se passa lista
        filterSugestionsByTag(TagResult, UsersList, List),
        possiblePath(User,List,List,Sugestions).   

%verificar a semantica das tags do user

verifySemantic(L,L2):-verifySemantic(L,[],L2).
verifySemantic([],L,L):-!.
verifySemantic([H|T],L,L2):-tag(H,Lista),verifySemantic(T,[[H|Lista]|L],L2),!;verifySemantic(T,[H|L],L2).

%%retorna uma lista com os users que se encontram até n níveis

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

checkTag(TagsList, User):-
    no(User,_,ListTags),
    (hasTag(TagsList, ListTags)).

hasTag(TagsList, []):-false.
hasTag(TagsList, [H|T]):-member(H,TagsList),!;hasTag(TagsList,T).

%verifica se é possível chegar aos nodes passando somente por nodes que tenham tags em comum
possiblePath(User,[],_,_).

possiblePath(User,[H|T],L,Result):-
    dfs(User,H,Cam), union(Cam,T,Temp), length(Temp,V1),length(L,V2),
    (V1==V2), (possiblePath(User,T, L, [H|Result]),!; possiblePath(User,T,L,Result)).

%%junta 2 listas e remove elementos repetidos
joinLists([],L,L).
joinLists([X|L],L1,LU):-member(X,L1),!,joinLists(L,L1,LU).
joinLists([X|L],L1,[X|LU]):-joinLists(L,L1,LU).