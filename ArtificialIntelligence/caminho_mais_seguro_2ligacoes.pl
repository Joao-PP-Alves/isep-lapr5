:- module(caminho_mais_seguro_2ligacoes, [dfsLength_safest_twoWays/5,
											plan_safestlig_twoWays/6]).

:-dynamic melhor_sol_safest_twoWays/2.
:-dynamic conta_sol/1.

dfsLength_safest_twoWays(Orig,Dest,Min,Cam,Len):-dfs2Length_safest_twoWays(Orig,Dest,Min,[Orig],Cam,Len).

dfs2Length_safest_twoWays(Dest,Dest,_,LA,Cam,0):-!,reverse(LA,Cam).
dfs2Length_safest_twoWays(Act,Dest,Min,LA,Cam,Len):-
	no(NAct,Act,_),
	ligacao(NAct,NX,Len1,Len4),
	ligacao(NX,NAct,Len3,Len5),
	Len1 >= Min,
	Len4 >= Min,
	Len5 >= Min,
	Len3 >= Min,
    no(NX,X,_),
	\+ member(X,LA),
	dfs2Length_safest_twoWays(X,Dest,Min,[X|LA],Cam,Len2),
    Len is Len2 + Len1 + Len3.


plan_safestlig_twoWays(Orig,Dest,Min,LCaminho_maxlig,LCaminho_length,N):-
		get_time(Ti),
		(melhor_caminho_safest_twoWays(Orig,Dest,Min);true),
		retract(melhor_sol_safest_twoWays(LCaminho_maxlig,LCaminho_length)),
		retract(conta_sol(N)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

melhor_caminho_safest_twoWays(Orig,Dest,Min):- asserta(melhor_sol_safest_twoWays(_,0)),asserta(conta_sol(0)),
		dfsLength_safest_twoWays(Orig,Dest,Min,LCaminho,Len),
		atualiza_melhor_safest_twoWays(LCaminho,Len),
		fail.

atualiza_melhor_safest_twoWays(LCaminho,LLength):-retract(conta_sol(NS)),NS1 is NS+1,asserta(conta_sol(NS1)),melhor_sol_safest_twoWays(_,N),
		LLength > N,retract(melhor_sol_safest_twoWays(_,_)),
		asserta(melhor_sol_safest_twoWays(LCaminho,LLength)).