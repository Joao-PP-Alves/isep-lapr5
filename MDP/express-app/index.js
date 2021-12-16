const mongoose = require("mongoose");
const Joi = require("joi");
const express = require("express");
const app = express();
require('dotenv/config');

app.use(express.json());

//Import Routes
const postsRoute = require('./routes/posts');
app.use('/posts', postsRoute);

const posts = [
	{ id: 1, name: "post1" },
	{ id: 2, name: "post2" },
	{ id: 3, name: "post3" },
];

//Connect to DB
mongoose.connect(
	process.env.DB_CONNECTION,
	{ useNewUrlParser: true },
	() => console.log("Connected to DB!!")
);


const port = process.env.PORT || 3000;
app.listen(port, () => {
	port, () => console.log("Listening on port 3000...");
});
