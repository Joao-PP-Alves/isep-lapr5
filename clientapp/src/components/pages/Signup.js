import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import DesktopDatePicker from "@mui/lab/DesktopDatePicker";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { FormCheck } from "react-bootstrap";
import { useState } from "react";
import PrivacyPolicy from "./privacyPolicy";
import LogIn from "./Login";
import axios from "axios";

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

export default function SignUp() {
	const [validated, setValidated] = useState(false);
	const [value, setValue] = React.useState(new Date());
	const [makingRequest, setMakingRequest] = useState(false);

	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [firstName, setFirstName] = useState("");
	const [lastName, setLastName] = useState("");
	const [phoneNumber, setPhoneNumber] = useState("");
	const [birthDate, setBirthDate] = useState(new Date());
	const [checked, setChecked] = React.useState([true, false]);
	const [setState] = useState("");

	function validateForm() {
		return (
			email.length > 0 &&
			password.length > 0 &&
			firstName.length > 0 &&
			lastName.length > 0 &&
			phoneNumber.length > 0 &&
			birthDate.length > 0 &&
			checked
		);
	}

	function handleSubmit(event) {
		event.preventDefault();

		console.log(
			"FirstName:",
			firstName,
			"LastName:",
			lastName,
			"Email:",
			email,
			"Password:",
			password,
			"PhoneNumebr:",
			phoneNumber,
			"BirthDate",
			birthDate
		);

		const user = {
			name: firstName + " " + lastName,
			email: email,
			password: password,
			phonenNumber: phoneNumber,
			//birthDate: birthDate
		}(async () => {
			const response = await fetch(
				"https://21s5dd20socialgame.azurewebsites.net/api/Users",

				{
					method: "POST",
					headers: { "Content-Type": "application/json" },
					body: JSON.stringify(user),
				}
			);

            const contente = await response.json();
            console.log(contente);
		})();
	}

	/*const data = new FormData(event.currentTarget);
        // eslint-disable-next-line no-console
        /*console.log({
            firstName: data.get("firstName"),
            lastName: data.get("lastName"),
            birthDate: data.get("birthDate"),
            phoneNumber: data.get("phoneNumber"),
            email: data.get("email"),
            password: data.get("password"),
        });*/

	return (
		<LocalizationProvider dateAdapter={AdapterDateFns}>
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
											value={firstName}
											onChange={(e) => setFirstName(e.target.value)}
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
											value={lastName}
											onChange={(e) => setLastName(e.target.value)}
										/>
									</Grid>
									<Grid item xs={12} sm={6}>
										<DesktopDatePicker
											label="Birth date"
											inputFormat="dd/MM/yyyy"
											value={birthDate}
											/* onChange={handleChange} */
											onChange={(e) => setBirthDate(e.target.value)}
											renderInput={(params) => <TextField {...params} />}
											value={birthDate}
											onChange={(e) => setBirthDate(e.target.value)}
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
											value={phoneNumber}
											onChange={(e) => setPhoneNumber(e.target.value)}
										/>
									</Grid>
									<Grid item xs={12}>
										<TextField
											required
											fullWidth
											id="email"
											label="Email Address"
											name="email"
											autoComplete="email"
											value={email}
											onChange={(e) => setEmail(e.target.value)}
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
											value={password}
											onChange={(e) => setPassword(e.target.value)}
										/>
									</Grid>
									<Grid item xs={12}>
										<FormCheck
											control={
												<Checkbox
													checked={checked}
													//onChange={handleChange}
													color="primary"
												/>
											}
											label="I agree with the terms and conditions."
											feedbackType="invalid"
										/>
									</Grid>
								</Grid>
								<Button
									type="submit"
									fullWidth
									variant="contained"
									sx={{ mt: 3, mb: 2 }}
									enabled={!validateForm() || makingRequest}
									onClick={handleSubmit}
								>
									Sign Up
								</Button>
								<Grid item>
									<Link href="/login" variant="body2">
										Already have an account? Login.
										{LogIn}
									</Link>
								</Grid>
								<Grid item>
									<Link href="/termsConditions" variant="body2">
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
