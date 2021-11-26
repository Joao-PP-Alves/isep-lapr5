import { useContext } from "react";
import { UserContext } from '../context/userController';

//check the logged user in the system
export default function userAuth(){
    const userContext = useContext(UserContext);
    return userContext;
}