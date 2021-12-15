import express, { json } from "express"
import { PostRepository } from "./infrastructure/schemas.js"
import mongoose from "mongoose"
import { PostController } from "./src/controllers/postsController.js"
import { PostService } from "./src/services/postService.js"

var schemas;

async function main() {
	console.log("");
	await mongoose.connect("[IP MONGOOSE]", {
		user: "[USER]",
		pass: "[PASSWORD]",
	});
}

const app = express();
app.get("/", (req, res) => {
	res.send("Hello World!");
});

app.all((req, res) => {
	console.log("Invalid request!");
	res.code = 404;
	res.send("Invalid request");
});

var postController = new PostController(new PostService(new PostRepository()));
app.use(json());
app.get("/api/posts", (req, res) => {
	postController.getAllPosts().then((posts) => res.send(posts));
});

app.post("/api/post", (req, res) => {
	postController
		.createPost(req.body.content, req.body.userId, req.body.date)
		.then((post) => {
			res.send(post);
		});
});

app.get("/api/posts/user/", (req, res) => {
	postController.getUserPosts(req.query.id).then((val) => {
		res.send(val);
	});
});

let port = 3000;
app.listen(port, () => {
	main().catch((err) => console.log(err));
	console.log(`Listening on port ${port}`);
	console.log(`http://localhost:${port}`);
});

const kittySchema = mongoose.Schema({
	name: String,
});

/*var express = require("express");
var app = express();
//var fs = require("fs");
var port = process.env.PORT || 8080;

app.listen(port);
console.log('RESTful API server started on: ' + port);
/*
var user = {
	user4: {
		name: "mohit",
		password: "password4",
		profession: "teacher",
		id: 4,
	},
};

app.post("/addUser", function (req, res) {
	// First read existing users.
	fs.readFile(__dirname + "/" + "users.json", "utf8", function (err, data) {
		data = JSON.parse(data);
		data["user4"] = user["user4"];
		console.log(data);
		res.end(JSON.stringify(data));
	});
});

var server = app.listen(8081, function () {
	var host = server.address().address;
	var port = server.address().port;
	console.log("Example app listening at http://%s:%s", host, port);
});*/
