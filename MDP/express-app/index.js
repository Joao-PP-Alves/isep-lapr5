const mongoose = require("mongoose");
const Joi = require("joi");
const express = require("express");
const app = express();

app.use(express.json());

const posts = [
	{ id: 1, name: "post1" },
	{ id: 2, name: "post2" },
	{ id: 3, name: "post3" },
];

async function main() {
	await mongoose.connect("mongodb://vs330.dei.isep.ipp.pt/admin", {
		user: "admin",
		pass: "iPeU6HhFssjJ",
	});
}

app.get("/", (req, res) => {
	res.send("Hello world");
});

app.get("/api/Posts", (req, res) => {
	res.send(posts);
});

app.get("/api/Posts/:id", (req, res) => {
	const post = posts.find((c) => c.id === parseInt(req.params.id));

	if (!post) res.status(404).send("The post with the given id was not found.");
	res.send(post);
});

app.post("/api/Posts", (req, res) => {
	const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	const post = {
		id: posts.length + 1,
		name: req.body.name,
	};
	posts.push(post);
	res.send(post);
});

app.put("/api/Posts/:id", (req, res) => {
	const post = posts.find((c) => c.id === parseInt(req.params.id));
	if (!post) res.status(404).send("The post with the given id was not found.");

	const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	post.name = req.body.name;
	res.send(post);
});

function validatePost(post) {
	const schema = {
		name: Joi.string().required(),
	};
	return Joi.validate(post, schema);
}

const port = process.env.PORT || 3000;
app.listen(port, () => {
	main(), port, () => console.log("Listening on port 3000...");
});
