import { useContext } from "react";
import { UserContext } from '../context/UserController';

//check the logged user in the system
export default function useAuth(){
    const userContext = useContext(UserContext);
    return userContext;
}