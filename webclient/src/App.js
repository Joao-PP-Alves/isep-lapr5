import logo from './logo.svg';
import './App.css';
import {Router,Route, BrowserRouter} from 'react-router-dom';
import React from 'react';

//import CreateUser from "./components/mdr/user/createUser";
//import User from "./components/mdr/user/user";
import CreateUser from './components/mdr/user/createUser';
import { Navbar } from 'react-bootstrap';

function App() {

  /*return (
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
    </div>*/

    //about! ver o que é
   /* return (
      <userController>
        <Router>
          <div className = "App">
            <Navbar />
            <Switch>
              <Route path="/" exact>
                <Home />
              </Route>
              <Route path = "/about" exact> 
                <About />{" "}
              </Route>
              <Route path="/login" exact>
                <Login />
              </Route>
            <Routes />
            </Switch>
          </div>
        </Router>
      </userController>
  );*/
  <div className = "wrapper">
    <h1>Marine Mammals</h1>
      <BrowserRouter>
      <Switch>
        <Route path="/">
          <Manatee />
        </Route>
      </Switch>
      </BrowserRouter>
  </div>
}

function Routes(){
  const {user, tempLoggedIn} = useAuth();

  if(!tempLoggedIn && !user){
    return(
      <Route path="*"> //o path será o que especificarmos
        <Redirect to = "/" />
      </Route>
    );
  }

  return(
    <>
    <Routes path="/user" exact>
      <User />
    </Routes>
    <Routes path="/user/createUser" exact>
      <CreateUser />
    </Routes>
    </>
  )
}

export default App;
