//import logo from './logo.svg';
import './App.css';
import { 
  BrowserRouter as Router, 
  Route,
  Switch, 
  Redirect
} from 'react-router-dom';
import React from 'react';
//import { Switch } from "react-router";
import User from "./components/mdr/user/user";
import CreateUser from './components/mdr/user/createUser';
import UseAuth from "./hooks/UserAuth";
import { UserContext, UserController } from './context/UserController';
import UserList from './components/mdr/user/UserList';
import LogIn from './components/pages/Login';
import SignUp from './components/pages/Signup';
import TermsAndConditions from './components/pages/termsAndConditions';
import PrivacyPolicy from './components/pages/privacyPolicy';
import Users from './components/mdr/user/user'
import ListPendentConnections from './components/pages/connections/ListPendentConnections';
import Dashboard from './components/pages/dashboard/Dashboard';
import EditProfile from './components/pages/editProfile/EditProfile';
import ListFriends from './components/pages/friends/ListFriends';
import ListPendentIntroductions from './components/pages/introductions/ListPendentIntroductions';
//import { Navbar } from 'react-bootstrap';


function App() {

  return(
  /*<UserController>
      <Router>
        <div className="App"></div>
      </Router>
      <Routes/>
  </UserController>*/
  /*<div className = "App">
    <SignUp />
  </div>*/
  <>
  <Router>
    <Switch>

      <Route exact path="/" component={SignUp} />
      <Route path="/login" component={LogIn}/>
      <Route path="/termsConditions" component={TermsAndConditions}/>
      <Route path="/privacyPolicy" component={PrivacyPolicy}/>
      <Route path="/users" component={Users}/>
      <Route path="/connections/pendent" component={ListPendentConnections}/>
      <Route path="/dashboard" component={Dashboard}/>
      <Route path="/editProfile" component={EditProfile}/>
      <Route path="/introductions/pendent" component={ListPendentIntroductions}/>
      <Route path="/friends" component={ListFriends}/>
      <Redirect to="/"/>
    </Switch>
  </Router>
  </>

  );
}

 /* return (
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
}*/

   /* return (
      <userController>
        <Router>
          <div className = "App">
            <p>
              Edit <code>ola</code>ola outra vez.
            </p>
          </div>
        </Router>
      </userController>
  );
    }*/



function Routes(){
 /* const {user, tempLoggedIn} = UseAuth();

  if(!tempLoggedIn && !user){
    return(
      <Routes path="*"> //o path será o que especificarmos
        <Redirect to = "/user" />
      </Routes>
    );
  }*/


  return(
    <>
    <Routes path="/user" exact>
      <User />
    </Routes>
    <Routes path="/user/createUser" exact>
      <CreateUser />
    </Routes>
    </>
  );
}

export default App;
