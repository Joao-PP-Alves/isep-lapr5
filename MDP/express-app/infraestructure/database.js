const mongoose = require("mongoose");
const server = "server.mlab.com:21234"; // Database server address
const database = "theDatabase"; // Database name

class Database {
	constructor() {
		this._connect();
	}
	async _connect() {
		mongoose.connect(`mongodb://vs330.dei.isep.ipp.pt/admin`, {
			useMongoClient: true,
		});
		then(() => {
			console.log("Database connection successful");
		}).catch((err) => {
			console.error("Database connection error");
		});
	}
}
module.exports = new Database();
