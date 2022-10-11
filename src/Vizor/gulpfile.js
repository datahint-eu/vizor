// Following steps are required to install gulp: see https://gulpjs.com/docs/en/getting-started/quick-start/
// npm install --global gulp-cli
//
// in the project dir:
//   npm init
//   npm install --save-dev gulp gulp-rename gulp-clean gulp-sass gulp-clean-css sass gulp-concat gulp-minify
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
	concat = require('gulp-concat'),
	minify = require('gulp-minify');

var vizorStyles = './Styles';
var vizorScripts = './Scripts';
var wwwroot = path.resolve(__dirname, "wwwroot");
var libroot = path.resolve(__dirname, "./node_modules");


var srcPaths = {
	css: [
		path.resolve(libroot, '@tabler/core/dist/css/tabler.min.css'),
		path.resolve(libroot, 'tom-select/dist/css/tom-select.bootstrap5.min.css')
	],

	js: [
		path.resolve(libroot, 'bootstrap/dist/js/bootstrap.bundle.min.js'),
		path.resolve(libroot, '@tabler/core/dist/js/tabler.min.js'),
		path.resolve(libroot, 'tom-select/dist/js/tom-select.complete.min.js'),
		path.resolve(vizorScripts, 'select.js')
	]
};


var destPaths = {
	css: path.resolve(wwwroot, 'css'),
	js: path.resolve(wwwroot, 'js')
};


gulp.task('clean', () => {
	return gulp.src(
		[path.resolve(wwwroot, 'css/**'), path.resolve(wwwroot, 'js/**')])
		.pipe(clean());
});

gulp.task('buildCss', () => {
	return gulp.src(srcPaths.css)
		.pipe(concat('vizor-all.css'))
		.pipe(cleancss())
		.pipe(gulp.dest(destPaths.css))
});

gulp.task('buildJs', () => {
	return gulp.src(srcPaths.js)
		.pipe(concat('vizor-all.js'))
		.pipe(minify())
		.pipe(gulp.dest(destPaths.js))
});

gulp.task('cleanup', gulp.series(['clean']));
gulp.task('default', gulp.series(['buildCss'], ['buildJs']));