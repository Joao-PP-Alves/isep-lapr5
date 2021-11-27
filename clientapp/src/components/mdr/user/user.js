import React, {useState, useEffect} from 'react';
import '../../../App.css';
import CreateUser from '../user/createUser';
import { Link } from 'react-router-dom';

function User(){
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetchUsers();
    },[]);

    const fetchUsers = async () => {
        const data = await fetch('https://localhost:5001/api/Users');
        const users = await data.json();
        console.log(users);
        setUsers(users);
    }

    const [show, setShow] = useState(false);

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