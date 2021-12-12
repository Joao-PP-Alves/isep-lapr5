var msg;
loadRecords = function (callback) {
	setTimeout(function () {
		//Simulates reading records from database.
		msg = "My blocking task.";
		callback();
	}, 3000);
};
printMsg = function () {
	console.log(msg);
};
loadRecords(printMsg);
console.log("Hello");
