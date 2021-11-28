import React, {useState, useEffect} from 'react';
import '../../../App.css';
import CreateUser from '../user/createUser';
import { Link } from 'react-router-dom';
import axios from 'axios';

function User(){
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetchAllUsers();
    },[]);

    const fetchUsers = async () => {
        const data = await fetch('https://localhost:5001/api/Users');
        const users = await data.json();
        console.log(users);
        setUsers(users);
    }

    const [show, setShow] = useState(false);

    const fetchAllUsers = () => {
        axios.get("http://localhost:5001/api/Users").then(function (response){
            alert(JSON.stringify(response.message));
        }).catch(function(error){ alert(error.message)
        console.log('fetchAllUsers',error);}).finally(function(){
            alert('Finally called');
        })
        setUsers(users);
      };

    return (
        <div data-testid = "userDivID" className = "w3-container">
            <h1>Users</h1>
                <h2>
                    Name
                    <h3>
                        
                    </h3>
                </h2>

        </div>
    )
}

export default User;