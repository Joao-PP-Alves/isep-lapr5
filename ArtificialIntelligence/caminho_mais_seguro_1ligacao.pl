:- module(caminho_mais_seguro_1ligacao, [dfsLength_safest_oneWay/5,
											plan_safestlig_oneWay/5]).

:-dynamic melhor_sol_safest_oneWay/2.

dfsLength_safest_oneWay(Orig,Dest,Min,Cam,Len):-dfs2Length_safest_oneWay(Orig,Dest,Min,[Orig],Cam,Len).

dfs2Length_safest_oneWay(Dest,Dest,_,LA,Cam,0):-!,reverse(LA,Cam).
dfs2Length_safest_oneWay(Act,Dest,Min,LA,Cam,Len):-
	no(NAct,Act,_),
	ligacao(NAct,NX,Len1),
	Len1 > Min,
    no(NX,X,_),
	\+ member(X,LA),
	dfs2Length_safest_oneWay(X,Dest,Min,[X|LA],Cam,Len2),
    Len is Len2 + Len1.


plan_safestlig_oneWay(Orig,Dest,Min,LCaminho_maxlig,LCaminho_length):-
		get_time(Ti),
		(melhor_caminho_safest_oneWay(Orig,Dest,Min);true),
		retract(melhor_sol_safest_oneWay(LCaminho_maxlig,LCaminho_length)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

melhor_caminho_safest_oneWay(Orig,Dest,Min):- asserta(melhor_sol_safest_oneWay(_,0)),
		dfsLength_safest_oneWay(Orig,Dest,Min,LCaminho,Len),
		atualiza_melhor_safest_oneWay(LCaminho,Len),
		fail.

atualiza_melhor_safest_oneWay(LCaminho,LLength):-melhor_sol_safest_oneWay(_,N),
		LLength > N,retract(melhor_sol_safest_oneWay(_,_)),
		asserta(melhor_sol_safest_oneWay(LCaminho,LLength)).