findAllConnectionsByTag(Tag, List):-tag(Tag, ListTag1), 
	append([Tag], ListTag1, ListTag),
	findAllConnectionsByTag_helper(ListTag, ListTemp),
	append(ListTemp, List).

findAllConnectionsByTag_helper([],[]).

findAllConnectionsByTag_helper([Head|Tail], [DefList|List]):-findall((UserA,UserB),(connects(UserA,UserB,_,ListTags),hasTag(ListTags, Head)), DefList),
	findAllConnectionsByTag_helper(Tail, List).

hasTag([Tag|_], Tag).

hasTag([_|List], Tag):-hasTag(List, Tag).