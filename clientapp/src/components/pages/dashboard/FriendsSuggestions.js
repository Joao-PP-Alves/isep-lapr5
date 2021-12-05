import * as React from 'react';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import Title from './Title';
import Links from '../../Links'
import List from '@mui/material/List';
import { styled } from '@mui/material/styles';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import MakeIntroductionDialog from './MakeIntroductionDialog';

function preventDefault(event) {
  event.preventDefault();
}

function generate(element) {
  return [0, 1, 2].map((value) =>
    React.cloneElement(element, {
      key: value,
    }),
  );
}

export default function FriendsSuggestions() {

  //get logged user
  const userId = localStorage.getItem('loggedInUser');

  const [open, setOpen] = React.useState(true);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  React.useEffect(() => {
    search();
  }, []);

  const [searchedVS, setSearchedVS] = React.useState([]);

  function search() {
    fetchFriendships();
  }

  const fetchFriendships= async () => {

    const data = await fetch(
      Links.MDR_URL() + "users/GetFriendsSuggestion/" + userId
    );
    const vsList = await data.json();
    console.log(vsList);
    setSearchedVS(vsList);

  };

  // transform json array to array sample[]

  let sample = [];

  for (var i = 0; i < searchedVS.length; i++){
    var obj = searchedVS[i];
	  
    console.log(obj);
    
    sample.push([obj.id,obj.name.text]);
  }

  function createData( id, name) {
    return { id, name };
  }

  // push the information from sample to rows

  let rows = [];

  for (let i = 0; i < searchedVS.length; i += 1) {
    rows.push(createData(...sample[i]));
  }

  const [dense, setDense] = React.useState(false);
  const [secondary, setSecondary] = React.useState(false);

  return (
    <React.Fragment>
      <Title> Friends sugestions </Title>
      <List sx={{
        width: '100%',
        maxWidth: 360,
        bgcolor: 'background.paper',
        position: 'relative',
        overflow: 'auto',
        maxHeight: 300,
        '& ul': { padding: 0 },
      }}>
      {rows.map((value) => {
        const labelId = value.id;
        return (
          <ListItem
            key={value.id}
            disablePadding
          >
            <MakeIntroductionDialog 
            render={(open) => (
              <ListItemButton onClick={open}>
              <ListItemAvatar>
                <Avatar/>
              </ListItemAvatar>
              <ListItemText id={labelId} primary={value.name} />
            </ListItemButton>
            )}/>
          </ListItem>
        );
      })}
    </List>
    </React.Fragment>
  );
}