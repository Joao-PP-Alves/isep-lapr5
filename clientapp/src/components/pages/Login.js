import * as React from "react";
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
import { useState } from "react";
import LoadingButton from "@mui/lab/LoadingButton";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { UserLoginDTO } from "../dtos/UserLoginDTO";
import Links from "../Links";
import history from "../../history";
import Snackbar from "@mui/material/Snackbar";
import { Email } from "../model/Email";
import { Password } from "../model/Password";
import MuiAlert from "@mui/material/Alert";

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

const Alert = React.forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

const theme = createTheme();

export default function SignIn() {

    const [input_email, setEmail] = useState("");
    const [input_password, setPassword] = useState("");

    const email = new Email(input_email);

    const password = new Password(input_password);

    const user = new UserLoginDTO(email, password);

    const handleSubmit = (event) => {
        event.preventDefault();
        setMakingRequest(true);
        //const data = new FormData(event.currentTarget);
        makeRequest(user);
        setMakingRequest(false);
    };

    

    function validate_form() {
        let not_valid =  
        user.email.emailAddress == "" ||
        user.password.value == "";
        
        if(not_valid) {
            return true;
        }
        return false;
    }

    

    const [makingRequest, setMakingRequest] = useState(false);
    const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

    const handleCloseError = (event, reason) => {
        setOpenSnackBarError(false);
    };

    async function makeRequest(user) {

        const axios = require("axios");

        let email = user.email.emailAddress;

        axios.get(Links.MDR_URL() + "Users/ByEmail/" + email)
        .then((res) => {
            const response = res.data;
            console.log(res);
            if (res.status == 200) {
                let id = response[0].id;
                localStorage.setItem("loggedInUser", id);
                history.push("/dashboard");
                window.location.reload();
            }
        }
        )
        .catch((err) => {
            setOpenSnackBarError(true);
        })
    }

    return (
        <ThemeProvider theme={theme}>
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
                    Failed To Login!
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
                        Sign in
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
                            id="email"
                            label="Email Address"
                            name="email"
                            value={input_email}
                            onChange={(e) =>
                                setEmail(e.target.value)
                            }
                            autoComplete="email"
                            autoFocus
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            value={input_password}
                            onChange={(e) =>
                                setPassword(e.target.value)
                            }
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
                            disabled={validate_form()}
                            loading={makingRequest}
                            loadingPosition="end"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Sign In
                        </LoadingButton>
                        <Grid container>
                            <Grid item xs>
                                <Link href="#" variant="body2">
                                    Forgot password?
                                </Link>
                            </Grid>
                            <Grid item>
                                <Link href="#" variant="body2">
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
