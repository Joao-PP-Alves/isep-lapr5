no(1,ana,[natureza,pintura,musica,sw,porto]).
no(11,antonio,[natureza,pintura,carros,futebol,lisboa]).
no(12,beatriz,[natureza,musica,carros,porto,moda]).
no(21,eduardo,[natureza,cinema,teatro,carros,coimbra]).
no(22,isabel,[natureza,musica,porto,lisboa,cinema]).
no(200,sara,[natureza,moda,musica,sw,coimbra]).

ligacao(1,11,10,2).
ligacao(11,1,8,2).
ligacao(11,12,2,3).
ligacao(12,11,6,3).
ligacao(12,21,5,1).
ligacao(21,12,7,1).
ligacao(11,22,2,1).
ligacao(22,11,-4,1).
ligacao(22,1,4,2).
ligacao(1,22,100,2).