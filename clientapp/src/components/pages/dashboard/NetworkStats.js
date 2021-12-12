import React from "react";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Typography from "@mui/material/Typography";

const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: "center",
    color: theme.palette.text.secondary,
}));

export default function NetworkStats() {
    //get logged user
    const userId = localStorage.getItem("loggedInUser");

    return (
        <React.Fragment>
            <Container maxWidth="false">
                <Grid container spacing={2}>
                    <Grid item xs={6}>
                        <Container>
                            <Typography>
                                Consultar tamanho da rede
                            </Typography>
                        </Container>
                    </Grid>
                    <Grid item xs={6}>
                        <Item>xs=4</Item>
                    </Grid>
                </Grid>
            </Container>
        </React.Fragment>
    );
}
