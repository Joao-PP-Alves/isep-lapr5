import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Links from "../../Links";
import { useState } from "react";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";

const Alert = React.forwardRef(function Alert(props, ref) {
	return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function AlertDialogSlide({ friendshipId, render }) {
	const [open, setOpen] = React.useState(false);

    const [input_requester_name, setRequesterName] = useState("");
    const [input_target_name, setTargetName] = useState("");
    const [input_description, setDescription] = useState("");
    const [input_decision, setDecision] = useState("");

    const [openSnackBar, setOpenSnackBar] = React.useState(false);
	const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

    const [makingRequest, setMakingRequest] = useState(false);

	const handleClickOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
	};

    const handleCloseError = (event, reason) => {
			setOpenSnackBarError(false);
	};

    const requester_name={
        text: input_requester_name,
    };

    const target_name={
        text: input_target_name,
    };

    const connection_description={
        text: input_description,
    };

    const connection_decision = {

    };

    const connection = {
        requesterName: requester_name,
        targetName: target_name,
        description: connection_description,
        decision: connection_decision,
    };

	 function handleSendRequest(event) {
			event.preventDefault();
			setMakingRequest(true);

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
						/* pôr aqui o parâmetro-nome*/ "?"}
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
