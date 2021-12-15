:- module(caminho_mais_curto, [one_dfs/3,
								dfs/3,
								all_dfs/3,
								plan_minlig/4]).

:-dynamic melhor_sol_minlig/2.
:-dynamic conta_sol/1.

all_dfs(Nome1,Nome2,LCam):-get_time(T1),
    findall(Cam,dfs(Nome1,Nome2,Cam),LCam),
    length(LCam,NLCam),
    get_time(T2),
    write(NLCam),write(' solucoes encontradas em '),
    T is T2-T1,write(T),write(' segundos'),nl,
    write('Lista de Caminhos possiveis: '),write(LCam),nl,nl.

one_dfs(Orig, Dest, Cam) :- dfs(Orig, Dest, Cam), !.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam).

dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).

dfs2(Act,Dest,LA,Cam):-melhor_sol_minlig(_,C),length(LA,D),D<C,no(NAct,Act,_),ligacao(NAct,NX,_,_),no(NX,X,_),\+member(X,LA),dfs2(X,Dest,[X|LA],Cam).

plan_minlig(Orig,Dest,LCaminho_minlig,N):-
		get_time(Ti),
		(melhor_caminho_minlig(Orig,Dest);true),
		retract(melhor_sol_minlig(LCaminho_minlig,_)),
		retract(conta_sol(N)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

melhor_caminho_minlig(Orig,Dest):-
		asserta(melhor_sol_minlig(_,10000)),
		asserta(conta_sol(0)),
		dfs(Orig,Dest,LCaminho),
		atualiza_melhor_minlig(LCaminho),
		fail.

atualiza_melhor_minlig(LCaminho):-retract(conta_sol(NS)),
		NS1 is NS+1,asserta(conta_sol(NS1)),
		melhor_sol_minlig(_,N),
		length(LCaminho,C),
		C<N,retract(melhor_sol_minlig(_,_)),
		asserta(melhor_sol_minlig(LCaminho,C)).