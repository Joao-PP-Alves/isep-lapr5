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
import Chip from "@mui/material/Chip";
import Autocomplete from "@mui/material/Autocomplete";

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

	const [input_email, setEmail] = useState("");
	const [input_password, setPassword] = useState("");
	const [input_firstName, setFirstName] = useState("");
	const [input_lastName, setLastName] = useState("");
	const [input_phoneNumber, setPhoneNumber] = useState("");
	const [input_birthDate, setBirthDate] = useState(new Date());
	const [input_tags, setTags] = useState([]);
	const [checked, setChecked] = React.useState([true, false]);
	const [setState] = useState("");

	function validateForm() {
		return (
			input_email.length > 0 &&
			input_password.length > 0 &&
			input_firstName.length > 0 &&
			input_lastName.length > 0 &&
			input_phoneNumber.length > 0 &&
			input_birthDate.length > 0 &&
			checked
		);
	}

	function handleSubmit(event) {
		event.preventDefault();

		console.log(
			"FirstName:",
			input_firstName,
			"LastName:",
			input_lastName,
			"Email:",
			input_email,
			"Password:",
			input_password,
			"PhoneNumber:",
			input_phoneNumber,
			"BirthDate",
			input_birthDate
		);

		const user_name = {
			text: input_firstName + " " + input_lastName,
		};

		const user_email = {
			emailAddress: input_email,
		};

		const user_password = {
			value: input_password,
		};

		const user_phoneNumber = {
			number: input_phoneNumber,
		};

		const user_birthDate = {
			date: input_birthDate.toJSON(),
		};

		const user_tag = {
			name: input_tags,
		};

		const user = {
			name: user_name,
			email: user_email,
			password: user_password,
			tags: [user_tag],
			phoneNumber: user_phoneNumber,
			birthDate: user_birthDate,
		};
		console.log(JSON.stringify(user));

		const response = fetch(
			"https://21s5dd20socialgame.azurewebsites.net/api/Users",

			{
				method: "POST",
				headers: { "Content-Type": "application/json" },
				body: JSON.stringify(user),
			}
		)
			.then((response) => response.json())
			.then((json) => console.log(json));

		if (!response.ok) {
			setMakingRequest(false);
			return null;
		} else {
			console.log("User created!");
		}
	}

	const handleDelete = (chipToDelete) => () => {
		console.log(input_tags);
		const index = input_tags.indexOf(chipToDelete);
		if (index > -1) {
			input_tags.splice(index, 1);
		}
		console.log(input_tags);
	};

	const handleAddChip = (chip) => {
		console.log(input_tags);
		input_tags.push(chip);
		console.log(input_tags);
	};

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
											value={input_firstName}
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
											value={input_lastName}
											onChange={(e) => setLastName(e.target.value)}
										/>
									</Grid>

									<Grid item xs={12} sm={6}>
										<DesktopDatePicker
											label="Birth date"
											inputFormat="dd/MM/yyyy"
											value={input_birthDate}
											/* onChange={handleChange} */
											onChange={(e) => setBirthDate(e.target.value)}
											renderInput={(params) => <TextField {...params} />}
											value={input_birthDate}
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
											value={input_phoneNumber}
											onChange={(e) => setPhoneNumber(e.target.value)}
										/>
									</Grid>

									<Grid item xs={12}>
										<Autocomplete
											multiple
											id="tags-filled"
											options={savedTags.map((option) => option.name)}
											//defaultValue={[savedTags[0].name]}
											freeSolo
											
											renderTags={(value, getTagProps) =>
												value.map((option, index) => (
													
													<Chip
														variant="outlined"
														label={option}
														onDelete={handleDelete(option)}
														{...getTagProps({ index })}
													/>
												))
											}
											renderInput={(params) => (
												<TextField
													{...params}
													variant="filled"
													label="Tags"
													placeholder="What interests you?"
												/>
											)}
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
											value={input_email}
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
											value={input_password}
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

const savedTags = [
	{ name: "isep" },
	{ name: "react" },
	{ name: "informática" },
];
