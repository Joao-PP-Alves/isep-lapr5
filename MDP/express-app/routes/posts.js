const express = require('express');
const router = express.Router();
const Post = require('../domain/post');
const Joi = require('joi')

//ROUTES

//Gets all posts
router.get("/", async (req, res) => {
	try{
	const posts = await Post.find();	
	res.json(posts);
	}catch(err){
		res.json({message: err});
	}
});

//Gets a single post
//ainda falta pôr bem
router.get("/:id", async (req, res) => {
	const posts = await Post.find();
	const post = posts.find((c) => c.id === parseInt(req.params.id));

	if (!post) res.status(404).send("The post with the given id was not found.");
	res.send(post);
});


//Submits a post
router.post("/", async (req, res) => {
	const post = new Post({
		name: req.body.name,
		content: req.body.content,
		userId: req.body.userId
	});
	try{
	const savedPost = await post.save();
	res.json(savedPost);
	} catch(err){
		res.json({ message: err});
	}
});


//Changes the information of a post
//ainda falta corrigir
router.put("/:_id", async (req, res) => {
	const posts = await Post.find();
	const post = posts.find((c) => c._id === parseInt(req.params._id));
	if (!post) res.status(404).send("The post with the given id was not found.");

	const { error } = validatePost(req.body);

	if (error) {
		res.status(404).send(error.details[0].message);
		return;
	}

	post.name = req.body.name;
	res.send(post);
});


//validates post information
function validatePost(post) {
	const schema = {
		name: Joi.string().required(),
	};
	return Joi.validate(post, schema);
}

module.exports = router;