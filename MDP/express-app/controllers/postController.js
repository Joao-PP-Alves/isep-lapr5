import { PostDTO } from "../dtos/postDto.js";

export class PostController {
	postService;

	constructor(postService) {
		this.postService = postService;
	}

	async getAllPosts() {
		let result = await this.postService.getAllPosts();
		console.log("Got All posts:");
		console.log(result);
		return result;
	}

	async getPostById(id) {
		return await this.postService.getPostById(id);
	}

	async deletePost(id) {
		return await this.postService.deletePost(id);
	}

	async createPost(content, userId) {
		let dto = new PostDTO(null, content, userId, Date.now());
		let post = await this.postService.createPost(dto);
		console.log("New post:");
		console.log(post);
		return post;
	}

	async getUserPosts(userId) {
		let userPosts = await this.postService.getAllUsersPosts(userId);
		console.log("All Users posts:");
		console.log(userPosts);
		return userPosts;
	}
}
