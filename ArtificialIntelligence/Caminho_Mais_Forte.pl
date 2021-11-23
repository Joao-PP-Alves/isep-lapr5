%Determinar o caminho mais forte (maximiza o somatório das forças de
% ligação) para determinado utilizador

:-dynamic melhor_sol_maxlig/2.

dfsLength(Orig,Dest,Cam,Len):-dfs2Length(Orig,Dest,[Orig],Cam,Len).

dfs2Length(Dest,Dest,LA,Cam,0):-!,reverse(LA,Cam).
dfs2Length(Act,Dest,LA,Cam,Len):-no(NAct,Act,_),(ligacao(NAct,NX,Len1,_);ligacao(NX,NAct,_,Len1)),
    no(NX,X,_),\+ member(X,LA),dfs2Length(X,Dest,[X|LA],Cam,Len2),
    Len is Len2 + Len1.


plan_maxlig(Orig,Dest,LCaminho_maxlig,LCaminho_length):-
		get_time(Ti),
		(melhor_caminho_maxlig(Orig,Dest);true),
		retract(melhor_sol_maxlig(LCaminho_maxlig,LCaminho_length)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

melhor_caminho_maxlig(Orig,Dest):- asserta(melhor_sol_maxlig(_,0)),
		dfsLength(Orig,Dest,LCaminho,Len),
		atualiza_melhor_maxlig(LCaminho,Len),
		fail.

atualiza_melhor_maxlig(LCaminho,LLength):-melhor_sol_maxlig(_,N),
		LLength > N,retract(melhor_sol_maxlig(_,_)),
		asserta(melhor_sol_maxlig(LCaminho,LLength)).
