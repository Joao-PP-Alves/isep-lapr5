import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import axios from 'axios';
import Links from '../../Links';

export default function AlertDialogSlide({connectionId,render}) {

  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
      setOpen(false);
  };

  const handleAccept = async () => {
    const requesterData = await axios.put(
        Links.MDR_URL() + "connections/accept/" + connectionId.id
      );
      handleClose();
      alert("Connection Accepted!");

  }

  const handleDecline = async () => {
    const requesterData = await axios.put(
        Links.MDR_URL() + "connections/decline/" + connectionId.id
      );
      handleClose();
      alert("Connection Declined!");
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
        <DialogTitle>{"Connection Actions"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-slide-description">
            Choose one of the options:
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button variant="contained" color="success"onClick={handleAccept}>Accept</Button>
          <Button variant="outlined" color="error" onClick={handleDecline}>Decline</Button>
          <Button variant="outlined" onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}