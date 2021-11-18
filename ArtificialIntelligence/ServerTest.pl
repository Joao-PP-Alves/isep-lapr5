helloworld(_Request):-
	format('Content-type: text/plain~n~n'),
	format('Hello World!~n').

:-http_handler('/test', helloworld, []).

:-use_module(library(http/thread_httpd)).
:-use_module(library(http/http_dispatch)).
:-use_module(library(http/http_client)).
:-use_module(library(http/http_parameters)).

:-http_handler('/register_user', register_user, []).

:-dynamic register_user/3.
:-dynamic user/3.


server(Port):-
	http_server(http_dispatch, [port(Port)]).

