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

const Alert = React.forwardRef(function Alert(props, ref) {
	return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function RequestConnection({ requesterId, targetName, render }) {
	console.log(requesterId + " " + targetName);
	const [open, setOpen] = React.useState(false);

    const [setRequesterEmail] = useState("");
    const [target_id, setTargetId] = useState("");
    const [input_description, setDescription] = useState("");

    const [openSnackBar, setOpenSnackBar] = React.useState(false);
	const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

    const [makingRequest, setMakingRequest] = useState(false);
	const [target, setTarget] = useState("");


	const handleClickOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
	};

    const handleCloseError = (event, reason) => {
			setOpenSnackBarError(false);
	};

    const connection_description={
        text: input_description,
    };

    const connection = {
			requester: requesterId,
            targetUser: target,
            description: connection_description,
			decision: "0",
    };

	/*useEffect(() => {
		getByNameTarget();
	}, []);*/

	function getByNameTarget(targetName) {
		console.log("está aqui por cima " + targetName);
		setMakingRequest(true);
		console.log("está aqui " + targetName);

		const response = async () => {
			const response2 = fetch(Links.MDR_URL() + "users/ByName/" +targetName);
			console.log("RESPONSE 2: "+response2);
			const response3 = await response2.json();
			console.log("RESPONSE 3:"+response3);
			setTarget(response2.id);
			console.log("TARGET AQUI:"+target);
			console.log(response2);
		}
			response();
		console.log(target);
	}



	 function handleSendRequest(event) {
		 	setTarget(getByNameTarget(targetName));
			event.preventDefault();
			setMakingRequest(true);
			console.log("requester:"+connection.requester);
			console.log("target:"+connection.targetUser);
			const response = fetch(
				Links.MDR_URL() + "Connections",

				{
					method: "POST",
					headers: { "Content-Type": "application/json" },
					body: JSON.stringify(connection),
				}
			)
				.then((response) => {
					response.json();
					if (!response.ok) {
						return null;
					} else {
					    setOpenSnackBar(true);
					}
					setMakingRequest(false);
				})
				.then((json) => console.log(json))

				.catch((err) => {
					setOpenSnackBarError(true);
					setMakingRequest(false);
				});
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
					{"Do you wanto to send a friendship request to " +
						targetName  + "?"}
				</DialogContent>
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
