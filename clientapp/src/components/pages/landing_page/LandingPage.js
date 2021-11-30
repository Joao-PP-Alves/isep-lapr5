import React from "react";
import { styled } from "@mui/material/styles";
import { makeStyles } from "@mui/styles";
import AppBar from "@mui/material/AppBar";
import Stack from "@mui/material/Stack";
import ToolBar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import CssBaseline from "@mui/material/CssBaseline";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Container from "@mui/material/Container";
import logo from "../../../img/reactjs-icon.svg";
import Link from "@mui/material/Link";
import Button from "@mui/material/Button";
import network from "../../../img/network.gif";

const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: "center",
    color: theme.palette.text.secondary,
}));

const useStyles = makeStyles({
    root: {
        background: "linear-gradient(45deg, #59a3cf 10%, #6391CA     80%)",
        border: 0,
        boxShadow: "0 3px 5px 2px rgba(255, 105, 135, .3)",
        color: "white",
        height: 48,
        padding: "0 30px",
    },
});

const appBar = {
    background: "#FCFCFF",
};

function Landing() {
    const classes = useStyles();
    return (
        <div>
            <link
                rel="preconnect"
                href="https://fonts.gstatic.com"
                crossorigin
                href="https://fonts.googleapis.com/css2?family=Poppins:wght@600&display=swap"
                rel="stylesheet"
            />
            <CssBaseline />
            <AppBar
                sx={{
                    background: "#FCFCFF",
                }}
                className={appBar}
                position="static"
                elevation={0}
                color="primary"
            >
                <ToolBar
                    className="AppBarWrapper"
                    sx={{
                        flexWrap: "wrap",
                        display: "flex",
                        background: "#FCFCFF",
                    }}
                    color="primary"
                >
                    <Stack
                        className="Logo_and_Name_Stack"
                        direction="row"
                        alignItems="center"
                    >
                        <img src={logo} alt="" width="150" height="90" />

                        <Typography
                            sx={{
                                ml: 0,
                                mr: 5,
                                fontSize: 30,
                                color: "#59a3cf",
                                fontFamily: "Poppins",
                            }}
                            noWrap
                            fontWeight={750}
                        >
                            Graphs4Social
                        </Typography>
                    </Stack>

                    <Box sx={{ flexGrow: 1 }}></Box>

                    <nav>
                        <Link
                            variant="button"
                            href="#"
                            underline="none"
                            sx={{
                                my: 1,
                                mx: 1.5,
                                color: "#59a3cf",
                                fontWeight: "bold",
                                fontfamily: "Merriweather Sans",
                            }}
                        >
                            LeaderBoard
                        </Link>

                        <Link
                            variant="button"
                            href="/about"
                            underline="none"
                            sx={{
                                my: 1,
                                mx: 1.5,
                                color: "#59a3cf",
                                fontWeight: "bold",
                                fontfamily: "Merriweather Sans",
                            }}
                        >
                            About
                        </Link>

                        <Button
                            href="/login"
                            variant="outlined"
                            sx={{
                                mr: 5,
                                my: 1,
                                mx: 1.5,
                                color: "#59a3cf",
                                fontWeight: "bold",
                                fontfamily: "Merriweather Sans",
                            }}
                        >
                            Login
                        </Button>
                    </nav>
                </ToolBar>
            </AppBar>
            {/* ============================================================================================== */}
            <Box
                sx={{ flexGrow: 1, background: "#FCFCFF", height:550}}
            >
                <Grid container spacing={0}>
                    <Grid item xs={6}>
                        <Container
                            disableGutters
                            maxWidth="sm"
                            component="main"
                            sx={{ pt: 8, pb: 6 }}
                        >
                            <Typography
                                align="left"
                                color="#E27F7A"
                                gutterBottom
                                sx={{
                                    ml: 10,
                                    fontWeight: "bold",
                                    fontfamily: "Merriweather Sans",
                                    fontSize: 55,
                                }}
                            >
                                A GROWING SOCIAL NETWORK
                            </Typography>
                            <Typography
                                variant="h5"
                                align="left"
                                color="text.secondary"
                                component="p"
                                sx={{
                                    ml: 10,
                                    fontWeight: "bold",
                                    fontfamily: "Merriweather Sans",
                                }}
                            >
                                Join over 512324 users all around the world in this new way of connecting people
                            </Typography>

                            <Container>
                                <Button
                                    className={classes.root}
                                    variant="contained"
                                    align="center"
                                    sx={{
                                        maxWidth: "250px",
                                        minWidth: "250px",
                                        ml: 20,
                                        my: 5,
                                        borderRadius:25
                                    }}
                                    href="/signup"
                                >
                                    SignUp
                                </Button>
                            </Container>
                        </Container>
                    </Grid>
                    <Grid item xs={6} sx={{ my: 5 }}>
                        <img
                            src={network}
                            alt=""
                            style={{
                                width: "100%",
                                top: "80%",
                                animation: "none",
                                animationDelay: "1.75s",
                            }}
                        />
                    </Grid>
                </Grid>
            </Box>
        </div>
    );
}

export default Landing;
