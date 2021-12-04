:- module(caminho_mais_forte_dupla_soma, [dfsLengthDouble/4,
											plan_maxlig2/4]).

:-dynamic melhor_sol_maxlig2/2.

dfsLengthDouble(Orig,Dest,Cam,Len):-dfs2LengthDouble(Orig,Dest,[Orig],Cam,Len).

dfs2LengthDouble(Dest,Dest,LA,Cam,0):-!,reverse(LA,Cam).
dfs2LengthDouble(Act,Dest,LA,Cam,Len):-no(NAct,Act,_),
		ligacao(NAct,NX,Len1),ligacao(NX,NAct,Len3),
		no(NX,X,_),
		\+ member(X,LA),
		dfs2LengthDouble(X,Dest,[X|LA],Cam,Len2),
		Len is Len2 + Len1 + Len3.

plan_maxlig2(Orig,Dest,LCaminho_maxlig,LCaminho_length):-
		get_time(Ti),
		(melhor_caminho_maxlig2(Orig,Dest);true),
		retract(melhor_sol_maxlig2(LCaminho_maxlig,LCaminho_length)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.


melhor_caminho_maxlig2(Orig,Dest):- asserta(melhor_sol_maxlig2(_,-9999)),
		dfsLengthDouble(Orig,Dest,LCaminho,Len),
		atualiza_melhor_maxlig2(LCaminho,Len),
		fail.

atualiza_melhor_maxlig2(LCaminho,LLength):-melhor_sol_maxlig2(_,N),
		LLength > N,retract(melhor_sol_maxlig2(_,_)),
		asserta(melhor_sol_maxlig2(LCaminho,LLength)).

