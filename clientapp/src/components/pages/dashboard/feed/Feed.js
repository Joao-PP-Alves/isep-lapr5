import * as React from "react";
import Typography from "@mui/material/Typography";
import Title from "../Title";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Post from "./Post";

function preventDefault(event) {
	event.preventDefault();
}

export default function Feed() {
	return (
		<Grid>
			<Title> Feed </Title>

			<Typography component="p" variant="h4"></Typography>
			<Typography color="text.secondary" sx={{ flex: 1 }}></Typography>
			<Paper></Paper>
			<Grid item variant="outlined">
				<Post />
				{/* mostrar aqui os posts dos amigos*/}
			</Grid>
		</Grid>
	);
}
