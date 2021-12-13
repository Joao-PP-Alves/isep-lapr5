import * as React from "react";
import {
	styled,
	createTheme,
	ThemeProvider,
	alpha,
} from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import MuiDrawer from "@mui/material/Drawer";
import Box from "@mui/material/Box";
import MuiAppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import List from "@mui/material/List";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import Badge from "@mui/material/Badge";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Link from "@mui/material/Link";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import { mainListItems } from "./ListItems";
import FriendsSuggestions from "./FriendsSuggestions";
import NetworkStats from "./NetworkStats";
import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import AccountCircleTwoToneIcon from "@mui/icons-material/AccountCircleTwoTone";
import Stack from "@mui/material/Stack";
import AutoAwesomeTwoToneIcon from "@mui/icons-material/AutoAwesomeTwoTone";
import LogoutTwoToneIcon from "@mui/icons-material/LogoutTwoTone";
import Modal from "@mui/material/Modal";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import FormHelperText from "@mui/material/FormHelperText";
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import parse from "autosuggest-highlight/parse";
import { useState, useEffect } from "react";
import MinimalizedNetwork from "./MinimalizedNetwork";
import match from "autosuggest-highlight/match";
import Links from "../../Links";
import RequestConnection from "./RequestConnection";
import MenuIcon from "@mui/icons-material/Menu";
import ArrowForwardIosTwoToneIcon from "@mui/icons-material/ArrowForwardIosTwoTone";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";
import SelectInput from "@mui/material/Select/SelectInput";

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

const drawerWidth = 240;

const AppBar = styled(MuiAppBar, {
	shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
	zIndex: theme.zIndex.drawer + 1,
	transition: theme.transitions.create(["width", "margin"], {
		easing: theme.transitions.easing.sharp,
		duration: theme.transitions.duration.leavingScreen,
	}),
	...(open && {
		marginLeft: drawerWidth,
		width: `calc(100% - ${drawerWidth}px)`,
		transition: theme.transitions.create(["width", "margin"], {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen,
		}),
	}),
}));

const Drawer = styled(MuiDrawer, {
	shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
	"& .MuiDrawer-paper": {
		position: "relative",
		whiteSpace: "nowrap",
		width: drawerWidth,
		transition: theme.transitions.create("width", {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen,
		}),
		boxSizing: "border-box",
		...(!open && {
			overflowX: "hidden",
			transition: theme.transitions.create("width", {
				easing: theme.transitions.easing.sharp,
				duration: theme.transitions.duration.leavingScreen,
			}),
			width: theme.spacing(7),
			[theme.breakpoints.up("sm")]: {
				width: theme.spacing(9),
			},
		}),
	},
}));

const mdTheme = createTheme();

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

function DashboardContent() {
	//----------------------------------------- FETCH DOS USERS TODOS DO SISTEMA PARA BARRA DE PESQUISA ------------------------
	//get logged user
	const userId = localStorage.getItem("loggedInUser");

	useEffect(() => {
		search();
	}, []);

	const [searchedVS, setSearchedVS] = useState([]);

	function search() {
		fetchUsers();
	}

	const fetchUsers = async () => {
		const data = await fetch(Links.MDR_URL() + "Users");
		const vsList = await data.json();
		console.log(vsList);
		setSearchedVS(vsList);
	};

	// transform json array to array sample[]

	let sample = [];

	for (var i = 0; i < searchedVS.length; i++) {
		var obj = searchedVS[i];

		const user = {
			name: obj.name.text,
			email: obj.email.emailAddress,
		};
		sample.push(user);
	}

	const [anchorEl, setAnchorEl] = React.useState(null);
	const [open, setOpen] = React.useState(true);
	const [openAccount, setOpenAccount] = useState(false);
	const [openModal, setOpenModal] = React.useState(false);
	const [emotion, setEmotion] = React.useState("");
	const [makingRequest, setMakingRequest] = useState(false);
	const [connectionModal, setOpenConnectionModal] = React.useState(false);
	const [targetName] = React.useState("");

	const [openSnackBar, setOpenSnackBar] = React.useState(false);
	const [openSnackBarError, setOpenSnackBarError] = React.useState(false);

	const [inputValue, setinputValue] = useState("");

	const toggleDrawer = () => {
		setOpen(!open);
	};

	const handleClick = (event) => {
		setAnchorEl(event.currentTarget);
	};
	const handleClose = () => {
		setAnchorEl(null);
	};
	const handleOpenModal = () => {
		setOpenModal(true);
	};
	const handleModalClose = () => {
		setOpenModal(false);
	};
	const handleEmotionChange = (event) => {
		setEmotion(event.target.value);
	};
	const handleUserClick = () => {
		setOpenConnectionModal(true);
	};
	const handleCloseConnectionModal = () => {
		setOpenConnectionModal(false);
	};

	const handleCloseError = (event, reason) => {
		setOpenSnackBarError(false);
	};

	function defineUser(inputValue){
		console.log("inputValue:"+inputValue);
		targetName = inputValue;
		console.log("targetName:" + targetName);
	}

	const Alert = React.forwardRef(function Alert(props, ref) {
		return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
	});

	const style = {
		position: "absolute",
		top: "50%",
		left: "50%",
		transform: "translate(-50%, -50%)",
		width: 400,
		bgcolor: "background.paper",
		border: "2px solid #000",
		boxShadow: 24,
		p: 4,
	};

	return (
		<ThemeProvider theme={mdTheme}>
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
					You must select an user in the search bar!
				</Alert>
			</Snackbar>
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
							Dashboard
						</Typography>
						<Autocomplete
						
							id="highlights-demo"
							sx={{ width: 300 }}
							options={sample}
							getOptionLabel={(option) => option.name || ""}
							renderInput={(params) => (
								<TextField {...params} label="Search Users" margin="normal"  onChange={e => setinputValue(e.target.value)}/>
							)}
							renderOption={(props, option, { inputValue }) => {
								const matches = match(option.name, inputValue);
								const users = parse(option.name, matches);
								console.log(inputValue);
								console.log("users:" + users);
								return (
									<li {...props}>
										<div>
											{users.map(
												(part, index) => (
													console.log(part),
													console.log(index),
													(
														<span
															key={index}
															style={{
																fontWeight: part.highlight ? 700 : 400,
															}}
														>
															{part.text}
														</span>
													)
												)
											)}
										</div>
									</li>
								);
							}}
						/>

						<RequestConnection
							render={(open) => (
								<IconButton onClick={open}>
									<ArrowForwardIosTwoToneIcon />
								</IconButton>
							)}
							targetName = {inputValue}
							requesterId = {userId}
						/>
						<IconButton
							color="inherit"
							id="accountButton"
							aria-controls="demo-customized-menu"
							aria-haspopup="true"
							aria-expanded={open ? "true" : undefined}
							variant="contained"
							disableElevation
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
							open={anchorEl}
							onClose={handleClose}
						>
							<Stack spacing={2}>
								<Button href="/" disableRipple>
									<LogoutTwoToneIcon />
									Logout
								</Button>
								<Button
									disableRipple
									type="button"
									class="btn btn-info btn-lg"
									data-toggle="modal"
									data-target="#myModal"
									onClick={handleOpenModal}
								>
									<AutoAwesomeTwoToneIcon />
									Change my mood
								</Button>

								<Modal
									hideBackdrop
									open={openModal}
									onClose={handleOpenModal}
									aria-labelledby="child-modal-title"
									aria-describedby="child-modal-description"
								>
									<Box sx={style}>
										<Typography
											id="modal-modal-title"
											variant="h6"
											component="h2"
										>
											Change my emotional state
										</Typography>
										<Stack>
											<FormControl sx={{ minWidth: 80 }}>
												<InputLabel id="demo-simple-select-autowidth-label">
													Emotional State
												</InputLabel>
												<Select
													labelId="demo-simple-select-autowidth-label"
													id="demo-simple-select-autowidth"
													value={emotion}
													onChange={handleEmotionChange}
													autoWidth
													label="Emotional State"
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
										</Stack>
										<Button onClick={handleModalClose}>Close</Button>
										<Button onClick={handleModalClose}>Save Changes</Button>
									</Box>
								</Modal>
							</Stack>
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
						overflow: "auto",
					}}
				>
					<Toolbar />
					<Container maxWidth="false" sx={{ mt: 4, mb: 4 }}>
						
						<Grid container spacing={3}>
							
							{/* Chart */}
							<Grid item xs={12} md={8} lg={9}>
								<Paper
									sx={{
										p: 2,
										display: "flex",
										flexDirection: "column",
										height: 530,
									}}
								>
									<MinimalizedNetwork />
								</Paper>
							</Grid>
							{/* Recent Deposits */}
							<Grid item xs={12} md={4} lg={3}>
								<Paper
									sx={{
										p: 2,
										display: "flex",
										flexDirection: "column",
										height: 530,
									}}
								>
									<FriendsSuggestions />
								</Paper>
							</Grid>
							{/* Recent Orders */}
							<Grid item xs={12}>
								<Paper
									sx={{
										p: 2,
										display: "flex",
										flexDirection: "column",
										height: 255,
									}}
									
								>

									



									<NetworkStats></NetworkStats>
								</Paper>
							</Grid>
						</Grid>
						
						{/* <Copyright sx={{ pt: 4 }} /> */}
						
					</Container>
				</Box>
			</Box>
		</ThemeProvider>
	);
}

export default function Dashboard() {
	return <DashboardContent />;
}
