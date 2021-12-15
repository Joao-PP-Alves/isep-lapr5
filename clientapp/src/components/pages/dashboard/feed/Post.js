import * as React from "react";
import Typography from "@mui/material/Typography";
import Title from "../Title";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";

function preventDefault(event) {
	event.preventDefault();
}

export default function Feed() {
	return (
		<Grid>
			<Title> Sandra's post </Title>

			<Typography component="p" variant="h4"></Typography>
			<Typography color="text.secondary" sx={{ flex: 1 }}></Typography>
			<Paper></Paper>
			<Grid item>{/* mostrar aqui os posts dos amigos*/}</Grid>
		</Grid>
	);
}
