import * as React from 'react';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import Title from './Title';

function preventDefault(event) {
  event.preventDefault();
}

export default function Deposits() {
  return (
    <React.Fragment>
      <Title> Friends sugestions </Title>
      <Typography component="p" variant="h4">
       
      </Typography>
      <Typography color="text.secondary" sx={{ flex: 1 }}>
        
      </Typography>
      <div>
        <Link color="primary" href="#" onClick={preventDefault}>
          
        </Link>
      </div>
    </React.Fragment>
  );
}