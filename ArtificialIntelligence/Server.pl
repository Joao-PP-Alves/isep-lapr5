%Base de conhecimento
:- dynamic no/3.
:- dynamic ligacao/3.


% Bibliotecas 
:- use_module(library(http/thread_httpd)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_parameters)).
:- use_module(library(http/http_open)).
:- use_module(library(http/http_cors)).
:- use_module(library(ssl)).

% Bibliotecas JSON
:- use_module(library(http/json_convert)).
:- use_module(library(http/http_json)).
:- use_module(library(http/json)).

% Nossos Modulos
:- use_module(caminho_mais_curto).
:- use_module(caminho_mais_forte_dupla_soma).
:- use_module(caminho_mais_forte).
:- use_module(caminho_mais_seguro_1ligacao).
:- use_module(caminho_mais_seguro_2ligacoes).
:- use_module(sugerir_conexoes).
:- use_module(tag_semantic).
:- use_module(tamanho_da_rede).


% JSON Objects
:- json_object friendship(requester:string, friend:string, connection_strength:float, relationship_strength:float, friendship_tag_id:string).
:- json_object tag(id:string, name:string).
:- json_object caminho_json(path:list(no_json)).
:- json_object no_json(email:string).


%Cors
:- set_setting(http:cors, [*]). 
% Relacao entre pedidos HTTP e predicados que os processam

:- http_handler('/create', create, []).
:- http_handler('/shortpath', shortestPath, []).
:- http_handler('/strongpath', strongestPath, []).







% ABRIR E FECHAR O SERVIDOR
server(Port) :-
        http_server(http_dispatch, [port(Port)]).

stop(Port) :- 
	http_stop_server(Port, []).



% URLS PARA FAZER PEDIDOS À API
users_url("https://21s5dd20socialgame.azurewebsites.net/api/Users").


% MÉTODOS PARA FAZER REQUESTS À API PARA IR BUSCAR DADOS À BASE DE DADOS
obter_users(Data) :- 
        users_url(URL),
        setup_call_cleanup(
            http_open(URL, In, []),
            json_read_dict(In, Data),
            close(In)).


% MÉTODOS PARA ADICIONAR DADOS À BASE DE CONHECIMENTO
adicionar_users() :- 
        obter_users(Data),
        %write(Data),
        parse_users(Data, Lids).


% PARSE DE USERS PARA ADICIONAR À BASE DE CONHECIMENTO
parse_users([], []).
parse_users([H|Data], [H.get(email)|Lids]) :-
    Email = H.get(email),
    Address = Email.get(emailAddress),
    Id = H.get(id),
    parse_tags(H.get(tags), Tags),
    \+no(Id,Address,Tags)
    -> assertz(no(Id,Address,Tags)), 
    adicionar_friendships(H.get(friendsList)),
    parse_users(Data,Lids);
    parse_users(Data,Lids).


parse_tags([], []).
parse_tags([H|Data], [H.get(name)|Lids]) :-
    TagName = H.get(name),
    parse_tags(Data, Lids).

adicionar_friendships(Friendships) :- parse_friendships(Friendships).

parse_friendships([]).
parse_friendships([H|Data]) :- (
    Strength = H.get(connection_strength),
    Friend = H.get(friend),
    Requester = H.get(requester),
    \+ atom(Strength),
    Strength_value = Strength.get(value),
    Friend_value = Friend.get(value),
    Requester_value = Requester.get(value),
    \+ligacao(Requester_value, Friend_value, Strength_value)
    -> assertz(ligacao(Requester_value, Friend_value, Strength_value)),
    parse_friendships(Data)) ; parse_friendships(Data).


% CRIA A BASE DE CONHECIMENTO COM VÁRIOS DADOS
adicionar_base_conhecimento() :-
    adicionar_users(). 



% OBTEM O CAMINHO MAIS CURTO ENTRE DOIS USERS
create(Request) :- 
    cors_enable,
    format('Content-type: text/plain~n~n'),
    adicionar_base_conhecimento().
    
shortestPath(Request) :-
    cors_enable,
    adicionar_base_conhecimento(),
    http_parameters(Request,
                    [ orig(Orig, []),
                      dest(Dest, [])
                    ]),
    % Converte os dois atoms para strings
    atom_string(Orig, Orig_str),
    atom_string(Dest, Dest_str),

    format('Content-type: text/plain~n~n'),
    one_dfs(Orig_str, Dest_str, Cam),
    createJSONArray(Cam, JSON_Array),
    Reply = caminho_json(JSON_Array),
    prolog_to_json(Reply, X),
    reply_json(X, [json_object(dict)]).



strongestPath(Request) :-
    cors_enable,
    adicionar_base_conhecimento(),
    http_parameters(Request,
                    [ orig(Orig, []),
                      dest(Dest, [])
                    ]),
    % Converte os dois atoms para strings
    atom_string(Orig, Orig_str),
    atom_string(Dest, Dest_str),

    format('Content-type: text/plain~n~n'),
    plan_maxlig(Orig_str, Dest_str, Cam, Length),
    createJSONArray(Cam, JSON_Array),
    Reply = caminho_json(JSON_Array),
    prolog_to_json(Reply, X),
    reply_json(X, [json_object(dict)]).
    

createJSONArray([], []).
createJSONArray([Email|Rest], [User|JsonArray]) :- 
        User = no_json(Email),
        createJSONArray(Rest, JsonArray).

% DÁ RESTART NO SERVER
restart(Port) :- 
    stop(Port),
    consult(server),
    server(Port).