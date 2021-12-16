const mongoose = require("mongoose");
const mongoose_validator = require("mongoose-id-validator");

var PostSchema = mongoose.Schema({
	id: String,
	name: String,
	content: String,
	userId: String,
	date: Date,
});

PostSchema.plugin(mongoose_validator);
module.exports = mongoose.model("Post", PostSchema);
