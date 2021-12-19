const express = require('express');
const router = express.Router();
const Post = require('../domain/post');
const Joi = require('joi')

//ROUTES
router.get("/add-posts", (req, res) => {
	//res.send("We are on posts");
	const post = new Post({
		id: '1',
		name: 'post1',
		content: 'Bom dia! #educados_respondem',
		userId: '1'
		//date: '18-12-2021'
	});

	post.save()
		.then((result) => {
			res.send(result)
		})
		.catch((err) => {
			console.log(err);
		});
});

router.get("/", (req, res) => {
	res.send(posts);
});

/*router.get("/:id", (req, res) => {
	const post = posts.find((c) => c.id === parseInt(req.params.id));

	if (!post) res.status(404).send("The post with the given id was not found.");
	res.send(post);
});*/

router.post("/add-posts", (req, res) => {
	const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	const post = {
		//id: posts.length + 1,
		name: req.body.name,
	};
	//posts.push(post);
	res.send(post);
});

/*router.put("/api/Posts/:id", (req, res) => {
	const post = posts.find((c) => c.id === parseInt(req.params.id));
	if (!post) res.status(404).send("The post with the given id was not found.");

	const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	post.name = req.body.name;
	res.send(post);
});*/

function validatePost(post) {
	const schema = {
		name: Joi.string().required(),
	};
	return Joi.validate(post, schema);
}


/*router.post("/", (req, res) => {
	const post = new Post({
		//id: String,
		name: req.body.name,
		//content: req.body.content,
		//userId: req.body.userId,
		//date: req.body.date,
	});

	post .save()
	.then(data => {
		res.json(data);
	})
	.catch(err => {
		res.json({message: err});
	})*/
	/*const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	const post = {
		id: posts.length + 1,
		name: req.body.name,
	};
	posts.push(post);
	res.send(post);*/


router.put("/:id", (req, res) => {
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