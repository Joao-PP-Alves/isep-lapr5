import * as React from 'react';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import DashboardCustomizeTwoToneIcon from '@mui/icons-material/DashboardCustomizeTwoTone';
import ManageAccountsTwoToneIcon from '@mui/icons-material/ManageAccountsTwoTone';
import PeopleAltTwoToneIcon from '@mui/icons-material/PeopleAltTwoTone';
import ConnectWithoutContactRoundedIcon from '@mui/icons-material/ConnectWithoutContactRounded';
import LayersIcon from '@mui/icons-material/Layers';
import GroupAddTwoToneIcon from '@mui/icons-material/GroupAddTwoTone';
import DashBoard from '../dashboard/Dashboard';
import Button from '@mui/material/Button';
import WebhookTwoToneIcon from "@mui/icons-material/WebhookTwoTone";

function toDashboard(event){
  <DashBoard/>
}

function toEdit(event){
  let path =`/editProfile`;
}

export const mainListItems = (
	<div>
		<ListItem>
			<Button
				id="dashboardButton"
				onClick={(event) => (window.location.href = "/dashBoard")}
				size="small"
				font="Montserrat"
			>
				<ListItemIcon>
					<DashboardCustomizeTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="Dashboard"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="dashboardButton"
				onClick={(event) => (window.location.href = "/dashBoard")}
				size="small"
				font="Montserrat"
			>
				<ListItemIcon>
					<DashboardCustomizeTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="Dashboard"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="editButton"
				onClick={(event) => (window.location.href = "/editProfile")}
				size="small"
			>
				<ListItemIcon>
					<ManageAccountsTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="Edit profile"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="friendsButton"
				onClick={(event) => (window.location.href = "/friends")}
				size="small"
			>
				<ListItemIcon>
					<PeopleAltTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="Friends"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="connectionsButton"
				onClick={(event) => (window.location.href = "/connections/pendent")}
				size="small"
			>
				<ListItemIcon>
					<GroupAddTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="Pending Connections"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="introductionsButton"
				onClick={(event) => (window.location.href = "/introductions/pendent")}
				size="small"
			>
				<ListItemIcon>
					<ConnectWithoutContactRoundedIcon />
				</ListItemIcon>
				<ListItemText
					primary="Pending Introductions"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
		<ListItem>
			<Button
				id="networkButton"
				onClick={(event) => (window.location.href = "/graph")}
				size="small"
				font="Montserrat"
			>
				<ListItemIcon>
					<WebhookTwoToneIcon />
				</ListItemIcon>
				<ListItemText
					primary="My network"
					primaryTypographyProps={{ fontSize: "12px" }}
				/>
			</Button>
		</ListItem>
	</div>
);