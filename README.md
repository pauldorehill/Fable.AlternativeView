# Fable + Preact + Inferno

Demo projects on how to get [Fable](https://fable.io/) working with some alternative view rendering frameworks:
 - [Preact](https://preactjs.com/)
 - [Inferno](https://infernojs.org/)

Also quickly compares to [Elm](https://elm-lang.org/).

The basic steps are:

1. Install alternative dom rendering framework
2. Alias in webpack [config](https://webpack.js.org/configuration/)  (use of the `resolve` option)

:bowtie:

## Inferno

Follow the [guide](https://infernojs.org/docs/guides/switching-to-inferno).

Install

```javascript
npm remove react react-dom
npm install inferno
// Not sure how many of these are needed... works with just inferno
// npm install inferno-compat
// npm install inferno-clone-vnode
// npm install inferno-create-class
// npm install inferno-create-element
```
webpack.config.js

```
{
    resolve: {
        "alias": {
            "react": "inferno-compat",
            "react-dom": "inferno-compat"
        }
    }
}
```

## Preact
Follow the [guide](https://preactjs.com/guide/v10/getting-started#aliasing-in-webpack).

Install

```
npm remove react react-dom
npm install preact preact-dom
```
webpack.config.js

```
{
    resolve: {
        "alias": {
            "react": "preact/compat",
            "react-dom/test-utils": "preact/test-utils",
            "react-dom": "preact/compat"
        }
    }
}
```

## Build + Run

Go to the appropriate project folder and run:

```
npm install
dotnet build src
npm start

i ｢wds｣: Project is running at http://localhost:8080/
i ｢wds｣: webpack output is served from /
i ｢wds｣: Content not from webpack is served from ./public
...
i ｢wdm｣: Compiled successfully.
```
:satisfied:

For elm:
```
cd src/elm
elm reactor
Go to http://localhost:8000 to see your project dashboard.
```

## Build Script

Open a terminal in `Fable.AlternativeView` & run `dotnet fsi build.fsx`. This will build all the projects (requires `npm`, `uglify-js`, and `elm` to be installed).

## Package Sizes

These were a very quick set of tests:
- webpack switching from `mode: "development"` to `mode: "production"`
- Elm following the [minification](https://guide.elm-lang.org/optimization/asset_size.html) guide

Library | Build Mode | Size
| --- | --- | --- |
Elm | standard | 106 KiB
Elm | optimized + minified | 16 KiB
Inferno | development | 829 KiB
Inferno | production | 78.9 KiB
Preact | development | 717 KiB
Preact | production | 57.7 KiB
React | development | 1750 Kib
React | production | 172 KiB

Informative discussion on package size [Webpack Performance Budgets](https://github.com/webpack/webpack/issues/3216).

And for something [different](https://svelte.dev/).