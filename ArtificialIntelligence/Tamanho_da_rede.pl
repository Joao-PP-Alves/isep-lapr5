% Tamanho da rede.
calcula_tamanho(_,0,Tamanho):-!,Tamanho is 1.
calcula_tamanho(Origem,N,Tamanho):-
	N1 is N-1,
	amigos_proximos(Origem,L),
	append([Origem],L,LX),
	mais_amigos(LX,Tamanho,N1).

amigos_proximos(Origem,L):-
findall(X,(ligacao(Origem,X,_,_);ligacao(X,Origem,_,_)),L).

mais_amigos(L2,X,0):-length(L2,X),!.

mais_amigos(L,Tamanho,N):-
	amigos_dos_amigos(L,L2),
	N1 is N-1,
	mais_amigos(L2,Tamanho,N1),!.

amigos_dos_amigos([],[]):-!.

amigos_dos_amigos([H|T],LR):-
	amigos_proximos(H,L),
	amigos_dos_amigos(T,L2),union(L,L2,LR).