# Fable + Preact

Quick demo project on how to get [Fable](https://fable.io/) + [Preact](https://preactjs.com/) working. Also quickly compares to Elm.

## 1. Install Preact

```
npm remove react react-dom
npm install preact preact-dom
```
#### package.json
```json
{
  "private": true,
  "scripts": {
    "start": "webpack-dev-server"
  },
  "dependencies": {
    "@babel/core": "^7.5.5",
    "fable-compiler": "^2.3.19",
    "fable-loader": "^2.1.8",
    "preact": "^10.0.1",
    "preact-dom": "^1.0.1",
    "webpack": "^4.41.2",
    "webpack-cli": "^3.3.6",
    "webpack-dev-server": "^3.8.0"
  }
}
```

## 2. Setup aliasing in webpack
Preact [aliasing](https://preactjs.com/guide/v10/getting-started#aliasing-in-webpack) + webpack [config](https://webpack.js.org/configuration/)  (use of the `resolve` option)
#### webpack.config.js
```js
var path = require("path");

module.exports = {
    mode: "development",
    entry: "./src/Fable.Preact.fsproj",
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        contentBase: "./public",
        port: 8080,
    },
    module: {
        rules: [{
            test: /\.fs(x|proj)?$/,
            use: "fable-loader"
        }]
    },
    resolve: {
        "alias": {
          "react": "preact/compat",
          "react-dom/test-utils": "preact/test-utils",
          "react-dom": "preact/compat",
        }
    }
}
```

## 3. Build + Run

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

## Build Script

Open at terminal in `Fable.Preact` & run `dotnet fsi build.fsx`. This will build both the Fable and Elm projects (requires `npm` and `elm` to be installed).

## Package Sizes

These were a very quick set of tests:
- webpack switching from `mode: "development"` to `mode: "production"`
- Elm following the [minification](https://guide.elm-lang.org/optimization/asset_size.html) guide

Library | Build Mode | Size
| --- | --- | --- |
Preact | development | 1.05Mib
Preact | production | 204 KiB
React | development | 2.1Mib
React | production | 318 KiB
Elm | standard | 106 KiB
Elm | optimized + minified | 23 KiB