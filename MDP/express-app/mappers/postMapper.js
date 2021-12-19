import { PostDTO } from "../dtos/postDto.js";
import { Post } from "../model/post.js";

export class PostMapper {
	constructor() {}

	toDomain(dto) {
		return new PostDTO(dto.id, dto.content, dto.userId, dto.data);
	}

	toDto(entity) {
		return new Post(entity.id, entity.content, entity.userId, entity.date);
	}
}
