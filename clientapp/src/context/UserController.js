import React, {useEffect, useState} from "react";
export const AUTH_KEY = "_USER_TOKEN_";
export const UserContext = React.createContext();

export function UserController({children}){
    const [user,setUser] = useState(null);
    const[tempLoggedIn, setTempLoggedIn] = useState(true);

    function handleLogin(user){
        setUser(user); //updates the logged in user
        setTempLoggedIn(true);
        localStorage.setItem(AUTH_KEY, user.id);
    }

    function handleLogout(){
        setUser(null);  
        setTempLoggedIn(false);
        localStorage.removeItem(AUTH_KEY);
    }

    useEffect(() => {
        const userToken = localStorage.getItem(AUTH_KEY);

        if(!userToken){
            setTempLoggedIn(false);
            return undefined;
        }
        fetch(`https://localhost:5001/api/Users/${userToken}`)
        .then((response) => {
            return response.json();
        }).then((user) => {
            setUser(user);
        });
    }, []);

    return (
        <UserContext.Provider
            value={{
                user, 
                tempLoggedIn,
                handleLogin,
                handleLogout,
            }}
        >
            {children}
        </UserContext.Provider>
    );
}