const mongoose = require("mongoose");
const mongoose_validator = require("mongoose-id-validator");
const Schema = mongoose.Schema;

const PostSchema = new Schema({
	id: String,
	name: String,
	content: String,
	userId: String,
	//date: Date,
});

//PostSchema.plugin(mongoose_validator);
const Post = mongoose.model('Posts', PostSchema);
module.exports = Post;