import mongoose from "mongoose";

import crypto from "crypto";
import { Post } from "../model/post.js";

var postSchema = new mongoose.Schema({
	postId: String,
	content: String,
	userId: String,
	date: Date,
});

class IdGenerator {
	hash;

	constructor() {
		this.hash = crypto.createHash("sha256");
	}

	generatePostId(date, userId) {
		return this.hash.update(date + userId).digest("hex");
	}
}

export class PostRepository {
	postSchema = postSchema;
	Post = mongoose.model("Post", this.postSchema);

	constructor() {}

	async generateNewPost(dto) {
		let post = new this.Post({
			postId: new IdGenerator().generatePostId(dto.date, dto.userId),
			content: dto.content,
			userId: dto.userId,
			date: dto.date,
		});

		let domain = new Post(post.postId, post.content, post.userId, post.date);
		await saveObject(post);
		return domain;
	}

	async savePost(post) {
		await post.save();
		return post;
	}

	async removePostById(id) {
		return this.Post.findOneAndDelete({ id: id });
	}

	async findPostById(id) {
		return this.Post.findOne({ id: id });
	}

	async findAll() {
		let entities = [];
		let posts = await this.Post.find();
		for (let i = 0; i < posts.length; i++) {
			let entity = posts[i];
			entities.push(
				new Post(entity.postId, entity.content, entity.userId, entity.date)
			);
		}
		return entities;
	}

	async findByUserId(userId) {
		let entites = [];
		let posts = await this.Post.find({ userId: userId }).sort({ date: -1 });
		for (let i = 0; i < posts.length; i++) {
			entites.push(
				new Post(posts[i].id, posts[i].content, posts[i].userId, posts[i].date)
			);
		}
		return entites;
	}
}

export async function saveObject(object) {
	await object.save();
}
