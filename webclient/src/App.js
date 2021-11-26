import logo from './logo.svg';
import './App.css';
//import {Router} from 'react-router-dom';
import React from 'react';

//import CreateUser from "./components/mdr/user/createUser";
//import User from "./components/mdr/user/user";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to  oi reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>

    
    

  );
}

/*function Routes(){
  const {user, tempLoggedIn} = useAuth();

  if(!tempLoggedIn && !user){
    return(
      <Route path="*"> //o path será o que especificarmos
        <Redirect to = "/" />
      </Route>
    );
  }
}*/

export default App;
