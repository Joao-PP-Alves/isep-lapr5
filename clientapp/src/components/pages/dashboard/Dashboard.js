import * as React from "react";
import { styled, createTheme, ThemeProvider, alpha } from "@mui/material/styles";
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
import MenuIcon from "@mui/icons-material/Menu";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import { mainListItems } from "./ListItems";
import Chart from "./Chart";
import Deposits from "./Deposits";
import Orders from "./Orders";
import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import AccountCircleTwoToneIcon from "@mui/icons-material/AccountCircleTwoTone";
import EditIcon from "@mui/icons-material/Edit";
import FileCopyIcon from "@mui/icons-material/FileCopy";
import MoreHorizIcon from "@mui/icons-material/MoreHoriz";
import ArchiveIcon from "@mui/icons-material/Archive";
import Landing from "../landing_page/LandingPage";
import Stack from "@mui/material/Stack";
import AutoAwesomeTwoToneIcon from '@mui/icons-material/AutoAwesomeTwoTone';
import LogoutTwoToneIcon from "@mui/icons-material/LogoutTwoTone";
import Modal from "@mui/material/Modal";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import FormHelperText from "@mui/material/FormHelperText";
import axios from "axios";
import { useState } from "react";


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
	const [anchorEl, setAnchorEl] = React.useState(null);
	const open = Boolean(anchorEl);
    const [setOpen] = React.useState(true);
    const [openModal, setOpenModal] = React.useState(false);
	const [emotion] = React.useState("");
	const [makingRequest, setMakingRequest] = useState(false);
	const [input_emotion, setEmotion] = useState("");

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

	function handleSave(event){
		event.preventDefault();
		console.log("Emotional state:", input_emotion);

		const user_emotion = {
			emotion: input_emotion,
		};

		const user = {
			emotionalState: user_emotion,
		};

		console.log(JSON.stringify(user));
		const response = fetch(
			"https://21s5dd20socialgame.azurewebsites.net/api/Users/0f277d33-df08-4954-bd3b-26adb739927d" /*falta pôr um id de um user*/,
			{
				method: "PUT",
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
			console.log("User updated!");
		}
	}

    return (
			<ThemeProvider theme={mdTheme}>
				<Box sx={{ display: "flex" }}>
					<CssBaseline />
					<AppBar position="absolute" open={open}>
						<Toolbar
							sx={{
								pr: "24px", // keep right padding when drawer closed
							}}
						>
							<Typography
								component="h1"
								variant="h6"
								color="inherit"
								noWrap
								sx={{ flexGrow: 1 }}
							>
								Dashboard
							</Typography>
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
								open={open}
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
										open={open}
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
														<MenuItem value="">
															<em>None</em>
														</MenuItem>
														<MenuItem value={10}>Twenty</MenuItem>
														<MenuItem value={21}>Twenty one</MenuItem>
														<MenuItem value={22}>
															Twenty one and a half
														</MenuItem>
													</Select>
													<FormHelperText>
														Tell me how are you feeling.
													</FormHelperText>
												</FormControl>
											</Stack>
											<Button onClick={handleClose}>Close</Button>
											<Button onClick={handleSave}>Save Changes</Button>
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
						<Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
							<Grid container spacing={3}>
								{/* Chart */}
								<Grid item xs={12} md={8} lg={9}>
									<Paper
										sx={{
											p: 2,
											display: "flex",
											flexDirection: "column",
											height: 240,
										}}
									>
										<Chart />
									</Paper>
								</Grid>
								{/* Recent Deposits */}
								<Grid item xs={12} md={4} lg={3}>
									<Paper
										sx={{
											p: 2,
											display: "flex",
											flexDirection: "column",
											height: 240,
										}}
									>
										<Deposits />
									</Paper>
								</Grid>
								{/* Recent Orders */}
								<Grid item xs={12}>
									<Paper
										sx={{
											p: 2,
											display: "flex",
											flexDirection: "column",
										}}
									>
										<Orders />
									</Paper>
								</Grid>
							</Grid>
							<Copyright sx={{ pt: 4 }} />
						</Container>
					</Box>
				</Box>
			</ThemeProvider>
		);
}

export default function Dashboard() {
    return <DashboardContent />;
}
