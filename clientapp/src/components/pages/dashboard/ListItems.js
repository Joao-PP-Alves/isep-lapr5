import * as React from 'react';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import PeopleIcon from '@mui/icons-material/People';
import BarChartIcon from '@mui/icons-material/BarChart';
import LayersIcon from '@mui/icons-material/Layers';
import AssignmentIcon from '@mui/icons-material/Assignment';
import DashBoard from '../dashboard/Dashboard';
import Button from '@mui/material/Button';


function toDashboard(event){
  <DashBoard/>
}

function toEdit(event){
  let path =`/editProfile`;
}

export const mainListItems = (
  <div>
    <ListItem>
       <Button id="dashboardButton" onClick={event => window.location.href='/dashBoard'}>
      <ListItemIcon>
        <DashboardIcon />
      </ListItemIcon>
      <ListItemText primary="Dashboard" />
      </Button>
    </ListItem>
    <Button id="editProfileButton" onClick={event => window.location.href='/editProfile'}>
    <ListItem>
      <ListItemIcon>
        <ShoppingCartIcon />
      </ListItemIcon>
      <ListItemText primary="Edit profile" />  
    </ListItem>
    </Button>
    <Button id="friendsButton" onClick={event => window.location.href='/friends'}>
    <ListItem button>
      <ListItemIcon>
        <PeopleIcon />
      </ListItemIcon>
      <ListItemText primary="Friends" />
    </ListItem>
    </Button>
    <Button id = "connectionsButton" onClick={event => window.location.href='/connections/pendent'}>
    <ListItem button>
      <ListItemIcon>
        <BarChartIcon />
      </ListItemIcon>
      <ListItemText primary="Pending Connections" />
    </ListItem>
    </Button>
    <Button id="introductions" onClick={event => window.location.href='/introductions'}>
    <ListItem button>
      <ListItemIcon>
        <LayersIcon />
      </ListItemIcon>
      <ListItemText primary="Pending Introductions" />
    </ListItem>
    </Button>
  </div>
);