import * as React from 'react';
import {useState} from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';
import axios from 'axios';
import Links from '../../Links';
import TextField from '@mui/material/TextField';
import Grid from "@mui/material/Grid";
import Box from '@material-ui/core/Box'

export default function AlertDialogSlide({friendshipId,render}) {

  const [open, setOpen] = React.useState(false);

  const [input_connection_strength, setConnectionStrength] = React.useState("");
	const [input_tag, setTag] = React.useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
      setOpen(false);
  };

  const handleEditConnectionStrength = async () => {
      if(input_connection_strength.length == 0){
        alert("Empty input");
        return;
      }
      try{
        let num = parseInt(input_connection_strength);
        if(num > 100){
          alert("Connection Strength maxium value is \"100\"");
        }
        if(num < 1){
          alert("Connection Strength minimum value is \"1\"");
        }
      }catch(Exception){
        alert("Input is not a number");
      }
      const response = await fetch(
        Links.MDR_URL() + "/api/users/ConnectionStrength/" + friendshipId.userid,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: "{\"Id\": \""+friendshipId.id+"\",\n\"connection_strength\":\""+input_connection_strength+"\"}",
        }
      )
        .then((response) => response.json())
        .then((json) => console.log(json));

      alert("Success!");
      handleClose();

  }

  const handleEditTag = async () => {
      if(input_tag.length == 0){
        alert("Empty input");
        return;
      }
      const response = await fetch(
        Links.MDR_URL() + "/api/users/Tag/" + friendshipId.userid,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: "{\"Id\": \""+friendshipId.id+"\",\n\"tag\":\""+input_tag+"\"}",
        }
      )
        .then((response) => response.json())
        .then((json) => console.log(json));
      alert("Success!");
      handleClose();
}

  return (
    <div>
        {render(handleClickOpen)}
      <Dialog
        open={open}
        keepMounted
        onClose={handleClose}
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogTitle>{"Edit Friendship"}</DialogTitle>
        <DialogContent>
          <TextField id="connection_strength" label="Connection Strength" variant="filled" sx={{pr:2}}
          onChange={(e) => setConnectionStrength(e.target.value)}
          type="number"
          placeholder={'[1-100]'}
          InputProps={{ inputProps: { min: 1 } }}/>
            <Button size="medium" sx={{mt:1}}
            style={{backgroundColor: '#f7a41e', color: '#FFFFFF'}}
            onClick={handleEditConnectionStrength}
            >Edit</Button>
        </DialogContent>
        <DialogContent>
          <TextField id="tag" label="Tag" variant="filled" sx={{pr:2}}
          onChange={(e) => setTag(e.target.value)}/>
            <Button size="medium" sx={{mt:1}} 
            style={{backgroundColor: '#f7a41e', color: '#FFFFFF'}}
            onClick={handleEditTag}
            >Edit</Button>
        </DialogContent>
        <DialogActions>
          <Button variant="outlined" onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}