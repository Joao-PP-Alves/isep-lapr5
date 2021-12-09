:- module(caminho_mais_forte, [dfsLength/4,
								plan_maxlig/5]).

:-dynamic melhor_sol_maxlig/2.
:-dynamic conta_sol/1.

dfsLength(Orig,Dest,Cam,Len):-dfs2Length(Orig,Dest,[Orig],Cam,Len).

dfs2Length(Dest,Dest,LA,Cam,0):-!,reverse(LA,Cam).
dfs2Length(Act,Dest,LA,Cam,Len):-no(NAct,Act,_),
        (ligacao(NAct,NX,Len1,Len3)),
        no(NX,X,_),
        \+ member(X,LA),
        dfs2Length(X,Dest,[X|LA],Cam,Len2),
        Len is Len2 + Len1 + Len3.

plan_maxlig(Orig,Dest,LCaminho_maxlig,LCaminho_length,N):-
		get_time(Ti),
		(melhor_caminho_maxlig(Orig,Dest);true),
		retract(melhor_sol_maxlig(LCaminho_maxlig,LCaminho_length)),
		retract(conta_sol(N)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

melhor_caminho_maxlig(Orig,Dest):- asserta(melhor_sol_maxlig(_,-9999)),asserta(conta_sol(0)),
		dfsLength(Orig,Dest,LCaminho,Len),
		atualiza_melhor_maxlig(LCaminho,Len),
		fail.

atualiza_melhor_maxlig(LCaminho,LLength):-retract(conta_sol(NS)),NS1 is NS+1,asserta(conta_sol(NS1)),melhor_sol_maxlig(_,N),
		LLength > N,retract(melhor_sol_maxlig(_,_)),
		asserta(melhor_sol_maxlig(LCaminho,LLength)).
