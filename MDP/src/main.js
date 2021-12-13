import express, { json } from "express";
import { PostRepository } from "./infrastructure/schemas.js";
import mongoose from "mongoose";
import { PostController } from "./controllers/postsController.js";
import { PostService } from "./services/postService.js";

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
