var express = require('express’);
app = express();8
var gummy = require('./app/routes/gummy.js');
app.use('/api/gummy',gummy);
var bears = require('./app/routes/posts.js');
app.use('/api/bears',bears);
app.listen(process.env.PORT || 8080);