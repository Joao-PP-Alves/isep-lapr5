import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Links from '../../Links';
import TextField from '@mui/material/TextField';

export default function MakeIntroductionDialog({friendshipId,render}) {

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
        Links.MDR_URL() + "users/ConnectionStrength/" + friendshipId.userid,
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

  const handleConfirm = async () => {
      
      handleClose();
}

  return (
    <div>
        {render(handleClickOpen)}
      <Dialog
        fullWidth
        maxWidth="sm"
        open={open}
        keepMounted
        onClose={handleClose}
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogTitle>{"Make Introduction"}</DialogTitle>
        <DialogContent>
        </DialogContent>
        <DialogContent>
          TO DO
        </DialogContent>
        <DialogActions>
        <Button variant="contained" color= 'success' onClick={handleConfirm}>Confirm</Button>
          <Button variant="outlined" onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}