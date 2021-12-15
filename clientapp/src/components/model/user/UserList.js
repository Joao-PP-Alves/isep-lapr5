import React from "react";

function userList(){
    const users =['olinda', 'olavo']
    const usersList = users.map(user => <h2>{user}</h2>)
    return(
        <div>
            {usersList}
        </div>
    )
}

export default userList