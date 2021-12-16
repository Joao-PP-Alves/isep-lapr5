const express = require('express');

const router = express.Router();


//ROUTES
router.get("/", (req, res) => {
	res.send("Hello world!");
});

router.get("/api/Posts", (req, res) => {
	res.send(posts);
});

router.get("/api/Posts/:id", (req, res) => {
	const post = posts.find((c) => c.id === parseInt(req.params.id));

	if (!post) res.status(404).send("The post with the given id was not found.");
	res.send(post);
});

router.post("/api/Posts", (req, res) => {
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

router.put("/api/Posts/:id", (req, res) => {
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

module.exports = router;