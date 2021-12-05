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

export default function AlertDialogSlide({ requesterEmail, targetEmail, render }) {
	const [open, setOpen] = React.useState(false);

    const [setRequesterEmail] = useState("");
    const [setTargetEmail] = useState("");
    const [input_description, setDescription] = useState("");

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

    const connection_description={
        text: input_description,
    };

    const connection = {
        requester_Email: requesterEmail,
        target_Email: targetEmail,
        description: connection_description,
        decision: "0",
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

        const requester = {
			name: obj.name.text,
			email: obj.email.emailAddress,
            id: obj.id,
		};

        const target = {
			name: obj.name.text,
			email: obj.email.emailAddress,
			id: obj.id,
		};

        function getByEmailRequester(requesterEmail){
            setMakingRequest(true);

            const response = fetch(
                Links.MDR_URL() + "users/ByEmail/"+email, 
                {
					method: "GET",
					headers: { "Content-Type": "application/json" },
					body: JSON.stringify(requester),
				}
            ).then((response) => {
                response.json();
                if(!response.ok) {
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

        function getByEmailTarget(targetEmail) {
			setMakingRequest(true);

			const response = fetch(Links.MDR_URL() + "users/ByEmail/" + email, {
				method: "GET",
				headers: { "Content-Type": "application/json" },
				body: JSON.stringify(requester),
			})
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
					{"Do you wanto to send a friendship request to " /*+
						targetName */+ "?"}
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
