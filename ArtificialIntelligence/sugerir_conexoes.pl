:- module(sugerir_conexoes, [suggestConnections/3,
                                    verifySemantic/2, 
                                    getUsers/3,
                                    directConnections/2, 
                                    moreFriends/3, 
                                    friendsoffriends/2,
                                    filterSugestionsByTag/3]).

suggestConnections(User, Level, Sugestions):-
        no(User,_,ListTags),
        verifySemantic(ListTags,TagResult),                                       
        getUsers(User, Level, UsersList), 
        filterSugestionsByTag(TagResult, UsersList, List),
        possiblePath(User,List,Sugestions).   

verifySemantic(L,L2):-verifySemantic(L,[],L2).
verifySemantic([],L,L):-!.
verifySemantic([H|T],L,L2):-tag(H,Lista),verifySemantic(T,[[H|Lista]|L],L2),!;verifySemantic(T,[H|L],L2).

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
findall(X,ligacao(Origem,X,_,_),L).

moreFriends(L2,X,0):-reverse(L2,X),!.

moreFriends(L,Tamanho,N):-
	friendsoffriends(L,L2),
	N1 is N-1,
	moreFriends(L2,Tamanho,N1),!.


friendsoffriends([],[]):-!.
friendsoffriends([H|T],LR):-
	directConnections(H,L),
	friendsoffriends(T,L2),union(L,L2,LR).

filterSugestionsByTag(L,U,R):- filterSugestionsByTag(L,U,[],R).

filterSugestionsByTag(L,[],R1,R1).
filterSugestionsByTag(L,[U|T],R,R1):-
    checkTag(L,U), (filterSugestionsByTag(L, T, [U|R],R1),!; filterSugestionsByTag(L, T, R, R1)).

checkTag(TagsList, User):-
    no(User,_,ListTags),
    (hasTag(TagsList, ListTags)).

hasTag(TagsList, []):-false.
hasTag(TagsList, [H|T]):-member(H,TagsList),!;hasTag(TagsList,T).

 possiblePath(User,[],[]):-!.
 possiblePath(User, [H|T], [H|Result]):-
    no(H,Name,_), no(User, UserName, _),
    intermediary(Name, UserName, Cam), length(Cam,R),
    R>0
    ->(possiblePath(User,T, Result))
     ;(possiblePath(User,T, Result)).


intermediary(Orig, Dest, Cam):- dfs(Orig, Dest, Cam)
                                ->true
                                    ;Cam = [].
