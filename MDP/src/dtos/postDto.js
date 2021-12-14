export class PostDTO {
	id;
	content;
	userId;
	date;

	constructor(id, content, userId, date) {
		this.id = id;
		this.content = content;
		this.userId = userId;
		this.date = date;
	}
}
