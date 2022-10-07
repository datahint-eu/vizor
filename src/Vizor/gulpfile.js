// Following steps are required to install gulp: see https://gulpjs.com/docs/en/getting-started/quick-start/
// npm install --global gulp-cli
//
// in the project dir:
//   npm init
//   npm install --save-dev gulp gulp-rename gulp-clean gulp-sass gulp-clean-css sass merge-stream
//   npm install @tabler/core
//
// to build:
//   gulp

var path = require('path'),
	rename = require('gulp-rename'),
	clean = require('gulp-clean'),
	gulp = require('gulp'),
	sass = require('gulp-sass')(require('sass')),
	cleancss = require('gulp-clean-css'),
	merge = require('merge-stream');

var vizorStyles = './Styles';
var vizorScripts = './Scripts';
var wwwroot = path.resolve(__dirname, "wwwroot");
var libroot = path.resolve(__dirname, "./node_modules");


var srcPaths = {
	tablerCss: [
		path.resolve(libroot, '@tabler/core/dist/css/tabler.min.css')
	],

	tablerJs: [
		path.resolve(libroot, 'bootstrap/dist/js/bootstrap.bundle.min.js'),
		path.resolve(libroot, '@tabler/core/dist/js/tabler.min.js'),
	]
};


var destPaths = {
	css: path.resolve(wwwroot, 'css'),
	js: path.resolve(wwwroot, 'js'),
	lib: path.resolve(wwwroot, 'lib')
};


gulp.task('clean', () => {
	return gulp.src(
		[path.resolve(wwwroot, 'css/**'), path.resolve(wwwroot, 'js/**')])
		.pipe(clean());
});

gulp.task('tablerCss', () => {
	return gulp.src(srcPaths.tablerCss)
		.pipe(gulp.dest(destPaths.css));
});

gulp.task('tablerJs', () => {
	return gulp.src(srcPaths.tablerJs)
		.pipe(gulp.dest(destPaths.js));
});

gulp.task('cleanup', gulp.series(['clean']));
gulp.task('default', gulp.series(['tablerCss'], ['tablerJs']));