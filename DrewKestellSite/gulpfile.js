/// <binding Clean='minify:css' ProjectOpened='watch' />
'use strict';

var gulp = require('gulp'),
    compileSass = require('gulp-sass'),
    cleanCSS = require('gulp-clean-css'),
    rename = require('gulp-rename'),
    sourcemaps = require('gulp-sourcemaps'),
    sassLint = require('gulp-sass-lint');

var webRoot = './wwwroot/';

gulp.task('lint:sass', function () {
    return gulp.src('./Styles/styles.scss')
        .pipe(sassLint())
        .pipe(sassLint.format())
        .pipe(sassLint.failOnError());
});

gulp.task('compile:sass', gulp.series('lint:sass', function () {
    return gulp.src('./Styles/styles.scss')
        .pipe(compileSass())
        .pipe(gulp.dest(webRoot + 'css'));
}));

gulp.task('minify:css', gulp.series('compile:sass', function () {
    gulp.src([webRoot + 'css/*.css', '!' + webRoot + 'css/*.min.css'])
        .pipe(sourcemaps.init())
        .pipe(cleanCSS({ compatibility: '*' }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(webRoot + 'css'));
}));

gulp.task('watch', function () {
    gulp.watch('./Styles/styles.scss', gulp.series('minify:css'));
});
