import * as React from "react";
import Avatar from "@mui/material/Avatar";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import LoadingButton from "@mui/lab/LoadingButton";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import FormControlLabel from "@mui/material/FormControlLabel";
import DesktopDatePicker from "@mui/lab/DesktopDatePicker";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";
import { useState } from "react";
import PrivacyPolicy from "./privacyPolicy";
import LogIn from "./Login";
import Chip from "@mui/material/Chip";
import Autocomplete from "@mui/material/Autocomplete";
import Stack from "@mui/material/Stack";
import Links from "../Links";
import history from "../../history";
import { CreatingUserDTO } from "../dtos/CreatingUserDTO";
import { BirthDate } from "../model/BirthDate";
import { Email } from "../model/Email";
import { Name } from "../model/Name";
import { Password } from "../model/Password";
import { PhoneNumber } from "../model/PhoneNumber";
import { Tag } from "../model/Tag";

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

export default function SignUp() {
    const [validated, setValidated] = useState(true);
    const [value, setValue] = React.useState(new Date());
    const [makingRequest, setMakingRequest] = useState(false);

    const [input_email, setEmail] = useState("");
    const [input_password, setPassword] = useState("");
    const [input_firstName, setFirstName] = useState("");
    const [input_lastName, setLastName] = useState("");
    const [input_phoneNumber, setPhoneNumber] = useState("");
    const [input_birthDate, setBirthDate] = useState(new Date());
    const [input_tags, setTags] = useState([]);
    const [checked, setChecked] = React.useState(false);

    const [openSnackBar, setOpenSnackBar] = React.useState(false);
    const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

    const handleDateChange = (newValue) => {
        setBirthDate(newValue);
    };

    const handleCheckChange = (event) => {
        //checked ? console.log("checked") : console.log("not checked");
        setChecked(event.target.checked);
    };

    const handleClick = () => {
        setOpenSnackBar(true);
    };

    const handleClose = (event, reason) => {
        setOpenSnackBar(false);
    };

    const handleCloseError = (event, reason) => {
        setOpenSnackBarError(false);
    };

    

    const user_name = new Name(input_firstName + " " + input_lastName);

    const user_email = new Email(input_email);

    const user_password = new Password(input_password);

    const user_phoneNumber = new PhoneNumber(input_phoneNumber);

    const user_birthDate = new BirthDate(input_birthDate.toJSON());

    const user_tag = new Tag("");

    const user = new CreatingUserDTO(
        user_name,
        user_email,
        [user_tag],
        user_password,
        user_phoneNumber,
        user_birthDate
    );

    user.createTags(input_tags);

    const handleSubmit =(event) => {
        event.preventDefault();
        
        console.log(user);

        setMakingRequest(true);
        makeRequest(user);
        setMakingRequest(false);
    }

    function validate_form() {
        //console.log("user name " + user_name.text);
        //checked ? console.log("checked") : console.log("not checked");
        let not_valid = !checked || user_name.text == "" ||
        user_email.emailAddress == "" ||
        user_password.value == "" ||
        user_phoneNumber.number == "" ||
        user.tags.length == 0;
        if(not_valid) {
            return true;
        }
        return false;
    }


	async function makeRequest(user) {
        const axios = require("axios");

        axios.post(Links.MDR_URL() + "Users", user)
        .then((res) => {
            const response = res.data;
            console.log(res);
            if (res.status == 201) {
                let id = response.id;
                localStorage.setItem("loggedInUser", id);
                history.push("/dashboard");
                window.location.reload();
            }
        }
        )
        .catch((err) => {
            console.log(err);
            setOpenSnackBarError(true);
        })
    }

    const handleDelete = (chipToDelete) => () => {
        setTags((chips) => input_tags.filter((chip) => chip !== chipToDelete));
    };

    return (
        <LocalizationProvider dateAdapter={AdapterDateFns}>
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
                    User Registered Successfully!
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
                    Failed To Register User!
                </Alert>
            </Snackbar>
            <ThemeProvider theme={theme}>
                <form className={"formulary"} onSubmit={handleSubmit}>
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
                                Register new account
                            </Typography>
                            <Box
                                component="form"
                                noValidate
                                onSubmit={handleSubmit}
                                sx={{ mt: 3 }}
                            >
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField
                                            autoComplete="given-name"
                                            name="firstName"
                                            required={true}
                                            fullWidth
                                            id="firstName"
                                            label="First Name"
                                            autoFocus
                                            value={input_firstName}
                                            onChange={(e) =>
                                                setFirstName(e.target.value)
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField
                                            required
                                            fullWidth
                                            id="lastName"
                                            label="Last Name"
                                            name="lastName"
                                            autoComplete="family-name"
                                            value={input_lastName}
                                            onChange={(e) =>
                                                setLastName(e.target.value)
                                            }
                                        />
                                    </Grid>

                                    <Grid item xs={12} sm={6}>
                                        <DesktopDatePicker
                                            label="Birth date"
                                            inputFormat="dd/MM/yyyy"
                                            value={input_birthDate}
                                            onChange={handleDateChange}
                                            renderInput={(params) => (
                                                <TextField {...params} />
                                            )}
                                        />
                                    </Grid>

                                    <Grid item xs={12} sm={6}>
                                        <TextField
                                            required
                                            fullWidth
                                            id="phoneNumber"
                                            label="Phone number"
                                            name="phoneNumber"
                                            autoComplete="phone-number"
                                            value={input_phoneNumber}
                                            onChange={(e) =>
                                                setPhoneNumber(e.target.value)
                                            }
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <Stack spacing={3} sx={{ width: 500 }}>
                                            <Autocomplete
                                                multiple
                                                id="tags-filled"
                                                options={savedTags.map(
                                                    (option) => option.name
                                                )}
                                                freeSolo
                                                value={input_tags}
                                                onChange={(
                                                    e,
                                                    newval,
                                                    reason
                                                ) => {
                                                    setTags(newval);
                                                }}
                                                renderTags={(
                                                    value,
                                                    getTagProps
                                                ) =>
                                                    value.map(
                                                        (option, index) => (
                                                            <Chip
                                                                variant="outlined"
                                                                label={option}
                                                                {...getTagProps(
                                                                    { index }
                                                                )}
                                                            />
                                                        )
                                                    )
                                                }
                                                renderInput={(params) => (
                                                    <TextField
                                                        {...params}
                                                        variant="filled"
                                                        label="Tags"
                                                        placeholder="What interests you?"
                                                        onKeyDown={(e) => {
                                                            if (
                                                                e.code ===
                                                                    "enter" &&
                                                                e.target.value
                                                            ) {
                                                                setTags(
                                                                    input_tags.concat(
                                                                        e.target
                                                                            .value
                                                                    )
                                                                );
                                                            }
                                                        }}
                                                    />
                                                )}
                                            />
                                        </Stack>
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextField
                                            required
                                            fullWidth
                                            id="email"
                                            label="Email Address"
                                            name="email"
                                            autoComplete="email"
                                            value={input_email}
                                            onChange={(e) =>
                                                setEmail(e.target.value)
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField
                                            required
                                            fullWidth
                                            name="password"
                                            label="Password"
                                            type="password"
                                            id="password"
                                            autoComplete="new-password"
                                            value={input_password}
                                            onChange={(e) =>
                                                setPassword(e.target.value)
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    checked={checked}
                                                    onChange={handleCheckChange}
                                                    color="primary"
                                                />
                                            }
                                            label="I agree with the terms and conditions"
                                        />
                                    </Grid>
                                </Grid>
                                <LoadingButton
                                    type="submit"
                                    fullWidth
                                    variant="contained"
                                    disabled={validate_form()}
                                    loading={makingRequest}
                                    loadingPosition="end"
                                    sx={{ mt: 3, mb: 2 }}
                                    onClick={handleSubmit}
                                >
                                    Sign Up
                                </LoadingButton>
                                <Grid item>
                                    <Link href="/login" variant="body2">
                                        Already have an account? Login.
                                        {LogIn}
                                    </Link>
                                </Grid>
                                <Grid item>
                                    <Link
                                        href="/termsConditions"
                                        variant="body2"
                                    >
                                        Terms and conditions.
                                    </Link>
                                </Grid>
                                <Grid item>
                                    <Link href="/privacyPolicy" variant="body2">
                                        {PrivacyPolicy}
                                        Privacy Policy.
                                    </Link>
                                </Grid>
                            </Box>
                        </Box>
                        <Copyright sx={{ mt: 5 }} />
                    </Container>
                </form>
            </ThemeProvider>
        </LocalizationProvider>
    );
}

const savedTags = [
    { name: "isep" },
    { name: "react" },
    { name: "informática" },
];
