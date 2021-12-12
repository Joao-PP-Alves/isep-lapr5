:- module(caminho_mais_seguro_1ligacao, [dfsLength_safest_oneWay/5,
											plan_safestlig_oneWay/6]).

:-dynamic melhor_sol_safest_oneWay/2.
:-dynamic conta_sol/1.

dfsLength_safest_oneWay(Orig,Dest,Min,Cam,Len):-dfs2Length_safest_oneWay(Orig,Dest,Min,[Orig],Cam,Len).

dfs2Length_safest_oneWay(Dest,Dest,_,LA,Cam,0):-!,reverse(LA,Cam).
dfs2Length_safest_oneWay(Act,Dest,Min,LA,Cam,Len):-
	no(NAct,Act,_),
	ligacao(NAct,NX,Len1,Len3),
	Len1 > Min,
	Len3 > Min,
    no(NX,X,_),
	\+ member(X,LA),
	dfs2Length_safest_oneWay(X,Dest,Min,[X|LA],Cam,Len2),
    Len is Len2 + Len1 + Len3.


plan_safestlig_oneWay(Orig,Dest,Min,LCaminho_maxlig,LCaminho_length,NS):-
		get_time(Ti),
		(melhor_caminho_safest_oneWay(Orig,Dest,Min);true),
		retract(melhor_sol_safest_oneWay(LCaminho_maxlig,LCaminho_length)),retract(conta_sol(NS)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl,
		write('Numero de solucoes:'),write(NS),nl.

melhor_caminho_safest_oneWay(Orig,Dest,Min):- asserta(melhor_sol_safest_oneWay(_,0)),asserta(conta_sol(0)),
		dfsLength_safest_oneWay(Orig,Dest,Min,LCaminho,Len),
		atualiza_melhor_safest_oneWay(LCaminho,Len),
		fail.

atualiza_melhor_safest_oneWay(LCaminho,LLength):-retract(conta_sol(NS)),NS1 is NS+1,asserta(conta_sol(NS1)),melhor_sol_safest_oneWay(_,N),
		LLength > N,retract(melhor_sol_safest_oneWay(_,_)),
		asserta(melhor_sol_safest_oneWay(LCaminho,LLength)).