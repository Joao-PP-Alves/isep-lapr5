import * as React from 'react';
import { styled, createTheme, ThemeProvider, alpha } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Link from '@mui/material/Link';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import { mainListItems} from '../dashboard/ListItems';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import AccountCircleTwoToneIcon from "@mui/icons-material/AccountCircleTwoTone";
import EditIcon from "@mui/icons-material/Edit";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import FormHelperText from "@mui/material/FormHelperText";
import { useState } from "react";
import DesktopDatePicker from "@mui/lab/DesktopDatePicker";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import Stack from "@mui/material/Stack";
import Chip from "@mui/material/Chip";
import Autocomplete from "@mui/material/Autocomplete";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";


function Copyright(props) {
  return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

const drawerWidth = 240;

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const StyledMenu = styled((props) => (
	<Menu
		elevation={0}
		anchorOrigin={{
			vertical: "bottom",
			horizontal: "right",
		}}
		transformOrigin={{
			vertical: "top",
			horizontal: "right",
		}}
		{...props}
	/>
))(({ theme }) => ({
	"& .MuiPaper-root": {
		borderRadius: 6,
		marginTop: theme.spacing(1),
		minWidth: 180,
		color:
			theme.palette.mode === "light"
				? "rgb(55, 65, 81)"
				: theme.palette.grey[300],
		boxShadow:
			"rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px",
		"& .MuiMenu-list": {
			padding: "4px 0",
		},
		"& .MuiMenuItem-root": {
			"& .MuiSvgIcon-root": {
				fontSize: 18,
				color: theme.palette.text.secondary,
				marginRight: theme.spacing(1.5),
			},
			"&:active": {
				backgroundColor: alpha(
					theme.palette.primary.main,
					theme.palette.action.selectedOpacity
				),
			},
		},
	},
}));


const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9),
        },
      }),
    },
  }),
);

const mdTheme = createTheme();

function EditProfileContent() {
	const [anchorEl, setAnchorEl] = React.useState(null);
	const open = Boolean(anchorEl);
	const [setOpen] = React.useState(true);
	const [emotion] = React.useState('');
	const [input_email, setEmail] = useState("");
	const [input_password, setPassword] = useState("");
	const [input_firstName, setFirstName] = useState("");
	const [input_lastName, setLastName] = useState("");
	const [input_phoneNumber, setPhoneNumber] = useState("");
	const [input_birthDate, setBirthDate] = useState(new Date());
	const [input_tags, setTags] = useState([]);
	const [input_emotion, setEmotion] = useState("");
	const [makingRequest, setMakingRequest] = useState(false);

	const [openSnackBar, setOpenSnackBar] = React.useState(false);
	const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

	const handleEmotionChange = (event) => {
		setEmotion(event.target.value);
	};

	const handleDateChange = (newValue) => {
		setBirthDate(newValue);
	};
	
	const toggleDrawer = () => {
		setOpen(!open);
	};

	const handleClick = (event) => {
	setAnchorEl(event.currentTarget);
	};

	const handleClose = () => {
	setAnchorEl(null);
	};

	const handleCloseError = (event, reason) => {
		setOpenSnackBarError(false);
	};

	const Alert = React.forwardRef(function Alert(props, ref) {
		return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
	});

	function handleSave(event) {
		event.preventDefault();
		console.log("Emotional state:", input_emotion);

		console.log(
			"FirstName:",
			input_firstName,
			"LastName:",
			input_lastName,
			"Email:",
			input_email,
			"Tags:",
			input_tags,
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
			name: "",
		};

		const user_tags = {
			tags: [user_tag],
		};

		const user_emotionalState ={
			emotion: input_emotion,
		}

		const user = {
			name: user_name,
			email: user_email,
			tags: [user_tag],
			createTags() {
				input_tags.forEach((element) => {
					const user_tag = {
						name: element,
					};
					this.tags.push(user_tag);
				});
				this.tags.shift();
			},
			phoneNumber: user_phoneNumber,
			emotionalState: user_emotionalState,
			friendsList: [],
		};
		console.log(JSON.stringify(user));

		user.createTags();
		const response = fetch(
			"https://localhost:5001/api/Users/0f277d33-df08-4954-bd3b-26adb739927d",
			//"https://21s5dd20socialgame.azurewebsites.net/api/Users/0f277d33-df08-4954-bd3b-26adb739927d" /*falta pôr um id de um user*/,
			{
				method: "PUT",
				headers: { "Content-Type": "application/json" },
				body: JSON.stringify(user),
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
				<Alert onClose={handleClose} severity="success" sx={{ width: "100%" }}>
					User Updated Successfully!
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
					Failed To Update User!
				</Alert>
			</Snackbar>
			<ThemeProvider theme={mdTheme}>
				<Box sx={{ display: "flex" }}>
					<CssBaseline />
					<AppBar position="absolute" open={open}>
						<Toolbar
							sx={{
								pr: "24px", // keep right padding when drawer closed
							}}
						>
							<IconButton
								edge="start"
								color="inherit"
								aria-label="open drawer"
								onClick={toggleDrawer}
								sx={{
									marginRight: "36px",
									...(open && { display: "none" }),
								}}
							>
								<MenuIcon />
							</IconButton>
							<Typography
								component="h1"
								variant="h6"
								color="inherit"
								noWrap
								sx={{ flexGrow: 1 }}
							>
								Edit Profile
							</Typography>
							<IconButton
								color="inherit"
								id="accountButton"
								aria-controls="demo-customized-menu"
								aria-haspopup="true"
								aria-expanded={open ? "true" : undefined}
								variant="contained"
								onClick={handleClick}
							>
								<Badge badgeContent={4} color="secondary">
									<AccountCircleTwoToneIcon />
								</Badge>
							</IconButton>
							<StyledMenu
								id="accountButton"
								MenuListProps={{
									"aria-labelledby": "accountButton",
								}}
								anchorEl={anchorEl}
								open={open}
								onClose={handleClose}
							>
								<Button href="/" disableRipple>
									<EditIcon />
									Logout
								</Button>
							</StyledMenu>
						</Toolbar>
					</AppBar>
					<Drawer variant="permanent" open={open}>
						<Toolbar
							sx={{
								display: "flex",
								alignItems: "center",
								justifyContent: "flex-end",
								px: [1],
							}}
						>
							<IconButton onClick={toggleDrawer}>
								<ChevronLeftIcon />
							</IconButton>
						</Toolbar>
						<Divider />
						<List>{mainListItems}</List>
						<Divider />
					</Drawer>
					<Box
						component="main"
						sx={{
							backgroundColor: (theme) =>
								theme.palette.mode === "light"
									? theme.palette.grey[100]
									: theme.palette.grey[900],
							flexGrow: 1,
							height: "100vh",
							width: "100vh",
							overflow: "auto",
						}}
					>
						<Toolbar />
						<Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
							{/* Recent Deposits */}
							<Grid item xs={12} md={4} lg={3}>
								<Paper
									sx={{
										p: 2,
										display: "flex",
										flexDirection: "column",
										height: 600,
										width: 1150,
									}}
								>
									<Typography component="h1" variant="h5">
										Edit profile
									</Typography>
									<Box component="form" sx={{ mt: 3 }}>
										<Grid container spacing={2}>
											<Grid item xs={12} sm={6}>
												<TextField
													autoComplete="given-name"
													name="firstName"
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
												<TextField
													fullWidth
													id="email"
													label="Email"
													name="email"
													autoComplete="email"
													value={input_email}
													onChange={(e) => setEmail(e.target.value)}
												/>
											</Grid>
											<Grid item xs={12} sm={6}>
												<Stack spacing={3} sx={{ width: 500 }}>
													<Autocomplete
														multiple
														id="tags-filled"
														options={savedTags.map((option) => option.name)}
														freeSolo
														value={input_tags}
														onChange={(e, newval, reason) => {
															setTags(newval);
														}}
														renderTags={(value, getTagProps) =>
															value.map((option, index) => (
																<Chip
																	variant="outlined"
																	label={option}
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
																onKeyDown={(e) => {
																	if (e.code === "enter" && e.target.value) {
																		setTags(input_tags.concat(e.target.value));
																	}
																}}
															/>
														)}
													/>
												</Stack>
											</Grid>
											<Grid item xs={12} sm={4}>
												<FormControl sx={{ minWidth: 80 }}>
													<InputLabel id="demo-simple-select-autowidth-label">
														Emotional State
													</InputLabel>
													<Select
														labelId="demo-simple-select-autowidth-label"
														id="demo-simple-select-autowidth"
														autoWidth
														label="Emotional State"
														value={input_emotion}
														onChange={handleEmotionChange}
													>
														<MenuItem value="esperança">
															<em>Esperança</em>
														</MenuItem>
														<MenuItem value="felicidade">
															<em>Felicidade</em>
														</MenuItem>
														<MenuItem value="tristeza">
															<em>Tristeza</em>
														</MenuItem>
														<MenuItem value="raiva">
															<em>Raiva</em>
														</MenuItem>
														<MenuItem value="stress">
															<em>Stress</em>
														</MenuItem>
														<MenuItem value="medo">
															<em>Medo</em>
														</MenuItem>
													</Select>
													<FormHelperText>
														Tell me how are you feeling.
													</FormHelperText>
												</FormControl>
											</Grid>
											<Grid item xs={12} sm={4}>
												<DesktopDatePicker
													label="Birth date"
													inputFormat="dd/MM/yyyy"
													value={input_birthDate}
													onChange={handleDateChange}
													renderInput={(params) => <TextField {...params} />}
												/>
											</Grid>
											<Grid item xs={12} sm={4}>
												<TextField
													fullWidth
													id="phoneNumber"
													label="Phone number"
													name="phoneNumber"
													autoComplete="phone-number"
													value={input_phoneNumber}
													onChange={(e) => setPhoneNumber(e.target.value)}
												/>
											</Grid>

											{/*<Grid item xs={12}>
											<TextField
												fullWidth
												name="password"
												label="Password"
												type="password"
												id="password"
												autoComplete="new-password"
											/>
										</Grid>
										<Grid item xs={12}>
											<TextField
												fullWidth
												name="confirmpassword"
												label="Confirm Password"
												type="confirmpassword"
												id="confirmpassword"
												autoComplete="confirm-password"
											/>
										</Grid>*/}
											<Grid item xs={12} sm={6}>
												<Button
													type="save"
													width="500"
													variant="contained"
													sx={{
														p: 2,
														display: "flex",
														flexDirection: "column",
														height: 2,
														width: 500,
														my: 5,
													}}
													xs={12}
													onClick={handleSave}
												>
													Save Changes
												</Button>
											</Grid>
											<Grid item xs={12} sm={6}>
												<Button
													type="discard"
													width="500"
													variant="contained"
													sx={{
														p: 2,
														display: "flex",
														flexDirection: "column",
														height: 2,
														width: 500,
														my: 5,
													}}
													xs={12}
													href="/editProfile"
												>
													Discard
												</Button>
											</Grid>
										</Grid>
									</Box>
								</Paper>
							</Grid>
							<Copyright sx={{ pt: 4 }} />
						</Container>
					</Box>
				</Box>
			</ThemeProvider>
		</LocalizationProvider>
	);
}

const savedTags = [
	{ name: "isep" },
	{ name: "react" },
	{ name: "informática" },
];


export default function EditProfile() {
  return <EditProfileContent />;
}