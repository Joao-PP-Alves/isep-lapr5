import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import { styled } from '@mui/material/styles';
import Paper from '@mui/material/Paper';
import * as React from 'react';
import Typography from '@mui/material/Typography';
import Links from '../../Links'

const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: "center",
    color: theme.palette.text.secondary,
}));

export default function NetworkStats() {
    //get logged user
  const userId = localStorage.getItem("loggedInUser");

  const [open, setOpen] = React.useState(true);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  React.useEffect(() => {
    search();
  }, []);

  const [searchedVS, setSearchedVS] = React.useState([]);

  function search() {
    fetchNetworkSize();
  }

  const fetchNetworkSize= async () => {

    const data = await fetch(
      "https://localhost:5001/api/users/NetworkSize/" + userId + "/" + 3
    );
    const vsList = await data.json();
    console.log(vsList);
    setSearchedVS(vsList);

  };

  // transform json array to array sample[]

  let sample = [];


  var obj = searchedVS;
	  
  console.log(obj);
    
  sample.push([obj.size]);

  console.log(sample[0]);

  function createData(size) {
    return { size };
  }

  // push the information from sample to rows

  let rows = [];

  for (let i = 0; i < searchedVS.length; i += 1) {
    rows.push(createData(...sample[0]));
  }

  console.log(rows);

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
                        <Item>{sample[0]}</Item>
                    </Grid>
                </Grid>
            </Container>
        </React.Fragment>
    );
}
