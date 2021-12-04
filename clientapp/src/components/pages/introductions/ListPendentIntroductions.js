import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
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
import NotificationsIcon from '@mui/icons-material/Notifications';
import { mainListItems} from '../dashboard/ListItems';
import TableCell from '@mui/material/TableCell';
import Table from '@mui/material/Table';
import PropTypes from 'prop-types';
import { withStyles } from '@mui/styles';
import clsx from 'clsx';
import Links from "../../Links";
import { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import AccountCircleTwoToneIcon from '@mui/icons-material/AccountCircleTwoTone';
import { DataGrid } from "@mui/x-data-grid";
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import TableContainer from '@mui/material/TableContainer';
import TableBody from '@mui/material/TableBody';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Collapse from '@mui/material/Collapse';
import axios from 'axios';  

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


const rows = [];

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

const handleApprove = async (introId) => {
  const response = await fetch(
    Links.MDR_URL() + "/api/introductions/approve/" + introId,
    {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: "",
    }
  )
    .then((response) => response.json())
    .then((json) => console.log(json));
  alert("Introduction Approved!");
}

const handleReprove = async (introId) => {
  console.log("reprovando");
  const requesterData = await axios.put(
    Links.MDR_URL() + "/api/introductions/reprove/" + introId
  );
  alert("Introduction Reproved!");
}

function Row(props) {
  const { row } = props;
  const [open, setOpen] = React.useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell>{row.requester}</TableCell>
        <TableCell>{row.enabler}</TableCell>
        <TableCell>{row.targetUser}</TableCell>
        <TableCell>{row.status}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Descriptions & Actions
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                </TableHead>
                <TableBody>
                    <TableRow>
                      <Typography sx={{ mt: 4, mb: 2 }} variant="body2" component="div">
                        Message To Enabler: {row.messageToEnabler}
                      </Typography>
                      <Typography sx={{ mt: 4, mb: 2 }} variant="body2" component="div">
                        Message To Target User: {row.messageToTargetUser}
                      </Typography>
                      
                      <Typography sx={{ mt: 4, mb: 2 }} variant="body2" component="div">
                        Message From Enabler to Target User: {row.messageFromEnablerToTargetUser}
                      </Typography>
                    </TableRow>
                    <Button variant="contained" color="success" onClick={() => {handleApprove(row.id)}}>Approve</Button>
                    <Button variant="outlined" color="error" sx={{ml:2}} onClick={() => {handleReprove(row.id)}}>Reprove</Button>
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

Row.propTypes = {
  row: PropTypes.shape({
    
  }).isRequired,
};
  
  const defaultTheme = createTheme();
  

function EditProfileContent() {
  const [open, setOpen] = React.useState(true);

  //get logged user
  const userId = localStorage.getItem('loggedInUser');

  const toggleDrawer = () => {
    setOpen(!open);
  };

  useEffect(() => {
    search();
  }, []);

  const [searchedVS, setSearchedVS] = useState([]);

function search() {
    fetchPendentIntroductions();
  } 

  const fetchPendentIntroductions= async () => {

    const data = await fetch(
			//mudar o id para uma parâmetro passado!
			Links.MDR_URL() + "introductions/pendentWithNames/" + userId
		);
    const vsList = await data.json();
    console.log(vsList);
    setSearchedVS(vsList);

  };

  // transform json array to array sample[]

  let sample = [];

  for (var i = 0; i < searchedVS.length; i++){
    var obj = searchedVS[i];
    console.log(obj);
    sample.push([obj.id,obj.requesterName,obj.enablerName,obj.targetUserName,obj.decisionStatus,
    obj.messageToIntermediate.text,obj.messageToTargetUser.text,
    obj.messageFromIntermediateToTargetUser === null ? "" : obj.messageFromIntermediateToTargetUser.text]);
  }

  function createData(id,requester,enabler,targetUser,status,messageToEnabler,messageToTargetUser,messageFromEnablerToTargetUser) {
    return {
      id,
      requester,
      enabler,
      targetUser,
      status,
      messageToEnabler,
      messageToTargetUser,
      messageFromEnablerToTargetUser
    };
  }

  // push the information from sample to rows

  const rows = [];

  for (let i = 0; i < searchedVS.length; i += 1) {
    rows.push(createData( ...sample[i]));
  }

  //TODO ia aqui!

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
							Pendent Introductions
						</Typography>
						<IconButton color="inherit">
							<Badge badgeContent={4} color="secondary">
								<AccountCircleTwoToneIcon />
							</Badge>
						</IconButton>
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
								<TableContainer component={Paper}>
                  <Table aria-label="collapsible table">
                    <TableHead>
                      <TableRow>
                        <TableCell />
                        <TableCell>Requester</TableCell>
                        <TableCell>Enabler</TableCell>
                        <TableCell>Target User</TableCell>
                        <TableCell>Status</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {rows.map((row) => (
                      <Row key={row.name} row={row} />
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
							</Paper>
						</Grid>
						<Copyright sx={{ pt: 4 }} />
					</Container>
				</Box>
			</Box>
		</ThemeProvider>
	);
}

export default function EditProfile() {
  return <EditProfileContent />;
}