import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Links from "../../Links";
import { useState, useEffect } from "react";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";
import TextareaAutosize from '@mui/material/TextareaAutosize';
import Container from '@mui/material/Container';


const Alert = React.forwardRef(function Alert(props, ref) {
	return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function RequestConnection({ requesterId, targetName, render }) {
	console.log("Requester Id = " + requesterId);
	console.log("Target Name = " + targetName);
	const [open, setOpen] = React.useState(false);

	const [setRequesterEmail] = useState("");
	const [input_description, setDescription] = useState("");

	const [openSnackBar, setOpenSnackBar] = React.useState(false);
	const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

	const [makingRequest, setMakingRequest] = useState(false);
	const [targetId,setTargetId] = useState("");


	const handleClickOpen = async () => {
		getByNameTarget();
		setOpen(true);

	};

	const handleClose = () => {
		setOpen(false);
	};

	const handleCloseError = (event, reason) => {
		setOpenSnackBarError(false);
	};

	const connection_description = {
		text: input_description,
	};

	const getByNameTarget = async () => {
		const response2 = await fetch(Links.MDR_URL() + "users/ByName/" + targetName);

		const response3 = await response2.json().then((data) => {
			var obj = data[0];
			setTargetId(obj.id);
			console.log(targetId);
		}
		);
	}



	function handleSendRequest(event) {
		event.preventDefault();
		setMakingRequest(true);
		console.log(targetId);
		getByNameTarget();
		console.log("{\"requester\": {\"value\":\"" + requesterId + "\"},\"targetUser\":{\"value\":\"" + targetId + "\"},\"description\": {\"text\":\"" + input_description + "\"}}");
		const response = fetch(
			Links.MDR_URL() + "Connections",

			{
				method: "POST",
				headers: { "Content-Type": "application/json" },
				body: "{\"requester\": {\"value\":\"" + requesterId + "\"},\"targetUser\":{\"value\":\"" + targetId + "\"},\"description\": {\"text\":\"" + input_description + "\"}}",
			}
		);
		console.log(response);
		handleClose();
		alert("Done!");
	}


	return (
		<div>
			<Snackbar
				anchorOrigin={{ vertical: "top", horizontal: "center" }}
				open={openSnackBar}
				autoHideDuration={1000}
				onClose={handleClose}
			>
				<Alert onClose={handleClose} severity="success" sx={{ width: "100%" }}>
					The request has been sent successfully!
				</Alert>
			</Snackbar>
			<Snackbar
				anchorOrigin={{ vertical: "top", horizontal: "center" }}
				open={openSnackBarError}
				autoHideDuration={1500}
				onClose={handleCloseError}
			>
				<Alert
					onClose={handleCloseError}
					severity="error"
					sx={{ width: "100%" }}
				>
					Failed to send request.
				</Alert>
			</Snackbar>
			{render(handleClickOpen)}
			<Dialog
				open={open}
				keepMounted
				onClose={handleClose}
				aria-describedby="alert-dialog-slide-description"
			>
				<DialogTitle>{"Request Connection"}</DialogTitle>
				<DialogContent>
					{"Do you want to send a friendship request to " +
						targetName + "?"}
				</DialogContent>
				<Container sx={{ mf: 50}}>
					<TextareaAutosize
						aria-label="Description"
						minRows={3}
						maxRows={5}
						onChange={(e) => setDescription(e.target.value)}
						placeholder="Description to target"
						style={{ width: 450, }}
					/>
				</Container>
				<DialogActions>
					<Button onClick={handleSendRequest}>Request</Button>
					<Button variant="outlined" onClick={handleClose}>
						Cancel
					</Button>
				</DialogActions>
			</Dialog>
		</div>
	);
}
