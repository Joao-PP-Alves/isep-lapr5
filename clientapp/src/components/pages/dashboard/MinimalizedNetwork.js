import * as React from "react";
import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";
import Title from "./Title";
import AppGraph from "../../../graph/AppGraph";
import Grid from "@mui/material/Grid";
import Graph from "../../../graph/graph";
import Paper from "@mui/material/Paper";

function preventDefault(event) {
	event.preventDefault();
}

function handleRedirect() {
	window.location.href = "/graph";
}

export default function Deposits() {
	return (
		<Grid>
			<Title> My web </Title>

			<Typography component="p" variant="h4"></Typography>
			<Typography color="text.secondary" sx={{ flex: 1 }}></Typography>
			<Paper>
				
			</Paper>
			<Grid item>
				<Link href="/graph" variant="body2">
					{"See network in fullscreen."}
				</Link>
			</Grid>
		</Grid>
	);
}
