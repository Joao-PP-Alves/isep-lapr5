import React, {useState, useEffect} from "react";
import "../../../App.css";
import "../../../css/create.css";

function CreateUser({show, onClose}) {
    const {register, handleSubmit, errors} = useForm();

    const [allUsersTypes, setAllUserTypes] = useState([]);
    const [userTypes, setAllUserTypes] = useState([]);
    
    useEffect(() => {
        fetchAllUserTypes();
        
    }, []);

    const fetchAllUserTypes = async() => {
        const data = await fetch(config.apiURL + "/")
    }
}