// Following steps are required to install gulp: see https://gulpjs.com/docs/en/getting-started/quick-start/
// npm install --global gulp-cli
//
// in the project dir:
//   npm init
//   npm install --save-dev gulp gulp-rename gulp-clean gulp-sass gulp-clean-css sass gulp-concat gulp-minify gulp-postcss child_process
//   npm install @tabler/core
//
// to build:
//   gulp

var path = require('path'),
	rename = require('gulp-rename'),
	clean = require('gulp-clean'),
	gulp = require('gulp'),
	sass = require('gulp-sass')(require('sass')),
	postcss = require('gulp-postcss'),
	cleancss = require('gulp-clean-css'),
	concat = require('gulp-concat'),
	minify = require('gulp-minify');
var exec = require("child_process").exec;

var vizorStyles = './Styles';
var vizorScripts = './Scripts';
var wwwroot = path.resolve(__dirname, "wwwroot");
var libroot = path.resolve(__dirname, "./node_modules");


var srcPaths = {
	scss: [
		path.resolve(vizorStyles, 'vizor.scss'),
	],

	css: [
		path.resolve(libroot, 'tom-select/dist/css/tom-select.bootstrap5.min.css')
	],

	js: [
		path.resolve(libroot, 'bootstrap/dist/js/bootstrap.bundle.min.js'),
		//TODO: this breaks bootstrap: path.resolve(libroot, '@tabler/core/dist/js/tabler.min.js'),
		path.resolve(libroot, 'tom-select/dist/js/tom-select.complete.min.js'),
		path.resolve(vizorScripts, 'modal.js'),
		path.resolve(vizorScripts, 'select.js'),
		path.resolve(vizorScripts, 'toast.js')
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

// see https://www.npmjs.com/package/gulp-sass
// see https://github.com/tabler/tabler/blob/dev/gulpfile.js
gulp.task('compileScss', () => {
	return gulp.src(srcPaths.scss)
		.pipe(concat('vizor.css'))
		.pipe(sass({
			style: 'expanded',
			precision: 7,
			importer: (url, prev, done) => {
				if (url[0] === '~') {
					url = path.resolve('node_modules', url.substr(1))
				}

				return { file: url }
			},
		}).on('error', sass.logError))
		.pipe(postcss([
			require('autoprefixer'),
		]))
		.pipe(gulp.dest(destPaths.css));
});

gulp.task('buildCss', () => {
	return gulp.src(srcPaths.css)
		.pipe(concat('vizor-vendor.css'))
		.pipe(cleancss())
		.pipe(gulp.dest(destPaths.css))
});

gulp.task('buildJs', () => {
	return gulp.src(srcPaths.js)
		.pipe(concat('vizor-all.js'))
		.pipe(minify())
		.pipe(gulp.dest(destPaths.js))
});

gulp.task("publishLocal", function (callback) {
	exec(
		"Powershell.exe -executionpolicy remotesigned . .\\publish_local.ps1 -Push:1",
		function (err, stdout, stderr) {
			console.log(stdout);
			callback(err);
		}
	);
});

gulp.task('cleanup', gulp.series(['clean']));
gulp.task('default', gulp.series(['compileScss'], ['buildCss'], ['buildJs']));