import React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Modal from "react-modal";
import { useState } from "react";
import LoadingButton from "@mui/lab/LoadingButton";
import MuiAlert from "@mui/material/Alert";
import Snackbar from "@mui/material/Snackbar";
import userAuth from "../../hooks/UserAuth";
import { useHistory } from "react-router-dom";

function Copyright(props) {
    return (
        <Typography
            variant="body2"
            color="text.secondary"
            align="center"
            {...props}
        >
            {"Copyright © "}
            <Link color="inherit" href="https://mui.com/">
                Your Website
            </Link>{" "}
            {new Date().getFullYear()}
            {"."}
        </Typography>
    );
}

const theme = createTheme();

const Alert = React.forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

function LogIn() {
    const [show, setShow] = React.useState(false);

    const [makingRequest, setMakingRequest] = useState(false);
    const [showSignUp, setSignUp] = useState(false);
    const [loginError, setLoginError] = useState("");

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [openSnackBar, setOpenSnackBar] = React.useState(false);
    const [openSnackBarError, setOpenSnackBarError] = React.useState(false);
    const [openSnackBarNoEmail, setOpenSnackBarNoEmail] = React.useState(false);

    const handleClose = (event, reason) => {
        setOpenSnackBar(false);
    };

    const handleCloseError = (event, reason) => {
        setOpenSnackBarError(false);
    };

    const handleCloseNoEmail = (event, reason) => {
        setOpenSnackBarNoEmail(false);
    };

    const history = useHistory();

    async function handle_login() {
        setMakingRequest(true);
        let item = { email, password };

        let result = await fetch("https://localhost:5000/api/Users/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
            body: JSON.stringify(item),
        })
            .then((response) => {
                response.json();
                if (!response.ok) {
                    return null;
                } else {
                    setOpenSnackBar(true);

                    localStorage.setItem(
                        "loggedInUser",
                        JSON.stringify(response)
                    );
                    history.push("/dashboard");
                }
                setMakingRequest(false);
            })
            .catch((err) => {
                setOpenSnackBarError(true);
                setMakingRequest(false);
            });
    }

    const handleSubmit = (data) => {
        //simulates logged in user
        localStorage.setItem(
            "loggedInUser",
            "be31c3c0-7b0f-4985-ba66-ebe1fb9ca60b"
        );
        console.log(localStorage.getItem("loggedInUser"));

        setMakingRequest(true);
        fetch("https://21s5dd20socialgame.azurewebsites.net/api/Users")
            .then((response) => {
                console.log(response.status);
                if (!response.ok) {
                    setLoginError("Wrong credentials! Try again.");
                    setMakingRequest(false);
                    return null;
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    console.log(data);
                    //handleLogin(data);
                    history.replace("/");
                } else {
                    setMakingRequest(false);
                }
            });
    };

    return (
        <ThemeProvider theme={theme}>
            <Snackbar
                anchorOrigin={{ vertical: "top", horizontal: "center" }}
                open={openSnackBar}
                autoHideDuration={1000}
                onClose={handleClose}
            >
                <Alert
                    onClose={handleClose}
                    severity="success"
                    sx={{ width: "100%" }}
                >
                    Login Successfull!
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
                    Invalid email/password combination!
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
                    No account was found with this email!
                </Alert>
            </Snackbar>
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Login
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{ mt: 1 }}
                    >
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            onChange={(e) => setEmail(e.target.value)}
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            onChange={(e) => setPassword(e.target.value)}
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                        />
                        <FormControlLabel
                            control={
                                <Checkbox value="remember" color="primary" />
                            }
                            label="Remember me"
                        />
                        <LoadingButton
                            type="submit"
                            fullWidth
                            variant="contained"
                            loading={makingRequest}
                            loadingPosition="end"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Login
                        </LoadingButton>
                        <Grid container>
                            <Grid item xs>
                                <Link href="#" variant="b2">
                                    Forgot your password?
                                </Link>

                                <Modal show={show} onHide={handleClose}>
                                    <Modal.Header closeButton>
                                        <Modal.Title>Modal heading</Modal.Title>
                                    </Modal.Header>
                                    <Modal.Body>
                                        Woohoo, you're reading this text in a
                                        modal!
                                    </Modal.Body>
                                    <Modal.Footer>
                                        <Button
                                            variant="secondary"
                                            onClick={handleClose}
                                        >
                                            Close
                                        </Button>
                                        <Button
                                            variant="primary"
                                            onClick={handleClose}
                                        >
                                            Save Changes
                                        </Button>
                                    </Modal.Footer>
                                </Modal>
                            </Grid>
                            <Grid item>
                                <Link href="/signup" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
                <Copyright sx={{ mt: 8, mb: 4 }} />
            </Container>
        </ThemeProvider>
    );
}

export default LogIn;
