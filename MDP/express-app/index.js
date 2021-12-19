const mongoose = require("mongoose");
const express = require("express");
const morgan = require('morgan');

/*const Joi = require("joi");
const bodyParser = require("body-parser");
require("dotenv/config");*/
const Post = require("./domain/post");

// express app
const app = express();

//Connect to DB
console.log("connection string: " + process.env.DB_CONNECTION);
mongoose
	.connect(
		"mongodb://PostsMasterData:Tropita123@test-shard-00-00.dloll.mongodb.net:27017,test-shard-00-01.dloll.mongodb.net:27017,test-shard-00-02.dloll.mongodb.net:27017/master-posts?ssl=true&replicaSet=atlas-jlwxwu-shard-0&authSource=admin&retryWrites=true&w=majority",
		{
			useNewUrlParser: true,
			useUnifiedTopology: true,
			//useCreateIndex: true,
		}
	)
	.then(() => console.log("Database connected!"))
	.catch((err) => console.log(err));

//listen for requests
const port = process.env.PORT || 3000;
app.listen(port, () => {
	port, () => console.log("Listening on port 3000...");
});

//register view engine
app.set('view engine', 'ejs');

//middleware and static files
app.use(express.static('public'));
app.use(morgan('dev'));

//mongoose and mongo sandbox routes
app.get("/add-posts", (req, res) => {
	const post = new Post({
		id: "1",
		name: "post1",
		content: "Boa noite! #educados_respondem",
		userId: "1",
	});

	post.save()
		.then((result) => {
			console.log(result);
			res.send(result);
		})
		.catch((err) => {
			console.log(err);
		});
});

/*app.use(express.json());
app.use(bodyParser.json());*/

//Import Routes
/*const postsRoute = require('./routes/posts');
app.use('/posts', postsRoute);

const posts = [
	{ id: 1, name: "post1" },
	{ id: 2, name: "post2" },
	{ id: 3, name: "post3" },
];*/