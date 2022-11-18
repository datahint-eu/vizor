// npm install --save-dev gulp child_process
// this exists only so I can double click in the task runner explorer to publish a package locally

var gulp = require('gulp');
var exec = require("child_process").exec;

gulp.task("publishLocal", function (callback) {
	exec(
		"Powershell.exe -executionpolicy remotesigned . .\\publish_local.ps1 -Push:1",
		function (err, stdout, stderr) {
			console.log(stdout);
			callback(err);
		}
	);
});