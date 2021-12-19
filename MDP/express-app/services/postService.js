import { PostMapper } from "../mappers/postMapper.js";

export class PostService {
	postRepository;
	mapper;
	constructor(postRepository) {
		this.postRepository = postRepository;
		this.mapper = new PostMapper();
	}

	getPostById(id) {
		return this.postRepository.findPostById(id);
	}

	async getAllPosts() {
		let dtos = [];
		let posts = await this.postRepository.findAll();
		for (let i = 0; i < posts.length; i++) {
			dtos.push(this.mapper.toDto(posts[i]));
		}

		return dtos;
	}

	async getAllUsersPosts(userID) {
		let dtos = [];
		let entities = await this.postRepository.findByUserId(userID);
		for (let i = 0; i < entities.length; i++) {
			dtos.push(this.mapper.toDto(entities[i]));
		}
		return dtos;
	}

	async createPost(dto) {
		const newObj = await this.postRepository.generateNewPost(dto);
		return this.mapper.toDto(newObj);
	}

	async deletePost(id) {
		return await this.postRepository.removePostById(id);
	}
}
