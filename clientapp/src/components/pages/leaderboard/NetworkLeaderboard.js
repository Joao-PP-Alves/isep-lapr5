import * as React from 'react';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import Links from '../../Links'
import List from '@mui/material/List';
import { styled } from '@mui/material/styles';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import TextField from '@mui/material/TextField';

export default function FriendsSuggestions() {

    //get logged user
    const userId = localStorage.getItem('loggedInUser');

    const [open, setOpen] = React.useState(true);
    const [level, setLevel] = React.useState("");
    const [loaded, setLoaded] = React.useState(false);
    const toggleDrawer = () => {
        setOpen(!open);
    };

    React.useEffect(() => {
        search();
    }, []);

    const [searchedVS, setSearchedVS] = React.useState([]);

    function search() {
        fetchUsers();
    }

    const fetchUsers = async () => {

        const data = await fetch(
            Links.MDR_URL() + "users/LeaderboardNetworkSize/3"
        );
        const vsList = await data.json();
        console.log(vsList);
        setSearchedVS(vsList);
        setLoaded(true);
    };

    // transform json array to array sample[]

    let sample = [];

    for (var i = 0; i < searchedVS.length; i++) {
        var obj = searchedVS[i];

        console.log(obj);

        sample.push([i, obj.userName, obj.size]);
    }

    function createData(id, name, size) {
        return { id, name, size };
    }

    // push the information from sample to rows

    let rows = [];

    for (let i = 0; i < searchedVS.length; i += 1) {
        rows.push(createData(...sample[i]));
    }
    
    if(!loaded){
        return (<React.Fragment>
            <Container sx={{ mt: 10 }}>
                <Typography variant="h4" color="primary" gutterBottom component="div" textAlign='center'>
                    Leaderboard
                </Typography>
                <Grid container justifyContent="center">
                    <List sx={{
                        width: '100%',
                        maxWidth: '40%',
                        position: 'center',
                        overflow: 'auto',
                        maxHeight: '100%',
                        '& ul': { padding: 0 },
                    }}>
                        
                    </List>
                    <ListItem>Loading...</ListItem>
                </Grid>
            </Container>
        </React.Fragment>);
    }else{

    return (
        <React.Fragment>
            <Container sx={{ mt: 10 }}>
                <Typography variant="h4" color="primary" gutterBottom component="div" textAlign='center'>
                    Leaderboard
                </Typography>
                <Grid container justifyContent="center">
                    <TextField id="level" label="Level" variant="filled" sx={{ pr: 2 }}
                        onChange={(e) => setLevel(e.target.value)}
                        type="number"
                        defaultValue="3"
                        placeholder={'[1-3]'}
                        InputProps={{ inputProps: { min: 1 } }} />
                    <List sx={{
                        width: '100%',
                        maxWidth: '40%',
                        position: 'center',
                        overflow: 'auto',
                        maxHeight: '100%',
                        '& ul': { padding: 0 },
                    }}>
                        {rows.map((value) => {
                            const labelId = value.id;
                            console.log(value);
                            return (
                                <ListItem
                                    key={value.id}
                                    disablePadding
                                >
                                    <ListItemButton>
                                        <ListItemAvatar>
                                            <Avatar />
                                        </ListItemAvatar>
                                        <ListItemText id={labelId} primary={`${value.name} `} />
                                        <ListItemText id={labelId}>
                                            <Typography variant="h7" gutterBottom component="div" textAlign='right'>
                                            {value.size}
                                            </Typography>
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                            );
                        })}
                    </List>
                </Grid>
            </Container>
        </React.Fragment>
    );
                    }
}