todas_combinacoes(X,LTags,LcombXTags):-findall(L,xCombinacoesTags(X,LTags,L),LcombXTags).

xCombinacoesTags(0,_,[]):-!.
xCombinacoesTags(X,[Tag|L],[Tag|T]):-X1 is X-1,xCombinacoesTags(X1,L,T).
xCombinacoesTags(X,[_|L],T):-xCombinacoesTags(X,L,T).